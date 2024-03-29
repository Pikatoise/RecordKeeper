﻿using System;
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
            if (MainGrid.SelectedItem != null)
            {
                if (MessageBox.Show("Вы уверены?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    accountData.RemoveAccount((AccountData.Account)MainGrid.SelectedItem);
                    MainGrid.Items.Refresh();
                }
            }
            else MessageBox.Show("Выберите аккаунт!", "Ошибка");
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (accountData.SearchLogin(TextBoxSearch.Text).Login != null)
            {
                MainGrid.SelectedItem = accountData.SearchLogin(TextBoxSearch.Text);
                MainGrid.ScrollIntoView(MainGrid.SelectedItem);
            }
            else MessageBox.Show("Аккаунт не найден!", "Ошибка");
        }

        private void ButtonSort_Click(object sender, RoutedEventArgs e)
        {
            accountData.Sort();
            MainGrid.Items.Refresh();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AccountAddWindow accountAddWindow = new AccountAddWindow();
            accountAddWindow.ShowDialog();

            if (accountAddWindow.login != null && accountAddWindow.password != null && accountAddWindow.access != null)
            {
                if (accountData.SearchLogin(accountAddWindow.login).Login == null)
                {
                    accountData.AddAccount(accountAddWindow.login, accountAddWindow.password, accountAddWindow.access);
                    MainGrid.Items.Refresh();
                }
                else MessageBox.Show("Логин занят!","Ошибка");
            }
        }
    }
}
