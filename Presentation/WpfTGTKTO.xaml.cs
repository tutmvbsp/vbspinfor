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
using System.IO;
using BLL;
using DAL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfSkeTo.xaml
    /// </summary>
    public partial class WpfTGTKTO : Window
    {
        public WpfTGTKTO()
        {
            InitializeComponent();
        }
        //ClsConnectLocal cls = new ClsConnectLocal();
       // private FileStream _fw;
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                cls.ClsConnect();
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = str.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[2] = "@Mau";
                if (Ration1.IsChecked != null && (bool) Ration1.IsChecked) giatri[2] = "1";
                else if (Ration2.IsChecked != null && (bool) Ration2.IsChecked) giatri[2] = "2";
                else giatri[2] = "3";

                // MessageBox.Show(giatri[0]+","+giatri[1] + ","+giatri[2] + ","+giatri[3] + ","+giatri[4]);
                dt = cls.LoadDataProcPara("usp_TGTKTO", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    rpt_TGTKTO01 rpt = new rpt_TGTKTO01();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(),
                        srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                var dtpos = cls.LoadDataText("select PO_MA,PO_TEN from DMPOS order by PO_MA");
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 5;
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message,"Mess");
            }
            //CboChon.Items.Add("003000 | Tất cả");
            cls.DongKetNoi();
        }
      
    }
}
