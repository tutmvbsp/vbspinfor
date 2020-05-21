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

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfImportText.xaml
    /// </summary>
    public partial class WpfDbf : Window
    {
        public WpfDbf()
        {
            InitializeComponent();
        }
        private FileStream _fw;
        ClsServer cls = new ClsServer();
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
                    string fileName = txtPath.Text.Trim();
                    string path = fileName.Substring(0);
                    int separator = path.LastIndexOf("\\", StringComparison.Ordinal);
                    string dataSource = fileName.Substring(0, separator) + "\\";
                    string file = fileName.Substring(separator);
                    int separator2 = file.LastIndexOf("\\", StringComparison.Ordinal);
                    string DBF = file.Remove(separator2, 1);
                    int separator3 = DBF.LastIndexOf(".", StringComparison.Ordinal);
                    // string DBF_Extension = DBF.Substring(Separator3);
                    string dbfFileName = DBF.Remove(separator3, 4);
                    string ExpFileName = dataSource + dbfFileName;
                    Encoding encode = Encoding.BigEndianUnicode;
                    _fw = new FileStream(ExpFileName, FileMode.Create, FileAccess.Write, FileShare.None);
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
                openFileDialog.Filter = "Dbase files (*.dbf)|*.dbf|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    txtPath.Text = openFileDialog.FileName.Trim(); //File.ReadAllText(openFileDialog.FileName);
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
                //string FileName = lstFiles.SelectedItem.ToString();
                string fileName = txtPath.Text.Trim();
                string path = fileName.Substring(0);
                int separator = path.LastIndexOf("\\", StringComparison.Ordinal);
                string dataSource = fileName.Substring(0, separator) + "\\";
                string file = fileName.Substring(separator);
                int separator2 = file.LastIndexOf("\\", StringComparison.Ordinal);
                string DBF = file.Remove(separator2, 1);
                int separator3 = DBF.LastIndexOf(".", StringComparison.Ordinal);
               // string DBF_Extension = DBF.Substring(Separator3);
                string dbfFileName = DBF.Remove(separator3, 4);
                //open the connection and read in all the airport data from .dbf file into a datatables
                cls.OleConnect(dataSource);
                string sql = "select * from " + dbfFileName;
                dt=cls.OleDbDataText(sql);
                if (dt.Rows.Count > 0)
                {
                    btnWrite.IsEnabled = true;
                }
                dgvData.ItemsSource = dt.DefaultView;
                cls.OleDongKetNoi();
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