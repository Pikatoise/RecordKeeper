using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordKeeper
{
    internal class AccountData
    {
        struct Account
        {
            public string Login;
            public string Password;
            public string Access;
        }

        Account currentAccount;

        List<Account> accounts = new List<Account>();

        public AccountData()
        {

        }

        void InitData()
        {

        }
    }
}
