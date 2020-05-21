using System;
using System.Collections;
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
using System.ComponentModel;
using System.IO;
using System.Data;
using DAL;
using BLL;
using Microsoft.Win32;
using System.Data.OleDb;
using System.Windows.Media.Animation;
using System.Windows.Shell;
//using Microsoft.Office.Interop.Word;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Paragraph = System.Windows.Documents.Paragraph;
using System.Xml;


namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfImportText.xaml
    /// </summary>
    public partial class WpfWord : Window
    {
        public WpfWord()
        {
            InitializeComponent();
        }
        private FileStream _fw;
        ToolBll bll = new ToolBll();
        DataTable dt = new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnWrite_Click(object sender, RoutedEventArgs e)
        {
            if (txtPath.Text == "") 
            {
                MessageBox.Show("Chưa chọn file Excel","Error",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    
                    
                    string FileName = "C:\\TEXT\\"+".txt";
                    //string[] arrStr = FileName.Split('\\');
                    Encoding encode = Encoding.BigEndianUnicode;
                    _fw = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                    StreamWriter sw = new StreamWriter(_fw, encode);
                    foreach (DataRow row in dt.Rows)
                    {
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            if (i + 1 < dt.Columns.Count)
                            {
                                sw.Write(row[i].ToString() + "#");
                            }
                            else
                            {
                                sw.Write(row[i].ToString());
                            }
                        }
                        sw.WriteLine();
                    }
                    sw.Close();
                    MessageBox.Show("Export OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnWrite.IsEnabled = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region

                /*
                // Create OpenFileDialog 
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".xlsx";
                //dlg.Filter ="XLSX Files (*.xlsx)|*.zlsx|JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = dlg.ShowDialog();


                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                    string filename = dlg.FileName;
                    txtPath.Text = filename;
                }
                 */
                #endregion
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Word files (*.docx)|*.docx|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    txtPath.Text = openFileDialog.FileName.Trim(); //File.ReadAllText(openFileDialog.FileName);
                MessageBox.Show(txtPath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
                object miss = System.Reflection.Missing.Value;
                //object path = @"C:\TEXT\Cauhoi.docx";
                string filename = @"C:\TEXT\dapan.txt";
                object path = txtPath.Text;
                object readOnly = true;
                Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);
                string totaltext = "";
                for (int i = 6; i < docs.Paragraphs.Count; i += 5)
                {
                    //totaltext += " \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString();
                    totaltext += docs.Paragraphs[i + 1].Range.Text;
                    //MessageBox.Show(totaltext);
                }
                //Console.WriteLine(totaltext);
                bll.WriteToText(totaltext, filename);
                docs.Close();
                word.Quit();
                MessageBox.Show("OK ", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            btnWrite.IsEnabled = false;
        }

    }
}

