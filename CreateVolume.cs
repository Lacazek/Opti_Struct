using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;


namespace Structure_optimisation
{
    internal class CreateVolume
    {
        private string name;
        private readonly char[] filterTags ={
                '|',
                '&',
                '-',
                '^',
                '+',
                '~',
                '!'};
        private StructureSet _ss;
        private StreamReader srf;
        private Dictionary<char, Action<Structure, Structure>> _structure;
        private string line;

        public CreateVolume(StructureSet ss)
        {
            name = string.Empty;
            _ss = ss;

            _structure = new Dictionary<char, Action<Structure, Structure>>()
{
    { filterTags[0], (arg1, arg2) => arg1.SegmentVolume = arg1.Or(arg2)},  // Or est la totalité
	{ filterTags[1], (arg1, arg2) => arg1.SegmentVolume = arg1.And(arg2)}, // And est  l'intersection
	{ filterTags[2], (arg1, arg2) => arg1.SegmentVolume = arg1.Sub(arg2)}, // sub est la soustraction et conserve que Arg1
	{ filterTags[3], (arg1, arg2) => arg1.SegmentVolume = arg1.Xor(arg2)}, // Xor est l'union
};

        }

        internal void CreationVolume(string _userFileChoice)
        {
            Structure BODY = _ss.Structures.Where(x => x.DicomType.ToUpper().Equals("EXTERNAL")).SingleOrDefault();
            MessageBox.Show(System.IO.Path.GetFileName(_userFileChoice));
            srf = new StreamReader(System.IO.Path.GetFileName(_userFileChoice));
            MessageBox.Show(srf.ToString());
            Structure test = BODY;
            while (line != null)
            {
                line = srf.ReadLine();
                name = line.Split(':')[0].Trim();
                line = line.Split(':')[1].Trim();
                MessageBox.Show("name");
                MessageBox.Show(line);
                try
                {
                    foreach (char key in _structure.Keys)
                    {
                        if (line.Contains(_structure.Keys.ToString()))
                        {
                            string[] V = line.Split(key);
                            Structure Struct1 = _ss.Structures.Where(x => x.Id.ToUpper().Equals(V[0].Trim())).SingleOrDefault();
                            Structure Struct2 = _ss.Structures.Where(x => x.Id.ToUpper().Equals(V[1].Trim())).SingleOrDefault();
                            Structure myStruct = _ss.AddStructure("Control", name);
                            myStruct.SegmentVolume = Struct1.Margin(0.00);
                            _structure[key](myStruct, Struct2);
                            myStruct.SegmentVolume = myStruct.And(BODY);
                        }
                        if (line.Contains(filterTags[4]))
                        {
                            string[] V = line.Split(filterTags[4]);
                            float floatValue = float.Parse(V[1]);
                            Structure Struct1 = _ss.Structures.Where(x => x.Id.ToUpper().Equals(V[0].Trim())).SingleOrDefault();
                            Structure myStruct = _ss.AddStructure("Control", name);
                            myStruct.SegmentVolume = Struct1.Margin(floatValue);
                            /*if (line.ToString().ToUpper().Contains("TEST"))
                            {
                                test = _ss.AddStructure("Control", "TEST");
                                test.SegmentVolume = Struct1.Margin(0.00);
                            }
                            else
                            {
                                Structure myStruct = _ss.AddStructure("Control", name);
                                myStruct.SegmentVolume = Struct1.Margin(floatValue);
                            }*/
                        }
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Une erreur est survenue : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            _ss.RemoveStructure(test);
            srf.Close();
        }

    }
}
