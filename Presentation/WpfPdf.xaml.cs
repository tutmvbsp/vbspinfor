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
using  PQScan.PDFToText;
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
    public partial class WpfPdf : Window
    {
        public WpfPdf()
        {
            InitializeComponent();
        }
        //private FileStream _fw;
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        DataTable dt = new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                #region
                if (ListBox.Items.Count > 0)
                {
                    string destFolder = txtPath.Text.Trim();
                    string sourceFolder = txtSourcePath.Text.Trim();
                    foreach (object t in ListBox.Items)
                    {
                        string file = destFolder + t;
                        string pos = t.ToString().Substring(0, 6);
                        string ngay = t.ToString().Substring(7, 8);
                        string fileTxt = sourceFolder + pos + "_" + ngay + "_LEND30.txt";
                        //MessageBox.Show(file+"  "+fileTxt);
                        bll.Pdf2Text(file, fileTxt);
                    }
                    ListText.Items.Clear();
                    DirectoryInfo dir = new DirectoryInfo(sourceFolder);
                    FileInfo[] files = dir.GetFiles("*.txt*");
                    foreach (FileInfo file in files)
                    {
                        
                        string ngay = file.Name.Trim().Substring(7, 8);
                        if (dtpNgay.SelectedDate != null && ngay == dtpNgay.SelectedDate.Value.ToString("ddMMyyyy"))
                        {
                            //MessageBox.Show(file.ToString());
                            ListText.Items.Add(file);
                        }
                    }
                    if (ListText.Items.Count == 0)
                    {
                        MessageBox.Show("Not Files found ! ", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        cls.ClsConnect();
                        foreach (object t in ListText.Items)
                        {

                            string mato = "";
                            string soku = "";
                            string file = sourceFolder + t;
                            string pos = t.ToString().Substring(0, 6);
                            string ngay = t.ToString().Substring(7, 8);
                            string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                            string sqlchk = "select top 1 * from PSPDF where NGAY='" + ng + "' and KU_MAPGD='" + pos + "'";
                            dt = cls.LoadDataText(sqlchk);
                            if (dt.Rows.Count > 0)
                            {
                                MessageBox.Show("Số liệu ngày : " + ng + "  Pos : " + pos + "   đã tồn tại", "Mess",
                                    MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            else
                            {
                                string str = bll.XoaKyTuTrang(File.ReadAllText(file));
                                string[] arrStr = str.Split(' ');
                                foreach (var item in arrStr)
                                {
                                    if ((item.Trim().Length == 8 && bll.Right(item.Trim(), 1) == ":") ||
                                        (item.Trim().Length == 16 && bll.Left(item.Trim(), 2) == "66"))
                                    {
                                        if (item.Trim().Length == 8 && bll.Right(item.Trim(), 1) == ":")
                                        {
                                            mato = bll.Left(item, 7);
                                        }
                                        if (item.Trim().Length == 16 && bll.Left(item.Trim(), 2) == "66")
                                        {
                                            soku = item.Trim();
                                            //MessageBox.Show(ng+"  "+pos+"   "+mato + "      " + soku);
                                            string sql = "insert into PSPDF (NGAY,KU_MAPGD,KU_MATO,KU_SOKU) values ('" +
                                                         ng + "','" + pos + "','" + mato + "','" + soku + "')";
                                            cls.UpdateDataText(sql);
                                        }
                                    }
                                }
                            }
                        }
                        cls.DongKetNoi();
                        MessageBox.Show("OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else
                {
                    MessageBox.Show("Không có file nào!", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                #region
                /*
                string[] files = Directory.GetFiles(destFolder);
                foreach (string file in files)
                {
                    string Ngay = file.Substring(14, 8);
                    if (dtpNgay.SelectedDate != null && Ngay == dtpNgay.SelectedDate.Value.ToString("ddMMyyyy"))
                    {
                        MessageBox.Show(file);
                    }
                }
                 */
                //MessageBox.Show(bll.PdfText(txtPath.Text.Trim()));
                //string str = bll.XoaKyTuTrang(bll.PdfText(txtPath.Text.Trim()));
                //MessageBox.Show(str);

                /*
               // MessageBox.Show(bll.XoaKyTuTrang(bll.pdfText(txtPath.Text.Trim())));
                for (int i = 0; i < ListBox.Items.Count; i++)
                {
                    string mato = "";
                    string soku = "";
                    string str = bll.XoaKyTuTrang(bll.pdfText(txtPath.Text.Trim()));
                    MessageBox.Show(str);
                    
                    string[] arrStr = str.Split(' ');
                    foreach (var item in arrStr)
                    {
                        if ((item.Trim().Length == 8 && bll.Right(item.Trim(), 1) == ":") ||
                            (item.Trim().Length == 16 && bll.Left(item.Trim(), 2) == "66"))
                        {
                            if (item.Trim().Length == 8 && bll.Right(item.Trim(), 1) == ":")
                            {
                                //MessageBox.Show("Ma to "+bll.Left(item,7));
                                mato = bll.Left(item, 7);
                                //strmoi = strmoi + mato+" ";
                            }
                            if (item.Trim().Length == 16 && bll.Left(item.Trim(), 2) == "66")
                            {
                                //MessageBox.Show("soku " + item.Trim());
                                soku = item.Trim();
                                //strmoi = strmoi + soku+" ";
                                MessageBox.Show(mato + "      " + soku);
                            }
                        }
                    }
                    //MessageBox.Show(strmoi);
                }
                */
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                #endregion
            bll.DeleteAllFile(txtPath.Text.Trim());
            bll.DeleteAllFile(txtSourcePath.Text.Trim());
            ListBox.Items.Clear();
            ListText.Items.Clear();
        }

        private void WpfPdf_OnLoaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgayKu.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            cls.DongKetNoi();
            /*
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00' order by PO_MA";
                DataTable dtpos = new DataTable();
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    ListPos.Items.Add(dtpos.Rows[i]["PO_MA"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
             */
        }


        private void LblGetFolder_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var directchoosedlg = new System.Windows.Forms.FolderBrowserDialog();
                if (directchoosedlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    txtPath.Text = directchoosedlg.SelectedPath;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LblGetFiles_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (txtPath.Text == "")
                {
                    MessageBox.Show("Folder ?  + ", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    ListBox.Items.Clear();
                    DirectoryInfo dir = new DirectoryInfo(txtPath.Text.Trim());
                    FileInfo[] files = dir.GetFiles("*.pdf*");
                    foreach (FileInfo file in files)
                    {
                        string diemGd = file.Name.Trim();
                        string ngay = diemGd.Substring(7, 8); //str.Right(DiemGd, 8); substring(pos,length)
                        //string MaPos = diemGd.Substring(0, 6);
                        //string Mau = diemGd.Substring(16, 7);
                        if (dtpNgay.SelectedDate != null && ngay == dtpNgay.SelectedDate.Value.ToString("ddMMyyyy"))
                        {
                            //MessageBox.Show(Ngay + "  " + MaPos);
                            ListBox.Items.Add(diemGd);
                        }

                    }
                    if (ListBox.Items.Count == 0)
                    {
                        MessageBox.Show("Not Files found ! ", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }                       
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

        private void BtnTest_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //MessageBox.Show(bll.PdfText(txtPath.Text.Trim()));
                //string file = "C:\\PDF\\003003_25052016_LEND_30_314675.pdf";
                //string str = bll.ExtractTextFromPdf(file);
                //string str = bll.PdfText(file);
                //MessageBox.Show(str);
                //File.WriteAllText("C:\\TEXT\\a.txt",str);
                //MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            //MessageBox.Show(txtPath.Text.Trim());
           
            //bll.Pdf2Text("C:\\PDF\\003005_25052016_LEND_30_314668.pdf", "C:\\PDF\\output-text.txt");
             
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ListBox.Items.Count > 0)
                {
                    string destFolder = txtPath.Text.Trim();
                    string sourceFolder = txtSourcePath.Text.Trim();
                    foreach (object t in ListBox.Items)
                    {
                        string mato = "";
                        string file = destFolder + t;
                        string pos = t.ToString().Substring(0, 6);
                        string ngay = t.ToString().Substring(7, 8);
                        cls.ClsConnect();
                        string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        string sqlchk = "select top 1 * from PSPDF where NGAY='" + ng + "' and KU_MAPGD='" + pos + "'";
                        dt = cls.LoadDataText(sqlchk);
                        if (dt.Rows.Count > 0)
                        {
                            MessageBox.Show("Số liệu ngày : " + ng + "  Pos : " + pos + "   đã tồn tại", "Mess",
                                MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            string str = bll.XoaKyTuTrang(bll.PdfText(file));
                            string[] arrStr = str.Split(' ');
                            foreach (var item in arrStr)
                            {
                                if ((item.Trim().Length == 8 && bll.Right(item.Trim(), 1) == ":") ||
                                    (item.Trim().Length == 16 && bll.Left(item.Trim(), 2) == "66"))
                                {
                                    if (item.Trim().Length == 8 && bll.Right(item.Trim(), 1) == ":")
                                    {
                                        mato = bll.Left(item, 7);
                                    }
                                    if (item.Trim().Length == 16 && bll.Left(item.Trim(), 2) == "66")
                                    {
                                        var soku = item.Trim();
                                        //MessageBox.Show(ng + "  " + pos + "   " + mato + "      " + soku);
                                         string sql = "insert into PSPDF (NGAY,KU_MAPGD,KU_MATO,KU_SOKU) values ('" +ng + "','" + pos + "','" + mato + "','" + soku + "')";
                                         cls.UpdateDataText(sql);
                                    }
                                }
                            }
                        }
                    }
                    cls.DongKetNoi();
                    MessageBox.Show("OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    bll.DeleteAllFile(txtPath.Text.Trim());
                    ListBox.Items.Clear();
                }
                else
                {
                    MessageBox.Show("Không có File nào trong listbox", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void BtnUpPsHsbt_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngayku";
                if (dtpNgayKu.SelectedDate != null)
                {
                    giatri[0] = dtpNgayKu.SelectedDate.Value.ToString("dd/MM/yyyy");
                    bien[1] = "@Ngaygd";
                    if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                cls.UpdateDataProcPara("usp_UpPsHsbtPdf", bien, giatri, thamso);
                MessageBox.Show("OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
    }
}