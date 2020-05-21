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
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfTTKU : Window
    {
        public WpfTTKU()
        {
            InitializeComponent();
        }
        ClsServer cls = new ClsServer();
        ToolBll bll  = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Now.AddDays(-1);
            try
            {
                cls.ClsConnect();
                var dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());
                //txtSoku.Text = "6000003000055524"; //"6600000702319965";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }
            cls.DongKetNoi();

        }

   

   

        private void lblXem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
           // try
           // {
                cls.ClsConnect();
                DataTable dt = new DataTable();
                int thamso = 2;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@Ngay";
                bien[1] = "@Soku";
                if (dtpNgay.SelectedDate == null)
                {
                    MessageBox.Show("Chưa chọn ngày", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                else
                {
                    giatri[0] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                  giatri[1] = txtSoku.Text.Trim();
                  dt = cls.LoadDataProcPara("usp_TTKU", bien, giatri, thamso);
                if (dt.Rows.Count > 0)
                {
                    lblKU.Content = "Đang xem thông tin KU  :  " + dt.Rows[0]["KU_SOKU"];
                    txtMakh.Text = dt.Rows[0]["KH_MAKH"].ToString();
                    txtCmt.Text = dt.Rows[0]["KH_CMT"].ToString();
                    lblTenkh.Content = "Tên Hộ Vay  :  "+dt.Rows[0]["KH_TENKH"];
                    lblTenVC.Content = "Tên Thừa kế  :  " +dt.Rows[0]["KH_TENVC"];
                    lblDiaChi.Content = "Địa chỉ  :  " + dt.Rows[0]["DIACHI"];
                    txtMsp.Text = dt.Rows[0]["KU_SPRD_CD"].ToString();
                    lblMsp.Content = dt.Rows[0]["SP_TEN"];
                    lblNV.Content = "Nguồn vốn : "+dt.Rows[0]["KU_NGUONVON"];
                    txtDvut.Text = dt.Rows[0]["TO_DVUT"].ToString();
                    lblDvut.Content = dt.Rows[0]["TENDV"];
                    txtCapqlv.Text = dt.Rows[0]["CAPQLV"].ToString();
                    lblCapqlv.Content = dt.Rows[0]["TEN_CAPQLV"];
                    txtMaTo.Text = dt.Rows[0]["TO_MATO"].ToString();
                    lblTenTT.Content = dt.Rows[0]["TO_TENTT"];
                    lblHTVAY.Content = dt.Rows[0]["HT_VAY"];
                    txtNgVay.Text = bll.Left(dt.Rows[0]["NGAYVAY"].ToString(),10);
                    txtNgDenHan.Text = bll.Left(dt.Rows[0]["NGAYDHAN_KU"].ToString(),10);
                    txtNgDenHanGH.Text = bll.Left(dt.Rows[0]["NGAYDHAN_GHAN"].ToString(),10);
                    txtNgDenHanGDX.Text = bll.Left(dt.Rows[0]["NGAYDHAN_GDXA"].ToString(),10);
                    txtGNDT.Text = bll.Left(dt.Rows[0]["NGAYGNDT"].ToString(),10);
                    txtNgGNCC.Text = bll.Left(dt.Rows[0]["NGAYGNCC"].ToString(),10);
                    txtNgTraGoc.Text = bll.Left(dt.Rows[0]["KU_NGAY_TGOC"].ToString(),10);
                    txtNgTraLai.Text = bll.Left(dt.Rows[0]["KU_NGAY_TLAI"].ToString(),10);
                    lblTenNDT.Content = "Nhà đầu tư : "+dt.Rows[0]["DT_TENDT"];
                    txtDtth.Text = dt.Rows[0]["DTTH"].ToString();
                    lblDtth.Content = dt.Rows[0]["TEN_DTTH"];
                    txtPNKT.Text = dt.Rows[0]["PNKT"].ToString();
                    lblPNKT.Content = dt.Rows[0]["TEN_PNKT"];
                    lblLoaiNo.Content = "Loại nợ : " + dt.Rows[0]["KU_TTHAINO"];
                    lblTrangThai.Content = "Tình Trạng : " + dt.Rows[0]["KU_TTMONVAY"];
                    lblLaiSuat.Content = "Lãi suất : " + dt.Rows[0]["KU_LSUAT"];
                    lblMaSv.Content = dt.Rows[0]["SV_MASV"];
                    lblTenSv.Content = dt.Rows[0]["SV_TENSV"];
                    lblCmtSv.Content = dt.Rows[0]["SV_CMT_SV"];
                    txtNhapHoc.Text = dt.Rows[0]["SV_NGNHAPHOC"].ToString();
                    txtRaTruong.Text = dt.Rows[0]["SV_NGRTRUONG"].ToString();
                    lblTruong.Content = dt.Rows[0]["TEN_TRUONG"];
                    txtLoaiDT.Text = dt.Rows[0]["SV_LOAIHDT"].ToString();
                    lblLoaiDT.Content = dt.Rows[0]["TEN_LOAIHDT"];
                    txtHeDT.Text = dt.Rows[0]["SV_HEDTAO"].ToString();
                    lblHeDT.Content = dt.Rows[0]["TEN_HEDT"];
                    txtHocPhi.Text = dt.Rows[0]["SV_DTHOCPHI"].ToString();
                    lblHocPhi.Content = dt.Rows[0]["DT_HOCPHI"];
                   // txtMucVay.Text = (string)dt.Rows[0]["KU_MUCVAY"];
                    txtMucVay.Text = dt.Rows[0]["KU_MUCVAY"].ToString();
                    txtGngan.Text = dt.Rows[0]["KU_GNGAN"].ToString();
                    txtDuNo.Text = dt.Rows[0]["DUNO"].ToString();
                    txtThuNo.Text = dt.Rows[0]["THUNO"].ToString();
                    txtThuLai.Text = dt.Rows[0]["THULAI"].ToString();
                    txtLaiTon.Text = dt.Rows[0]["LAITON"].ToString();
                    txtChuyenQH.Text = dt.Rows[0]["KU_CHUYENQH"].ToString();
                    txtGiaHan.Text = dt.Rows[0]["KU_GHANNO"].ToString();
                    txtRPA.Text = dt.Rows[0]["KU_TON_RPA"].ToString();
                    txtGui.Text = dt.Rows[0]["CS_A_GUITK"].ToString();
                    txtRut.Text = dt.Rows[0]["CS_A_RUTTK"].ToString();
                    txtSoDu.Text = dt.Rows[0]["CS_SODU_TK"].ToString();
                    txtTK.Text = dt.Rows[0]["CS_SO_TK2"].ToString();
                    txtTKTH.Text = dt.Rows[0]["TKTH"].ToString();
                    txtTKQH.Text = dt.Rows[0]["TKQH"].ToString();
                    txtTKNK.Text = dt.Rows[0]["TKNK"].ToString();
                    txtTKTL.Text = dt.Rows[0]["TKTL"].ToString();
                    lblTKTH.Content = dt.Rows[0]["TEN_TKTH"];
                    lblTKQH.Content = dt.Rows[0]["TEN_TKQH"];
                    lblTKNK.Content = dt.Rows[0]["TEN_TKNK"];
                    lblTKTL.Content = dt.Rows[0]["TEN_TKTL"];

                    string sql = "select ROW_NUMBER() over (partition by KH_SOKU order by (select 1)) as KH_LANTNO,KH_NGDHAN,KH_GOCDHAN,KH_LAITONPB from khtn where KH_SOKU='" + giatri[1] + "' order by KH_NGDHAN";
                    var dtkhtn = cls.LoadDataText(sql);
                    dgvData.ItemsSource = dtkhtn.DefaultView;
                    string strgh = "select CHEQ_HIST,GH_TSLAN,GH_SOTHG from HSGH_HISTORY where SOKU='" + giatri[1] + "'";
                    var dtgh = cls.LoadDataText(strgh);
                    if (dtgh.Rows.Count>0) txtGH.Text = dtgh.Rows[0]["CHEQ_HIST"] + "  /  " + dtgh.Rows[0]["GH_SOTHG"];
                    string strlv = "select NGAYHL NGAY,SOTHANG from HSLV_HISTORY where SOKU='" + giatri[1] + "'";
                    var dtlv = cls.LoadDataText(strlv);
                    if (dtlv.Rows.Count>0) txtLV.Text = dtlv.Rows[0]["NGAY"] + "  /  " + dtlv.Rows[0]["SOTHANG"];
                }
                else
                {
                    MessageBox.Show("Không tìm thấy", "Thông báo",MessageBoxButton.OK,MessageBoxImage.Warning);
                }
                cls.DongKetNoi();
            /*
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            */
            
        }

        private void LblTimkiem_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
            var f = new WpfTimKiem();
            f.ShowDialog();
        }

        private void LblThoat_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
