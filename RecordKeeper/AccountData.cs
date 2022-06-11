using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RecordKeeper
{
    public class AccountData
    {
        string docPath;
        string accPath;

        public struct Account
        {
            public string Login;
            public string Password;
            public string Access;

            public Account (string[] data)
            {
                Login = data[0];
                Password = data[1];
                Access = data[2];
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
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))   // 1 = Вход под обычным пользователем;
            {                                                                    // 2 = Вход под админом
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
    }
}
