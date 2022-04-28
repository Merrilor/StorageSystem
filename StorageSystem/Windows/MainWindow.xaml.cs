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
using REghZyFramework.Themes;
using StorageSystem.Windows;

namespace StorageSystem.Windows
{

    public partial class MainWindow : Window
    {

        private string CurrentTheme = "Dark";

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if(CurrentTheme == "Dark")
            {
                ThemesController.SetTheme(ThemesController.ThemeTypes.Light);
                CurrentTheme = "Light";
            }
            else
            {
                ThemesController.SetTheme(ThemesController.ThemeTypes.ColourfulDark);
                CurrentTheme = "Dark";
            }



        }
    }
}
