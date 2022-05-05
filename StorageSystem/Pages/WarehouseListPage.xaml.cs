using StorageSystem.DataAccess;
using StorageSystem.Model;
using StorageSystem.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StorageSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для WarehouseListPage.xaml
    /// </summary>
    public partial class WarehouseListPage : Page
    {

        public ObservableCollection<WarehouseUnit> _WarehouseList { get; set; } = new ObservableCollection<WarehouseUnit>();

        private bool _EditMode = false;


        public WarehouseListPage()
        {
            InitializeComponent();

            

            ConfigurePage();
            UpdateDatagrid();

            

        }



        private void ConfigurePage()
        {

            if(CurrentUser.Role == UserRole.Employee)
            {

                AvaiabilityGridColumn.Visibility = Visibility.Collapsed;

            }



        }

        private async void UpdateDatagrid()
        {

            Mouse.OverrideCursor = Cursors.Wait;

            if (CurrentUser.Role == UserRole.Employee)
            {
                _WarehouseList = new ObservableCollection<WarehouseUnit>(await StorageDbOperations.GetAvailableWarehouses());

            }
            else
            {

                _WarehouseList = new ObservableCollection<WarehouseUnit>(await StorageDbOperations.GetAllWarehouses());

            }

            WarehouseDatagrid.ItemsSource = _WarehouseList;

            Mouse.OverrideCursor = Cursors.Arrow;

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {            

            if (_EditMode)
            {
                EditButton.ClearValue(Button.BackgroundProperty);
                _EditMode = false;

                AmountColumn.IsReadOnly = true;
                MinAmountColumn.IsReadOnly = true;

                MinAmountColumn.ClearValue(DataGridColumn.HeaderStyleProperty);
                AmountColumn.ClearValue(DataGridColumn.HeaderStyleProperty);
                
            }
            else
            {

                EditButton.Background = Brushes.Goldenrod;
                _EditMode = true;

                AmountColumn.IsReadOnly = false;
                MinAmountColumn.IsReadOnly = false;
            
                MinAmountColumn.HeaderStyle = StyleCreator.EditableGridHeader();
                AmountColumn.HeaderStyle = StyleCreator.EditableGridHeader();
                
            }

            
            

        }

        private void WarehouseDatagrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            StorageDbOperations.SaveChanges();

        }
    }
}
