using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfSkeTo.xaml
    /// </summary>
    public partial class WpfChamCongChk
    {
        public WpfChamCongChk()
        {
            InitializeComponent();
        }
        //ClsConnectLocal cls = new ClsConnectLocal();
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtxa = new DataTable();
        //private string str = "";
        //private string FileName = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //var lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
            cls.ClsConnect();
            try
            {
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ma";
                giatri[0] = bll.Left(CboCanbo.SelectedValue.ToString().Trim(), 10);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                dt = cls.LoadDataProcPara("usp_ChamCongPC", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    rpt_ChamCong06 rpt = new rpt_ChamCong06();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                else MessageBox.Show("Không có bản ghi nào!","Thông báo", MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            dtpNgay.SelectedDate = DateTime.Now;
            try
            {
                cls.ClsConnect();
                var sql = BienBll.NdCapbc.Trim() == "02" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";                //MessageBox.Show(sql);
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    //comboBox1.Items.Add(ds.Tables[0].Rows[i][0] + " " + ds.Tables[0].Rows[i][1] + " " + ds.Tables[0].Rows[i][2]);
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                if (BienBll.NdMadv.Trim() == BienBll.MainPos.Trim())
                    dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('18','19','20','21','22') order by MA");
                else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboPhong.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message,"Mess");
            }
            cls.DongKetNoi();
        }

        private void cboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(bll.Left(cboPos.SelectedValue.ToString().Trim(),6));
                CboPhong.Items.Clear();
                cls.ClsConnect();
                if (bll.Left(cboPos.SelectedValue.ToString().Trim(), 6) == BienBll.MainPos.Trim())
                    dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('18','19','20','21','22') order by MA");
                else dtxa = cls.LoadDataText("select * from DM_PHONGBAN where MA in ('29','30','31') order by MA");
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboPhong.Items.Add(dtxa.Rows[i][0].ToString().Trim() + " | " + dtxa.Rows[i][1]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void CboPhong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                //MessageBox.Show(str.Left(cboXa.SelectedValue.ToString().Trim(), 8));
                CboCanbo.Items.Clear();
                cls.ClsConnect();
                if (dtpNgay.SelectedDate != null)
                {
                    string sql = "select MA_CIF,ND_TEN from DM_CANBO where ND_MADV='"+bll.Left(cboPos.SelectedValue.ToString().Trim(),6)+"' and ND_PHONGBAN='"+ bll.Left(CboPhong.SelectedValue.ToString().Trim(), 2) + "' order by STT";
                    //MessageBox.Show(sql);
                    var dtto = cls.LoadDataText(sql);
                    for (int i = 0; i < dtto.Rows.Count; i++)
                    {
                        CboCanbo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }
    }
}
