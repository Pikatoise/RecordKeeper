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
            switch (accountData.TryAuth(TextBoxLogin.Text, TextBoxPassword.Password))
            {
                case -1:
                    MessageBox.Show("Введите логин и пароль!", "Ошибка");
                    return;
                case 0:
                    MessageBox.Show("Неверный логин или пароль!", "Ошибка");
                    return;
                case 1:
                    OpenNextWindow(accountData.currentAccount.Access, accountData.currentAccount.Login);
                    return;
                case 2:
                    OpenNextWindow("Admin");
                    return;
            }


           /* AdminWindow adminWindow = new AdminWindow(this);
            adminWindow.Show();
            this.Visibility = Visibility.Collapsed;
            this.UpdateLayout();*/
        }

        void OpenNextWindow(string access, string login = "Admin")
        {

        }
    }
}
