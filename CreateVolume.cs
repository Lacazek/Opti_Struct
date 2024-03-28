using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Packaging;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;


namespace Structure_optimisation
{
    internal class CreateVolume
    {
        private string name;
        private readonly char[] filterTags;
        private StructureSet _ss;
        private StreamReader srf;
        private Dictionary<char, Action<Structure, Structure>> _structure;
        private string line;
        private string _message;
        public event EventHandler<string> MessageChanged;

        public CreateVolume(StructureSet ss)
        {
            name = string.Empty;
            _ss = ss;
            filterTags = new char[] {
                '|', // totalite
                '&', // intersection
                '#', // soustraction  
                '^', // union
                '!', // Marge
                '(', // parenthese ouvre
                ')', // parenthese fermee
                '/' // Suppression structure
				//'!' // inversion
				};

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
            srf = new StreamReader(System.IO.Path.GetFullPath(_userFileChoice));
            List<string> nSplit = new List<string>();
            List<char> indexeur = new List<char>();
            List<string> _operation = new List<string>();
            line = "Start";
            while ((line = srf.ReadLine()) != null)
            {
                name = line.Split(':')[0].Trim();
                line = line.Split(':')[1].Trim();
                nSplit.Clear();
                indexeur.Clear();
                _operation.Clear();
                Message = $"Travail sur la structure {name} en cours ....";
                Structure myStruct, _StructureInter, _StructureInter1, _StructureInter2;
                try
                {
                    foreach (char key in filterTags)
                    {
                        if (line.Contains(key))
                        {
                            if (key == filterTags[4]) // cas x+marge
                            {
                                string[] V = line.Split(filterTags[4]);
                                float floatValue = float.Parse(V[1]);

                                // test REGEX 
                                //Structure Struct1 = _ss.Structures.Where(x => x.Id.ToLower().Equals(V[0].Trim())).SingleOrDefault();
                                Structure Struct1 = _ss.Structures.FirstOrDefault(x => Regex.IsMatch(x.Id, string.Join(".*", V[0].Trim().Select(c => Regex.Escape(c.ToString()))), RegexOptions.IgnoreCase));                               
                                
                                if (Struct1 != null) { MessageBox.Show("OK"); }
                                if (!_ss.Structures.Any(x => x.Id.ToLower().Equals(name.ToLower())))
                                    myStruct = _ss.AddStructure("Control", name);
                                else if (name.ToLower() == Struct1.Id.ToLower())
                                    myStruct = Struct1;
                                else
                                    myStruct = _ss.Structures.Where(x => x.Id.ToLower().Equals(name.ToLower())).SingleOrDefault();
                                myStruct.SegmentVolume = Struct1.Margin(floatValue);
                                if (!name.ToLower().Contains("externe"))
                                    myStruct.SegmentVolume = myStruct.And(BODY);
                                Message = $"********** Operation simple **********\nStructure {name} créée";
                            }

                            // OK
                            else if (key != filterTags[4] && key != filterTags[5] && key != filterTags[6] && key != filterTags[7]) // toutes les autres opérations
                            {
                                string[] _parts = line.Split(filterTags, StringSplitOptions.RemoveEmptyEntries);

                                if (filterTags.Any(c => line.Count(ch => ch == c) > 1)) // plusieurs structures
                                {
                                    foreach (string part in _parts)
                                    {
                                        nSplit.Add(part.Trim());
                                    }
                                    foreach (char c in line)
                                    {
                                        if (Array.IndexOf(filterTags, c) != -1)
                                        {
                                            indexeur.Add(c);
                                        }
                                    }

                                    if (!_ss.Structures.Any(x => x.Id.ToLower().Equals(name.ToLower())))
                                        _StructureInter = _ss.AddStructure("Control", name);
                                    else
                                        _StructureInter = _ss.Structures.Where(x => x.Id.ToLower().Equals(name.ToLower())).SingleOrDefault();

                                    if (name == nSplit[0])
                                        _StructureInter1 = _StructureInter;
                                    else
                                        _StructureInter1 = _ss.Structures.Where(x => x.Id.ToLower().Equals(nSplit[0].Trim().ToLower())).SingleOrDefault();

                                    _StructureInter2 = _ss.Structures.Where(x => x.Id.ToLower().Equals(nSplit[1].Trim().ToLower())).SingleOrDefault();
                                    _operation.Add(nSplit[0] + " " + indexeur[0] + " " + nSplit[1]);

                                    foreach (var key2 in _structure.Keys)
                                    {
                                        if (_operation[0].Contains(key2))
                                        {
                                            _StructureInter.SegmentVolume = _StructureInter1.Margin(0.00);
                                            _structure[key2](_StructureInter, _StructureInter2);
                                        }
                                    }

                                    nSplit[0] = name.ToLower();

                                    for (int i = 2; i < nSplit.Count; i++)
                                    {
                                        _operation.Add(nSplit[0] + " " + indexeur[i - 1] + " " + nSplit[i]);
                                        foreach (var key2 in _structure.Keys)
                                        {
                                            if (_operation[i - 1].Contains(key2))
                                            {
                                                _StructureInter1 = _ss.Structures.Where(x => x.Id.ToLower().Equals(nSplit[i].ToLower())).SingleOrDefault();
                                                _structure[key2](_StructureInter, _StructureInter1);
                                            }
                                        }
                                    }
                                    _StructureInter.SegmentVolume = _StructureInter.And(BODY);
                                    //_StructureInter.VMS.TPS.Common.Model.API.Color = "blue";
                                    Message = $"********** Operation multiple **********\nStructure {name} créée";
                                    break;
                                }
                                else // 2 structures
                                {
                                    string[] V = line.Split(key);
                                    Structure Struct1 = _ss.Structures.Where(x => x.Id.ToLower().Equals(V[0].Trim().ToLower())).SingleOrDefault();
                                    Structure Struct2 = _ss.Structures.Where(x => x.Id.ToLower().Equals(V[1].Trim().ToLower())).SingleOrDefault();
                                    if (!_ss.Structures.Any(x => x.Id.ToLower().Equals(name.ToLower())))
                                        myStruct = _ss.AddStructure("Control", name);
                                    else if (name.ToLower() == Struct1.Id.ToLower())
                                        myStruct = Struct1;
                                    else
                                        myStruct = _ss.Structures.Where(x => x.Id.ToLower().Equals(name.ToLower())).SingleOrDefault();
                                    myStruct.SegmentVolume = Struct1.Margin(0.00);
                                    _structure[key](myStruct, Struct2);
                                    myStruct.SegmentVolume = myStruct.And(BODY);
                                    Message = $"********** Operation simple **********\nStructure {name} créée";
                                }
                            }
                            else if (key == filterTags[7])
                            {
                                // Test REGEX
                                //_ss.RemoveStructure(_ss.Structures.First(x => x.Id.ToLower().Contains("test")));
                                _ss.RemoveStructure(_ss.Structures.FirstOrDefault(x => Regex.IsMatch(x.Id, @"t\s*e?\s*s?\s*t\s*", RegexOptions.IgnoreCase)));

                                Message = $"********** Operation simple **********\nStructure {name} supprimée";
                            }
                        }
                        else if (line.Length == 0) // Créé une structure vide
                        {
                            myStruct = _ss.AddStructure("Control", name);
                            Message = $"********** Operation simple **********\nStructure {name} créée";
                            break;
                        }
                        else if (Regex.IsMatch(name, @"c\s*o?\s*n\s*t\s*o?(?:u|o)?\s*r\s*\s*e?\s*x\s*t\s*o?\s*r\s*n\s*e?", RegexOptions.IgnoreCase))
                        {
                            //ISearchBodyParameters impl = null;

                            /* SearchBodyParameters myParameters = new SearchBodyParameters
                             {
                                 FillAllCavities = true,
                                 KeepLargestParts = false,
                                 LowerHUThreshold = int.Parse(line.Trim())
                             };
                             BODY = _ss.CreateAndSearchBody(myParameters);*/
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue sur la structure {name} : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    Message = $"Une erreur est survenue sur la structure {name} :\n\n{ex.Message}\n";
                }
            }
            MessageBox.Show("Les structures ont été créées.\nMerci de les vérifier !", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            Message = $"\nFin du programme : {DateTime.Now}";
            Message = $"*****************Script terminé*****************";
            srf.Close();
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
        protected virtual void OnMessageChanged()
        {
            MessageChanged?.Invoke(this, _message);
        }
    }
}