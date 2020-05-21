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
    public partial class WpfCtCanDoi : Window
    {
        public WpfCtCanDoi()
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
                int thamso = 1;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                _dt = _cls.LoadDataProcPara("usp_CtCanDoi", bien, giatri, thamso);
                if (_dt.Rows.Count>0)
                {
                    rpt_CtCanDoi rpt = new rpt_CtCanDoi();
                    RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                }
                else
                    MessageBox.Show("Chưa có số liệu ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);

                #endregion
            }
            catch (Exception ex)
            {
                
               MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }
     
    }
}
