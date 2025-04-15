/******************************************************************************
 * Nom du fichier : GetFile.cs
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

using System;
using System.IO;
using System.Windows;
using System.ComponentModel;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace Structure_optimisation
{

    internal class GetFile : INotifyPropertyChanged
    {
        private string _userFileChoice;
        private CreateVolume _createVolume;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<string> MessageChanged;
        private string _path;
        private string _prescriptionPath;
        private string _volumePath;
        private string _checkPath;
        private string _message;
        private List<string> _targets;

        public GetFile(UserInterfaceModel model)
        {
            _userFileChoice = string.Empty;
            _createVolume = new CreateVolume(model);
            _path = Path.Combine(System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location).ToString(), @"File");
            try
            {
                _volumePath = Path.Combine(_path, $@"{Environment.UserName}");
            }
            catch
            {
                _volumePath = _path;
            }
            _createVolume.MessageChanged += VolumeMessageChanged;
        }

        internal void CreateUserVolumeFile ()
        {
            string workingfile = System.IO.Path.Combine(_volumePath, _userFileChoice + ".txt");
            Message = $"Fichier choisi : {_userFileChoice}";
            Message = $"Chemin du fichier : {workingfile}\n";
            try
            {
                for (int target = 0; target < _targets.Count(); target++)
                {
                    Message = $"La cible n°{target + 1} choisie est : \n{_targets[target]}";
                }
                _createVolume.CreationVolume(workingfile, _targets);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Une erreur est survenue dans la création des volumes : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                Message = $"Une erreur est survenue dans la création des volumes : {ex.Message} \n";
            }
        }

        #region Get and Set
        internal string UserFile
        {
            get { return _userFileChoice; }
            set
            {
                _userFileChoice = value;
                OnPropertyChanged(nameof(UserFile));
            }
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
        internal string GetPath
        {
            get { return _path; }
            set { _path = value; }
        }
        internal string GetPrescription
        {
            get { return _prescriptionPath; }
        }
        internal string GetVolumePath
        {
            get { return _volumePath; }
            set { _volumePath = value; }
        }
        internal string GetCheckPath
        {
            get { return _checkPath; }
            set { _checkPath = value; }
        }
        internal List<string> Targets
        {
            get { return _targets; }
            set { _targets = value; }
        }

        #region Update log file
        private void VolumeMessageChanged(object sender, string e)
        {
            Message = e;
        }
        protected virtual void OnMessageChanged()
        {
            MessageChanged?.Invoke(this, _message);
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_createVolume.Message));
        }
        #endregion
        #endregion
    }
}
