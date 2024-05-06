using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Linq;
using VMS.TPS.Common.Model.API;

namespace Opti_Struct
{
    internal class Struct_dictionnary
    {
        //private readonly Dictionary<string, (string regexPattern, string ssName)> _allStruct;
        private readonly Dictionary<string, string> _allStruct;

        public Struct_dictionnary(StructureSet _ss)
        {
            //_allStruct = new Dictionary<string, (string regexPattern, string ssName)>();
            _allStruct = new Dictionary<string, string>();
            CreateDictionnary(_ss);
        }

        internal string SearchStruct(StructureSet _ss, string name)
        {
            try
            {
                // ici utilise name pour chercher la clé dans le dictionnaire puis avec le regex cherche le nom de la structure qui match le regex.
                // Cette structure est renvoyé et écrase la précédente

                foreach (var keys in _allStruct.Keys)
                {
                    if (Regex.IsMatch(name, keys, RegexOptions.IgnoreCase))
                    {
                        return _allStruct[keys];
                    }
                }
                return name;
            }
            catch
            {
                return name;
            }
        }

        private void CreateDictionnary(StructureSet _ss)
        {

            // Volume cible
            // GTV
            // ORL
            _allStruct.Add(@"(?i)g\s*t\s*v\s*\s*\+", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\\s * t\\s * v\\s *\\s *\\+", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s*t\s*v\s*n\s*\+", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s*t\s*v\s*n\s*\+", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s * t\s * v\s * b\s * r", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s * t\s * v\s * b\s * r", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s * t\s * v\s * b\s * r \ *1", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s * t\s * v\s * b\s * r \ *1", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s * t\s * v\s * b\s * r \ *2", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s * t\s * v\s * b\s * r \ *2", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s * t\s * v\s * h\s * r", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s * t\s * v\s * h\s * r", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s * t\s * v\s * h\s * r \ *1", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s * t\s * v\s * h\s * r \ *1", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s * t\s * v\s * h\s * r \ *2", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s * t\s * v\s * h\s * r \ *2", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s*t\s*v\s*n\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s*t\s*v\s*n\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s*t\s*v\s*n\s*d\s*1", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s * t\s * v\s * n\s * d\s * 1", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)g\s*t\s*v\s*n\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)g\s*t\s*v\s*n\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Sein
            _allStruct.Add(@"(?i)c\s*t\s*v\s*c\s*m\s*i\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*c\s*m\s*i\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*c\s*m\s*i\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*c\s*m\s*i\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*s\s*e\s*i\s*n\s*\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*s\s*e\s*i\s*n\s*\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*s\s*e\s*i\s*n\s*\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*s\s*e\s*i\s*n\s*\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*s\s*o\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*s\s*o\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*s\s*o\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*s\s*o\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*n\s*\s*s\s*o\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*n\s*\s*s\s*o\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*n\s*\s*s\s*o\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*n\s*\s*s\s*o\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*s\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*s\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*s\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*s\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*n\s*\s*s\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*n\s*\s*s\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*n\s*\s*s\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*n\s*\s*s\s*u\s*s\s*\s*c\s*l\s*a\s*v\s*\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*\s*a\s*x\s*i\s*l\s*\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*\s*a\s*x\s*i\s*l\s*\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*\s*a\s*x\s*i\s*l\s*\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*\s*a\s*x\s*i\s*l\s*\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*n\s*\s*a\s*x\s*i\s*l\s*\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*n\s*\s*a\s*x\s*i\s*l\s*\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*\s*n\s*\s*a\s*x\s*i\s*l\s*\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*\s*n\s*\s*a\s*x\s*i\s*l\s*\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*t\s*v\s*_*l\s*i\s*t\s*u\s*m(?:\s*a\s*l)?(?:\s*f\s*b)?\b|c\s*t\s*v\s*l\s*i\s*t\s*u\s*t\s*o\s*m\s*r\s*a\s*l\b",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*t\s*v\s*_*l\s*i\s*t\s*u\s*m(?:\s*a\s*l)?(?:\s*f\s*b)?\b|c\s*t\s*v\s*l\s*i\s*t\s*u\s*t\s*o\s*m\s*r\s*a\s*l\b", RegexOptions.IgnoreCase))?.Id ?? "");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // ITV
            // Poumons
            _allStruct.Add(@"(?i)i\s*t\s*v", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)i\s*t\s*v", RegexOptions.IgnoreCase))?.Id ?? "");

            // PTV
            // ORL
            _allStruct.Add(@"(?i)p\s * t\s * v\s * b\s * r", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * b\s * r", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s * t\s * v\s * b\s * r \ *1", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * b\s * r \s *1", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s * t\s * v\s * b\s * r \ *2", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * b\s * r \s *2", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s * t\s * v\s * b\s * r \ *3", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * b\s * r \s *3", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s * t\s * v\s * h\s * r", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * h\s * r", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s * t\s * v\s * h\s * r \ *1", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * h\s * r \s *1", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s * t\s * v\s * h\s * r \ *2", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * h\s * r \s *2", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s * t\s * v\s * h\s * r \ *3", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * h\s * r \s *3", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s * t\s * v\s * i\s * r", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s * t\s * v\s * i\s * r", RegexOptions.IgnoreCase))?.Id ?? "");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // pelvis
            _allStruct.Add(@"(?i)(?:p\s*t\s*v\s*\s*t|p\s*t\s*v\s*t)", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*t\s*v\s*\s*t|p\s*t\s*v\s*t)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:p\s*t\s*v\s*\s*t\s*1|p\s*t\s*v\s*t\s*1)", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*t\s*v\s*\s*t|p\s*t\s*v\s*t\s*1)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:p\s*t\s*v\s*\s*t\s*2|p\s*t\s*v\s*t\s*2)", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*t\s*v\s*\s*t|p\s*t\s*v\s*t\s*2)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:p\s*t\s*v\s*\s*n|p\s*t\s*v\s*n)", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*t\s*v\s*\s*t|p\s*t\s*v\s*n)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:p\s*t\s*v\s*\s*n\s*1|p\s*t\s*v\s*n\s*1)", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*t\s*v\s*\s*t|p\s*t\s*v\s*n\s*1)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:p\s*t\s*v\s*\s*n\s*2|p\s*t\s*v\s*n\s*2)", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*t\s*v\s*\s*t|p\s*t\s*v\s*n\s*2)", RegexOptions.IgnoreCase))?.Id ?? "");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // prostate
            _allStruct.Add(@"(?i)p\s*t\s*v\s*1", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*t\s*v\s*1", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s*t\s*v\s*2", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*t\s*v\s*2", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s*t\s*v\s*g\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*t\s*v\s*g\s*g", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s*t\s*v\s*n", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*t\s*v\s*n", RegexOptions.IgnoreCase))?.Id ?? "");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // Poumons et stéréo
            _allStruct.Add(@"(?i)p\s*t\s*v", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*t\s*v", RegexOptions.IgnoreCase))?.Id ?? "");


            // Volume cible DAZ (nomenclature n'incluant pas CTV)
            _allStruct.Add(@"(?i)p\s*t\s*v\s*2", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*t\s*v\s*2", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)v\s*s", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)v\s*s", RegexOptions.IgnoreCase))?.Id ?? "");
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            // OAR (contourage IA)
            // Ordre OAR ( CE - Vessie - Coeur - moelle - TC - parotide D & G - oesophage - Trachée - NOG - NOD - Chiasma - Encephale - paroi - poumons - poumon d & g - thyroide
            // sein d & g - foie

            _allStruct.Add(@"(?i)(?:c\s *[kc]\s * o\s * u\s * n\s * t\s * o\s * u\s * r\s *[\stkcntr]\s * e\s * x\s * t\s *[\stkcnrt]\s * e\s * r\s * n\s * e)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:c\s *[kc]\s * o\s * u\s * n\s * t\s * o\s * u\s * r\s *[\stkcntr]\s * e\s * x\s * t\s *[\stkcnrt]\s * e\s * r\s * n\s * e)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:v\s*e\s*s\s*s\s*i\s*e|v\s*e\s*s\s*s\s*i\s*o|v\s*e\s*s\s*i\s*e|v\s*e\s*s\s*i\s*e\s*t|\s*o\s*a\s*r\s*v\s*e\s*s\s*s\s*s\s*i\s*e\s*)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:v\s*e\s*s\s*s\s*i\s*e|v\s*e\s*s\s*s\s*i\s*o|v\s*e\s*s\s*i\s*e|v\s*e\s*s\s*i\s*e\s*t|\s*o\s*a\s*r\s*v\s*e\s*s\s*s\s*s\s*i\s*e\s*)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:o\s*a\s*r\s*c\s*o\s*e\s*u\s*r|c\s*o\s*e\s*u\s*r|\b(c\s*o\s*e\s*u\s*r|o\s*a\s*r\s*c\s*o\s*e\s*u\s*r)\b)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:o\s*a\s*r\s*c\s*o\s*e\s*u\s*r|c\s*o\s*e\s*u\s*r|\b(c\s*o\s*e\s*u\s*r|o\s*a\s*r\s*c\s*o\s*e\s*u\s*r)\b)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:m\s*[mn]\s*o\s*[ou]\s*e\s*[eo]\s*l\s*[ln]\s*l\s*[ln]\s*e|\s*o\s*a\s*r\s*(?:\s*m\s*o\s*e\s*l\s*l\s*e|m\s*o\s*e\s*l\s*l\s*e))",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:m\s*[mn]\s*o\s*[ou]\s*e\s*[eo]\s*l\s*[ln]\s*l\s*[ln]\s*e|\s*o\s*a\s*r\s*(?:\s*m\s*o\s*e\s*l\s*l\s*e|m\s*o\s*e\s*l\s*l\s*e))", RegexOptions.IgnoreCase))?.Id ?? "");

            //_allStruct.Add(@"(?i)(?:t\s*[tc]\s*c)", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:t\s*[tc]\s*c)", RegexOptions.IgnoreCase))?.Id ?? "");
            //_allStruct.Add(@"(?i)(?:t\s*[tr]\s*o\s*n\s*c\s*[sc]\s*c\s*[ci]\s*r\s*[re]\s*b\s*r\s*a\s*l)",
            //   _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:t\s*[tr]\s*o\s*n\s*c\s*[sc]\s*c\s*[ci]\s*r\s*[re]\s*b\s*r\s*a\s*l)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)t\s*[tc]\s*c|t\s*r\s*o\s*n\s*c\s*_*c\s*e\s*r\s*e\s*b\s*r\s*a\s*l", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)t\s*[tc]\s*c|t\s*r\s*o\s*n\s*c\s*_*c\s*e\s*r\s*e\s*b\s*r\s*a\s*l", RegexOptions.IgnoreCase))?.Id ?? "");

            _allStruct.Add(@"(?i)p\s*a\s*r\s*o\s*i\s*d\s*e\s*[_\s]*[dg]",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*a\s*r\s*o\s*i\s*d\s*e\s*[_\s]*[dg]", RegexOptions.IgnoreCase))?.Id ?? "");

            //_allStruct.Add(@"(?i)(?:p\s*[pb]\s*a\s*r\s*[aiy]\s*o\s*u\s*i\s*d\s*e\s*g)",
            //  _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*[pb]\s*a\s*r\s*[aiy]\s*o\s*u\s*i\s*d\s*e\s*g)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:o\s*[o0]\s*e\s*[es]\s*s\s*[cs]\s*o\s*p\s*h\s*[fg]\s*a\s*g\s*[ep]|\s*(?:o\s*a\s*r\s*o\s*e\s*s\s*p\s*h\s*[fg]\s*a\s*g\s*[ep]|o\s*e\s*s\s*p\s*h\s*[fg]\s*a\s*g\s*[ep]))",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:o\s*[o0]\s*e\s*[es]\s*s\s*[cs]\s*o\s*p\s*h\s*[fg]\s*a\s*g\s*[ep]|\s*(?:o\s*a\s*r\s*o\s*e\s*s\s*p\s*h\s*[fg]\s*a\s*g\s*[ep]|o\s*e\s*s\s*p\s*h\s*[fg]\s*a\s*g\s*[ep]))", RegexOptions.IgnoreCase))?.Id ?? "");

            _allStruct.Add(@"(?i)(?:t\s*[tr]\s*a\s*[ca]\s*h\s*[ae]{1,2}|\s*o\s*a\s*r\s*(?:\s*t\s*r\s*a\s*c\s*h\s*e\s*e|_t\s*r\s*a\s*c\s*h\s*e\s*e))",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:t\s*[tr]\s*a\s*[ca]\s*h\s*[ae]{1,2}|\s*o\s*a\s*r\s*(?:\s*t\s*r\s*a\s*c\s*h\s*e\s*e|_t\s*r\s*a\s*c\s*h\s*e\s*e))", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:n\s*o\s*g|n\s*e\s*r\s*f\s*o\s*p\s*t\s*i\s*q\s*u\s*e?\s*g)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:n\s*o\s*g|n\s*e\s*r\s*f\s*o\s*p\s*t\s*i\s*q\s*u\s*e?\s*g)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:n\s*o\s*d|n\s*e\s*r\s*f\s*o\s*p\s*t\s*i\s*q\s*u\s*e?\s*d)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:n\s*o\s*d|n\s*e\s*r\s*f\s*o\s*p\s*t\s*i\s*q\s*u\s*e?\s*d)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)c\s*h\s*i\s*a\s*s\s*m\s*a", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)c\s*h\s*i\s*a\s*s\s*m\s*a", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:c\s*[ce]\s*r\s*v\s*e\s*a?\s*u?|e\s*n\s*c\s*e\s*p\s*h\s*[ea]\s*l\s*e?)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:c\s*[ce]\s*r\s*v\s*e\s*a?\s*u?|e\s*n\s*c\s*e\s*p\s*h\s*[ea]\s*l\s*e?)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:p\s*a?\s*r\s*o?\s*i\s*(?:t\s*h\s*o\s*r\s*a\s*c\s*i\s*q\s*u\s*e?)?|p\s*a?\s*r\s*o\s*i)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*a?\s*r\s*o?\s*i\s*(?:t\s*h\s*o\s*r\s*a\s*c\s*i\s*q\s*u\s*e?)?|p\s*a?\s*r\s*o\s*i)", RegexOptions.IgnoreCase))?.Id ?? "");

            _allStruct.Add(@"(?i)(?:p\s*o\s*u\s*m\s*o\s*n\s*s?(\s*[_\s]*[dg])?|\s*o\s*a\s*r\s*(_?\s*p\s*o\s*u\s*m\s*o\s*n\s*(_?\s*[dg])?))",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*o\s*u\s*m\s*o\s*n\s*s?(\s*[_\s]*[dg])?|\s*o\s*a\s*r\s*(_?\s*p\s*o\s*u\s*m\s*o\s*n\s*(_?\s*[dg])?))", RegexOptions.IgnoreCase))?.Id ?? "");
            /*_allStruct.Add(@"(?i)p\s*[pb]\s*o\s*u\s*m\s*o?\s*n\s*s?)", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*[pb]\s*o\s*u\s*m\s*o?\s*n\s*s?)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s*o\s*u\s*m\s*[on]\s*\s*d", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*o\s*u\s*m\s*[on]\s*\s*d", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)p\s*o\s*u\s*m\s*[on]\s*\s*g", _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)p\s*o\s*u\s*m\s*[on]\s*\s*g", RegexOptions.IgnoreCase))?.Id ?? "");*/

            _allStruct.Add(@"(?i)(?:p\s*h\s*a\s*r\s*y\s*n\s*x|\s*o\s*a\s*r\s*_?\s*p\s*h\s*a\s*r\s*y\s*n\s*x)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:p\s*h\s*a\s*r\s*y\s*n\s*x|\s*o\s*a\s*r\s*_?\s*p\s*h\s*a\s*r\s*y\s*n\s*x)", RegexOptions.IgnoreCase))?.Id ?? "");
            _allStruct.Add(@"(?i)(?:l\s*a\s*r\s*y\s*n\s*x|\s*o\s*a\s*r\s*_?\s*l\s*a\s*r\s*y\s*n\s*x)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:l\s*a\s*r\s*y\s*n\s*x|\s*o\s*a\s*r\s*_?\s*l\s*a\s*r\s*y\s*n\s*x)", RegexOptions.IgnoreCase))?.Id ?? "");

            _allStruct.Add(@"(?i)(?:t\s*h\s*y\s*r\s*o\s*i\s*d\s*e|\s*o\s*a\s*r\s*(_\s*t\s*h\s*y\s*r\s*o\s*i\s*d\s*e|\s*t\s*h\s*y\s*r\s*o\s*i\s*d\s*e))",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:t\s*h\s*y\s*r\s*o\s*i\s*d\s*e|\s*o\s*a\s*r\s*(_\s*t\s*h\s*y\s*r\s*o\s*i\s*d\s*e|\s*t\s*h\s*y\s*r\s*o\s*i\s*d\s*e))", RegexOptions.IgnoreCase))?.Id ?? "");

            _allStruct.Add(@"(?i)(?:\bse?i?n(?:\s*[dg]\b|\s*_[dg]\b)?|\boar\s*se?i?n(?:\s*[dg]\b|\s*_[dg]\b)?)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:\bse?i?n(?:\s*[dg]\b|\s*_[dg]\b)?|\boar\s*se?i?n(?:\s*[dg]\b|\s*_[dg]\b)?)", RegexOptions.IgnoreCase))?.Id ?? "");

            _allStruct.Add(@"(?i)(?:foie|\s*o\s*a\s*r\s*(_?\s*foie)?)",
                _ss.Structures.FirstOrDefault(s => Regex.IsMatch(s.Id, @"(?i)(?:foie|\s*o\s*a\s*r\s*(_?\s*foie)?)", RegexOptions.IgnoreCase))?.Id ?? "");         


            foreach (var key in _allStruct.Keys.ToList())
            {
                if (!_ss.Structures.Any(s => s.Id.Equals(_allStruct[key], StringComparison.OrdinalIgnoreCase)))
                {
                    _allStruct.Remove(key);
                }
            }
        }
    }
}
        

    

