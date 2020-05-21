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
    /// Interaction logic for WpfBaoCao.xaml
    /// </summary>
    public partial class WpfBaoCao : Window
    {
        public WpfBaoCao()
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
            _cls.ClsConnect();
            try
            {
                sql = "select * from KQTHINV where NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'";
                _dt=_cls.LoadDataText(sql);
                if (_dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy dữ liệu thi ngày :" + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Close();
                }
                else
                {
                        int thamso = 1;
                        string[] bien = new string[thamso];
                        object[] giatri = new object[thamso];
                        bien[0] = "@Ngay";
                        if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                        _dt = _cls.LoadDataProcPara("usp_BaoCaoTN", bien, giatri, thamso);
                        rpt_BaoCaoTN rpt = new rpt_BaoCaoTN();
                        RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
 
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
