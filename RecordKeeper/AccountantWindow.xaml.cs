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

            orders = new Orders(ListViewOrders, GridDescribe, "Accountant");
        }

        private void ListViewOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateStatus(); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            orders.CheckOrder((sender as Button).Tag as string, (ListViewItem)ListViewOrders.SelectedItem);

            UpdateStatus();
        }

        void UpdateStatus()
        {
            if (orders.IsOrderStatus(((ListViewItem)ListViewOrders.SelectedItem).Tag, '0'))
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
