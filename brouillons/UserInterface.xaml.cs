using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VMS.TPS.Common.Model.API;
using Ookii.Dialogs.Wpf;

namespace Structure_optimisation
{
    /// <summary>
    /// Logique d'interaction pour UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
        private UserInterfaceModel _model;
        private List<string> _targets;

        public UserInterface(ScriptContext context)
        {
            InitializeComponent();
            _model = new UserInterfaceModel(context, _targets);
            _targets = new List<string>();
            DataContext = _model;

            // Combobox des cibles
            Box_Loc_cible2.Visibility = Visibility.Collapsed;
            text_Cible2.Visibility = Visibility.Collapsed;
            Box_Loc_cible3.Visibility = Visibility.Collapsed;
            text_Cible3.Visibility = Visibility.Collapsed;
            Box_Loc_cible4.Visibility = Visibility.Collapsed;
            text_Cible4.Visibility = Visibility.Collapsed;
            Box_Loc_cible5.Visibility = Visibility.Collapsed;
            text_Cible5.Visibility = Visibility.Collapsed;
            Box_Loc_cible6.Visibility = Visibility.Collapsed;
            text_Cible6.Visibility = Visibility.Collapsed;

            // Bloc du bas
            Box_Loc.Visibility = Visibility.Collapsed;
            Text_loc.Visibility = Visibility.Collapsed;
            Info_path.Visibility = Visibility.Collapsed;
            Bouton_path.Visibility = Visibility.Collapsed;
            OK_Button.Visibility = Visibility.Collapsed;

            // Boutons à cocher
            Erreur1.Visibility = Visibility.Collapsed;
            Erreur2.Visibility = Visibility.Collapsed;
            Erreur3.Visibility = Visibility.Collapsed;
            Erreur4.Visibility = Visibility.Collapsed;
            Erreur5.Visibility = Visibility.Collapsed;

            foreach (string item in _model.Localisation)
            {
                Box_Loc.Items.Add(item);
            }
            foreach (var item in (_model.GetContext.StructureSet.Structures.OrderBy(s => s.Id)))
            {
                Box_Loc_cible1.Items.Add(item.Id);
                Box_Loc_cible2.Items.Add(item.Id);
                Box_Loc_cible3.Items.Add(item.Id);
                Box_Loc_cible4.Items.Add(item.Id);
                Box_Loc_cible5.Items.Add(item.Id);
                Box_Loc_cible6.Items.Add(item.Id);
            }
        }

