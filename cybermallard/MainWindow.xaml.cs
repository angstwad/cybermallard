﻿using System;
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
using net.openstack.Core.Domain;
using System.Windows.Threading;

namespace pulsar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static Credentials creds;
        public static Credentials credentials
        {
            get
            {
                if (creds == null)
                {
                    load_creds();
                }
                return creds;
            }
            set { creds = value; }
        }

        private static void load_creds()
        {
            try
            {
                creds = CredsFile.readConfig();
            }
            catch (Exception)
            {
                creds = new Credentials();
            }

            if (String.IsNullOrEmpty(creds.apikey) || String.IsNullOrEmpty(creds.username))
            {
                CredsWindow cw = new CredsWindow();
                cw.Show();
                MainWindow.ErrorMessage("Credentials are required.", "Credentials Required");
            }
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
            CloudFiles file = new CloudFiles();
            Container cont = file.get_container();

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] filePaths = (string[])e.Data.GetData(DataFormats.FileDrop);

                ProgressWindow pw = new ProgressWindow();
                pw.Show();

                Action emptyDelegate = delegate { };
                
                double progress = 0;
                int numFiles = filePaths.Length;
                int count = 0;   
                foreach (string path in filePaths)
                {
                    count++;
                    pw.lblFileStatus.Content = path;
                    progress = ((double)count / numFiles) * 100;
                    pw.lblNumberFile.Content = String.Format("File {0} of {1}", count, numFiles);
                    pw.progressBar.Value = progress;
                    pw.Dispatcher.Invoke(emptyDelegate, DispatcherPriority.Render);
                    file.upload_file(cont.Name, path);
                }
            }
        }
    }
}
