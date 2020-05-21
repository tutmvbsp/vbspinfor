using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfKhtc01.xaml
    /// </summary>
    public partial class WpfKHTC
    {
        public WpfKHTC()
        {
            InitializeComponent();
        }

        readonly ClsServer _cls = new ClsServer();
        DataTable _dt = new DataTable();
        readonly ToolBll _str = new ToolBll();
        private string sql = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show(BienBll.Ndma);
            ServerInfor srv = new ServerInfor();
            _cls.ClsConnect();
            int thamso = 2;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@Ngay";
            if (dtpNgay.SelectedDate != null) giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
            bien[1] = "@MaPos";
            giatri[1] = _str.Left(cboPos.SelectedValue.ToString().Trim(), 6);
            _dt = _cls.LoadDataProcPara("usp_KHTC", bien, giatri, thamso);
            if (_dt.Rows.Count > 0)
            {
                if (BienBll.NdMadv.Trim() == BienBll.MainPos.Trim())
                {
                    rpt_QT_KHTC_NEW rpt = new rpt_QT_KHTC_NEW();
                    RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());
                    //FileName = Thumuc + "\\" + giatri[1] + "_KHTC_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    //_str.ExportToExcel(_dt, FileName);
                    //MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    //_str.OpenExcel(FileName);
                }
                else
                {
                    rpt_QT_KHTCHuyen_NEW rpt = new rpt_QT_KHTCHuyen_NEW();
                    RPUtility.ShowRp(rpt, _dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),srv.DbPassSerVer());}
 

            }
            else
            {
                MessageBox.Show("Chưa có số liệu", "Thông báo");
            }
            _cls.DongKetNoi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
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
        private void LblManual_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (BienBll.NdMadv == BienBll.MainPos)
            {
                WpfQttcNhapTay f = new WpfQttcNhapTay();
                f.ShowDialog();
            }
            else
            {
                WpfQttcNhapKH f = new WpfQttcNhapKH();
                f.ShowDialog();
            }
        }
    }
}
