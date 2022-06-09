using StorageSystem.DataAccess;
using StorageSystem.Model;
using StorageSystem.UserInteraction;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StorageSystem.Pages
{
    /// <summary>
    /// Interaction logic for RegisterSupplyPage.xaml
    /// </summary>
    public partial class RegisterSupplyPage : Page
    {
        public RegisterSupplyPage()
        {
            InitializeComponent();

            FillData();

        }

        private async void FillData()
        {

            ProductComboBox.ItemsSource = await StorageDbOperations.GetProductRange(0, 99999);
            SupplierComboBox.ItemsSource = await StorageDbOperations.GetAllSuppliers();
            DeliveryDateTextBox.Text = DateTime.Now.ToString("dd.MM.yyyy");

            ProductComboBox.SelectedIndex = 0;
            SupplierComboBox.SelectedIndex = 0;

        }

        private bool Validate()
        {

            string errorMessage = "";

            

            if (string.IsNullOrEmpty(AmountTextBox.Text))
            {
                errorMessage += "Необходимо ввести количество \n";
            }
            else
            {

                var amount = Convert.ToInt32(AmountTextBox.Text);

                if(amount == 0)
                {
                    errorMessage += "Количество должно быть больше 0 \n";
                }

            }

            if (string.IsNullOrEmpty(OrderDateTextBox.Text))
            {
                errorMessage += "Необходимо ввести дату заказа \n";

            }
            else
            {
                DateTime date;

                DateTime.TryParse(OrderDateTextBox.Text,out date);

                if(date == DateTime.MinValue)
                {
                    errorMessage += "Неверный формат даты заказа \n";
                }
                else
                {

                    if(date > DateTime.Now)
                    {
                        errorMessage += "Дата заказа не может быть больше даты поставки \n";
                    }

                }

            }
            

            if (string.IsNullOrEmpty(PriceTextBox.Text))
            {

                errorMessage += "Необходимо ввести цену \n";

            }
            else
            {

                var price = Convert.ToDecimal(PriceTextBox.Text.Replace('.', ','));
                if (price == 0)
                    errorMessage += "Цена не может быть равна 0 \n";


            }


            


            if (errorMessage.Length > 0)
            {
                MessageBoxDisplay.DisplayError(errorMessage);

                return false;
            }

            return true;

        }
        

        private void AmountTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (InputValidation.IsSpaceKey(e.Key))
            {
                e.Handled = true;
                return;
            }

            if (!InputValidation.IsDigitKey(e.Key) && e.Key != Key.Back)
            {
                e.Handled = true;
                return;
            }
        }

        private void PriceTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!InputValidation.ValidatePrice(e.Key, PriceTextBox.Text))
            {
                e.Handled = true;
            }
        }

        private async void AddSupplyOrder()
        {

            if (!Validate())
                return;

            SupplyOrder supplyOrder = new SupplyOrder
            {

                SupplierId = ((Supplier)SupplierComboBox.SelectedItem).SupplierId,
                Amount = Convert.ToInt32(AmountTextBox.Text),
                PricePerProduct = Convert.ToDecimal(PriceTextBox.Text),
                DeliveryStartDate = Convert.ToDateTime(OrderDateTextBox.Text),
                DeliveryEndDate = Convert.ToDateTime(DeliveryDateTextBox.Text),


            };

            await StorageDbOperations.RegisterSupplyOrder(supplyOrder, (Product)ProductComboBox.SelectedItem);


            MessageBoxDisplay.DisplayNotification("Заказ успешно подтвержден");


            this.NavigationService.Navigate(new Uri("/Pages/RegisterSupplyPage.xaml", UriKind.Relative));


        }

        private void AddSupplyOrderButton_Click(object sender, RoutedEventArgs e)
        {

            AddSupplyOrder();

        }

    }
}
