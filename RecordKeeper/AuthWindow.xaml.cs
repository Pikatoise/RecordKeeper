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

namespace RecordKeeper
{
    /// <summary>
    /// Interaction logic for AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        AccountData accountData;

        public AuthWindow()
        {
            InitializeComponent();
            accountData = new AccountData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



           /* AdminWindow adminWindow = new AdminWindow(this);
            adminWindow.Show();
            this.Visibility = Visibility.Collapsed;
            this.UpdateLayout();*/
        }
    }
}
