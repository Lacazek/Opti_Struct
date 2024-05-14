using System;
using System.IO;
using System.Windows;
using System.ComponentModel;
using VMS.TPS.Common.Model.API;
using System.Reflection;

namespace Structure_optimisation
{

    internal class GetFile : INotifyPropertyChanged
    {
        private string _userFileChoice;
        private CreateVolume _createVolume;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<string> MessageChanged;
        private string _path;
        private string _userPath;
        private string _message;


        public GetFile(StructureSet ss)
        {
            _userFileChoice = string.Empty;
            _createVolume = new CreateVolume(ss);
            _path = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString(), "File");
            _userPath = string.Empty;
            _createVolume.MessageChanged += VolumeMessageChanged;
        }

        internal void CreateUserFile()
        {
            try
            {
                _userPath = System.IO.Path.Combine(_path, _userFileChoice + ".txt");
                Message = $"Fichier choisi : {_userFileChoice} \n";
                _createVolume.CreationVolume(_userPath);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur est survenue : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                Message = $"Une erreur est survenue : {ex.Message} \n";
            }
        }
        internal string UserFile
        {
            get { return _userFileChoice; }
            set
            {
                _userFileChoice = value;
                OnPropertyChanged(nameof(UserFile));
                CreateUserFile();
            }
        }
        internal string UserPath
        {
            get { return _userPath; }
            set
            { _userPath = value; }
        }
        internal string GetPath
        {
            get { return _path; }
        }
        internal string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                OnMessageChanged();
            }
        }
        #region Update log file
        private void VolumeMessageChanged(object sender, string e)
        {
            Message = e;
        }
        protected virtual void OnMessageChanged()
        {
            MessageChanged?.Invoke(this, _message);
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_createVolume.Message));
        }
        #endregion
    }
}
