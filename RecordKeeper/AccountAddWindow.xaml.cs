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
    /// Логика взаимодействия для AccountAddWindow.xaml
    /// </summary>
    public partial class AccountAddWindow : Window
    {
        public string login;
        public string password;
        public string access;

        public AccountAddWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text) || string.IsNullOrEmpty(TextBoxPassword.Text))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка");
                return;
            }

            login = TextBoxLogin.Text.Replace(" ", "");
            password = TextBoxPassword.Text.Replace(" ", "");

            if ((bool)RadioButtonAccountant.IsChecked)
            {
                access = "Accountant";
            }
            else if ((bool)RadioButtonStorekeeper.IsChecked)
            {
                access = "Storekeeper";
            }
            else access = "Kitchen";

            this.Close();
        }
    }
}
