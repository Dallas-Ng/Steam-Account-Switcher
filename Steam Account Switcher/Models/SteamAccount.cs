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
        string _name, _username, _password;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }
        public string Password
        {
            get { return Hash.Encrypt(_password, Properties.Settings.Default.EncryptionKey); }
            set { _password = Hash.Decrypt(value, Properties.Settings.Default.EncryptionKey); }
        }

        public SteamAccount(string Name, string pUsername, string pPassword)
        {
            _name = Name;
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
