using System;
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
using System.IO;
using System.Globalization;
using DAL;
using BLL;
using System.Data;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfChamCongTK : Window
    {
        public WpfChamCongTK()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        DataTable dtxa = new DataTable();
        private string str = "",mau="";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate=DateTime.Now;
            cls.ClsConnect();
            //string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA=" + "'" + BienBll.NdMadv.Trim() + "'";
            string sql = BienBll.NdCapbc.Trim() == "1" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
            var dtpos = cls.LoadDataText(sql);
            for (int i = 0; i < dtpos.Rows.Count; i++)
            {
                CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
            }
            //CboPos.SelectedIndex = 0;
            CboPB.Items.Clear();
            if (BienBll.NdMadv.Trim() == BienBll.MainPos.Trim())
                dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('17','18','19','20','21','22') order by MA");
            else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
            for (int i = 0; i < dtxa.Rows.Count; i++)
            {
                CboPB.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
            }
            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            //MessageBox.Show(bll.Left(comboBoxMonth.SelectedValue.ToString().Trim(),2));
            //MessageBox.Show(comboBoxYear.SelectedValue.ToString().Trim());
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(bll.Left(cboPos.SelectedValue.ToString().Trim(),6));
                CboPB.Items.Clear();
                cls.ClsConnect();
                if (bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) == BienBll.MainPos.Trim())
                    dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('18','19','20','21','22') order by MA");
                else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboPB.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string thang = dtpNgay.SelectedDate.Value.Month.ToString();
            string nam = dtpNgay.SelectedDate.Value.Year.ToString();
            //string phong = bll.Left(CboPB.SelectedValue.ToString().Trim(), 2);
            try
            {
                cls.ClsConnect();
                {
                    try
                    {
                        int thamso = 4;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@MaPos";
                        giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                        bien[1] = "@Ngay";
                        if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        bien[2] = "@Phong";
                        giatri[2] = bll.Left(CboPB.SelectedValue.ToString().Trim(), 2);
                        bien[3] = "@Mau";
                        if (Ration1.IsChecked == true) giatri[3] = "CC";
                        else if (Ration2.IsChecked == true) giatri[3] = "LT";
                        else giatri[3] = "NB";
                        mau = giatri[3].ToString();
                        if (Ration1.IsChecked == true || Ration2.IsChecked == true || Ration3.IsChecked == true)
                        {
                            dt = cls.LoadDataProcPara("usp_ChamCongTK", bien, giatri, thamso);
                            if (dt.Rows.Count > 0)
                            {
                                if (mau == "CC")
                                {
                                    rpt_ChamCong02 rpt = new rpt_ChamCong02();
                                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                                        srv.DbUserSerVer(), srv.DbPassSerVer());
                                }
                                else if (mau == "LT")
                                {
                                    rpt_ChamCong03 rpt = new rpt_ChamCong03();
                                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                                        srv.DbUserSerVer(), srv.DbPassSerVer());
                                }
                                else
                                {
                                    rpt_ChamCong04 rpt = new rpt_ChamCong04();
                                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                                        srv.DbUserSerVer(), srv.DbPassSerVer());
                                }
                            } 
                            else
                                MessageBox.Show("Không có bản ghi nào !", "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                        } // ration 4 hoac 5
                        else if (Ration4.IsChecked ==true|| Ration5.IsChecked==true )
                        {
                            int thamso1 = 2;
                            string[] bien1 = new string[thamso1];
                            object[] giatri1 = new object[thamso1];
                            bien1[0] = "@MaPos";
                            giatri1[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                            bien1[1] = "@Ngay";
                            if (dtpNgay.SelectedDate != null) giatri1[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                            dt = cls.LoadDataProcPara("usp_ChamCongPhep", bien1, giatri1, thamso1);
                            if (Ration4.IsChecked == true)
                            {
                                rpt_ChamCong05 rpt = new rpt_ChamCong05();
                                RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                                    srv.DbUserSerVer(), srv.DbPassSerVer());
                            }
                            if (Ration5.IsChecked == true)
                            {
                                rpt_ChamCong09 rpt1 = new rpt_ChamCong09();
                                RPUtility.ShowRp(rpt1, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                                    srv.DbUserSerVer(), srv.DbPassSerVer());
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lổi, kiểm tra lại thông tin " + ex.Message, "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Error);

                    }

                }
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
   

    


 

   
    }
}
