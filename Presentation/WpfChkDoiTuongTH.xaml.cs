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
    public partial class WpfChkDoiTuongTH : Window
    {

        public WpfChkDoiTuongTH()
        {
            InitializeComponent();
        }
        ToolBll bll = new ToolBll();
        private ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            ClsServer cls = new ClsServer();
            try
            {
                cls.ClsConnect();
                int thamso = 5;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[1] = "@MaPos";
                giatri[1] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[2] = "@MaXa";
                giatri[2] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                bien[3] = "@Nam";
                giatri[3] = comboBoxYear.SelectedValue.ToString().Trim();
                bien[4] = "@Chtr";
                giatri[4] = bll.Left(CboChTr.SelectedValue.ToString(), 2);
                dt = cls.LoadLdbf("usp_TTDSHN", bien, giatri, thamso);
                rpt_ChkDoituong rpt = new rpt_ChkDoituong();
                RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

                //MessageBox.Show("Insert OK", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error + " + ex.Message , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
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

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ClsServer cls = new ClsServer();
                if (bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) != "003000")
                {
                    CboXa.Items.Clear();
                    cls.ClsConnect();
                    DataTable dtxa = new DataTable();
                    string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" +
                                 bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                    dtxa = cls.LoadDataText(sql);
                    for (int i = 0; i < dtxa.Rows.Count; i++)
                    {
                        CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                    }
                }
                else
                {
                    CboXa.Items.Add("003000 | Tất cả");
                }
                CboXa.SelectedIndex = 0;
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
    }
}