using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		private string _path;
		private string _userPath;

		public GetFile(StructureSet ss)
		{
			_userFileChoice = string.Empty;
            _createVolume = new CreateVolume(ss);
            _path = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString(), "File");
            _userPath = string.Empty;
        }

		internal void CreateUserFile()
        {
            try
            {
                _userPath = System.IO.Path.Combine(_path, _userFileChoice+".txt");
                _userFileChoice = File.ReadAllText(_userPath);
				_createVolume.CreationVolume(_userFileChoice);
			}
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur est survenue : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
			}
        }
		internal string UserFile
		{
			get { return _userFileChoice; }
			set {
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


        protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
