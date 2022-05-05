using StorageSystem.DataAccess;
using StorageSystem.Model;
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
                ChangeAmountColumn.Visibility = Visibility.Collapsed;

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
    }
}
