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
    /// Interaction logic for UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {

        public ObservableCollection<User> UserCollection { get; set; } = new ObservableCollection<User>();

        public UserListPage()
        {
            InitializeComponent();

            LoadData();

            UserListItemsControl.ItemsSource = UserCollection;

        }

        public async void LoadData()
        {

            var users = await StorageDbOperations.GetAllUsers();

            var userRoles = await StorageDbOperations.GetAllRoles();
            userRoles.Insert(0,new Role { Name = "Все" });

            UserRoleComboBox.ItemsSource = userRoles;
            UserRoleComboBox.SelectedIndex = 0;

            UserCollection.Clear();

            foreach (var user in users)
            {
                UserCollection.Add(user);
            }


        }

        public async void UpdateData()
        {
            var users = await StorageDbOperations.GetAllUsers();

            var selectedRole = (Role)UserRoleComboBox.SelectedItem;


            if(selectedRole.Name != "Все")
            {

                users = users.Where(u => u.Role.Name == selectedRole.Name).ToList();

            }


            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {

                users = users.Where(u => u.Login.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();

            }

            UserCollection.Clear();

            foreach (var user in users)
            {
                UserCollection.Add(user);
            }



        }

        private void UserRoleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }
    }
}
