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
    /// Interaction logic for AddSupplierPage.xaml
    /// </summary>
    public partial class AddSupplierPage : Page
    {
        public AddSupplierPage()
        {
            InitializeComponent();
        }




        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {

            if (!IsPageValid())
                return;

            AddNewSupplier();

        }



        private bool IsPageValid()
        {

            if (string.IsNullOrEmpty(NameTextBox.Text)
                || string.IsNullOrEmpty(AddressTextBox.Text)
                || string.IsNullOrEmpty(EmailTextBox.Text)
                || string.IsNullOrEmpty(PhoneTextBox.Text))
            {

                MessageBoxDisplay.DisplayError("Все данные должны быть введены");

                return false;
            }

            if (PhoneTextBox.Text.Length < 12)
            {
                MessageBoxDisplay.DisplayError("Номер телефона должен быть больше 7 символов");
                return false;
            }



            return true;

        }


        private async void AddNewSupplier()
        {
            var newSupplier = new Supplier();

            newSupplier.Address = AddressTextBox.Text;
            newSupplier.Name = NameTextBox.Text;
            newSupplier.Email = EmailTextBox.Text;
            newSupplier.PhoneNumber = PhoneTextBox.Text.Replace(" ",string.Empty);

            try
            {
                await StorageDbOperations.AddNewSupplier(newSupplier);
            }
            catch (Exception ex)
            {

                MessageBoxDisplay.DisplayError(ex.Message);
                return;

            }

            MessageBoxDisplay.DisplayNotification("Новый поставщик успешно добавлен");

            this.NavigationService.Navigate(new Uri("/Pages/AddSupplierPage.xaml", UriKind.Relative));

        }

        private void PhoneTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (!InputValidation.IsDigitKey(e.Key) && e.Key != Key.Back)
            {
                e.Handled = true;
            }


        }



    }
}