        internal GetFile File
        {
            get { return _model.File; }
        }
        internal void IsOpened(bool test)
        {
            _model.IsOpened(test);
        }

      

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Box_Loc.SelectedItem != null)
                OK_Button.Visibility = Visibility.Visible;
            else
                OK_Button.Visibility = Visibility.Collapsed;
        }
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _model.Targets = _targets;
                _model.UserFile = (string)Box_Loc.SelectedItem;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
       
        private void Button_Click_path(object sender, RoutedEventArgs e)
        {
            var folderPicker = new VistaFolderBrowserDialog
            {
                Description = "Sélectionnez un dossier",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = true
            };

            string initialPath = _model.UserPath;

            if (!string.IsNullOrEmpty(initialPath) && System.IO.Directory.Exists(initialPath))
            {
                folderPicker.SelectedPath = initialPath;
            }

            bool? result = folderPicker.ShowDialog();

            try
            {
                _model.UserPath = folderPicker.SelectedPath;
                _model.Message = $"\nChangement du dossier initial, le nouveau dossier est le suivant :";
                _model.Message += $"{_model.UserPath}\n";
                Box_Loc.Items.Clear();
                _model.ClearList();
                _model.FillList();
                foreach (string item in _model.Localisation)
                {
                    Box_Loc.Items.Add(item);
                }
            }
            catch
            {
                _model.Message = "Recherche de dossier : aucun dossier sélectionné ou action annulée.";
            }
        }

        private void ComboBox_SelectionChanged_cible1(object sender, SelectionChangedEventArgs e)
        {
            if (Box_Loc_cible1.SelectedItem != null)
            {
                if (_targets.Count < 1)
                    _targets.Add(Box_Loc_cible1.SelectedItem.ToString());
                else
                    _targets[0] = Box_Loc_cible1.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show("Voulez-vous sélectionner une autre cible ?", "Choix des cibles", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    Box_Loc.Visibility = Visibility.Visible;
                    Text_loc.Visibility = Visibility.Visible;
                    Info_path.Visibility = Visibility.Visible;
                    Bouton_path.Visibility = Visibility.Visible;
                }
                else
                {
                    Box_Loc_cible2.Visibility = Visibility.Visible;
                    text_Cible2.Visibility = Visibility.Visible;
                    Erreur1.Visibility = Visibility.Visible;
                    Box_Loc.Visibility = Visibility.Collapsed;
                    Text_loc.Visibility = Visibility.Collapsed;
                    Info_path.Visibility = Visibility.Collapsed;
                    Bouton_path.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void ComboBox_SelectionChanged_cible2(object sender, SelectionChangedEventArgs e)
        {
            if (Box_Loc_cible2.SelectedItem != null)
            {
                if (_targets.Count <= 1)
                    _targets.Add(Box_Loc_cible2.SelectedItem.ToString());
                else
                    _targets[1] = Box_Loc_cible2.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show("Voulez-vous sélectionner une autre cible ?", "Choix des cibles", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    Box_Loc.Visibility = Visibility.Visible;
                    Text_loc.Visibility = Visibility.Visible;
                    Info_path.Visibility = Visibility.Visible;
                    Bouton_path.Visibility = Visibility.Visible;
                }
                else
                {
                    Box_Loc_cible3.Visibility = Visibility.Visible;
                    text_Cible3.Visibility = Visibility.Visible;
                    Erreur2.Visibility = Visibility.Visible;
                    Box_Loc.Visibility = Visibility.Collapsed;
                    Text_loc.Visibility = Visibility.Collapsed;
                    Info_path.Visibility = Visibility.Collapsed;
                    Bouton_path.Visibility = Visibility.Collapsed;
                }
                Erreur1.IsChecked = false;
            }

        }

        private void ComboBox_SelectionChanged_cible3(object sender, SelectionChangedEventArgs e)
        {
            if (Box_Loc_cible3.SelectedItem != null)
            {
                if (_targets.Count <= 2)
                    _targets.Add(Box_Loc_cible3.SelectedItem.ToString());
                else
                    _targets[2] = Box_Loc_cible3.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show("Voulez-vous sélectionner une autre cible ?", "Choix des cibles", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    Box_Loc.Visibility = Visibility.Visible;
                    Text_loc.Visibility = Visibility.Visible;
                    Info_path.Visibility = Visibility.Visible;
                    Bouton_path.Visibility = Visibility.Visible;
                }
                else
                {
                    Box_Loc_cible4.Visibility = Visibility.Visible;
                    text_Cible4.Visibility = Visibility.Visible;
                    Erreur3.Visibility = Visibility.Visible;
                    Box_Loc.Visibility = Visibility.Collapsed;
                    Text_loc.Visibility = Visibility.Collapsed;
                    Info_path.Visibility = Visibility.Collapsed;
                    Bouton_path.Visibility = Visibility.Collapsed;
                }
                Erreur2.IsChecked = false;
            }

        }

        private void ComboBox_SelectionChanged_cible4(object sender, SelectionChangedEventArgs e)
        {
            if (Box_Loc_cible4.SelectedItem != null)
            {
                if (_targets.Count <= 4)
                    _targets.Add(Box_Loc_cible4.SelectedItem.ToString());
                else
                    _targets[3] = Box_Loc_cible4.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show("Voulez-vous sélectionner une autre cible ?", "Choix des cibles", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    Box_Loc.Visibility = Visibility.Visible;
                    Text_loc.Visibility = Visibility.Visible;
                    Info_path.Visibility = Visibility.Visible;
                    Bouton_path.Visibility = Visibility.Visible;
                }
                else
                {
                    Box_Loc_cible5.Visibility = Visibility.Visible;
                    text_Cible5.Visibility = Visibility.Visible;
                    Erreur4.Visibility = Visibility.Visible;
                    Box_Loc.Visibility = Visibility.Collapsed;
                    Text_loc.Visibility = Visibility.Collapsed;
                    Info_path.Visibility = Visibility.Collapsed;
                    Bouton_path.Visibility = Visibility.Collapsed;
                }
                Erreur3.IsChecked = false;
            }
        }

        private void ComboBox_SelectionChanged_cible5(object sender, SelectionChangedEventArgs e)
        {
            if (Box_Loc_cible5.SelectedItem != null)
            {
                if (_targets.Count <= 5)
                    _targets.Add(Box_Loc_cible5.SelectedItem.ToString());
                else
                    _targets[4] = Box_Loc_cible5.SelectedItem.ToString();
                MessageBoxResult result = MessageBox.Show("Voulez-vous sélectionner une autre cible ?", "Choix des cibles", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No)
                {
                    Box_Loc.Visibility = Visibility.Visible;
                    Text_loc.Visibility = Visibility.Visible;
                    Info_path.Visibility = Visibility.Visible;
                    Bouton_path.Visibility = Visibility.Visible;
                }
                else
                {
                    Box_Loc_cible6.Visibility = Visibility.Visible;
                    text_Cible6.Visibility = Visibility.Visible;
                    Erreur5.Visibility = Visibility.Visible;
                    Box_Loc.Visibility = Visibility.Collapsed;
                    Text_loc.Visibility = Visibility.Collapsed;
                    Info_path.Visibility = Visibility.Collapsed;
                    Bouton_path.Visibility = Visibility.Collapsed;
                }
                Erreur4.IsChecked = false;
            }

        }
        private void ComboBox_SelectionChanged_cible6(object sender, SelectionChangedEventArgs e)
        {
            if (Box_Loc_cible6.SelectedItem != null)
            {
                if (_targets.Count <= 5)
                    _targets.Add(Box_Loc_cible6.SelectedItem.ToString());
                else
                    _targets[5] = Box_Loc_cible6.SelectedItem.ToString();
                Box_Loc.Visibility = Visibility.Visible;
                Text_loc.Visibility = Visibility.Visible;
                Info_path.Visibility = Visibility.Visible;
                Bouton_path.Visibility = Visibility.Visible;
            }
            else
            {
                Box_Loc.Visibility = Visibility.Collapsed;
                Text_loc.Visibility = Visibility.Collapsed;
                Info_path.Visibility = Visibility.Collapsed;
                Bouton_path.Visibility = Visibility.Collapsed;
            }
            Erreur5.IsChecked = false;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            if (Erreur1.IsChecked == true)
                _targets[1] = null;
            else if (Erreur1.IsChecked == false)
                _targets[1] = Box_Loc_cible2.SelectedItem.ToString();
        }

        private void CheckBox_Checked_2(object sender, RoutedEventArgs e)
        {
            if (Erreur2.IsChecked == true)
                _targets[2] = null;
            else if (Erreur2.IsChecked == false)
                _targets[2] = Box_Loc_cible3.SelectedItem.ToString();
        }


        private void CheckBox_Checked_3(object sender, RoutedEventArgs e)
        {
            if (Erreur3.IsChecked == true)
                _targets[3] = null;
            else if (Erreur1.IsChecked == false)
                _targets[3] = Box_Loc_cible4.SelectedItem.ToString();
        }

        private void CheckBox_Checked_4(object sender, RoutedEventArgs e)
        {
            if (Erreur4.IsChecked == true)
                _targets[4] = null;
            else if (Erreur1.IsChecked == false)
                _targets[4] = Box_Loc_cible5.SelectedItem.ToString();
        }

        private void CheckBox_Checked_5(object sender, RoutedEventArgs e)
        {
            if (Erreur5.IsChecked == true)
                _targets[5] = null;
            else if (Erreur1.IsChecked == false)
                _targets[5] = Box_Loc_cible6.SelectedItem.ToString();
        }
    }
}
