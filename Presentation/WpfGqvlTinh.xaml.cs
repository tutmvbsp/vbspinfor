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
    /// Interaction logic for WpfDienbao.xaml
    /// </summary>
    public partial class WpfGqvlTInh : Window
    {
        public WpfGqvlTInh()
        {
            InitializeComponent();
        }

        private ClsServer _cls = new ClsServer();
        private ServerInfor srv = new ServerInfor();
        private ToolBll _str = new ToolBll();
        DataTable _dt = new DataTable();
        //private string sql = "";

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
                #region
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                bien[1] = "@Mau";
                giatri[1] = "";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                _dt = _cls.LoadDataProcPara("usp_QlyGqvlTinh", bien, giatri, thamso);
                rpt_NguonCqlvTinh rpt = new rpt_NguonCqlvTinh();
                RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                rpt_NguonCqlvTinh1 rpt1 = new rpt_NguonCqlvTinh1();
                RPUtility.ShowRp(rpt1, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                #endregion
            }
            catch (Exception ex)
            {
                
               MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }


        private void LblNguon_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (BienBll.Ndma.Trim() == "thampth3005" || BienBll.Ndma.Trim() == "tutm3005")
            //{
               WpfNguonCqlvTinh f = new WpfNguonCqlvTinh();
                f.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Bạn không vào mục này !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            //}

        }
    }
}
