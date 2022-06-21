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
    public partial class KitchenWindow : Window
    {
        Orders orders;

        public KitchenWindow()
        {
            InitializeComponent();

            orders = new Orders(ListViewOrders,GridDescribe,GridCreation);
        }

        private void ButtonInsert_Click(object sender, RoutedEventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(TextBoxName.Text) || string.IsNullOrWhiteSpace(TextBoxPrice.Text) || string.IsNullOrWhiteSpace(TextBoxCount.Text)))
            {
                orders.AddItemToTemp(new string[] {
                ((TextBlock)((ListBoxItem)ListBoxType.SelectedItem).Content).Text,
                TextBoxName.Text, 
                TextBoxCount.Text,
                ((TextBlock)((ListBoxItem)ListBoxUnit.SelectedItem).Content).Text,
                TextBoxPrice.Text });
            }
            else MessageBox.Show("Неверные данные!", "Ошибка");

            GridCreation.Items.Refresh();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (GridCreation.Items.Count > 0 && GridCreation.SelectedItem != null)
            {
                orders.RemoveItemFromTemp((Orders.Item)GridCreation.SelectedItem);

                GridCreation.SelectedItem = null;
            }
            else MessageBox.Show("Выберите предмет!", "Ошибка");

            GridCreation.Items.Refresh();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (GridCreation.Items.Count > 0)
            {
                orders.SaveNewOrder();

                MessageBox.Show("Заказ отправлен!", "Сообщение");
            }
            else MessageBox.Show("Нельзя отправить пустой заказ!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);

            GridCreation.Items.Refresh();
            GridDescribe.Items.Refresh();
        }
    }
}
