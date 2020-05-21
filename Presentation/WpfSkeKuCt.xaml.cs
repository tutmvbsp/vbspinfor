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
    public partial class WpfSkeKuCt : Window
    {
        public WpfSkeKuCt()
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
                string sqlstr = "";
                if (str.Right(str.Left(cboPos.SelectedValue.ToString().Trim(), 6), 2) == "00")
                {
                    if (Ration1.IsChecked == true)
                        sqlstr = "select a.TENHUYEN,a.TENXA,a.KU_MATO,a.TO_TENTT,a.KH_TENKH,CHAR(39)+a.KU_SOKU SOKU" +
                                 ",left(a.KU_NGAYGNCC,10) NGAYGN,LEFT(a.KU_NGAYVAY,10) NGAYVAY,LEFT(a.KU_NGAYDHAN_2,10) NGAYDHAN,LEFT(a.KU_NGAYDHAN_3,10) NGAYDHANHDX"
                                 +",a.KU_DNOTHAN,a.KU_DNOQHAN,a.KU_DNOKHOANH,a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH DUNO from LDBF a "
                                 + " where a.NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +
                                 "' and  a.KU_CHTRINH='" + str.Left(CboChon.SelectedValue.ToString().Trim(), 2) +
                                 "' and a.KU_TTMONVAY<>'CLOSE'"
                                 + " order by a.MAXA,a.KU_MATO";
                    else
                        sqlstr = "select a.TENHUYEN,a.TENXA,a.KU_MATO,a.TO_TENTT,a.KH_TENKH,CHAR(39)+a.KU_SOKU SOKU" +
                                 ",left(a.KU_NGAYGNCC,10) NGAYGN,LEFT(a.KU_NGAYVAY,10) NGAYVAY,LEFT(a.KU_NGAYDHAN_2,10) NGAYDHAN,LEFT(a.KU_NGAYDHAN_3,10) NGAYDHANHDX"
                                 + ",a.KU_DNOTHAN,a.KU_DNOQHAN,a.KU_DNOKHOANH,a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH DUNO from LDBF a "
                                 + " where a.NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +
                                 "' and  a.KU_CHTRINH='" + str.Left(CboChon.SelectedValue.ToString().Trim(), 2) +
                                 "' and a.KU_TTMONVAY<>'CLOSE'"
                                 + " and convert(date,LEFT(a.KU_NGAYGNCC,10),103)>='" +
                                 dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +
                                 "' and convert(date,LEFT(a.KU_NGAYGNCC,10),103)<='" +
                                 dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'"
                                 + " order by a.MAXA,a.KU_MATO";
                }
                else
                {
                    if (Ration1.IsChecked == true)
                        sqlstr = "select a.TENHUYEN,a.TENXA,a.KU_MATO,a.TO_TENTT,a.KH_TENKH,CHAR(39)+a.KU_SOKU SOKU" +
                                 ",left(a.KU_NGAYGNCC,10) NGAYGN,LEFT(a.KU_NGAYVAY,10) NGAYVAY,LEFT(a.KU_NGAYDHAN_2,10) NGAYDHAN,LEFT(a.KU_NGAYDHAN_3,10) NGAYDHANHDX"
                                 + ",a.KU_DNOTHAN,a.KU_DNOQHAN,a.KU_DNOKHOANH,a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH DUNO from LDBF a "
                                 + " where a.NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +
                                 "' and a.KH_MAPGD='" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) +
                                 "' and  a.KU_CHTRINH='" + str.Left(CboChon.SelectedValue.ToString().Trim(), 2) +
                                 "' and a.KU_TTMONVAY<>'CLOSE'"
                                 + " order by a.MAXA,a.KU_MATO";
                    else
                        sqlstr = "select a.TENHUYEN,a.TENXA,a.KU_MATO,a.TO_TENTT,a.KH_TENKH,CHAR(39)+a.KU_SOKU SOKU" +
                                 ",left(a.KU_NGAYGNCC,10) NGAYGN,LEFT(a.KU_NGAYVAY,10) NGAYVAY,LEFT(a.KU_NGAYDHAN_2,10) NGAYDHAN,LEFT(a.KU_NGAYDHAN_3,10) NGAYDHANHDX"
                                 + ",a.KU_DNOTHAN,a.KU_DNOQHAN,a.KU_DNOKHOANH,a.KU_DNOTHAN+a.KU_DNOQHAN+a.KU_DNOKHOANH DUNO from LDBF a "
                                 + " where a.NGAY='" + dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +
                                 "' and a.KH_MAPGD='" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) +
                                 "' and  a.KU_CHTRINH='" + str.Left(CboChon.SelectedValue.ToString().Trim(), 2) +
                                 "' and a.KU_TTMONVAY<>'CLOSE'"
                                 + " and convert(date,LEFT(a.KU_NGAYGNCC,10),103)>='" +
                                 dtpTuNgay.SelectedDate.Value.ToString("yyyy-MM-dd") +
                                 "' and convert(date,LEFT(a.KU_NGAYGNCC,10),103)<='" +
                                 dtpDenNgay.SelectedDate.Value.ToString("yyyy-MM-dd") + "'"
                                 + " order by a.MAXA,a.KU_MATO";

                }
                //MessageBox.Show(sqlstr);
                
                cls.ClsConnect();
                dt = cls.LoadDataText(sqlstr);
                if (dt.Rows.Count > 0)
                {
                    
                    FileName = Thumuc + "\\" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_ChTr_"+str.Left(CboChon.SelectedValue.ToString().Trim(),2)+".csv";
                    FileStream fs = new FileStream(FileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                    str.ToCSV(dt, sw, true);
                    MessageBox.Show("Export to Excel : " + FileName, "Thông báo");
                }
                else
                {
                    MessageBox.Show("Chưa có số liệu", "Thông báo");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
                cboPos.SelectedIndex = 0;
                DataTable dtchon = new DataTable();
                string sql1 = "select CHTRINH,TEN_CT from DM_CHTRINH where CHTRINH<>'00' order by CHTRINH";
                dtchon = cls.LoadDataText(sql1);
                for (int i = 0; i < dtchon.Rows.Count; i++)
                {
                    CboChon.Items.Add(dtchon.Rows[i][0] + " | " + dtchon.Rows[i][1]);
                }
                CboChon.SelectedIndex = 0;
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                //string ng = "01/01/"+str.Right(dtng.Rows[0]["NGMAX"].ToString(),4);
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                dtpTuNgay.SelectedDate = DateTime.Parse("01/01/" + dtpNgay.SelectedDate.Value.ToString("yyyy"));
                dtpDenNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

            }
            catch(Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message,"Mess");
            }
            cls.DongKetNoi();
        }

      

        private void Ration1_OnClick(object sender, RoutedEventArgs e)
        {
            dtpTuNgay.IsEnabled = false;
            dtpDenNgay.IsEnabled = false;
        }

        private void Ration2_OnClick(object sender, RoutedEventArgs e)
        {
            dtpTuNgay.IsEnabled = true;
            dtpDenNgay.IsEnabled = true;
        }
    }
}
