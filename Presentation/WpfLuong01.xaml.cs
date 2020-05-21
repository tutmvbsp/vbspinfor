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
namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLuong01.xaml
    /// </summary>
    public partial class WpfLuong01 : Window
    {
        public WpfLuong01()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtkt = new DataTable();
        DataTable dtnew = new DataTable();
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnIn_Click(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            string Nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
            string Pos = bll.Left(CboPos.SelectedValue.ToString(), 6);
            string sql = "select mapos,manv,ten,qlc,stlv,l1,a1,l2,a2,l3,a3,l4,a4,l5,a5,l6,a6,l7,a7,l8,a8,l9,a9,l10,a10,l11,a11,l12,a12,phongto" +
                         " from LuuHeSoLuong where nam = "+"'"+Nam+"'"+" and mapos= "+"'"+Pos+"' order by mapos,manv";
            dt = cls.LoadDataText(sql);
            dgvData.ItemsSource = dt.DefaultView;
           // rpt_HeSo rpt = new rpt_HeSo();
           // RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            dtpNgay.SelectedDate = DateTime.Parse("31/12/" + DateTime.Now.AddYears(-1).ToString("yyyy"));
            try
            {
                cls.ClsConnect();
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    //comboBox1.Items.Add(ds.Tables[0].Rows[i][0] + " " + ds.Tables[0].Rows[i][1] + " " + ds.Tables[0].Rows[i][2]);
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            dtnew = dt.GetChanges();
            if (dtnew == null)
            {
                MessageBox.Show("Chưa thay đổi thông số dữ liệu cập nhật ", "Thông báo", MessageBoxButton.OK,
                                MessageBoxImage.Stop);
            }
            else
            {
                dgvTarGet.ItemsSource = dtnew.DefaultView;
                try
                {
                    cls.ClsConnect();
                    int thamso = 28;
                    string[] bien = new string[thamso];
                    object[] giatri = new object[thamso];
                    bien[0] = "@MaPos";
                    giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
                    foreach (DataRow dr in dtnew.Rows)
                    {
                        bien[1] = "@manv";
                        giatri[1] = dr[1].ToString().Trim();
                        bien[2] = "@qlc";
                        giatri[2] = dr[3];
                        bien[3] = "@stlv";
                        giatri[3] = dr[4];
                        bien[4] = "@l1";
                        giatri[4] = dr[5];
                        bien[5] = "@a1";
                        giatri[5] = dr[6];
                        bien[6] = "@l2";
                        giatri[6] = dr[7];
                        bien[7] = "@a2";
                        giatri[7] = dr[8];
                        bien[8] = "@l3";
                        giatri[8] = dr[9];
                        bien[9] = "@a3";
                        giatri[9] = dr[10];
                        bien[10] = "@l4";
                        giatri[10] = dr[11];
                        bien[11] = "@a4";
                        giatri[11] = dr[12];
                        bien[12] = "@l5";
                        giatri[12] = dr[13];
                        bien[13] = "@a5";
                        giatri[13] = dr[14];
                        bien[14] = "@l6";
                        giatri[14] = dr[15];
                        bien[15] = "@a6";
                        giatri[15] = dr[16];
                        bien[16] = "@l7";
                        giatri[16] = dr[17];
                        bien[17] = "@a7";
                        giatri[17] = dr[18];
                        bien[18] = "@l8";
                        giatri[18] = dr[19];
                        bien[19] = "@a8";
                        giatri[19] = dr[20];
                        bien[20] = "@l9";
                        giatri[20] = dr[21];
                        bien[21] = "@a9";
                        giatri[21] = dr[22];
                        bien[22] = "@l10";
                        giatri[22] = dr[23];
                        bien[23] = "@a10";
                        giatri[23] = dr[24];
                        bien[24] = "@l11";
                        giatri[24] = dr[25];
                        bien[25] = "@a11";
                        giatri[25] = dr[26];
                        bien[26] = "@l12";
                        giatri[26] = dr[27];
                        bien[27] = "@a12";
                        giatri[27] = dr[28];
                        //MessageBox.Show(giatri[0] + "  " + giatri[1] + "  " + giatri[2] + "  " + giatri[3]);
                        cls.UpdateDataProcPara("usp_UpdateHeSoLuong", bien, giatri, thamso);
                        MessageBox.Show("Update OK", "Mess");
                    }

                    cls.DongKetNoi();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lổi : " + ex.Message);
                }

            }
        }

        private
            void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            string Nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
            string Pos = bll.Left(CboPos.SelectedValue.ToString(), 6);
            cls.ClsConnect();
            string sql = "select mapos,manv,ten,qlc,stlv,l1,a1,l2,a2,l3,a3,l4,a4,l5,a5,l6,a6,l7,a7,l8,a8,l9,a9,l10,a10,l11,a11,l12,a12,phongto" +
             " from LuuHeSoLuong where nam = " + "'" + Nam + "'" + " and mapos= " + "'" + Pos + "' order by mapos,manv";

            dtkt = cls.LoadDataText(sql);
            if (dtkt.Rows.Count > 0)
            {
                MessageBox.Show("Đã có dữ liệu ", "Thông báo");
                dt = dtkt;

            }
            else
            {
                MessageBox.Show("Chưa có dữ liệu ", "Thông báo");
                InsertToTable();
                dt = cls.LoadDataText(sql);
            }
            dgvData.ItemsSource = dt.DefaultView;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            string Nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
            string Pos = bll.Left(CboPos.SelectedValue.ToString(), 6);
            cls.ClsConnect();
            //MessageBox.Show(Nam + "   " + Pos);
            string sql = "select mapos,manv,ten,qlc,stlv,l1,a1,l2,a2,l3,a3,l4,a4,l5,a5,l6,a6,l7,a7,l8,a8,l9,a9,l10,a10,l11,a11,l12,a12,phongto" +
                         " from HeSoLuong where nam = " + "'" + Nam + "'" + " and mapos= " + "'" + Pos + "' order by mapos, manv";
            MessageBox.Show(sql);
            dt = cls.LoadDataText(sql);
            dgvData.ItemsSource = dt.DefaultView;
            //rpt_HeSo rpt = new rpt_HeSo();
            //RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
        }


        private void InsertToTable()
        {
            cls.ClsConnect();
            int thamso = 2;
            string[] bien = new string[thamso];
            object[] giatri = new object[thamso];
            bien[0] = "@MaPos";
            giatri[0] = bll.Left(CboPos.SelectedValue.ToString().Trim(), 6);
            bien[1] = "@Nam";
            giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy");
            //string sql = "insert LuuHeSoLuong select "+Nam+" as Nam, * from HeSoLuong where nam = " + "'" + Nam + "'" + " and mapos= " +"'" + Pos+"'";
            cls.UpdateDataProcPara("usp_InsertHeSoLuong", bien, giatri, thamso);
            MessageBox.Show("Insert OK", "Mess");
        }
    }
}
