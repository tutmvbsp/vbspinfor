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
using System.Data;
using DAL;
using BLL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfKhGnTn.xaml
    /// </summary>
    public partial class WpfTLGDXA : Window
    {
        public WpfTLGDXA()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll bll = new ToolBll();
        //DataTable dt = new DataTable();
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                if (giatri[1] == null)
                {
                    MessageBox.Show("Chưa chọn ngày : ", "Mess");
                    return;
                }
                bien[2] = "@MaXa";
                giatri[2] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                var dt = cls.LoadDataProcPara(ChkTh.IsChecked==true ? "usp_TLGDXA_TH" : "usp_TLGDXA", bien, giatri, thamso);

                if (dt.Rows.Count > 0)
                {
                    if (ChkTh.IsChecked == true)
                    {
                        rpt_TLGDXA_TH rpt = new rpt_TLGDXA_TH();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                            srv.DbPassSerVer());
                    }
                    else
                    {
                        rpt_TLGDXA rpt = new rpt_TLGDXA();
                        RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu ", "Mess");
                }
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            try
            {
                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                string sql = "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00' order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 4;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                cls.ClsConnect();
                string sql = "select MA,TEN from DMXA where right(MA,2)<>'00' and PGD_QL= " + "'" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                var dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }


        }
  
  
    }
}
