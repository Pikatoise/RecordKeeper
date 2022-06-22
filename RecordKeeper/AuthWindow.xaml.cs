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
        public AccountData accountData;

        public AuthWindow()
        {
            InitializeComponent();
            accountData = new AccountData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch (accountData.TryAuth(TextBoxLogin.Text, TextBoxPassword.Password))
            {
                case -1:
                    MessageBox.Show("Введите логин и пароль!", "Ошибка");
                    return;
                case 0:
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка");
                    return;
                case 1:
                    OpenNextWindow(accountData.GetCurrentAccess());
                    return;
                case 2:
                    OpenNextWindow("Admin");
                    return;
            }
        }

        void OpenNextWindow(string access)
        {
            this.Visibility = Visibility.Collapsed;
            ClearTextBox();
            this.UpdateLayout();

            switch (access)
            {
                case "Admin":
                    AdminWindow adminWindow = new AdminWindow(this);
                    adminWindow.Show();
                    return;
                case "Kitchen":
                    KitchenWindow kitchenWindow = new KitchenWindow();
                    kitchenWindow.Show();
                    return;
                case "Accountant":
                    AccountantWindow accountantWindow = new AccountantWindow();
                    accountantWindow.Show();
                    return;
                case "Storekeeper":
                    return;
            }
        }

        void ClearTextBox()
        {
            TextBoxLogin.Text = string.Empty;
            TextBoxPassword.Password = string.Empty;
        }
    }
}
