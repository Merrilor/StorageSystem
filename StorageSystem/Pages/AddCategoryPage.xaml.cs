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
    /// Interaction logic for AddCategoryPage.xaml
    /// </summary>
    public partial class AddCategoryPage : Page
    {
        public AddCategoryPage()
        {
            InitializeComponent();
        }

        private void SaveCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(CategoryNameTextBox.Text))
            {

                MessageBoxDisplay.DisplayError("Неоходимо ввести название категории");

            }

            Category newCategory = new Category() { Name = CategoryNameTextBox.Text };

            AddNewCategory(newCategory);
        }

        private async void AddNewCategory(Category category)
        {



            if (await StorageDbOperations.TryAddNewCategory(category) == true)
            {

                MessageBoxDisplay.DisplayNotification("Новая категория успешно добавлена");
                CategoryNameTextBox.Text = "";

            }
            else
            {

                MessageBoxDisplay.DisplayNotification("Эта категория уже существует");
            }





        }
    }
}
