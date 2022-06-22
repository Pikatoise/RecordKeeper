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
    /// Логика взаимодействия для AccountantWindow.xaml
    /// </summary>
    public partial class AccountantWindow : Window
    {
        Orders orders;

        public AccountantWindow()
        {
            InitializeComponent();

            orders = new Orders(ListViewOrders, GridDescribe);
        }

        private void ListViewOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStatus(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool AcceptOrDecline = ((sender as Button).Tag as string).Contains("Accept");

            orders.CheckOrder(AcceptOrDecline, (ListViewItem)ListViewOrders.SelectedItem);

            UpdateStatus();
        }

        void UpdateStatus()
        {
            if (orders.IsOrderChecked(((ListViewItem)ListViewOrders.SelectedItem).Tag))
            {
                ButtonVisibility(false);
            }
            else ButtonVisibility(true);
        }

        void ButtonVisibility(bool IsShow)
        {
            if (IsShow)
            {
                GridDescribe.Height = 300;
                PanelButtons.Visibility = Visibility.Visible;
            }
            else
            {
                GridDescribe.Height = 335;
                PanelButtons.Visibility = Visibility.Collapsed;
            }
        }
    }
}
