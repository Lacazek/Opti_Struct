using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

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
        private List<string> _targets;


        public GetFile(UserInterfaceModel model)
        {
            _targets = new List<string>();
            _createVolume = new CreateVolume(model.GetContext.StructureSet, model.GetContext.Course, model.GetContext.Image);
            try
            {
                _path = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString(), $@"Opti_Struct\File\{Environment.UserName}");
            }
            catch
            {
                _path = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString(), $@"Opti_Struct\File");
            }
            _userFileChoice = string.Empty;
            _userPath = string.Empty;
            _createVolume.MessageChanged += VolumeMessageChanged;
        }

        internal void CreateUserFile()
        {
            _userPath = System.IO.Path.Combine(_path, _userFileChoice + ".txt");
        }
        internal void LaunchSegmentation()
        {
            try
            {
                for (int target = 0; target < _targets.Count(); target++)
                {
                    Message = $"La cible n°{target + 1} choisie est : \n{_targets[target]}";
                }
                Message = $"\nFichier choisi : {_userFileChoice} \n";
                _createVolume.CreationVolume(_userPath, _targets);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur est survenue : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                Message = $"Une erreur est survenue : {ex.Message} \n";
            }
        }

        // Nom du fichier sélectionné par l'utilisateur
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
        internal List<string> Targets
        {
            set { _targets = value; }
        }

        // Chemin complet jusqu'au fichier.txt
        internal string GetUserPath
        {
            get { return _userPath; }
            set { _userPath = value; }
        }

        // Chemin jusqu'au répertoire de travail
        internal string GetDirectoryPath
        {
            get { return _path; }
            set { _path = value; }
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
