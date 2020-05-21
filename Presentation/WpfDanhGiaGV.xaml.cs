using System;
using System.Data;
using System.Security.AccessControl;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfDanhGiaGV
    {
        public WpfDanhGiaGV()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        private DataTable dtNew = new DataTable();
        private ServerInfor srv = new ServerInfor();
        private string strsql = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
              dtpNgay.SelectedDate = DateTime.Now;
        }


        private void btnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }



        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            dt = null;
            dgvData.ItemsSource = null;
            try
            {
                cls.ClsConnect();
                strsql =
                    " select '"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"' NGAY,b.PO_MA,b.PO_TEN,c.ND_MA,c.ND_TEN,a.* from DANHGIAGV a,DMPOS b, NG_DUNG c where b.PO_MA = c.ND_MADV and ND_MA='"+BienBll.Ndma.Trim()+"'";
                //MessageBox.Show(strsql);
                dt =cls.LoadDataText(strsql);
                if (dt.Rows.Count > 0) dgvData.ItemsSource = dt.DefaultView;
                else
                    MessageBox.Show("Không có dữ liệu !", "Thông Báo", MessageBoxButton.OK,
                        MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();
        }

     
 
 

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtchk = cls.LoadDataText("select * from LUU_DANHGIAGV where NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "' and ND_MA='" + BienBll.Ndma.Trim() + "'");
                if (dtchk.Rows.Count == 0)
                {
                    dtNew = dt.Clone();
                    foreach (DataRow dr in dt.Rows)
                    {
                        if ((bool) dr["TOT"] == true || (bool) dr["KHA"] == true || (bool) dr["TB"] == true)
                        {
                            dtNew.ImportRow(dr);
                        }
                    }
                    if (dtNew == null || dtNew.Rows.Count == 0)
                    {
                        MessageBox.Show("Chưa chọn khách hàng nào !", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                    }
                    
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        strsql =
                            "insert into LUU_DANHGIAGV(NGAY,PO_MA,PO_TEN,ND_MA,ND_TEN,TT,CHITIEU,TOT,KHA,TB,DEXUAT)" +
                            " Values('" + dr["NGAY"] + "','" + dr["PO_MA"] + "',N'" + dr["PO_TEN"] + "','" + dr["ND_MA"] +
                            "',N'"
                            + dr["ND_TEN"] + "','" + dr["TT"] + "',N'" + dr["CHITIEU"] + "','" + dr["TOT"] + "','"
                            + dr["KHA"] + "','" + dr["TB"] + "',N'" + dr["DEXUAT"] + "')";
                        cls.UpdateDataText(strsql);
                        // MessageBox.Show(strsql);
                    }
                }
                //MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                var dtin = cls.LoadDataText("select * from LUU_DANHGIAGV where NGAY='"+dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd")+"' and ND_MA='"+BienBll.Ndma.Trim()+"' order by TT");
                rpt_DanhGiaGV rpt = new rpt_DanhGiaGV();
                RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                cls.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi "+ex.Message,"Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }
    }
}
