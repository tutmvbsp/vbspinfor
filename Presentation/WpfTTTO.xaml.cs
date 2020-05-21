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
    public partial class WpfTTTO : Window
    {
        public WpfTTTO()
        {
            InitializeComponent();
        }

        private ToolBll s = new ToolBll();
        private readonly ClsServer cls = new ClsServer();
        private DataTable dt = new DataTable();
        private DataTable dtNew = new DataTable();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               // dtpNgay.SelectedDate = DateTime.Now;

                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString()).AddMonths(-1);
                DateTime lastMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month,
                    DateTime.DaysInMonth(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month));
                dtpNgay.SelectedDate = lastMonth;
                string sql;
                //sql = "select PO_MA MA,PO_TEN TEN from DMPOS where PO_MA=" + "'" + BienBll.NdMadv.Trim() + "'";
                sql = "select PO_MA MA,PO_TEN TEN from DMPOS order by PO_MA";
                var dtpos = cls.LoadDataText(sql);
                CboPos.ItemsSource = dtpos.DefaultView;
                
                CboPos.DisplayMemberPath = "TEN";
                CboPos.SelectedValuePath = "MA";
                CboPos.SelectedIndex = 0;
                //var sqlpb = "select a.MA,a.TEN from DMXA a,NG_DUNG b,CBTD c where a.CMT_CBTD=ND_CMT and a.CMT_CBTD=c.CMT_CBTD and a.PGD_QL='"+CboPos.SelectedValue+"' and upper(b.ND_MA)='"+BienBll.Ndma.Trim().ToUpper()+"'";
                var sqlpb = "select a.MA,a.TEN from DMXA a where a.PGD_QL='" + CboPos.SelectedValue + "'";
                var dtloaits = cls.LoadDataText(sqlpb);
                CboXa.ItemsSource = dtloaits.DefaultView;
                CboXa.DisplayMemberPath = "TEN";
                CboXa.SelectedValuePath = "MA";
                CboXa.SelectedIndex = 0;

                // bo sung to con thieu
                string strins = "insert into TTTO select '0' STT,'0' TT,a.TO_MAPGD MAPOS,LEFT(a.TO_MADP,6) MAXA,a.TO_MADP MATHON,a.TO_MATO MATO,a.TO_MATT MAKH,a.TO_TENTT TEN "
                +" ,(select TEN from DMTHON where MA = a.TO_MADP) DIACHI "
                +" ,'0' + SUBSTRING(b.KH_MOBILE, 6, len(b.kh_mobile)) KH_MOBILE,'' NGAY,'' GIO,'' LICHHOP,'' DIADIEM,a.TO_DVUT DVUT, a.TRANGTHAI,'' TEN_TO "
                +" from HSTO a, HSKH b where a.TO_MATO not in (select MATO from TTTO where MATO = a.TO_MATO) and a.TRANGTHAI <> 'C' and a.TO_MATT = b.KH_MAKH";
                cls.UpdateDataText(strins);
                //---

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
        private void ShowGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                dtNew = null;
                dgvTarGet.ItemsSource = null;
                cls.ClsConnect();
                string sqlload ="select * from TTTO where MAXA='"+CboXa.SelectedValue+"' order by MATHON";
                dt = cls.LoadDataText(sqlload);
                if (dt.Rows.Count > 0)
                {
                    dgvData.ItemsSource = dt.DefaultView;
                }
                else
                    MessageBox.Show("Không có dữ liệu !", "Thông báo", MessageBoxButton.OK,
                        MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                dtNew = dt.GetChanges();
                if (dtNew != null)
                {
                    dgvTarGet.ItemsSource = dtNew.DefaultView;
                }
                else
                    MessageBox.Show("Không có thay đổi nào !", "Thông báo", MessageBoxButton.OK,MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                foreach (DataRow dr in dtNew.Rows)
                {
                    string upd = "update TTTO set TEN_TO=N'"+dr["TEN_TO"]+"',MATHON='"+dr["MATHON"]+ "',MATO='" + dr["MATO"] + "',DIACHI=N'" + dr["DIACHI"] + "',DIADIEM=N'" + dr["DIADIEM"] 
                        + "',NGAY='" + dr["NGAY"] + "',GIO='" + dr["GIO"] + "',MOBILE='" + dr["MOBILE"] + "',ND_MA='" + BienBll.Ndma.Trim()+ "',ND_TEN=N'" + BienBll.NdTen.Trim()
                        + "' where MATO='"+ dr["MATO"] + "'";
                    MessageBox.Show(upd);
                    //cls.UpdateDataText(upd);
                }
                MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
            cls.DongKetNoi();
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var sqlpb = "select a.MA,a.TEN from DMXA a where a.PGD_QL='" + CboPos.SelectedValue + "' order by a.MA";
                var dtloaits = cls.LoadDataText(sqlpb);
                CboXa.ItemsSource = dtloaits.DefaultView;
                CboXa.DisplayMemberPath = "TEN";
                CboXa.SelectedValuePath = "MA";
                CboXa.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }
    }

}
