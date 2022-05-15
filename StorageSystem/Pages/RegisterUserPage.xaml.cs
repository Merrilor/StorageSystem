using Microsoft.Win32;
using StorageSystem.DataAccess;
using StorageSystem.Model;
using StorageSystem.UserInteraction;
using StorageSystem.Windows;
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

namespace StorageSystem.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegisterUserPage.xaml
    /// </summary>
    public partial class RegisterUserPage : Page
    {

        private string _ImagePath = "/images/user/placeholderUser.jpg";

        private string _FullImagePath = "";

        public RegisterUserPage()
        {
            InitializeComponent();


            LoadData();


        }


        private async void LoadData()
        {

            UserRoleComboBox.ItemsSource = await StorageDbOperations.GetAllRoles();


        }

        private void ChangeImageButton_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image (*.png);(*.jpg);(*.jpeg) | *.png;*.jpg;*.jpeg;";


            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;

                _FullImagePath = path;

                _ImagePath = $@"/images/user/" + Path.GetFileName(path);

                UserImage.Source = new BitmapImage(new Uri(path, UriKind.Absolute));


            }


        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {

            if(string.IsNullOrEmpty(UserLoginTextBox.Text) || string.IsNullOrEmpty(UserPasswordTextBox.Text))
            {

                MessageBoxDisplay.DisplayError("Не введены все данные");
                return;
            }

            if(UserPasswordTextBox.Text.Length <= 6)
            {
                MessageBoxDisplay.DisplayError("Пароль должен быть длиннее 6 символов");
                return;
            }


            User newUser = new User
            {
                Login = UserLoginTextBox.Text,
                Password = UserPasswordTextBox.Text,
                RoleId = ((Role)UserRoleComboBox.SelectedItem).RoleId,
                Photo = _ImagePath

            };

            StorageDbOperations.AddNewUser(newUser);

            if(_FullImagePath != "")
            {
                File.Copy(_FullImagePath, Directory.GetCurrentDirectory() + $@"/images/user/{Path.GetFileName(_FullImagePath)}", true);

            }


            MessageBoxDisplay.DisplayNotification("Пользователь успешно зарегистрирован");

            ResetPage();

        }

        private void ResetPage()
        {

            _FullImagePath = "";
            _ImagePath = "/images/user/placeholderUser.jpg";
            UserImage.Source = new BitmapImage(new Uri("/Image/placeholderUser.jpg", UriKind.Relative));

            UserRoleComboBox.SelectedIndex = 0;

            UserLoginTextBox.Text = "";
            UserPasswordTextBox.Text = "";


        }
    }
}
