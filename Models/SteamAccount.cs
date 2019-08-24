using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamAccountSwitcher.Models
{
    [Serializable]
    public class SteamAccount
    {
        string _username, _password;

        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public SteamAccount(string pUsername, string pPassword)
        {
            _username = pUsername;
            _password = pPassword;
        }

        public SteamAccount()
        {

        }

        public string getStartParameters()
        {
            return "-login " + _username + " " + _password;
        }
    }
}
