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
using System.IO;
using BLL;
using DAL;
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfSkeTo.xaml
    /// </summary>
    public partial class WpfPLN : Window
    {
        public WpfPLN()
        {
            InitializeComponent();
        }
        //ClsConnectLocal cls = new ClsConnectLocal();
       // private FileStream _fw;
        ClsServer cls = new ClsServer();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\Saoke";
        //private string Mau = "";
        private string FileName = "";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            str.TaoThuMuc(Thumuc);
            try
            {
                cls.ClsConnect();
                string strsql = "select a.PLN_MAPGD,(select PO_TEN from dmpos where po_ma=a.pln_mapgd) TENPOS"
                             + " , left(a.PLN_MADP, 6) MAXA,(select ten from dmxa where ma = left(a.PLN_MADP, 6)) TENXA "
                             + " ,a.PLN_MATO,a.PLN_TENTT,a.PLN_MAKH,a.PLN_TENKH,CHAR(39) + a.PLN_SOKU SOKU "
                             + " ,a.PLN_DNOTHAN,a.PLN_DNOQHAN,a.PLN_DNOKHOANH,a.PLN_K_KNTN_SODU from PLN_KNTN_CL a "
                             + " where a.PLN_NGAYBC = '2018-06-30' and a.PLN_TT_MONVAY <> 'CLOSE' and a.PLN_TRANGTHAI = 'S' and a.PLN_K_KNTN_SODU > 0 "
                             + " and a.PLN_K_KNTN_SD" + str.Left(CboNgNhan.SelectedValue.ToString(), 2) + ">0 order by a.PLN_MADP,a.PLN_MATO,a.PLN_MAKH";
                dt = cls.LoadDataText(strsql);
                FileName = Thumuc + "\\" + str.Left(cboPos.SelectedValue.ToString(),6) + "_"+CboNgNhan.SelectedValue.ToString().Substring(4, CboNgNhan.SelectedValue.ToString().Trim().Length-4) +"_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                MessageBox.Show(FileName);
                str.ExportToExcel(dt, FileName);
                MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                str.OpenExcel(FileName);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    cboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                cboPos.SelectedIndex = 5;
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

                string sqlnn = "select * from PLN_NGNHAN order by MA";
                var dtnn = cls.LoadDataText(sqlnn);
                for (int i = 0; i < dtnn.Rows.Count; i++)
                {
                    CboNgNhan.Items.Add(dtnn.Rows[i][0] + " | " + dtnn.Rows[i][1]);
                }
                CboNgNhan.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message,"Mess");
            }
            //CboChon.Items.Add("003000 | Tất cả");
            cls.DongKetNoi();
        }
    
    }
}
