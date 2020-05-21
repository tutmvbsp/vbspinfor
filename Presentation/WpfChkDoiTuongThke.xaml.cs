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
    public partial class WpfChkDoiTuongThke : Window
    {

        public WpfChkDoiTuongThke()
        {
            InitializeComponent();
        }
        ToolBll bll = new ToolBll();
        private ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            ClsServer cls = new ClsServer();
            try
            {
                bll.TaoThuMuc(Thumuc);
                string pos = bll.Right(bll.Left(CboPos.SelectedValue.ToString().Trim(), 6),4);
                cls.ClsConnect();
                string strsql = "with lst1 as ( select MA, TEN from DMTHON where LEFT(MA,4)= '" + pos + "' and TRANGTHAI = 'A' ), lst2 as ( "
                    + " select a.CT,a.NAM,LEFT(a.DP, 4) POS,LEFT(a.DP, 6) Xa, a.DP THON,(select ten from dmthon where a.dp = MA) TENTHON,COUNT(a.HOTEN) DEM "
                    +" from DANHSACH a where LEFT(DP,4)= '"+pos+"' and CT = '"+bll.Left(CboChTr.SelectedValue.ToString(),2)+"' and NAM = '"+comboBoxYear.SelectedValue+"' "
                    +" group by LEFT(a.DP, 4),LEFT(a.DP, 6), a.DP,a.CT,a.NAM ) "
                    +" select LEFT(a.MA, 4) POS,(select po_ten from dmpos where LEFT(a.MA, 4) = right(po_ma, 4)) TENPOS "
                    + " ,left(a.MA, 6) N'Xã' ,(select TEN from dmxa where MA = left(a.MA, 6)) N'Tên Xã',a.MA N'Thôn',a.TEN N'Tên Thôn','" + comboBoxYear.SelectedValue + "' NAM,'" + bll.Left(CboChTr.SelectedValue.ToString(), 2) + "' CT "
                    + " ,(select GIATRI from dmkhac where khoa_1 = '07' and MOTA = b.CT) N'Tên CHTR',isnull(b.DEM, 0) N'Số khẩu' "
                    +" from lst1 a left join lst2 b on a.MA = b.THON order by a.MA";
                dt = cls.LoadDataText(strsql);
                FileName = Thumuc + "\\" + pos + "_" + comboBoxYear.SelectedValue + "_" + bll.Left(CboChTr.SelectedValue.ToString(),2) + "_Danh sách" + ".csv";
                if (dt.Rows.Count > 0)
                {
                    bll.ExportToExcel(dt, FileName);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    bll.OpenExcel(FileName);
                }
                else
                {
                    MessageBox.Show("Không có số liệu", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                //MessageBox.Show(strsql, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
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
            CboPos.SelectedIndex = 0;
            DataTable dtchon = new DataTable();
            string sqlch = "select CHTRINH,TEN_CT from DM_CHTRINH where CHTRINH in ('01','09','19') order by CHTRINH";
            dtchon = cls.LoadDataText(sqlch);
            for (int i = 0; i < dtchon.Rows.Count; i++)
            {
                CboChTr.Items.Add(dtchon.Rows[i][0] + " | " + dtchon.Rows[i][1]);
            }
            CboChTr.SelectedIndex = 0;

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
    }
}