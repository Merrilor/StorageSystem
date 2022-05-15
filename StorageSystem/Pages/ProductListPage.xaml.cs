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
    /// Interaction logic for ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {

        private const int _ProductsPerPage = 30;

        private int _CurrentPage = 0;

        public ObservableCollection<Product> ProductList { get; set; } = new ObservableCollection<Product>();


        public ProductListPage()
        {
            InitializeComponent();
        }

        private async void LoadListView()
        {


            var products = await StorageDbOperations.GetProductRange(_CurrentPage * _ProductsPerPage, _ProductsPerPage);





            ProductList.Clear();
            foreach (var product in products)
            {
                ProductList.Add(product);
            }


        }


        private void ProductEditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            LoadListView();
            ProductListView.ItemsSource = ProductList;
        }
    }
}
