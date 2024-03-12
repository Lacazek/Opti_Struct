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


namespace Structure_optimisation
{

	internal class UserInterfaceModel
    {
		private string _userChoice;
		private string _rename;
		private GetFile _file;
        private List<string> _localisation;

		public UserInterfaceModel(StructureSet ss)
		{
            _userChoice = string.Empty;
			_rename = string.Empty;
			_file = new GetFile(ss);
			_localisation= new List<string>();
			 FillList();
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
			set { _file.UserFile = value;}
		}
        internal string UserPath
        {
            get { return _file.GetPath; }
        }
        internal List<string> Localisation
		{
			get { return _localisation; }
		}
    }
}

