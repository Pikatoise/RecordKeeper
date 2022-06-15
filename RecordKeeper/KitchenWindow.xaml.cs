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

namespace RecordKeeper
{
    /// <summary>
    /// Логика взаимодействия для KitchenWindow.xaml
    /// </summary>
    public partial class KitchenWindow : Window
    {
        string currentLogin;

        public KitchenWindow(string login)
        {
            InitializeComponent();

            currentLogin = login;
        }

        private void ButtonInsert_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
