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
    public partial class WpfSkeKu : Window
    {
        public WpfSkeKu()
        {
            InitializeComponent();
        }
        //ClsConnectLocal cls = new ClsConnectLocal();
       // private FileStream _fw;
        ClsServer cls = new ClsServer();
        ClsOracle ora = new ClsOracle();
        ToolBll str = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        string Thumuc = "C:\\Saoke";
        private string Mau = "";
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

                if (OptAll.IsChecked == true)
                {
                    Mau = "1";
                    FileName = Thumuc + "\\" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_SaokeKuAll.csv";
                }
                else if (OptChtr.IsChecked == true)
                {
                    Mau = "2";
                    FileName = Thumuc + "\\" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_Chtr" + str.Left(CboChon.SelectedValue.ToString().Trim(), 2) + ".csv";
                }
                else if (OptDvut.IsChecked == true)
                {
                    Mau = "3";
                    FileName = Thumuc + "\\" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_DVUT_" + str.Left(CboChon.SelectedValue.ToString().Trim(), 2) + ".csv";
                }
                else if (OptTT.IsChecked == true)
                {
                    Mau = "4";
                    FileName = Thumuc + "\\" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_VayTrucTiep.csv";
                }
                else if (OptXa.IsChecked == true)
                {
                    Mau = "5";
                    FileName = Thumuc + "\\" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + "_XA_" + str.Left(CboChon.SelectedValue.ToString().Trim(), 6) + ".csv";
                }
                cls.ClsConnect();
                int thamso = 5;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = str.Left(cboPos.SelectedValue.ToString().Trim(), 6);
                bien[1] = "@Ngay";
                giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[2] = "@MaXa";
                giatri[2] = str.Left(CboChon.SelectedValue.ToString().Trim(), 6);
                bien[3] = "@Bien";
                giatri[3] = str.Left(CboChon.SelectedValue.ToString().Trim(), 2);
                bien[4] = "@Mau";
                giatri[4] = Mau;
                //if (OptXa.IsChecked == true)
                //{
                //  string  strsql = "select substr(ku_madp,1,6) maxa, (select ten from dmxa where ma=substr(ku_madp,1,6)) tenxa,ku_mato,to_tentt"
                //          +",kh_makh,kh_tenkh,kh_diachi,ku_nguonvon,concat(chr(39),ku_soku) soku,to_char(ku_ngayvay, 'dd/MM/yyyy') ng_vay,to_char(ku_ngaydhan_1, 'dd/MM/yyyy') ng_dhan "
                //          +" ,to_char(ku_ngaydhan_2, 'dd/MM/yyyy') ng_dhan_ghan,to_char(ku_ngaydhan_3, 'dd/MM/yyyy') ng_dhan_gdx"
                //          + ",to_char(ku_ngayhhkh, 'dd/MM/yyyy') ng_hethankhoanh"
                //          + ",ku_maqd,(select giatri from dmkhac where khoa_1 = '07' and ku_maqd = khoa_2) ten_chtr"
                //          + ",ku_dnothan,ku_dnoqhan,ku_dnokhoanh,ku_laitonthan + ku_laitonqhan laiton,to_dvut"
                //          + ",(select giatri from dmkhac where khoa_1 = '17' and to_dvut = khoa_2) ten_dvut"
                //          +",ku_capqlv,(select giatri from dmkhac where khoa_1 = '19' and ku_capqlv = khoa_2) ten_capqlv"
                //          + ",ku_mandt,dt_tendt,sv_tensv,sv_ngnhaphoc,sv_ngrtruong from ("
                //          + " select * from hscv_daily left join hsto on ku_mato = to_mato"
                //          +" where ku_ngaybc ='"+dtpNgay.SelectedDate.Value.ToString("dd/MMM/yyyy")+"' and ku_ttmonvay <> 'CLOSE' and ku_dnothan+ku_dnoqhan + ku_dnokhoanh > 0 and substr(ku_madp,1, 6)='"+ str.Left(CboChon.SelectedValue.ToString().Trim(), 6) + "'"
                //          +") left join hsdt on ku_mandt = dt_madt left join hskh on ku_makh = kh_makh left join hssv on ku_soku = sv_soku";
                //    ora.ClsConnect();
                //    dt = ora.LoadDataText(strsql);
                //    ora.DongKetNoi();
                //} else
                dt = cls.LoadDataProcPara("usp_SkeKuAll", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    //if (OptAll.IsChecked == true)
                    //{
                    FileStream fs = new FileStream(FileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
                    str.ToCSV(dt, sw, true);
                    //}
                    //else
                    //{

                    //    str.ExportToExcel(dt, FileName);
                    //}
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                   str.OpenExcel(FileName);
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
            OptAll.IsChecked = true;
            //CboChon.IsEnabled = false;
            lbl.Content = "";
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
                cboPos.SelectedIndex = 0;
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

            }
            catch(Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message,"Mess");
            }
            //CboChon.Items.Add("003000 | Tất cả");
            cls.DongKetNoi();
        }

   

  
        private void OptAll_Checked(object sender, RoutedEventArgs e)
        {
            CboChon.IsEnabled = false;
            CboChon.Items.Clear();
            CboChon.Items.Add("003000 | Tất cả");
            CboChon.SelectedIndex = 0;
            lbl.Content = "Tất cả";
  

        }

        private void OptChtr_Checked(object sender, RoutedEventArgs e)
        {
            lbl.Content = "Chương Trinh";
            CboChon.IsEnabled = true;
            //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
            CboChon.Items.Clear();
            cls.ClsConnect();
            DataTable dtchon = new DataTable();
            string sql = "select MAQD,TEN from CHTRINH order by MAQD";
            dtchon = cls.LoadDataText(sql);
            for (int i = 0; i < dtchon.Rows.Count; i++)
            {
                CboChon.Items.Add(dtchon.Rows[i][0] + " | " + dtchon.Rows[i][1]);
            }
            CboChon.SelectedIndex = 0;
        }

        private void OptDvut_Checked(object sender, RoutedEventArgs e)
        {
            lbl.Content = "Đơn vị ủy thác";
            CboChon.IsEnabled = true;
            //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
            CboChon.Items.Clear();
            cls.ClsConnect();
            DataTable dtchon = new DataTable();
            string sql = "select DVUT,TENDV from DVUT where DVUT<>'00' order by DVUT";
            dtchon = cls.LoadDataText(sql);
            for (int i = 0; i < dtchon.Rows.Count; i++)
            {
                CboChon.Items.Add(dtchon.Rows[i][0] + " | " + dtchon.Rows[i][1]);
            }
            CboChon.SelectedIndex = 0;
        }

        private void OptTT_Checked(object sender, RoutedEventArgs e)
        {
            CboChon.IsEnabled = false;
            lbl.Content = "Cho vay trực tiếp";

        }


        private void OptXa_OnChecked(object sender, RoutedEventArgs e)
        {
            lbl.Content = "Xã :";
            CboChon.IsEnabled = true;
            //MessageBox.Show(str.Left(cboPos.SelectedValue.ToString().Trim(),6));
            CboChon.Items.Clear();
            cls.ClsConnect();
            DataTable dtchon = new DataTable();
            string sql = "select MA,TEN from DMXA where right(MA,2)<>'00' and PGD_QL='" + str.Left(cboPos.SelectedValue.ToString().Trim(), 6) + "' order by MA";
            dtchon = cls.LoadDataText(sql);
            for (int i = 0; i < dtchon.Rows.Count; i++)
            {
                CboChon.Items.Add(dtchon.Rows[i][0] + " | " + dtchon.Rows[i][1]);
            }
            CboChon.SelectedIndex = 0;      
        }
    }
}
