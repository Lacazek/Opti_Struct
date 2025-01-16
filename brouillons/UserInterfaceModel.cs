using System;
using System.Collections.Generic;
using System.ComponentModel;
using VMS.TPS.Common.Model.API;
using System.IO;
using System.Reflection;

namespace Structure_optimisation
{

    internal class UserInterfaceModel : INotifyPropertyChanged
    {
        private ScriptContext _context;
		private readonly string _fisherMan;
		private GetFile _file;
        private List<string> _localisation;
        private StreamWriter _logFile;
        public event PropertyChangedEventHandler PropertyChanged;

        public UserInterfaceModel(ScriptContext context, List<string> targets)
        {
            _context = context;
            _file = new GetFile(this);
            _localisation = new List<string>();
			_fisherMan = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString(), "fisherMan4.png");
			FillList();

            FileInfo _fileinfo = new FileInfo("Opti_Struct\\LogFile.txt");
            if (_fileinfo.Exists && _fileinfo.Length > 500 * 1000)
                _fileinfo.Delete();
            _logFile = new StreamWriter("Opti_Struct\\LogFile.txt", true);

            _file.MessageChanged += MessageChanged;
            Message = $"\n**********************************";
            Message = $"************ Benvenue *************";
            Message = $"**********************************\n";
            Message = $"Debut de programme : {DateTime.Now}";
            Message = $"Ordinateur utilisé : {Environment.MachineName}";
            Message = $"OS : {Environment.OSVersion}";
            Message = $"Domaine windows : {Environment.UserDomainName}";
            Message = $"Dossier de travail : {Environment.SystemDirectory}";
            Message = $"Taille du jeu de travail : {Environment.WorkingSet}";
            Message = $"User : {Environment.UserName}\n";
            Message = $"Fichier ouvert\n";
        }

        internal void FillList()
        {
            foreach (var item in Directory.GetFiles(_file.GetPath))
            {
                _localisation.Add(Path.GetFileNameWithoutExtension(item));
            }
            _localisation.Sort();
        }
        internal void ClearList()
        {
            _localisation.Clear();
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
        internal List<string> Targets
        {
            set { _file.Targets = value; }
        }
        internal string UserPath
        {
            get { return _file.GetPath; }
            set { _file.GetPath = value; }
        }
        internal List<string> Localisation
        {
            get { return _localisation; }
        }
        internal ScriptContext GetContext
        {
            get { return _context; }
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
        internal void IsOpened(bool test)
        {
            if (test == true)
            {
                _logFile.WriteLine($"Fichier Log fermé");
                _logFile.WriteLine($"Fin du programme : {DateTime.Now}");
                _logFile.WriteLine($"***************************Script terminé***************************");
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

