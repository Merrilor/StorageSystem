using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StorageSystem.UserInteraction
{
    public static class MessageBoxDisplay
    {


        public static void DisplayError(string errorMessage)
        {

            MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);


        }

        public static MessageBoxResult DisplayQuestion(string question)
        {

           return MessageBox.Show(question, "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

        }



    }
}
