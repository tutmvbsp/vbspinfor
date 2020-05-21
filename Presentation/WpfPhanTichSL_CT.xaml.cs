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
    public partial class WpfPhanTichSL_CT : Window
    {
        public WpfPhanTichSL_CT()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll bll = new ToolBll();
        DataTable dtpos = new DataTable();
        DataTable dt = new DataTable();
        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                int thamso = 6;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                giatri[1] = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                if (giatri[1] == null)
                {
                    MessageBox.Show("Chưa chọn ngày : ", "Mess");
                    return;
                }
                bien[2] = "@HTVAY";
                if (Ration1.IsChecked == true)
                {
                    giatri[2] = "1";
                } else if (Ration2.IsChecked == true)
                {
                    giatri[2] = "2";
                }
                else
                {
                    giatri[2] = "3";
                }

                bien[3] = "@Nguon";
                if (Ration4.IsChecked == true)
                {
                    giatri[3] = "1";
                }
                else if (Ration5.IsChecked == true)
                {
                    giatri[3] = "2";
                }
                else
                {
                    giatri[3] = "3";
                }
                bien[4] = "@Mau";
                if (Ration7.IsChecked == true)
                {
                    giatri[4] = "1";
                }
                else if (Ration8.IsChecked == true)
                {
                    giatri[4] = "2";
                }
                bien[5] = "@MaXa";
                giatri[5] = bll.Left(CboXa.SelectedValue.ToString().Trim(), 6);
                dt = cls.LoadDataProcPara("usp_PhanTichSL_CT", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    rpt_PhanTichSL_CT rpt = new rpt_PhanTichSL_CT();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
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
            Ration7.IsChecked = true;
            CboXa.IsEnabled = false;
            try
            {
                cls.ClsConnect();
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
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
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + bll.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
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

        private void Ration8_Checked(object sender, RoutedEventArgs e)
        {
            if (CboXa.IsEnabled == false)
            {
                CboXa.IsEnabled = true;
            }
            Ration1.IsEnabled = false;
            Ration2.IsEnabled = false;
            Ration3.IsChecked = true;

        }

        private void Ration7_Checked(object sender, RoutedEventArgs e)
        {
            CboXa.IsEnabled = false;
            Ration1.IsEnabled = true;
            Ration2.IsEnabled = true;
            Ration1.IsChecked = true;
        }
  
    }
}
