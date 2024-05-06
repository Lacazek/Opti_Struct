using Opti_Struct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using VMS.TPS.Common.Model.API;

namespace Opti_Struct
{
    internal class VolumErrorModel
    {
        private List<string> _structError;
        private StructureSet _ss;
        private List<string> _structSS;
        private List<string> _NewStruct;

        public VolumErrorModel(StructureSet ss)
        {
            _structError = new List<string>();
            _ss = ss;
            _structSS = new List<string>();
            _NewStruct= new List<string>();
        }

        internal List<string> getErrorList
        {
            get { return _structError; }
        }
        internal List<string> getSSList
        {
            get { return _structSS; }
        }

        internal string setErrorList
        {
            set
            {
                _structError.Add(value);
            }
        }
        internal List<string> GetNewStruct
        {
            get { return _NewStruct; }
        }
        internal string SetNewStruct
        {
            set { _NewStruct.Add(value); }
        }
        internal StructureSet GetSS
        {
            get { return _ss; }
        }
    }

}