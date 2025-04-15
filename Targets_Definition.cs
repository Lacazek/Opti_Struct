/******************************************************************************
 * Nom du fichier : Target_Definition.cs
 * Auteur         : LACAZE Killian
 * Date de création : [02/10/2024]
 * Description    : [Brève description du contenu ou de l'objectif du code]
 *
 * Droits d'auteur © [2024], [LACAZE.K].
 * Tous droits réservés.
 * 
 * Ce code a été développé exclusivement par LACAZE Killian. Toute utilisation de ce code 
 * est soumise aux conditions suivantes :
 * 
 * 1. L'utilisation de ce code est autorisée uniquement à titre personnel ou professionnel, 
 *    mais sans modification de son contenu.
 * 2. Toute redistribution, copie, ou publication de ce code sans l'accord explicite 
 *    de l'auteur est strictement interdite.
 * 3. L'auteur assume la responsabilité de l'utilisation de ce code dans ses propres projets.
 * 
 * CE CODE EST FOURNI "EN L'ÉTAT", SANS AUCUNE GARANTIE, EXPRESSE OU IMPLICITE. 
 * L'AUTEUR DÉCLINE TOUTE RESPONSABILITÉ POUR TOUT DOMMAGE OU PERTE RÉSULTANT 
 * DE L'UTILISATION DE CE CODE.
 *
 * Toute utilisation non autorisée ou attribution incorrecte de ce code est interdite.
 ******************************************************************************/


using Structure_optimisation;
using System.Collections.Generic;


//***************************************************************
//
// Cette classe permet d'alimenter les Text bloc pour guider l'utilisateur dans le choix des volumes
// Elle est static afin d'être accessible de n'importe où

namespace Opti_Struct
{
    static class Targets_Definition
    {
        private static List<string[]> _targets;

        internal static List<string[]> Targets
        {
            get { return _targets; }
        }
        static Targets_Definition()
        {
            _targets = new List<string[]>();
        }

        internal static void FillTargets(UserInterfaceModel model, string index)
        {
            _targets.Clear();

            // sein
            if (index.ToLower().Contains("sein"))
            {
                if (index.ToLower().Contains("seul"))
                    _targets.Add(new[] { "CTV sein" });
                else if (index.ToLower().Contains("complet") && index.ToLower().Contains("boost"))
                    _targets.Add(new[] { "CTV sein", "CTV CMI", "CTV Sus Clav", "CTV Sous Clav", "CTV Axillaire", "CTV Boost" });
                else if (index.ToLower().Contains("boost"))
                    _targets.Add(new[] { "CTV sein", "CTV Boost" });
                else if (index.ToLower().Contains("complet"))
                    _targets.Add(new[] { "CTV sein", "CTV CMI", "CTV Sus Clav", "CTV Sous Clav", "CTV Axillaire" });
            }

            //Col uterin
            if (index.ToLower().Contains("pelvis"))
            {
                if (index.ToLower().Contains("gg"))
                    _targets.Add(new[] { "PTV T", "PTV N", "PTV GG" });
                else
                    _targets.Add(new[] { "PTV T", "PTV N" });
            }

            // Stereo
            if (index.ToLower().Contains("stereo"))
            {
                if (index.ToLower().Contains("4d"))
                    _targets.Add(new[] { "PTV", "ITV" });
                else
                    _targets.Add(new[] { "PTV" });
            }


            // ORL
            if (index.ToLower().Contains("orl"))
            {
                if (index.ToLower().Contains("2niveaux"))
                    _targets.Add(new[] { "PTV HR", "PTV BR", "GTV +" });
                else if (index.ToLower().Contains("3niveaux"))
                    _targets.Add(new[] { "PTV HR", "PTV RI", "PTV BR", "GTV +", "GTV N +" });
                else
                    _targets.Add(new[] { "PTV HR" });
            }


            // à étudier


            // Poumons
            if (index.ToLower().Contains("poumon"))
            {
                if (index.ToLower().Contains("4d"))
                    _targets.Add(new[] { "ITV", "PTV" });
                else if (index.ToLower().Contains("2niveaux"))
                    _targets.Add(new[] { "PTV RI", "PTV BR" });
                else if (index.ToLower().Contains("3niveaux"))
                    _targets.Add(new[] { "PTV HR", "PTV RI", "PTV BR" });
                else
                    _targets.Add(new[] { "PTV" });
            }

            //Prostate
            if (index.ToLower().Contains("prostate"))
            {
                if (index.ToLower().Contains("aire"))
                    _targets.Add(new[] { "PTV 2", "PTV 1", "PTV N" });
                else if (index.ToLower().Contains("gg"))
                    _targets.Add(new[] { "PTV 2", "PTV 1", "PTV N", "PTV GG" });
                else if (index.ToLower().Contains("daz"))
                    _targets.Add(new[] { "prostate", "VS" });
                else if (index.ToLower().Contains("aire") && index.ToLower().Contains("daz"))
                    _targets.Add(new[] { "prostate", "VS", "CTV N" });
                else if (index.ToLower().Contains("gg") && index.ToLower().Contains("daz"))
                    _targets.Add(new[] { "prostate", "VS", "CTV N", "CTV GG" });
                else
                    _targets.Add(new[] { "PTV 2", "PTV 1" });
            }

            //Loge
            if (index.ToLower().Contains("loge"))
            {
                if (index.ToLower().Contains("daz"))
                    _targets.Add(new[] { "Prostate", "gg" });
                else
                    _targets.Add(new[] { "PTV1", "PTV2" });
            }

            //Rectum
            if (index.ToLower().Trim().Contains("rectum"))
            {
                if (index.ToLower().Contains("gg"))
                    _targets.Add(new[] { "PTV T", "PTV N", "GTV" });
                else if (index.ToLower().Contains("rp"))
                    _targets.Add(new[] { "PTV T", "PTV N", "GTV" });
                else
                    _targets.Add(new[] { "PTV T" });
            }
            // Canal anal

            if (index.ToLower().Trim().Contains("canal anal"))
            {
                if (index.ToLower().Contains("gg"))
                    _targets.Add(new[] { "PTV T", "PTV N", "GTV"});
                else
                    _targets.Add(new[] { "PTV T" });
            }

            //Encephale
            if (index.ToLower().Contains("encephale"))
            {
                if (index.ToLower().Contains("gg"))
                    _targets.Add(new[] { "PTV BR", " PTV HR" });
                else
                    _targets.Add(new[] { "PTV" });
            }

        }
    }
}
