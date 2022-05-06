using StorageSystem.DataAccess;
using StorageSystem.Model;
using StorageSystem.Style;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        public ObservableCollection<WarehouseUnit> WarehouseList { get; set; } = new ObservableCollection<WarehouseUnit>();


        private bool _EditMode = false;


        public WarehouseListPage()
        {
            InitializeComponent();

            DataContext = this;
           
        }



        private void ConfigurePage()
        {

            if(CurrentUser.Role == UserRole.Employee)
            {

                EditButton.Visibility = Visibility.Collapsed;

            }



        }

        private void LoadDatagrid()
        {

            if (AvailabilityComboBox is null || AmountComboBox is null || WarehouseDatagrid is null )
                return;

            Mouse.OverrideCursor = Cursors.Wait;
          

            var items = StorageDbOperations.GetAllWarehouses();


            switch (((ComboBoxItem)AvailabilityComboBox.SelectedItem).Content.ToString())
            {

                case "Доступен":
                    items = items.Where(w => w.IsAvailable == true).ToList();
                    break;
                case "Не доступен":
                        items = items.Where(w => w.IsAvailable == false).ToList();
                    break;

            }

            switch (((ComboBoxItem)AmountComboBox.SelectedItem).Content.ToString())
            {

                case "Меньше минимума":
                    items = items.Where(w => w.Amount <= w.MinimalAmount).ToList();
                    break;
                case "Больше минимума":
                    items = items.Where(w => w.Amount >= w.MinimalAmount).ToList();
                    break;

            }

            WarehouseList.Clear();

            foreach (var warehouse in  items)
            {

                WarehouseList.Add(warehouse);

            }

                                          

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
                AvailabilityGridColumn.IsReadOnly = true;

                MinAmountColumn.ClearValue(DataGridColumn.HeaderStyleProperty);
                AmountColumn.ClearValue(DataGridColumn.HeaderStyleProperty);
                AvailabilityGridColumn.ClearValue(DataGridColumn.HeaderStyleProperty);

            }
            else
            {

                EditButton.Background = Brushes.Goldenrod;
                _EditMode = true;

                AmountColumn.IsReadOnly = false;
                MinAmountColumn.IsReadOnly = false;
                AvailabilityGridColumn.IsReadOnly = false;


                MinAmountColumn.HeaderStyle = StyleCreator.EditableGridHeader();
                AmountColumn.HeaderStyle = StyleCreator.EditableGridHeader();
                AvailabilityGridColumn.HeaderStyle = StyleCreator.EditableGridHeader();

            }

        }

        private void WarehouseDatagrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            StorageDbOperations.SaveChanges();

        }

        private void AvailabilityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDatagrid();
        }

        private void AmountComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDatagrid();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigurePage();
            LoadDatagrid();
            WarehouseDatagrid.ItemsSource = WarehouseList;

        }
    }
}
