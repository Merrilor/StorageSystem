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
using StorageSystem.DataAccess;
using StorageSystem.UserInteraction;

namespace StorageSystem.Windows
{
  
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrEmpty(UserLoginTextBox.Text) || string.IsNullOrEmpty(UserPasswordBox.Password))
            {

                MessageBoxDisplay.DisplayError("Не введены все данные");
                return;

            }

            if(await StorageDbOperations.TryLoginUser(UserLoginTextBox.Text, UserPasswordBox.Password))
            {

                new MainWindow().Show();

                this.Close();

            }
            else
            {

                MessageBoxDisplay.DisplayError("Пользователь не найден");
                return;

            }


        }
    }
}
