using SteamAccountSwitcher.Models;
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

namespace SteamAccountSwitcher
{
    /// <summary>
    /// Interaction logic for AddAccount.xaml
    /// </summary>
    public partial class AddAccount : Window
    {
        SteamAccount account;

        public SteamAccount Account
        {
            get { return account; }
        }

        public AddAccount()
        {
            InitializeComponent();
        }

        public AddAccount(SteamAccount editAccount)
        {
            InitializeComponent();
            account = editAccount;

            tb_Name.Text = editAccount.Name;
            tb_Username.Text = editAccount.Username;
            tb_Password.Password = editAccount.Password;

        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                account = new SteamAccount(tb_Name.Text, tb_Username.Text, tb_Password.Password);
            }
            catch
            {
                account = null;
            }

            Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (account != null)
            {
                if (account.Username == "" || account.Username == null ||
                    account.Password == "" || account.Password == null)
                {
                    account = null;
                }
            }
        }
    }
}
