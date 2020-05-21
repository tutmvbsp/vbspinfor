using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.IO;
using System.Data;
using DAL;
using BLL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfImportText.xaml
    /// </summary>
    public partial class WpfCutFile : Window
    {
        public WpfCutFile()
        {
            InitializeComponent();
        }
  
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void LblCopyFile_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            /*
             // bll.CopyDir(@"D:\\BDA\\3001", @"C:\\PDF\\3001");
             try
             {
                 string destFolder = txtPath.Text.Trim();
                 bll.DeleteAllFile(destFolder);
                 for (int i = 0; i < ListPos.Items.Count; i++)
                 {
                     string sourceFolder = txtSourcePath.Text.Trim() + bll.Right(ListPos.Items[i].ToString().Trim(), 4);
                     string[] files = Directory.GetFiles(sourceFolder);
                     foreach (string file in files)
                     {
                         string Ngay = file.Substring(19, 8);
                         if (dtpNgay.SelectedDate != null && Ngay == dtpNgay.SelectedDate.Value.ToString("ddMMyyyy"))
                         {
                             string name = System.IO.Path.GetFileName(file);
                             string dest = System.IO.Path.Combine(destFolder, name);
                             if (!File.Exists(dest)) File.Copy(file, dest);
 
                         }
 
                     }
                 }
                 var list = Directory.GetFiles(destFolder, "*.pdf");
                 if (list.Length > 0)
                 {
                     MessageBox.Show("OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                 }
                 else
                 {
                     MessageBox.Show("Không có file nào!", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
             }
             */
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DirectoryInfo sourceDir = new DirectoryInfo(txtSourcePath.Text.Trim());
                DirectoryInfo destinationDir = new DirectoryInfo(txtPath.Text.Trim()+@"\");
               // MessageBox.Show(sourceDir + "          " + destinationDir);
                CopyDirectory(sourceDir, destinationDir);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            txtSourcePath.Text = dialog.SelectedPath.Trim();

        }
        private void btnBrowseDes_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            txtPath.Text = dialog.SelectedPath.Trim();

        }
        static void CopyDirectory(DirectoryInfo source, DirectoryInfo destination)
        {
            try
            {
                ToolBll bll = new ToolBll();
                if (!destination.Exists)
                {
                    destination.Create();
                }
                // Copy all files.
                FileInfo[] files = source.GetFiles();
                MessageBox.Show("Tong so file : ",files.Length.ToString());
                if (files.Length == 0)
                    MessageBox.Show("Không có file nào!", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                {
                    foreach (FileInfo file in files)
                    {
                        if (file.Name.Substring(0, 4) == "0030" && file.Name.Substring(file.Name.Length - 3) == "pdf") // doan nay luu file tai lieu pdf
                        {
                            string pos = destination + file.Name.Substring(0, 6);
                            string tmcha = pos + @"\" + file.Name.Substring(11, 4).Trim();
                            string tmcon = tmcha + @"\" + file.Name.Substring(9, 6).Trim();
                            bll.TaoThuMuc(pos);
                            bll.TaoThuMuc(tmcha);
                            bll.TaoThuMuc(tmcon);
                            if (!Directory.Exists(tmcon))
                                MessageBox.Show("Không có thư mục : " + tmcon, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            else
                                if (!File.Exists(pos + @"\" + file.Name.Substring(11, 4).Trim() + @"\" + file.Name.Substring(9, 6).Trim() + @"\" + file.Name))
                                file.MoveTo(pos + @"\" + file.Name.Substring(11, 4).Trim() + @"\" + file.Name.Substring(9, 6).Trim() + @"\" + file.Name);
                            //MessageBox.Show(pos);
                            //MessageBox.Show(tmcha);
                            //MessageBox.Show(tmcon);
                        }
                        else // doan nay luu file du lieu offline
                        {
                            string pos = destination + "00"+file.Name.Substring(4, 4);
                            string tmcha = pos + @"\" + file.Name.Substring(10, 4).Trim();
                            string tmcon = tmcha + @"\" + file.Name.Substring(14, 2).Trim() + file.Name.Substring(10, 4).Trim();
                            bll.TaoThuMuc(pos);
                            bll.TaoThuMuc(tmcha);
                            bll.TaoThuMuc(tmcon);
                            if (!Directory.Exists(tmcon))
                                MessageBox.Show("Không có thư mục : " + tmcon, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                            else if (file.Name.Substring(0, 3) == "TXN")
                                //MessageBox.Show(file.Name.Substring(file.Name.Length - 7) +"     "+file.Name.Substring(file.Name.Length - 4));
                                if (!File.Exists(tmcon + @"\" + file.Name.Trim()))
                                    file.MoveTo(tmcon + @"\" + file.Name.Trim());
                        }
                        
                    }
                }
                MessageBox.Show("Move OK!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                //// Process subdirectories.
                //DirectoryInfo[] dirs = source.GetDirectories();
                //foreach (DirectoryInfo dir in dirs)
                //{
                //    // Get destination directory.
                //    string destinationDir = Path.Combine(destination.FullName, dir.Name);
                //    // Call CopyDirectory() recursively.
                //    CopyDirectory(dir, new DirectoryInfo(destinationDir));
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}