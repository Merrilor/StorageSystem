using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using REghZyFramework.Themes;
using StorageSystem.DataAccess;
using StorageSystem.Pages;
using StorageSystem.UserInteraction;
using StorageSystem.Windows;

namespace StorageSystem.Windows
{

    public partial class MainWindow : Window
    {

        private string CurrentTheme = "Dark";

        public MainWindow()
        {
            InitializeComponent();            

            ApplyUserInfo();

        }

        private void ApplyUserInfo()
        {

            UserImage.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + CurrentUser.UserImagePath,UriKind.Absolute));
            UserNameTextBlock.Text = CurrentUser.Login;
            if(CurrentUser.Role != UserRole.Admin)
            {
                AdminExpander.Visibility = Visibility.Collapsed;
            }

        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {

            if(CurrentTheme == "Dark")
            {
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
                CurrentTheme = "Light";
                ChangeThemeButton.Content = "\uf186";
               

            }
            else
            {
                ThemesController.SetTheme(ThemesController.ThemeTypes.ColourfulDark);
                CurrentTheme = "Dark";
                ChangeThemeButton.Content = "\uf185";

            }



        }


        private void NavigateToPage(Page page)
        {

            ContentFrame.Navigate(page);
            AddButtonDelay();
            WindowHeaderTextBlock.Text = page.Title;

        }

        private void WarehouseListButton_Click(object sender, RoutedEventArgs e)
        {

            NavigateToPage(new WarehouseListPage());
            
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new RegisterUserPage());

        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBoxDisplay.DisplayQuestion("Выйти из аккаунта?");


            if(result == MessageBoxResult.Yes)
            {

                try
                {
                    StorageDbOperations.LogoutUser();
                }
                catch (Exception ex)
                {

                    MessageBoxDisplay.DisplayError(ex.Message);
                    
                }

                new LoginWindow().Show();

                this.Close();


            }

        }

        

        private void ProductListButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new ProductListPage());

        }


        private async void AddButtonDelay(object sender)
        {

            UIElement element = (UIElement)sender;

            element.IsEnabled = false;
            await Task.Delay(200); 
            element.IsEnabled = true;


        }
        private async void AddButtonDelay()
        {

            MenuStackPanel.IsEnabled = false;
            await Task.Delay(200);
            MenuStackPanel.IsEnabled = true;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new AddEditProductPage());

        }

        private void AddNewCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new AddCategoryPage());
        }

        private void UserButton_Click(object sender, RoutedEventArgs e)
        {

            NavigateToPage(new UserListPage());

        }

        private void LoginHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new LoginHistoryPage());
        }

        private void AddSupplierButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToPage(new AddSupplierPage());
        }
    }
}
