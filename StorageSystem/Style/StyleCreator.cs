using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace StorageSystem.Style
{
    public static class StyleCreator
    {

        public static System.Windows.Style EditableGridHeader()
        {

            System.Windows.Style newStyle = new System.Windows.Style(typeof(DataGridColumnHeader));
            newStyle.Setters.Add(new Setter(DataGridColumnHeader.BackgroundProperty, Brushes.Goldenrod));
            

            

            return newStyle;

        }




    }
}
