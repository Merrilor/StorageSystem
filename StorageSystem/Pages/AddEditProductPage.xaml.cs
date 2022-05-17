using StorageSystem.DataAccess;
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
    /// Interaction logic for AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        public AddEditProductPage()
        {
            InitializeComponent();

            ConfigurePage();
            

        }


        public AddEditProductPage(int productId) : this()
        {

            AddEditProductButton.Content = "Изменить продукт";

            LoadProductValues();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {




        }


        private async void ConfigurePage()
        {

            ProductTypeComboBox.ItemsSource = await StorageDbOperations.GetAllProductTypes();
            ProductTypeComboBox.SelectedIndex = 0;

            var lastProduct =  StorageDbOperations.GetLastProduct();
            ProductCodeTextBox.Text = (lastProduct.Code + 1).ToString();

        }

        private async void LoadProductValues()
        {



        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddEditProductButton_Click(object sender, RoutedEventArgs e)
        {

            if(AddEditProductButton.Content.ToString() == "Добавить продукт")
            {



            }else if (AddEditProductButton.Content.ToString() == "Изменить продукт")
            {



            }




        }


        private void AddProduct()
        {



        }

        private void EditProduct()
        {


        }

        private bool Validate()
        {






            return false;
        }

        private void NumberTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
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

        private void DefaultPriceTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if(!InputValidation.ValidatePrice(e.Key, DefaultPriceTextBox.Text))
            {
                e.Handled = true;
            }


        }

        private void UpdateTotalPrice(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(DefaultPriceTextBox.Text) || string.IsNullOrEmpty(DiscountTextBox.Text))
            {
                TotalPriceTextBlock.Text = "0 руб";
                return;
            }
                

            decimal defaultPrice = Convert.ToDecimal(DefaultPriceTextBox.Text);
            int discountPercent = Convert.ToInt32(DiscountTextBox.Text);

            decimal onePercent = defaultPrice / 100;

            decimal totalPrice = defaultPrice - (discountPercent * onePercent);

            TotalPriceTextBlock.Text = $"{totalPrice:N} руб";

        }
    }
}
