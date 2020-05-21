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
using Microsoft.WindowsAPICodePack.Dialogs;
using Microsoft.Win32;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfImportText.xaml
    /// </summary>
    public partial class WpfBack : Window
    {
        public WpfBack()
        {
            InitializeComponent();            
        }
        //private BackgroundWorker backgroundWorker;
        //private FileStream _fw;
        private ToolBll bll = new ToolBll();
        private DataTable dt = new DataTable();
        private DataTable dtku = new DataTable();
        private DataTable dttk = new DataTable();
        private ClsConnectLocal cls = new ClsConnectLocal();
        string Thumuc = "C:\\TEXT";
       // private string strup = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            bll.TaoThuMuc(Thumuc);
            try
            {
                if (txtPath.Text == "")
                {
                    
                    MessageBox.Show("Chưa chọn đường dẫn","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                    return;
                }
                else
                {
                    string BackUpLocation = txtPath.Text.Trim();
                    string DatabaseName = CboDb.SelectedValue.ToString().Trim();
                    string BackUpFileName = CboDb.SelectedValue.ToString().Trim() + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".bak";
                    BackupDatabase(BackUpLocation, BackUpFileName, DatabaseName);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Now.AddDays(-1); //Convert.ToDateTime(dtver.Rows[0]["NGBTMAX"]); //
            CboDb.DisplayMemberPath = "Text";
            CboDb.SelectedValuePath="Value";
            var items = new[] 
            { 
                    new { Text = "VBSPINFOR", Value = "VBSPINFOR" }, 
                    new { Text = "Offline", Value = "VbspOffline" }, 
                    new { Text = "QBIM", Value = "QBIM" },
            };
            CboDb.ItemsSource = items;
            CboDb.SelectedIndex = 1;
        }
        public void BackupDatabase(string BackUpLocation, string BackUpFileName, string  DatabaseName)
        {

            string SQLBackUp = "";
            DatabaseName = "[" + DatabaseName + "]";
            string fileUNQ = DateTime.Now.Day.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString();            
            //BackUpFileName = BackUpFileName + fileUNQ + ".bak";
            if (Option1.IsChecked == true)
            {
                SQLBackUp = @"BACKUP DATABASE " + DatabaseName + " TO DISK = N'" + BackUpLocation + @"\" + BackUpFileName + @"'";
            }
            else
            {
                SQLBackUp = @"RESTORE DATABASE " + DatabaseName + " FROM DISK = N'" + BackUpLocation + @"'";
                //restore database testdb1 from disk='c:\testdb1.bak'
            }
            MessageBox.Show(SQLBackUp);
            try
            {
                cls.ClsConnect();
                lblMess.Content = "Đang restore ....";
                string alter = "alter database " + DatabaseName + "set single_user with rollback immediate";
                cls.UpdateDataText(alter);
                string drop = "drop database " + DatabaseName ;
                cls.UpdateDataText(drop);
                cls.UpdateDataText(alter);
                cls.UpdateDataText(SQLBackUp);
                lblMess.Content = "Done";
                //string mess = SQLBackUp + " ######## Server name  Database " + DatabaseName + " successfully backed up to " + BackUpLocation + @"\" + BackUpFileName + "\n Back Up Date : " + DateTime.Now.ToString();
                string mess = "Backup " + DatabaseName + " successfully backed up to " + BackUpLocation + @"\" + BackUpFileName;
                MessageBox.Show(mess, "Successfully backed",MessageBoxButton.OK,MessageBoxImage.Information);
            }

            catch (Exception ex)
            {
                //lblResult.Content = ex.ToString();
                //lblPath.Content = SQLBackUp + " ######## Server name  Database " + DatabaseName + " successfully backed up to " + BackUpLocation + @"\" + BackUpFileName + "\n Back Up Date : " + DateTime.Now.ToString();
                MessageBox.Show(ex.Message);
            }

            finally
            {
                cls.DongKetNoi();
            }
           
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
           /* 
            var dlg = new CommonOpenFileDialog();
            dlg.Title = "My Title";
            dlg.IsFolderPicker = true;
            dlg.InitialDirectory = currentDirectory;

            dlg.AddToMostRecentlyUsedList = false;
            dlg.AllowNonFileSystemItems = false;
            dlg.DefaultDirectory = currentDirectory;
            dlg.EnsureFileExists = true;
            dlg.EnsurePathExists = true;
            dlg.EnsureReadOnly = false;
            dlg.EnsureValidNames = true;
            dlg.Multiselect = false;
            dlg.ShowPlacesList = true;

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                var folder = dlg.FileName;
                // Do something with selected folder string
            }
            */
            if (Option1.IsChecked == true)
            {
                var dialog = new System.Windows.Forms.FolderBrowserDialog();
                System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                txtPath.Text = dialog.SelectedPath.Trim();
            }
            else
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Backup files (*.bak)|*.bak|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                    txtPath.Text = openFileDialog.FileName.Trim(); //File.ReadAllText(openFileDialog.FileName);

            }
        }

        private void btnOkNew_Click(object sender, RoutedEventArgs e)
        {
            string BackUpLocation = txtPath.Text.Trim();
            string DatabaseName = CboDb.SelectedValue.ToString().Trim();
            try
            {
                if (Option1.IsChecked == true)
                {
                    bll.RestoreDb(DatabaseName, BackUpLocation);
                    MessageBox.Show("Successfully Restore", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    string BackUpFileName = CboDb.SelectedValue.ToString().Trim() + dtpNgay.SelectedDate.Value.ToString("yyyyMMdd") + ".bak";
                    string tuPath = BackUpLocation + BackUpFileName;
                    MessageBox.Show(DatabaseName + "      " + tuPath);
                    bll.BackUpDb(DatabaseName, tuPath);
                    MessageBox.Show("Successfully Backup", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
   
    }
}