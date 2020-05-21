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
    public partial class WpfKHTD : Window
    {
        public WpfKHTD()
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
            try
            {
                _cls.ClsConnect();
                if (BienBll.NdMadv == BienBll.MainPos)
                {
                    sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                }
                else
                {
                    sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='" + BienBll.NdMadv + "'";
                }
                var dtpos = _cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 1;
                var dtng = _cls.LoadDataText("select MAX(convert(date,NGAY,105)) as NGMAX from U_CANDOI");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            _cls.DongKetNoi();

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
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[1] = "@MaPos";
                giatri[1] = _str.Left(cboPos.SelectedValue.ToString().Trim(), 6);

                _dt = _cls.LoadDataProcPara("usp_KHTD", bien, giatri, thamso);
                if (_dt.Rows.Count != 0)
                {
                    rpt_KHTD rpt = new rpt_KHTD();
                    RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu " + dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy"), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
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
