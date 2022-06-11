using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RecordKeeper
{
    public class AccountData
    {
        string docPath;
        string accPath;

        public struct Account
        {
            public string Login { get; set; }
            public string Password { get; set; }
            public string Access { get; set; }

            public Account (string[] data)
            {
                this.Login = data[0];
                this.Password = data[1];
                this.Access = data[2];
            }
        }

        Account currentAccount;

        List<Account> accounts = new List<Account>();

        public AccountData()
        {
            InitData();
        }

        void InitData()
        {
            docPath = Directory.CreateDirectory($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\RecordKeeper").FullName;
            accPath = $@"{docPath}\accounts.txt";

            if (!File.Exists(accPath))
            {
                File.Create(accPath).Close();
                return;
            }

            string[] temp;

            using (StreamReader stream = new StreamReader(accPath))
            {
                temp = stream.ReadToEnd().Split(new char[] { '\n' });
                stream.Close();
            }

            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] == "") return;
                temp[i] = temp[i].Replace("\r", "");
                temp[i] = temp[i].Replace("\n", "");

                this.accounts.Add(new Account(temp[i].Split(new char[] {'_'})));
            }
        }

        public int TryAuth(string login, string password)                        // -1 = Отсутствует логин или пароль;   
        {                                                                        // 0 = Логин или пароль неверные;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))   // 1 = Успешный вход под обычным пользователем;
            {                                                                    // 2 = Успешный вход под админом
                return -1;
            }

            if (login == "Admin" && password == "ont2022")
            {
                return 2;
            }

            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].Login == login)
                {
                    if (accounts[i].Password == password)
                    {
                        currentAccount = accounts[i];
                        return 1;
                    }
                }
            }

            return 0;
        }

        public string GetCurrentLogin()
        {
            return currentAccount.Login;
        }

        public string GetCurrentAccess()
        {
            return currentAccount.Access;
        }

        public void ResetCurrentUser()
        {
            currentAccount = new Account();
        }

        public void SaveData()
        {
            using(StreamWriter stream = new StreamWriter(accPath, false))
            {
                for (int i = 0; i < accounts.Count; i++)
                {
                    if (i == accounts.Count-1)
                    {
                        stream.Write($"{accounts[i].Login}_{accounts[i].Password}_{accounts[i].Access}");
                    } else stream.WriteLine($"{accounts[i].Login}_{accounts[i].Password}_{accounts[i].Access}");
                }
            }
        }

        public void DataToGrid(DataGrid grid)
        {
            grid.ItemsSource = accounts;
            /*for (int i = 0; i < accounts.Count; i++)
            {
                
            }*/
        }
    }
}
