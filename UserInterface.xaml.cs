﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using VMS.TPS.Common.Model.API;
using Ookii.Dialogs.Wpf;
using System.Windows.Forms.Design;
using Opti_Struct;

namespace Structure_optimisation
{
    /// <summary>
    /// Logique d'interaction pour UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
        private UserInterfaceModel _model;
        private List<string> _targets;
        private int _ciblesTrouvees;

        public UserInterface(ScriptContext context)
        {
            InitializeComponent();
            _model = new UserInterfaceModel(context, _targets);
            _targets = new List<string>();
            DataContext = _model;

            // Choix du fichier à utiliser
            Box_Loc.Visibility = Visibility.Visible;
            Text_loc.Visibility = Visibility.Visible;
            Info_path.Visibility = Visibility.Visible;
            Bouton_path.Visibility = Visibility.Visible;

            // Combobox des cibles
            Text_Targets_title.Visibility = Visibility.Collapsed;
            Box_Loc_cible1.Visibility = Visibility.Collapsed;
            text_Cible1.Visibility = Visibility.Collapsed;
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

            OK_Button.Visibility = Visibility.Collapsed;

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

        #region ComboBox for file choice
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OK_Button.Visibility = Visibility.Visible;

            if (Box_Loc.SelectedItem != null)
            {
                try
                {
                    _model.Targets = _targets;
                    _model.UserFile = (string)Box_Loc.SelectedItem;

                    _ciblesTrouvees = Enumerable.Range(1, 6)
                                       .Select(i => $"cible{i}")
                                       .Where(cible => System.IO.File.ReadAllText(System.IO.Path.GetFullPath(_model.GetUserPath)).Contains(cible))
                                       .Distinct()
                                       .Count();

                    switch (_ciblesTrouvees)
                    {
                        case 1:

                            Targets_Definition.FillTargets(_model, _model.UserFile);

                            Text_Targets_title.Visibility = Visibility.Visible;
                            Box_Loc_cible1.Visibility = Visibility.Visible;
                            text_Cible1.Visibility = Visibility.Visible;


                            text_Cible1.Text = (string)Targets_Definition.Targets.First()[0];


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
                            break;

                        case 2:
                            Targets_Definition.FillTargets(_model, _model.UserFile);

                            Text_Targets_title.Visibility = Visibility.Visible;
                            Box_Loc_cible1.Visibility = Visibility.Visible;
                            text_Cible1.Visibility = Visibility.Visible;

                            Box_Loc_cible2.Visibility = Visibility.Visible;
                            text_Cible2.Visibility = Visibility.Visible;

                            text_Cible1.Text = (string)Targets_Definition.Targets.First()[0];
                            text_Cible2.Text = (string)Targets_Definition.Targets.First()[1];


                            Box_Loc_cible3.Visibility = Visibility.Collapsed;
                            text_Cible3.Visibility = Visibility.Collapsed;

                            Box_Loc_cible4.Visibility = Visibility.Collapsed;
                            text_Cible4.Visibility = Visibility.Collapsed;

                            Box_Loc_cible5.Visibility = Visibility.Collapsed;
                            text_Cible5.Visibility = Visibility.Collapsed;

                            Box_Loc_cible6.Visibility = Visibility.Collapsed;
                            text_Cible6.Visibility = Visibility.Collapsed;
                            break;

                        case 3:
                            Targets_Definition.FillTargets(_model, _model.UserFile);

                            Text_Targets_title.Visibility = Visibility.Visible;
                            Box_Loc_cible1.Visibility = Visibility.Visible;
                            text_Cible1.Visibility = Visibility.Visible;

                            Box_Loc_cible2.Visibility = Visibility.Visible;
                            text_Cible2.Visibility = Visibility.Visible;

                            Box_Loc_cible3.Visibility = Visibility.Visible;
                            text_Cible3.Visibility = Visibility.Visible;

                            text_Cible1.Text = (string)Targets_Definition.Targets.First()[0];
                            text_Cible2.Text = (string)Targets_Definition.Targets.First()[1];
                            text_Cible3.Text = (string)Targets_Definition.Targets.First()[2];


                            Box_Loc_cible4.Visibility = Visibility.Collapsed;
                            text_Cible4.Visibility = Visibility.Collapsed;

                            Box_Loc_cible5.Visibility = Visibility.Collapsed;
                            text_Cible5.Visibility = Visibility.Collapsed;

                            Box_Loc_cible6.Visibility = Visibility.Collapsed;
                            text_Cible6.Visibility = Visibility.Collapsed;
                            break;

                        case 4:
                            Targets_Definition.FillTargets(_model, _model.UserFile);

                            Text_Targets_title.Visibility = Visibility.Visible;
                            Box_Loc_cible1.Visibility = Visibility.Visible;
                            text_Cible1.Visibility = Visibility.Visible;

                            Box_Loc_cible2.Visibility = Visibility.Visible;
                            text_Cible2.Visibility = Visibility.Visible;

                            Box_Loc_cible3.Visibility = Visibility.Visible;
                            text_Cible3.Visibility = Visibility.Visible;

                            Box_Loc_cible4.Visibility = Visibility.Visible;
                            text_Cible4.Visibility = Visibility.Visible;

                            text_Cible1.Text = (string)Targets_Definition.Targets.First()[0];
                            text_Cible2.Text = (string)Targets_Definition.Targets.First()[1];
                            text_Cible3.Text = (string)Targets_Definition.Targets.First()[2];
                            text_Cible4.Text = (string)Targets_Definition.Targets.First()[3];


                            Box_Loc_cible5.Visibility = Visibility.Collapsed;
                            text_Cible5.Visibility = Visibility.Collapsed;

                            Box_Loc_cible6.Visibility = Visibility.Collapsed;
                            text_Cible6.Visibility = Visibility.Collapsed;
                            break;

                        case 5:

                            Targets_Definition.FillTargets(_model, _model.UserFile);

                            Text_Targets_title.Visibility = Visibility.Visible;
                            Box_Loc_cible1.Visibility = Visibility.Visible;
                            text_Cible1.Visibility = Visibility.Visible;

                            Box_Loc_cible2.Visibility = Visibility.Visible;
                            text_Cible2.Visibility = Visibility.Visible;

                            Box_Loc_cible3.Visibility = Visibility.Visible;
                            text_Cible3.Visibility = Visibility.Visible;

                            Box_Loc_cible4.Visibility = Visibility.Visible;
                            text_Cible4.Visibility = Visibility.Visible;

                            Box_Loc_cible5.Visibility = Visibility.Visible;
                            text_Cible5.Visibility = Visibility.Visible;

                            text_Cible1.Text = (string)Targets_Definition.Targets.First()[0];
                            text_Cible2.Text = (string)Targets_Definition.Targets.First()[1];
                            text_Cible3.Text = (string)Targets_Definition.Targets.First()[2];
                            text_Cible4.Text = (string)Targets_Definition.Targets.First()[3];
                            text_Cible5.Text = (string)Targets_Definition.Targets.First()[4];


                            Box_Loc_cible6.Visibility = Visibility.Collapsed;
                            text_Cible6.Visibility = Visibility.Collapsed;
                            break;

                        case 6:
                            Targets_Definition.FillTargets(_model, _model.UserFile);

                            Text_Targets_title.Visibility = Visibility.Visible;
                            Box_Loc_cible1.Visibility = Visibility.Visible;
                            text_Cible1.Visibility = Visibility.Visible;

                            Box_Loc_cible2.Visibility = Visibility.Visible;
                            text_Cible2.Visibility = Visibility.Visible;

                            Box_Loc_cible3.Visibility = Visibility.Visible;
                            text_Cible3.Visibility = Visibility.Visible;

                            Box_Loc_cible4.Visibility = Visibility.Visible;
                            text_Cible4.Visibility = Visibility.Visible;

                            Box_Loc_cible5.Visibility = Visibility.Visible;
                            text_Cible5.Visibility = Visibility.Visible;

                            Box_Loc_cible6.Visibility = Visibility.Visible;
                            text_Cible6.Visibility = Visibility.Visible;

                            text_Cible1.Text = (string)Targets_Definition.Targets.First()[0];
                            text_Cible2.Text = (string)Targets_Definition.Targets.First()[1];
                            text_Cible3.Text = (string)Targets_Definition.Targets.First()[2];
                            text_Cible4.Text = (string)Targets_Definition.Targets.First()[3];
                            text_Cible5.Text = (string)Targets_Definition.Targets.First()[4];
                            text_Cible6.Text = (string)Targets_Definition.Targets.First()[5];

                            break;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show($"La ComboBox est vide", "Erreur", MessageBoxButton.OKCancel, MessageBoxImage.Error);
        }
        #endregion

        #region Boutons OK et Close
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
                _model.File.LaunchSegmentation();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
        #endregion

        #region Path
        private void Button_Click_path(object sender, RoutedEventArgs e)
        {
            var folderPicker = new VistaFolderBrowserDialog
            {
                Description = "Sélectionnez un dossier",
                UseDescriptionForTitle = true,
                ShowNewFolderButton = true
            };

            string initialPath = _model.GetDirectoryPath;

            if (!string.IsNullOrEmpty(initialPath) && System.IO.Directory.Exists(initialPath))
            {
                folderPicker.SelectedPath = initialPath;
            }

            bool? result = folderPicker.ShowDialog();

            try
            {
                _model.GetDirectoryPath = folderPicker.SelectedPath;
                _model.Message = $"\nChangement du dossier initial, le nouveau dossier est le suivant :";
                _model.Message += $"{_model.GetDirectoryPath}\n";
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
        #endregion

        private void ComboBox_SelectionChanged_cible1(object sender, SelectionChangedEventArgs e)
        {
            if (Box_Loc_cible1.SelectedItem != null)
            {
                if (_targets.Count < 1)
                    _targets.Add(Box_Loc_cible1.SelectedItem.ToString());
                else
                    _targets[0] = Box_Loc_cible1.SelectedItem.ToString();
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

            }
        }

    }
}
