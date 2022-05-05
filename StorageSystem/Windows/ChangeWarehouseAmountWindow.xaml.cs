using StorageSystem.DataAccess;
using StorageSystem.Model;
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

namespace StorageSystem.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeWarehouseAmountWindow.xaml
    /// </summary>
    public partial class ChangeWarehouseAmountWindow : Window
    {

        private WarehouseUnit _WarehouseUnit;

        public ChangeWarehouseAmountWindow(int warehouseId)
        {
            InitializeComponent();

            LoadWarehouseUnit(warehouseId);

        }

        private async void LoadWarehouseUnit(int id)
        {
            _WarehouseUnit = await StorageDbOperations.GetWarehouseUnit(id);
            NewAmountUpDown.Text = _WarehouseUnit.Amount.ToString();
        }

        private void SetAmountButton_Click(object sender, RoutedEventArgs e)
        {
            var newAmount = Convert.ToInt32(NewAmountUpDown);
        }
    }
}
