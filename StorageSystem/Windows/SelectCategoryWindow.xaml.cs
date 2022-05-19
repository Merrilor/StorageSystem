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
    /// Interaction logic for AddNewCategoryWindow.xaml
    /// </summary>
    public partial class SelectCategoryWindow : Window
    {

        public Category SelectedCategory;

        public SelectCategoryWindow()
        {
            InitializeComponent();

            FillComboBox();

        }

        private async void FillComboBox()
        {

            var categories = await StorageDbOperations.GetCategories();

            CategoryComboBox.ItemsSource = categories;
            CategoryComboBox.SelectedIndex = 0;
            

        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {

            SelectedCategory = (Category)CategoryComboBox.SelectedItem;
            Window.GetWindow(this).DialogResult = true;
            this.Close();


        }
    }
}
