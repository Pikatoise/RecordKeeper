using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    internal class AccountData
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

        public Account currentAccount;

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
    }
}
