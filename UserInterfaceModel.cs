using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Structure_optimisation;
using VMS.TPS.Common.Model.API;
using System.IO;
using System.Xml.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices;


namespace Structure_optimisation
{

    internal class UserInterfaceModel : INotifyPropertyChanged
    {
        private string _userChoice;
        private string _rename;
        private GetFile _file;
        private List<string> _localisation;
        private StreamWriter _logFile;
        public event PropertyChangedEventHandler PropertyChanged;

        public UserInterfaceModel(StructureSet ss)
        {
            _userChoice = string.Empty;
            _rename = string.Empty;
            _file = new GetFile(ss);
            _localisation = new List<string>();
            FillList();
            _logFile = new StreamWriter("LogFile.txt", true);
            _file.MessageChanged += MessageChanged;
            Message = $"\n**********************************";
            Message = $"Debut de programme : {DateTime.Now}";
            Message = $"Ordinateur utilisé : {Environment.MachineName}";
            Message = $"OS : {Environment.OSVersion}";
            Message = $"Domaine windows : {Environment.UserDomainName}";
            Message = $"Dossier de travail : {Environment.SystemDirectory}";
            Message = $"Taille du jeu de travail : {Environment.WorkingSet}";
            Message = $"User : {Environment.UserName}\n";
            Message = $"Fichier ouvert\n";
        }

        private void FillList()
        {
            foreach (var item in Directory.GetFiles(_file.GetPath))
            {
                _localisation.Add(Path.GetFileNameWithoutExtension(item));
            }
        }
        internal string UserChoice
        {
            get { return _userChoice; }
        }
        internal string Rename
        {
            get { return _rename; }
        }
        internal GetFile File
        {
            get { return _file; }
        }
        internal string UserFile
        {
            get { return _file.UserFile; }
            set { _file.UserFile = value; }
        }
        internal string UserPath
        {
            get { return _file.GetPath; }
        }
        internal List<string> Localisation
        {
            get { return _localisation; }
        }
        internal string Message
        {
            get { return _file.Message; }
            set
            {
                _logFile.WriteLine(value);
                _logFile.Flush();
                OnPropertyChanged(nameof(_file.Message));
            }
        }
        internal void isOpened(bool test)
        {
            if (test == true)
            {
                _logFile.WriteLine($"\nFichier Log fermé");
                _logFile.Close();
                }
        }

        private void MessageChanged(object sender, string e)
        {
            _logFile.WriteLine(_file.Message);
            _logFile.Flush();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

