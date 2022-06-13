using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RecordKeeper
{
    partial class Styles
    {
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement).TemplatedParent as Window).WindowState = WindowState.Minimized;
        }

        private void CloseDialog_Click(object sender, RoutedEventArgs e)
        {
            ((sender as FrameworkElement).TemplatedParent as Window).Close();
        }
    }
}
