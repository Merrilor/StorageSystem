﻿using Microsoft.Win32;
using StorageSystem.DataAccess;
using StorageSystem.Model;
using StorageSystem.UserInteraction;
using StorageSystem.Windows;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace StorageSystem.Pages
{
    /// <summary>
    /// Interaction logic for AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {

        public Product CurrentProduct { get; set; }

        public ObservableCollection<ProductImage> ProductImageList { get; set; } = new ObservableCollection<ProductImage>();

        public AddEditProductPage()
        {
            InitializeComponent();

            ConfigurePage();
            

        }


        public AddEditProductPage(int productId) : this()
        {

            AddEditProductButton.Content = "Изменить товар";

            LoadProductValues(productId);

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

            ImageListView.ItemsSource = ProductImageList;

        }

        private async void LoadProductValues(int productId)
        {

            CurrentProduct = await StorageDbOperations.GetProduct(productId);

            this.DataContext = CurrentProduct;

            ProductCodeTextBox.Text = CurrentProduct.Code.ToString();           
            ProductTypeComboBox.SelectedItem = CurrentProduct.ProductType;

            
            ProductImageList.Clear();

            foreach (var image in CurrentProduct.ProductImage)
            {
                ProductImageList.Add(image);
            }


        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image (*.png);(*.jpg);(*.jpeg) | *.png;*.jpg;*.jpeg;";


            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;

                var newImage = new ProductImage
                {

                    ImagePath = "/images/product/" + Path.GetFileName(path)


                };

                if (!File.Exists(newImage.FullImagePath))
                {

                    File.Copy(path, Directory.GetCurrentDirectory() + $@"/images/product/{Path.GetFileName(path)}", true);

                }

                ProductImageList.Add(newImage);


            }
        }

        private void EditImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteImageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddEditProductButton_Click(object sender, RoutedEventArgs e)
        {

            if(AddEditProductButton.Content.ToString() == "Добавить товар")
            {
                AddProduct();


            }else if (AddEditProductButton.Content.ToString() == "Изменить товар")
            {

                EditProduct();

            }




        }


        private async void AddProduct()
        {


            if (!Validate())
                return;

            CurrentProduct = new Product();

            FillProductFields();

            await StorageDbOperations.AddNewProduct(CurrentProduct);

            MessageBoxDisplay.DisplayNotification("Товар успешно добавлен");


            this.NavigationService.Navigate(new Uri("/Pages/AddEditProductPage.xaml", UriKind.Relative));

        }

        private async void EditProduct()
        {
            if (!Validate())
                return;

            FillProductFields();


            await StorageDbOperations.EditProduct(CurrentProduct);

            MessageBoxDisplay.DisplayNotification("Товар успешно изменен");

            NavigationService.GoBack();

        }

        private void FillProductFields()
        {

            CurrentProduct.Title = ProductTitleTextBox.Text;
            CurrentProduct.BrandName = ProductBrandTextBox.Text;
            CurrentProduct.Code = Convert.ToDecimal(ProductCodeTextBox.Text);
            CurrentProduct.Barcode = ProductBarcodeTextBox.Text;
            CurrentProduct.Weight = Convert.ToInt32(ProductWeightTextBox.Text);
            CurrentProduct.DefaultPrice = Convert.ToDecimal(DefaultPriceTextBox.Text.Replace('.',','));
            CurrentProduct.DiscountPercent = Convert.ToInt32(DiscountTextBox.Text);
            CurrentProduct.ProductTypeId = ((ProductType)ProductTypeComboBox.SelectedValue).ProductTypeId;

        }


        private bool Validate()
        {

            string errorMessage = "";

            if (string.IsNullOrEmpty(ProductTitleTextBox.Text))
            {
                errorMessage += "Необходимо ввести название продукта \n";
            }

            if (string.IsNullOrEmpty(ProductBrandTextBox.Text))
            {
                errorMessage += "Необходимо ввести название бренда \n";
            }

            if (string.IsNullOrEmpty(ProductBarcodeTextBox.Text))
            {
                errorMessage += "Необходимо ввести штрихкод \n";

            }
            else if (ProductBarcodeTextBox.Text.Length != 13)
            {
                errorMessage += "Штрихкод должен быть равен 13 символам \n";

            }            

            if(string.IsNullOrEmpty(ProductWeightTextBox.Text))
            {
                errorMessage += "Необходимо ввести вес \n";
            }
            else
            {
                var weight = Convert.ToInt32(ProductWeightTextBox.Text);

                if(weight == 0)
                    errorMessage += "Вес не может быть равен 0 \n";

            }

            if (string.IsNullOrEmpty(DefaultPriceTextBox.Text))
            {

                errorMessage += "Необходимо ввести цену \n";

            }
            else
            {

                var price = Convert.ToDecimal(DefaultPriceTextBox.Text.Replace('.',','));
                if (price == 0)
                    errorMessage += "Цена не может быть равна 0 \n";


            }


            if (string.IsNullOrEmpty(DiscountTextBox.Text))
            {

                errorMessage += "Необходимо ввести процент скидки \n";

            }

            
            if(errorMessage.Length > 0)
            {
                MessageBoxDisplay.DisplayError(errorMessage);

                return false;
            }

            return true;
            
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
                

            decimal defaultPrice = Convert.ToDecimal(DefaultPriceTextBox.Text.Replace('.',','));
            int discountPercent = Convert.ToInt32(DiscountTextBox.Text);

            decimal onePercent = defaultPrice / 100;

            decimal totalPrice = defaultPrice - (discountPercent * onePercent);

            TotalPriceTextBlock.Text = $"{totalPrice:N} руб";

        }
    }
}
