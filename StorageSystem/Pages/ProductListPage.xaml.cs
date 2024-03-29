﻿using StorageSystem.DataAccess;
using StorageSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
    /// Interaction logic for ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {

        private bool _IsLoaded;

        public ObservableCollection<Product> ProductList { get; set; } = new ObservableCollection<Product>();



        public ProductListPage()
        {
            InitializeComponent();
           
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            if (_IsLoaded)
            {
                await UpdateListView();
                return;
            }
                

            await LoadFilterValues();
            await UpdateListView();

            MinCodeUpDown.ValueChanged += CodeUpDown_ValueChanged;
            MaxCodeUpDown.ValueChanged += CodeUpDown_ValueChanged;
            SortComboBox.SelectionChanged += Filter_Changed;
            SortOrderComboBox.SelectionChanged += Filter_Changed;
            BrandComboBox.SelectionChanged += Filter_Changed;
            ProductTypeComboBox.SelectionChanged += Filter_Changed;



            ProductListView.ItemsSource = ProductList;

            _IsLoaded = true;

        }

        private async Task LoadFilterValues()
        {

            var brands = await StorageDbOperations.GetAllBrands();
            brands.Insert(0, "Все");
            BrandComboBox.ItemsSource = brands;
            BrandComboBox.SelectedIndex = 0;

            var productTypes = await StorageDbOperations.GetAllProductTypes();
            productTypes.Insert(0, new ProductType { Name = "Все" });
            ProductTypeComboBox.ItemsSource = productTypes;
            ProductTypeComboBox.SelectedIndex = 0;

            SortComboBox.SelectedIndex = 0;
            SortOrderComboBox.SelectedIndex = 0;

            MinCodeUpDown.Text = "1";
            MinCodeUpDown.DefaultValue = 1;


            MaxCodeUpDown.Text = "100";
            MaxCodeUpDown.DefaultValue = 100;

        }

        private async Task UpdateListView()
        {


            var products = await StorageDbOperations.GetProductRange((int)MinCodeUpDown.Value, (int)MaxCodeUpDown.Value);
          

            if(BrandComboBox.SelectedValue.ToString() != "Все")
            {

                products = products.Where(p => p.BrandName == BrandComboBox.SelectedValue.ToString()).ToList();

            }
           

            var selectedProductType = (ProductType)ProductTypeComboBox.SelectedItem;

            if (selectedProductType is null)
                return;

            if (selectedProductType.Name != "Все" )
            {

                products = products.Where(p => p.ProductType.Name == selectedProductType.Name).ToList();

            }          

            var selectedSort = SortComboBox.SelectedValue.ToString();

            if(SortOrderComboBox.SelectedValue.ToString() == "Возрастанию")
            {

                switch (selectedSort)
                {

                    case "Код(Стандарт)":
                        products = products.OrderBy(p => p.Code).ToList();
                        break;

                    case "Вес":
                        products = products.OrderBy(p => p.Weight).ToList();
                        break;
                    case "Цена":
                        products = products.OrderBy(p => p.DefaultPrice).ToList();
                        break;
                    case "Скидка":
                        products = products.OrderBy(p => p.DiscountPercent).ToList();
                        break;

                }



            }else if (SortOrderComboBox.SelectedValue.ToString() == "Убыванию")
            {

                switch (selectedSort)
                {

                    case "Код(Стандарт)":
                        products = products.OrderByDescending(p => p.Code).ToList();
                        break;

                    case "Вес":
                        products = products.OrderByDescending(p => p.Weight).ToList();
                        break;
                    case "Цена":
                        products = products.OrderByDescending(p => p.DefaultPrice).ToList();
                        break;
                    case "Скидка":
                        products = products.OrderByDescending(p => p.DiscountPercent).ToList();
                        break;

                }

            }
           

            if(SearchTextBox.Text.Length > 1)
            {
                var searchQuery = SearchTextBox.Text.ToLower();
                products = products.Where(p => p.Title.ToLower().Contains( searchQuery) || p.Code.ToString().Contains( searchQuery)).ToList();

            }

            var productList =  products.ToList();

            ProductList.Clear();
            foreach (var product in productList)
            {
                ProductList.Add(product);
            }


        }
       


        private async void CodeUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
           await UpdateListView();
        }

        private async void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
          await UpdateListView();
           
        }

        private async void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
          await  UpdateListView();
        }

        private void AddNewProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEditProductPage());
        }

        private void ProductEditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            int productId = Convert.ToInt32(button.Tag);

            NavigationService.Navigate(new AddEditProductPage(productId));

        }
    }
}
