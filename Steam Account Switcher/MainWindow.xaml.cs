using SteamAccountSwitcher.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace SteamAccountSwitcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AccountList accountList;
        Steam steam;

        string Path = System.IO.Path.Combine(Environment.CurrentDirectory, "Accounts.xml");

        public MainWindow()
        {
            InitializeComponent();
            accountList = new AccountList();

            try
            {
                GetAccounts();
            }
            catch
            {
                /* This happens when either
                 * 1) The list is empty and the hashing conversion fails
                 * 2) List does not exist
                */
            }

            li_Accounts.ItemsSource = accountList.Accounts;
            li_Accounts.Items.Refresh();
        }


        private void GetAccounts()
        {
            try
            {
                string text = File.ReadAllText(Path);
                accountList = Serialize.FromXML<AccountList>(Hash.Decrypt(text, "key"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        private void WriteAccounts()
        {
            string xmlAccounts = Serialize.ToXML<AccountList>(accountList);
            StreamWriter file = new StreamWriter(Path);
            file.Write(Hash.Encrypt(xmlAccounts, "key"));
            file.Close();

        }


        private void li_Accounts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            steam = new Steam(@"C:\Program Files (x86)\Steam\Steam.exe");
            SteamAccount selectedAcc = (SteamAccount)li_Accounts.SelectedItem;
            steam.StartSteamAccount(selectedAcc);
        }


        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {
            AddAccount newWindow = new AddAccount();
            newWindow.Owner = this;
            newWindow.ShowDialog();

            if (newWindow.Account != null)
            {
                accountList.Accounts.Add(newWindow.Account);
                li_Accounts.Items.Refresh();
            }
        }


        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            if (li_Accounts.SelectedItem != null)
            {
                AddAccount newAccWindow = new AddAccount((SteamAccount)li_Accounts.SelectedItem);
                newAccWindow.Owner = this;
                newAccWindow.ShowDialog();

                if (newAccWindow.Account.Username != "" && newAccWindow.Account.Password != "")
                {
                    accountList.Accounts[li_Accounts.SelectedIndex] = newAccWindow.Account;

                    li_Accounts.Items.Refresh();
                }
            }
        }


        private void btn_Remove_Click(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = (Button)e.Source;
            SteamAccount selectedAcc = (SteamAccount)buttonClicked.DataContext;

            MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {selectedAcc.Username}", "Delete Account", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    accountList.Accounts.Remove(selectedAcc);
                    li_Accounts.Items.Refresh(); break;
                case MessageBoxResult.Cancel:
                    break;
            }


        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WriteAccounts();
        }
    }
}
