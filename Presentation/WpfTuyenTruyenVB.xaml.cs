using System;
using System.Data;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Globalization;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDelete.xaml
    /// </summary>
    public partial class WpfTuyenTruyenVB : Window
    {
        public WpfTuyenTruyenVB()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                string pos = RadCboPos.SelectedValue.ToString().Trim();
                string quy = ((int.Parse(dtpNgay.SelectedDate.Value.ToString("MM"))-1)/3+1).ToString();
                string thang = dtpNgay.SelectedDate.Value.ToString("MM");
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string ngayky = dtpNgayKy.SelectedDate.Value.ToString("yyyy-MM-dd");
                string strsql = "INSERT INTO TT_VBCD (NGAY, THANG, NAM,SOVB,NOIDUNG,MA_CIF,MA_CVU,POS,QUY,NGAYKY) " +
                                " VALUES ('"+ng+"', '"+thang+"', '"+nam+"',N'"+txtSoVB.Text+"',N'"+txtNoiDung.Text+"','"+RadCboKy.SelectedValue+"','"+RadCboCvu.SelectedValue+"','"+pos+"','"+quy+ "','" + ngayky + "'); ";
                _cls.UpdateDataText(strsql);
                MessageBox.Show("Lưu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _cls.DongKetNoi();
            }

        }
   


     

  
        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
            var f = new WpfTuyenTruyen();
            f.ShowDialog();
        }

        private void RadCboKy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RadCboCvu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void WpfTuyenTruyenVB_OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dtpNgay.SelectedDate = DateTime.Now;
                _cls.ClsConnect();
                string strpos = BienBll.NdCapbc.Trim() == "02" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00'";
                var dtpos = _cls.LoadDataText(strpos);
                RadCboPos.ItemsSource = dtpos.DefaultView;
                RadCboPos.DisplayMemberPath = "PO_TEN";
                RadCboPos.SelectedValuePath = "PO_MA";
                string strng = "";
                if (BienBll.NdCapbc.Trim() == "02")
                    strng =
                        "select MA_CIF,ND_TEN from ng_dung where ND_PHONGBAN='29' and ND_MADV='"+ BienBll.NdMadv.Trim() + "' and ND_TTHAI='A' order by ND_CHUCVU";
                else
                    strng =
                        "select MA_CIF,ND_TEN from ng_dung where ND_PHONGBAN='17' and ND_TTHAI='A' order by ND_CHUCVU";
                var dtky =_cls.LoadDataText(strng);
                RadCboKy.ItemsSource = dtky.DefaultView;
                RadCboKy.DisplayMemberPath = "ND_TEN";
                RadCboKy.SelectedValuePath = "MA_CIF";
                var dtcv = _cls.LoadDataText("select * from DM_CHUCVU where ma in ('1','2')");
                RadCboCvu.ItemsSource = dtcv.DefaultView;
                RadCboCvu.DisplayMemberPath = "TEN";
                RadCboCvu.SelectedValuePath = "MA";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }
    }
}
