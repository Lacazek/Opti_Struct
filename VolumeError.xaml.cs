using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VMS.TPS.Common.Model.API;

namespace Opti_Struct
{
    /// <summary>
    /// Logique d'interaction pour VolumeError.xaml
    /// </summary>
    /// 


    // A voir comment assigner les structures en erreur à leurs noms


    public partial class VolumeError : Window
    {
        private VolumErrorModel _volErrorModel;
        public VolumeError(StructureSet ss)
        {
            InitializeComponent();
            _volErrorModel = new VolumErrorModel(ss);
            DataContext = _volErrorModel;
        }

        internal List <string> getErrorList
        {
            get { return _volErrorModel.getErrorList; }
        }
        internal List<string> getSSList
        {
            get { return _volErrorModel.getSSList; }
        }

        internal string setErrorList
        {
            set
            {
                _volErrorModel.setErrorList = value;
            }
        }
        internal List<string> GetNewStruct
        { 
            get { return _volErrorModel.GetNewStruct; }

        }
        internal string SetNewStruct
        {
            set
            {
                _volErrorModel.SetNewStruct = value;
            }
        }


        private void ListBox_Item_StructureSet(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_SS.SelectedIndex != -1)
            {
                string selected_item = ListBox_SS.SelectedItem.ToString();

                //if (!_volErrorModel.GetNewStruct.Any(x=>x.singlselected_item(_volErrorModel.GetSS).Structure.singlselected_item))
                {
                    SetNewStruct = selected_item;
                }
            }
        }

        private void ListBox_Item_Error(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBox_Item_Selection(object sender, SelectionChangedEventArgs e)
        {
           // ListBox_Selection.Item.Add(GetNewStruct);
        }
    }

}
