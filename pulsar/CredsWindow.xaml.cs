using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace pulsar
{
    /// <summary>
    /// Interaction logic for credentials.xaml
    /// </summary>
    public partial class CredsWindow : Window
    {
        public CredsWindow()
        {
            InitializeComponent();

            if (File.Exists(CredsActions.path)) 
                this.loadWithCreds();
        }

        private void loadWithCreds()
        {
            var creds = CredsActions.readConfig();
            txtUsername.Text = creds.username;
            txtApikey.Text = creds.apikey;
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtApikey.Text = "";
            txtUsername.Text = "";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CredsActions.writeConfig(txtUsername.Text, txtApikey.Text);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem saving the configuration." + ex.Message, 
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }
    }
}
