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
    public partial class WpfImpPortDS : Window
    {

        public WpfImpPortDS()
        {
            InitializeComponent();
        }
        private FileStream _fw;
        ToolBll bll = new ToolBll();
        DataTable dt = new DataTable();
        string thumuc = "C:\\KT740";
        private string FileName = "";
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
                    //MessageBox.Show(bll.Left(bll.Right(txtPath.Text.Trim(), 16), 12)+'_'+bll.Right(bll.Left(CboSheet.SelectedValue.ToString().Trim(), 3),2));
                    string FileName = "C:\\TEXT\\"+bll.XoaHetKyTuTrang(bll.Left(bll.Right(txtPath.Text.Trim(), 16), 12) + '_' + bll.Right(bll.Left(CboSheet.SelectedValue.ToString().Trim(), 3), 2)) +".txt";
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
                                sw.Write(row[i].ToString() + "$");
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
                openFileDialog.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
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
                OleDbCommand cmd = new OleDbCommand("Select ct,nam,dp,ttho,tttv,hoten,namsinh,quanhe,namsinh1,ten,cmt From [" + name + "]", con);
                con.Open();
                OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                ad.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = dt.DefaultView;
                    MessageBox.Show("Read Excel OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                    //btnWrite.IsEnabled = true;
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

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            ClsServer cls = new ClsServer();
            cls.ClsConnect();
            try
            {
                string FileName = "C:\\TEXT\\" + bll.XoaHetKyTuTrang(bll.Left(bll.Right(txtPath.Text.Trim(), 16), 12) + '_' + bll.Right(bll.Left(CboSheet.SelectedValue.ToString().Trim(), 3), 2)) + ".txt";
                MessageBox.Show(FileName);
                int thamso = 1;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@PathDir";
                giatri[0] = FileName;
                if (File.Exists(giatri[0].ToString().Trim()))
                {
                    cls.UpdateLdbf("usp_InsertDS", bien, giatri, thamso);
                    File.Delete(giatri[0].ToString().Trim());
                    MessageBox.Show("Insert OK : " + giatri[1]);
                }
                else
                {
                    MessageBox.Show(" Chưa có file : " + giatri[1].ToString().Trim());
                }
                // lbl.Content = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thong bao", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string ins = "";
            ClsServer cls = new ClsServer();
            String name = CboSheet.SelectedValue.ToString().Trim();
            //String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +"d:\\DBIMP\\DBEXCEL.xlsx" +";Extended Properties='Excel 12.0 XML;HDR=YES;';";
            String constr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + txtPath.Text.Trim() +
                            ";Extended Properties='Excel 12.0 XML;HDR=YES;';";
            try
            {
                OleDbConnection con = new OleDbConnection(constr);
                OleDbCommand cmd =
                    new OleDbCommand(
                        "Select ct,nam,dp,ttho,tttv,hoten,namsinh,quanhe,namsinh1,ten,cmt From [" + name +"]", con);
                con.Open();
                OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                ad.Fill(dt);
                if (dt.Rows.Count > 0) dgvData.ItemsSource = dt.DefaultView;
                //MessageBox.Show("Read Excel OK", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                //btnWrite.IsEnabled = true;

                else MessageBox.Show("Không có bản ghi nào", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);
                con.Close();
                con.Dispose();
                cls.ClsConnect();
                foreach (DataRow dr in dt.Rows)
                {
                    ins = "insert into DANHSACH (CT,NAM,DP,TTHO,TTTV,HOTEN,NAMSINH,QUANHE,NAMSINH1,TEN,CMT)"
                                 + " values ('" + dr["CT"] + "','" + dr["NAM"] + "','" + dr["DP"] + "','" + dr["TTHO"] + "','" +
                                 dr["TTTV"] + "',N'" + dr["HOTEN"] + "','" +dr["NAMSINH"] + "',N'" + dr["QUANHE"] + "','" + dr["NAMSINH1"] + "',N'" + dr["TEN"] + "','" +
                                 dr["CMT"] + "')";
                    //MessageBox.Show(upd);
                    cls.UpdateDataText(ins);
                }
                MessageBox.Show("Insert OK", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message+"     "+ins , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            //dt = null;
            dt.Dispose();
            dt.Clear();
            dgvData.ItemsSource = null;
            dgvData.Items.Refresh();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateMonthsAndYears();
            ClsServer cls = new ClsServer();
            cls.ClsConnect();
            string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
            //var sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
            var dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
            DataTable dtchon = new DataTable();
            string sqlch = "select CHTRINH,TEN_CT from DM_CHTRINH where CHTRINH in ('01','09','19') order by CHTRINH";
            dtchon = cls.LoadDataText(sqlch);
            for (int i = 0; i < dtchon.Rows.Count; i++)
            {
                CboChTr.Items.Add(dtchon.Rows[i][0] + " | " + dtchon.Rows[i][1]);
            }
            CboChTr.SelectedIndex = 0;
            CboPos.SelectedIndex = 0;
            cls.DongKetNoi();
        }
        private void PopulateMonthsAndYears()
        {
            //comboBoxMonth.ItemsSource = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames.Take(12).ToList();
            //comboBoxMonth.SelectedItem = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames[DateTime.Now.AddMonths(-1).Month - 1];
            //for (int x = 0; x < 12; x++)
            //{
            //    comboBoxMonth.Items.Add
            //    (
            //       (x + 1).ToString("00")
            //       + " "
            //       + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames.GetValue(x)
            //     );
            //}
            //comboBoxMonth.SelectedIndex = 0;
            comboBoxYear.ItemsSource = Enumerable.Range(2010, DateTime.Now.Year - 2010 + 5).ToList();
            comboBoxYear.SelectedItem = DateTime.Now.Year;
            comboBoxYear.SelectedIndex = 7;
        }
        private void LblCheck_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ClsServer cls = new ClsServer();
                cls.ClsConnect();
                string nam = comboBoxYear.SelectedValue.ToString().Trim();
                string pos = bll.Right(bll.Left(CboPos.SelectedValue.ToString(), 6), 4);
                string chtr = bll.Left(CboChTr.SelectedValue.ToString(), 2);
                FileName = thumuc + "\\" + nam + "_" + pos + "_" + chtr + "_" + DateTime.Now.ToString("ddMMyyyy") + ".csv";
                string chk = "with lst1 as ( select b.MA MAXA, b.TEN TENXA, a.MA MATHON, a.TEN TENTHON from DMTHON a, DMXA b where a.XA = b.MA and left(a.MA, 4) ='" + pos + "'"
                            + " ), lst2 as (select distinct a.CT,a.NAM,a.DP from DANHSACH a, DMTHON b where a.NAM = '" + nam + "' and a.CT = '" + chtr + "' and left(a.DP, 4)= '" + pos + "'"
                            + ") select "+chtr+" N'Chương Trình',"+nam+" N'Năm', a.MAXA N'Mã Xã',a.TENXA N'Tên Xã',a.MATHON N'Mã Thôn',a.TENTHON N'Tên Thôn',(select 'X' from lst2 where DP = a.MATHON) N'Đã nhập' from lst1 a order by a.MATHON";
                var dt = cls.LoadDataText(chk);
                bll.ExportToExcel(dt, FileName);
                bll.OpenExcel(FileName);
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}