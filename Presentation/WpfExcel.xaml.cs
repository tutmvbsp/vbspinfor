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
    public partial class WpfExcel : Window
    {
        public WpfExcel()
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
                    
                    
                    string FileName = "C:\\TEXT\\"+bll.XoaHetKyTuTrang(CboSheet.SelectedValue.ToString().Trim())+".txt";
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
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    txtPath.Text = openFileDialog.FileName.Trim(); //File.ReadAllText(openFileDialog.FileName);
                // load seet to combo
                DataTable dtexc = new DataTable();
                String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtPath.Text.Trim() +";Extended Properties='Excel 12.0 XML;HDR=YES;';";
                OleDbConnection con = new OleDbConnection(constr);
                con.Open();
                dtexc = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (dtexc == null)
                {
                    MessageBox.Show("Không có Sheet nào");
                    return;
                }
                else
                {
                    CboSheet.ItemsSource = dtexc.DefaultView;
                    CboSheet.DisplayMemberPath = "TABLE_NAME";
                    CboSheet.SelectedValuePath = "TABLE_NAME";
                    CboSheet.SelectedIndex = 0;
                }
                con.Close();
                con.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            String name = CboSheet.SelectedValue.ToString().Trim();
            //String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +"d:\\DBIMP\\DBEXCEL.xlsx" +";Extended Properties='Excel 12.0 XML;HDR=YES;';";
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtPath.Text.Trim() + ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
            try
            {
                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand cmd = new OleDbCommand("Select * From [" + name + "]", con);
                con.Open();
                OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = dt.DefaultView;
                    MessageBox.Show("Read Excel OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnWrite.IsEnabled = true;
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                con.Close();
                con.Dispose();
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