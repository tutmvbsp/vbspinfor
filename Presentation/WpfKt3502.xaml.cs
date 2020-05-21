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
using CrystalDecisions.Shared;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDienbao.xaml
    /// </summary>
    public partial class WpfKt3502 : Window
    {
        public WpfKt3502()
        {
            InitializeComponent();
        }

        private ClsServer _cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll _str = new ToolBll();
        DataTable _dt = new DataTable();
        private string sql = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);

        }

   
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnOk_OnClick(object sender, RoutedEventArgs e)
        {
            string ng = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
            string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
            _cls.ClsConnect();
            try
            {
                sql = "select a.NAMBC,'"+ ng + "' NGAY,convert(datetime,left(NGAY_NHAP,7)+'-'+D1,102) NGAYBC,a.MAPGD,b.PO_TEN,a.MA MAXA,a.TEN TENXA,(case when a.D16=2 then 'X' else '' end) CAPTINH"
                    + " ,(case when a.D16<>2 then 'X' else '' end) CAPHUYEN ,cast(a.D15 as numeric(10, 1)) SODIEM,a.NGAY_NHAP,a.NGUOI_NHAP from DULIEU_NT a"
                    +" left join DMPOS b on a.MAPGD = b.PO_MA where nambc = '"+ nam + "' and khoa = 'TDNN_001' and cast(a.D15 as numeric) > 0 order by a.MA,a.NGAYBC,a.NGAY_NHAP";
                string sql1 = "with lst1 as "
                                + " ( select distinct a.MA from dulieu_nt a where a.khoa = 'TDNN_001' and a.nambc = '" + nam+ "' and a.MA not in "
                                + " (select distinct b.MA from dulieu_nt b where b.khoa = 'TDNN_001' and b.nambc = '" + nam + "' and cast(b.D15 as numeric) > 0 " + " and a.MA = b.MA) )"
                                + " select '" + nam + "' NAMBC,'"+ng+"' NGAY,'' NGAYBC,a.PGD_QL MAPGD, c.PO_TEN,a.MA MAXA, a.TEN TENXA,'' CAPTINH,'' CAPHUYEN,0 SODIEM,'' " + "NGAY_NHAP,'' NGUOI_NHAP "
                                   +" from dmxa a left join DMPOS c on a.PGD_QL = c.PO_MA, lst1 b where a.MA = b.MA order by a.ma";
                _dt=_cls.LoadDataText(sql);
                var dt1 = _cls.LoadDataText(sql1);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }
                else
                {
                        rpt_KT3502 rpt = new rpt_KT3502();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                        RPUtility.ShowRp(rpt, dt1, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
            }
            catch (Exception ex)
            {
                
               MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }

    }
}
