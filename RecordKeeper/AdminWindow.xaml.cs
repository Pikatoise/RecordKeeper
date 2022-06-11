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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        AuthWindow authWindow;
        AccountData accountData;

        public AdminWindow(AuthWindow authWindow)
        {
            InitializeComponent();
            this.authWindow = authWindow;
            this.accountData = authWindow.accountData;
        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            authWindow.Visibility = Visibility.Visible;
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            accountData.DataToGrid(MainGrid);
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (!(MainGrid.SelectedItem == null))
            {
                if (MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    accountData.RemoveAccount((AccountData.Account)MainGrid.SelectedItem);
                    MainGrid.Items.Refresh();
                }
            }
            else MessageBox.Show("Выберите аккаунт!", "Ошибка");
        }
    }
}
