using System;
using System.Windows;
using System.Windows.Controls;
using VMS.TPS.Common.Model.API;
using Image = VMS.TPS.Common.Model.API.Image;

namespace Structure_optimisation
{
    /// <summary>
    /// Logique d'interaction pour UserInterface.xaml
    /// </summary>
    public partial class UserInterface : Window
    {
        private UserInterfaceModel _model;

        public UserInterface(StructureSet ss, Course course, Image image)
        {
            InitializeComponent();
            _model = new UserInterfaceModel(ss,course,image);
            DataContext = _model;
            OK_Button.Visibility = Visibility.Collapsed;
            foreach (string item in _model.Localisation)
            {
                Box_Loc.Items.Add(item);
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
                _model.UserFile = (string)Box_Loc.SelectedItem;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }
    }
}
