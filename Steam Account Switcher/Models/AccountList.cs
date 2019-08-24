using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountSwitcher.Models
{
    [Serializable]
    public class AccountList
    {
        List<SteamAccount> _accounts;

        public List<SteamAccount> Accounts
        {
            get { return _accounts; }
            set { _accounts = value; }
        }

        public AccountList()
        {
            _accounts = new List<SteamAccount>();
        }
    }
}
