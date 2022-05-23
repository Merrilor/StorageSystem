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
    /// Interaction logic for LoginHistoryPage.xaml
    /// </summary>
    public partial class LoginHistoryPage : Page
    {

        public ObservableCollection<LoginHistory> HistoryList { get; set; } = new ObservableCollection<LoginHistory>();

        public LoginHistoryPage()
        {
            InitializeComponent();

            UpdateDatagrid();

            DataContext = this;

        }



        public async void UpdateDatagrid()
        {

            var historyRecords = await StorageDbOperations.GetAllHistory();


            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {

                historyRecords = historyRecords.Where(hr => hr.User.Login.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();

            }


            HistoryList.Clear();

            foreach (var historyRecord in historyRecords)
            {
                HistoryList.Add(historyRecord);
            }


        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateDatagrid();
        }
    }
}
