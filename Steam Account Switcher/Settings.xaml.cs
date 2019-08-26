using Microsoft.Win32;
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
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
            reload();
        }


        private void reload()
        {
            btn_File.Content = Properties.Settings.Default.SteamDirectory.ToString();
        }

        private void btn_File_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.DefaultExt = ".exe";
            fileDialog.Filter = "Application (.exe)|*.exe";

            Nullable<bool> result = fileDialog.ShowDialog();

            if (result == true)
            {
                Properties.Settings.Default.SteamDirectory = fileDialog.FileName;
                reload();
            }
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (btn_File.Content.ToString() == "Select Steam")
            {
                requiredValidator.Visibility = Visibility.Visible;
            }
            else
            {
                Close();
            }
        }
    }
}
