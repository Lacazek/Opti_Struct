using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using VMS.TPS.Common.Model.API;
using VMS.TPS.Common.Model.Types;
using Structure_optimisation;

// This line is necessary to "write" in database
[assembly: ESAPIScript(IsWriteable = true)]
[assembly: AssemblyVersion("1.0.0.1")]

namespace VMS.TPS
{
	public class Script
	{
		public Script()
		{
		}

		[MethodImpl(MethodImplOptions.NoInlining)]
		public void Execute(ScriptContext context)
		{
			UserInterface _interface = new UserInterface(context.StructureSet);
            Patient patient = context.Patient;
            patient.BeginModifications();
            _interface.ShowDialog();
			_interface.IsOpened(true);
		}
	}
}
