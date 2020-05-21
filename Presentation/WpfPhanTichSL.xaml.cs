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
    public partial class WpfPhanTichSL : Window
    {
        public WpfPhanTichSL()
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
                int thamso = 4;
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
                bien[2] = "@Mau";
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

                //dt = cls.LoadDataProcPara("usp_PhanTichSL", bien, giatri, thamso);
                dt = cls.LoadLdbf("usp_PhanTichSL", bien, giatri, thamso);
                //dataGrid1.ItemsSource = dt.DefaultView;
                rpt_PhanTichSL rpt = new rpt_PhanTichSL();
                RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }
  
    }
}
