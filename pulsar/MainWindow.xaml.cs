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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pulsar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Credentials creds;

        public static Credentials credentials
        {
            get
            {
                if (String.IsNullOrEmpty(creds.username) || String.IsNullOrEmpty(creds.apikey))
                {
                    try
                    {
                        creds = CredsFile.readConfig();
                    }
                    catch (Exception ex)
                    {
                        CredsWindow cw = new CredsWindow();
                        cw.Show();
                        MainWindow.ErrorMessage("Credentials are required." + ex.Message, "Credentials Required");
                    }
                }
                return creds;
            }
            set { creds = value; }
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        public static void ErrorMessage(string message, string title)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void menuItemCredentials_Click(object sender, RoutedEventArgs e)
        {
            var cw = new CredsWindow();
            cw.Show();
        }

        private void image1_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
