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
using System.IO;
using DAL;
using BLL;
using System.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UI;
using Presentation.XTraReport;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfDvut.xaml
    /// </summary>
    public partial class WpfKTNB_06 : Window
    {
        public WpfKTNB_06()
        {

            InitializeComponent();
        }

        private ClsServer cls = new ClsServer();
        private ToolBll s = new ToolBll();
        private ServerInfor srv = new ServerInfor();
        private DataTable dt = new DataTable();
        private DataTable dtNew = new DataTable();
        //string Thumuc = "C:\\Saoke";
        private string strsql = "";
        private string Mau = "";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
            dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
            var firstDayOfMonth = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 1);
            var lastDay = new DateTime(dtpNgay.SelectedDate.Value.Year, dtpNgay.SelectedDate.Value.Month, 25);
            var sql = BienBll.NdCapbc.Trim() == "02"
                ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim())
                : "select PO_MA,PO_TEN from DMPOS order by PO_MA";
            var dtpos = cls.LoadDataText(sql);
            CboPos.ItemsSource = dtpos.DefaultView;
            CboPos.DisplayMemberPath = "PO_TEN";
            CboPos.SelectedValuePath = "PO_MA";

            cls.DongKetNoi();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string pos = CboPos.SelectedValue.ToString().Trim();
                string thang = dtpNgay.SelectedDate.Value.Month.ToString();
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                string cap = "";
                if (Opt1.IsChecked == true) cap = "1";else cap = "2";

                foreach (DataRow dr in dt.Rows)
                {
                    string strup = "update LUUKTNB01 set COT3='"+dr["COT3"]+ "',COT4='" + dr["COT4"] + "',COT5='" + dr["COT5"] + "',COT6='" + dr["COT6"] + "',COT7='" + dr["COT7"] + "',COT8='" + dr["COT8"] + "',COT9='" + dr["COT9"] + "',COT10='" + dr["COT10"] + "',COT11='" + dr["COT11"] + "',COT12='" + dr["COT12"] + "',COT13='" + dr["COT13"] + "',COT14='" + dr["COT14"] + "',COT15='" + dr["COT15"] + "',COT16='" + dr["COT16"] + "',COT17='" + dr["COT17"] + "',COT18='" + dr["COT18"] + "',GHICHU='" + dr["GHICHU"] + "' where POS='" + pos+"' and thang='"+thang+"' and nam='"+nam+"' and cap='"+cap+"' and KT_KHOA='"+dr["KT_KHOA"]+"'";
                    //MessageBox.Show(strup);
                    cls.UpdateDataText(strup);
                }
                MessageBox.Show("Lưu thành công !", "Mess", MessageBoxButton.OK, MessageBoxImage.Information);
                string strin = "select * from LUUKTNB01 where POS='" + pos + "' and thang='" + thang + "' and nam='" +
                               nam + "' and cap='" + cap + "'";
                MessageBox.Show(strin);
                var dtin =cls.LoadDataText(strin);
                Xtra_KTNB_06 rpt = new Xtra_KTNB_06();//xtra_KTNB_06 rpt = new xtra_KTNB_06();
                rpt.DataSource = dtin;
                rpt.DataMember = rpt.DataMember;
                rpt.ShowPreviewDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();

        }

        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                int thamso = 5;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = s.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                if (dtpNgay.SelectedDate != null) giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[2] = "@Cap";
                if (Opt1.IsChecked == true) giatri[2] = "1";
                else giatri[2] = "2";
                bien[3] = "@Mau";
                giatri[3] = "06";
                bien[4] = "@NG_NHAP";
                giatri[4] = BienBll.Ndma.Trim();
                dt = cls.LoadDataProcPara("usp_KTNB", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                    //dgvSource.ItemsSource = dt.DefaultView;
                    dgvData.ItemsSource = dt.DefaultView;
                else MessageBox.Show("Không có xã nào !", "Mess", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

        private void LblGetSua_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                cls.ClsConnect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

        private void TableView_ShowingEditor(object sender, DevExpress.Xpf.Grid.ShowingEditorEventArgs e)
        {
            if (e.Row == dgvData.GetFocusedRowCellValue("NHAP"))
            {
                e.Cancel = true;
            }
        }

        private void CboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GridControl_OnSelectionChanged(object sender, DevExpress.Xpf.Grid.GridSelectionChangedEventArgs e)
        {
        }
    }
}
