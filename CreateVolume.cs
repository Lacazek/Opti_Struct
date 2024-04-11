using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
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
        private readonly Dictionary<char, Action<Structure, Structure>> _structure;
        private string line;
        private string _message;
        public event EventHandler<string> MessageChanged;
        private int verbose;

        public CreateVolume(StructureSet ss)
        {
            name = string.Empty;
            _ss = ss;
            verbose = 1;
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
                Structure myStruct, _StructureInter, _StructureInter1, _StructureInter2;
                Message = $"Travail sur la structure {name} en cours ....";
                try
                {

                    foreach (char key in filterTags)
                    {
                        if (name.ToLower().Contains("verbose"))
                        {
                            verbose = int.Parse(line);
                            continue;
                        }
                        if (line.Contains(key))
                        {
                            if (key == filterTags[4]) // cas x+marge
                            {
                                string[] V = line.Split(filterTags[4]);
                                float floatValue = float.Parse(V[1]);

                                // test REGEX 
                                //Structure Struct1 = _ss.Structures.Where(x => x.Id.ToLower().Equals(V[0].Trim())).SingleOrDefault();
                                //Structure Struct1 = _ss.Structures.FirstOrDefault(x => Regex.IsMatch(x.Id, string.Join(".*", V[0].Trim().Select(c => Regex.Escape(c.ToString()))), RegexOptions.IgnoreCase));                               
                                Structure Struct1 = _ss.Structures.FirstOrDefault(x => x.Id.Equals(V[0].Trim(), StringComparison.OrdinalIgnoreCase))
                    ?? _ss.Structures.FirstOrDefault(x => Regex.IsMatch(x.Id, string.Join(@".*", V[0].Trim().Select(c => Regex.Escape(c.ToString() + @"\s*"))), RegexOptions.IgnoreCase));

                                if (!_ss.Structures.Any(x => x.Id.ToLower().Equals(name.ToLower())))
                                    if (Regex.IsMatch(name, @"\b(?:.*(?:ITV|PTV|GTV).*\b)", RegexOptions.IgnoreCase))
                                        myStruct = _ss.AddStructure("PTV", name);
                                    else
                                        myStruct = _ss.AddStructure("Control", name);
                                else if (name.ToLower() == Struct1.Id.ToLower())
                                    myStruct = Struct1;
                                else
                                    myStruct = _ss.Structures.Where(x => x.Id.ToLower().Equals(name.ToLower())).SingleOrDefault();
                                myStruct.SegmentVolume = Struct1.Margin(floatValue);
                                if (!name.ToLower().Contains("externe"))
                                    myStruct.SegmentVolume = myStruct.And(BODY);
                                Message = $"********** Operation simple **********\nStructure {name} créée";
                                Message = $"Marge de {floatValue} mm sur la structure : {Struct1.Id}\n";
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
                                        if (Regex.IsMatch(name, @"\b(?:.*(?:ITV|PTV|GTV).*\b)", RegexOptions.IgnoreCase))
                                            _StructureInter = _ss.AddStructure("PTV", name);
                                        else
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
                                    Message = $"Operation sur les structures : {_StructureInter1.Id} et {_StructureInter2.Id}\n";
                                    break;
                                }
                                else // 2 structures
                                {
                                    string[] V = line.Split(key);
                                    Structure Struct1 = _ss.Structures.Where(x => x.Id.ToLower().Equals(V[0].Trim().ToLower())).SingleOrDefault();
                                    Structure Struct2 = _ss.Structures.Where(x => x.Id.ToLower().Equals(V[1].Trim().ToLower())).SingleOrDefault();
                                    if (!_ss.Structures.Any(x => x.Id.ToLower().Equals(name.ToLower())))
                                        if (Regex.IsMatch(name, @"\b(?:.*(?:ITV|PTV|GTV).*\b)", RegexOptions.IgnoreCase))
                                            myStruct = _ss.AddStructure("PTV", name);
                                        else
                                            myStruct = _ss.AddStructure("Control", name);
                                    else if (name.ToLower() == Struct1.Id.ToLower())
                                        myStruct = Struct1;
                                    else
                                        myStruct = _ss.Structures.Where(x => x.Id.ToLower().Equals(name.ToLower())).SingleOrDefault();
                                    myStruct.SegmentVolume = Struct1.Margin(0.00);
                                    _structure[key](myStruct, Struct2);
                                    myStruct.SegmentVolume = myStruct.And(BODY);
                                    Message = $"********** Operation simple **********\nStructure {name} créée";
                                    Message = $"Operation sur les structures : {Struct1.Id} et {Struct2.Id}\n";
                                }
                            }
                            else if (key == filterTags[7])
                            {
                                // Test REGEX
                                //_ss.RemoveStructure(_ss.Structures.First(x => x.Id.ToLower().Contains("test")));
                                //_ss.RemoveStructure(_ss.Structures.FirstOrDefault(x => Regex.IsMatch(x.Id, @"t\s*e?\s*s?\s*t\s*", RegexOptions.IgnoreCase)));
                                _ss.RemoveStructure(_ss.Structures.FirstOrDefault(x => Regex.IsMatch(x.Id, @"\b[t|té|tè|tê|të]est\b.*?(?!\bintestin\b)", RegexOptions.IgnoreCase)));
                                Message = $"********** Operation simple **********\nStructure {name} supprimée";
                                Message = $"Suppression de la structure : {name}\n";
                            }
                        }
                        else if (line.Length == 0) // Créé une structure vide
                        {
                            if (Regex.IsMatch(name, @"\b(?:.*(?:ITV|PTV|GTV).*\b)", RegexOptions.IgnoreCase))
                                myStruct = _ss.AddStructure("PTV", name);
                            else
                                myStruct = _ss.AddStructure("Control", name);
                            Message = $"********** Operation simple **********\nStructure {name} créée";
                            Message = $"Ajout de la structure : {name}\n";
                            break;
                        }
                        else if (Regex.IsMatch(name, @"c\s*o?\s*n\s*t\s*o?(?:u|o)?\s*r\s*\s*e?\s*x\s*t\s*o?\s*r\s*n\s*e?", RegexOptions.IgnoreCase))
                        {
                            /*SearchBodyParameters myParameters = new SearchBodyParameters
                            {
                                FillAllCavities = true,
                                KeepLargestParts = false,
                                ISearchBodyParameters.LowerHUThreshold = int.Parse(line.Trim())
                            };
                            BODY = _ss.CreateAndSearchBody(myParameters);*/
                        }
                    }
                }
                catch (Exception ex)
                {
                    if (verbose >= 1)
                        MessageBox.Show($"Une erreur est survenue sur la structure {name} : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    Message = $"Une erreur est survenue sur la structure {name} :\n\n{ex.Message}\n";
                }

            }

            if (verbose >= 0)
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