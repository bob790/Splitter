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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using FolderPickerLib;


namespace Splitter
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

        private void btnInputDir_Click(object sender, RoutedEventArgs e)
        {
            txtInputDir.Text = getDir();
        }

        private string getDir()
        {
            var dlg = new FolderPickerDialog();
            if (dlg.ShowDialog() == true)
            {
                return dlg.SelectedPath;
            }
            return null;
        }

        private void btnOutputDir2_Click(object sender, RoutedEventArgs e)
        {
            txtOutputDir2.Text = getDir();
        }

        private void btnOutputDir1_Click(object sender, RoutedEventArgs e)
        {
            txtOutputDir1.Text = getDir();
        }

        private void btnBigRed_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(txtInputDir.Text))
            {
                MessageBox.Show("Invalid Input Directory");
                return;
            }
            if (!Directory.Exists(txtOutputDir1.Text))
            {
                Directory.CreateDirectory(txtOutputDir1.Text);
            }
            if (!Directory.Exists(txtOutputDir2.Text))
            {
                Directory.CreateDirectory(txtOutputDir2.Text);
            }
            if (txtOutputDir1.Text == txtInputDir.Text || txtOutputDir2.Text == txtInputDir.Text)
            {
                MessageBox.Show("Input and Output directories must be different");
                return;
            }
            if (Directory.Exists(txtInputDir.Text))
            {
                List<string> files = Directory.GetFiles(txtInputDir.Text).ToList<String>();
                Random rnd = new Random();
                files.OrderBy(x => rnd.Next()).ToList();
                bool a = true;

                foreach (string s in files)
                {
                    string fileName = System.IO.Path.GetFileName(s);
                    string destFile = null;
                    if (a)
                    {
                        destFile = System.IO.Path.Combine(txtOutputDir1.Text, fileName);
                        a = false;
                    }
                    else
                    {
                        destFile = System.IO.Path.Combine(txtOutputDir2.Text, fileName);
                        a = true;
                    }
                    try
                    {
                        File.Move(s, destFile);
                    }
                    catch { }
                    
                }
            }
            else
            {
                MessageBox.Show("Invalid Input Directory");
                return;
            }


        }
    }
}
