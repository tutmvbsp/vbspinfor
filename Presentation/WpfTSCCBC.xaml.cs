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
using DAL;
using BLL;
using System.Data;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfTSCCBC : Window
    {
        public WpfTSCCBC()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        HardwareInfo infor= new HardwareInfo();
        private readonly ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";
        string strpos = "";
        string strphong = "";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(NGAYBC) as NGMAX from QT_TSCC");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                //DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                //if (BienBll.NdMadv == BienBll.MainPos)
                //{
                //    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS  order by PO_MA";
                //    strphong = "select * from DM_PHONGBAN order by MA";
                //}
                //else
                //{
                //    strpos = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA='"+CboPos.SelectedValue.ToString().Trim()+"'";
                //    strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";
                //}
                strpos = "select PO_MA MA,PO_TEN TEN from DMPOS order by PO_MA";
                //if (BienBll.NdMadv == BienBll.MainPos)
                //    strphong = "select * from DM_PHONGBAN where ma  in ('17','18','19','20','21','22','34') order by MA";
                //else strphong = "select * from DM_PHONGBAN where ma not in ('17','18','19','20','21','22','34')";
                //strphong = "select * from DM_PHONGBAN where MA='"+BienBll.PhongBan+"'";
                var dtpos = cls.LoadDataText(strpos);
                CboPos.ItemsSource = dtpos.DefaultView;
                CboPos.DisplayMemberPath = "TEN";
                CboPos.SelectedValuePath = "MA";
    

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }



        private void Close_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
 
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string strup = "";
            try
            {
                cls.ClsConnect();
                string sqlload =
                    "select '"+dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy")+"' NGAY,a.POS_CD,b.PO_TEN,a.MA_NHANHIEU_TS,a.TEN_NHANHIEU_TS,a.MAPHONG,a.TENPHONG,convert(varchar(4),DATEPART(yyyy,NGAY_MUA)) NAM,A.MA_TS,a.MOTA,MA_CIF,CB_QUANLY,a.GDX from LUU_TSCC a left join DMPOS b on a.POS_CD = b.PO_MA where a.POS_CD = '"+CboPos.SelectedValue+"' and LOAI_TS_CHITIET = 'TI1' and a.TRANGTHAI = 'A' order by MAPHONG,MA_NHANHIEU_TS,MA_CIF";
                var dtin = cls.LoadDataText(sqlload);
                if (dtin.Rows.Count > 0)
                {
                    rpt_TSCC_BC rpt = new rpt_TSCC_BC();
                    RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                }
                else
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
    }

}
