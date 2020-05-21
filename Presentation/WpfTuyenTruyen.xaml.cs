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
    public partial class WpfTuyenTruyen : Window
    {
        public WpfTuyenTruyen()
        {
            InitializeComponent();
        }
        readonly ClsServer _cls = new ClsServer();
        ToolBll bll = new ToolBll();
        ServerInfor srv = new ServerInfor();
        DataTable _dt = new DataTable();
        DataTable dtload = new DataTable();
        string Thumuc = "C:\\KT740";
        private string FileName = "";
        private void OK_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                string quy = ((int.Parse(dtpNgay.SelectedDate.Value.ToString("MM"))-1)/3+1).ToString();
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string ngay = dtpNgay.SelectedDate.Value.ToString("ddMMyyyy");
                string pos = RadCboPos.SelectedValue.ToString();
                string theloai = RadCboTheLoai.SelectedValue.ToString();
                string nguon = RadCboNguon.SelectedValue.ToString();
                string tluong = RadCboThoiLuong.SelectedValue.ToString();
                string cap = RadCboCap.SelectedValue.ToString();
                string phong = RadCboPhong.SelectedValue.ToString().Trim();
                string loaitin = RadCboLoaiTin.SelectedValue.ToString();
                string matin = ngay.Trim() + pos.Trim() + phong.Trim() + theloai.Trim() + nguon.Trim();
                string sele = "select * from TT_CAPNHAT where MATIN='" +matin+ "'";
                var dtchk = _cls.LoadDataText(sele);
                if (dtchk.Rows.Count == 0)
                {
                    string str = "insert into TT_CAPNHAT values('" +matin+ "','" + ng + "','" +
                                 pos + "','" + nam + "','" + quy + "','" + theloai + "','',N'" + txtTieuDe.Text + "','" +
                                 txtLink.Text + "','" + nguon + "','',N'" + BienBll.NdTen + "',N'" + txtGhiChu.Text +"','"+tluong+"','','"+cap+"','','"+phong+"','','"+loaitin+"','')";
                    _cls.UpdateDataText(str);
                    MessageBox.Show("Lưu thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ReLoad();
                } else MessageBox.Show("Mẫu tin "+ matin + "đã tồn tại !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                /*
                string strin = "select a.NGAY N'Ngày',a.POS,b.PO_TEN N'Tên POS', c.TEN N'Thể loại', d.TEN N'Nguồn',a.TIEUDE N'Tiêu đề',a.LINK "
                     +" from TT_CAPNHAT a,DMPOS b, TT_THELOAI c,TT_NGUONTIN d where a.POS = b.PO_MA and a.MA_THELOAI = c.MA and a.MA_NGUON = d.MA"
                     +" and a.POS = '"+pos+"'order by a.NGAY";
                var dtin = _cls.LoadDataText(strin);
                string FileName = Thumuc + "\\" + pos + "_Tổng hợp thông tin tuyên truyền_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                bll.ExportToExcel(dtin, FileName);
                */
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
   


        private void WpfTdChamDiem_OnLoaded(object sender, RoutedEventArgs e)
        {
            //dtpNgay.SelectedDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM") + "-" + DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month).ToString());
            dtpNgay.SelectedDate=DateTime.Now;
            try
            {

                _cls.ClsConnect();
                //var dtpos = _cls.LoadDataText("select PO_MA,PO_TEN from DMPOS where right(PO_MA,2)<>'00' order by PO_MA");
                var sql = BienBll.NdCapbc.Trim() == "02" ? string.Format("select PO_MA,PO_TEN from DMPOS where PO_MA='{0}'", BienBll.NdMadv.Trim()) : "select PO_MA,PO_TEN from DMPOS order by PO_MA";
                var dtpos = _cls.LoadDataText(sql);
                RadCboPos.ItemsSource = dtpos.DefaultView;
                RadCboPos.DisplayMemberPath = "PO_TEN";
                RadCboPos.SelectedValuePath = "PO_MA";
               // RadCboPos.SelectedIndex = 0;
                var dtdot = _cls.LoadDataText("select * from TT_THELOAI order by MA");
                RadCboTheLoai.ItemsSource = dtdot.DefaultView;
                RadCboTheLoai.DisplayMemberPath = "TEN";
                RadCboTheLoai.SelectedValuePath = "MA";
               // RadCboTheLoai.SelectedIndex = 0;
                var dtchde =_cls.LoadDataText("select * from TT_NGUONTIN order by MA");
                RadCboNguon.ItemsSource = dtchde.DefaultView;
                RadCboNguon.DisplayMemberPath = "TEN";
                RadCboNguon.SelectedValuePath = "MA";
                // RadCboNguon.SelectedIndex = 0;
                var dttl = _cls.LoadDataText("select * from TT_THOILUONG order by MA");
                RadCboThoiLuong.ItemsSource = dttl.DefaultView;
                RadCboThoiLuong.DisplayMemberPath = "TEN";
                RadCboThoiLuong.SelectedValuePath = "MA";
                // RadCboCap.SelectedIndex = 0;
                var dtcap = _cls.LoadDataText("select * from TT_CAP  order by MA");
                RadCboCap.ItemsSource = dtcap.DefaultView;
                RadCboCap.DisplayMemberPath = "TEN";
                RadCboCap.SelectedValuePath = "MA";

                var dtltin = _cls.LoadDataText("select * from TT_LOAITIN  order by MA");
                RadCboLoaiTin.ItemsSource = dtltin.DefaultView;
                RadCboLoaiTin.DisplayMemberPath = "TEN";
                RadCboLoaiTin.SelectedValuePath = "MA";

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
        private void BtnPrint_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                var dt = _cls.LoadDataText("select a.NAM,a.POS,b.PO_TEN, c.TEN TENNGUON, d.TEN NGUON,count(a.TIEUDE) DEM "
                      +" from TT_CAPNHAT a, DMPOS b, TT_THELOAI c, TT_NGUONTIN d where a.POS = b.PO_MA and a.MA_THELOAI = c.MA and a.MA_NGUON = d.MA "
                      +" and a.NAM = '"+ dtpNgay.SelectedDate.Value.ToString("yyyy") + "' and right(a.POS, 2) <> '00' group by a.NAM, a.POS, b.PO_TEN, c.TEN, d.TEN order by a.POS, c.TEN, d.TEN");
                if (dt.Rows.Count > 0)
                {
                    rpt_TuyenTruyen rpt = new rpt_TuyenTruyen();
                    RPUtility.ShowRp(rpt, dt, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(),
                        srv.DbPassSerVer());
                } else MessageBox.Show("Không có tin nào ! : ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : "+ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }
        private void BtnBaoCao_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                int thamso = 3;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                bien[0] = "@MaPos";
                giatri[0] = RadCboPos.SelectedValue;
                bien[1] = "@Ngay";giatri[1] = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                bien[2] = "@TH";
                if (RadCboPos.SelectedValue.ToString()=="003000")
                    giatri[2] = "1";
                else giatri[2] = "0";
                var dtin = _cls.LoadDataProcPara("usp_TT_BaoCao", bien, giatri, thamso);
                if (dtin.Rows.Count > 0)
                {
                    rpt_TuyenTruyen01 rpt = new rpt_TuyenTruyen01();RPUtility.ShowRp(rpt, dtin, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
                    FileName = Thumuc + "\\" + giatri[0] + "_Tuyen Truyen_" + dtpNgay.SelectedDate.Value.ToString("ddMMyyyy") + ".csv";
                    bll.ExportToExcel(dtin, FileName);
                    MessageBox.Show("Copy Excel to : " + FileName, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    bll.OpenExcel(FileName);

                }
                else MessageBox.Show("Không có tin nào ! : ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }
        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string quy = ((int.Parse(dtpNgay.SelectedDate.Value.ToString("MM")) - 1) / 3 + 1).ToString();
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                string ngay = dtpNgay.SelectedDate.Value.ToString("ddMMyyyy");
                string pos = RadCboPos.SelectedValue.ToString();
                string phong = RadCboPhong.SelectedValue.ToString().Trim();
                string theloai = RadCboTheLoai.SelectedValue.ToString();
                string nguon = RadCboNguon.SelectedValue.ToString();
                string matin = ngay + pos + phong + theloai + nguon;
                string tluong = RadCboThoiLuong.SelectedValue.ToString();
                string cap = RadCboCap.SelectedValue.ToString();
                _cls.ClsConnect();
                string strup="update TT_CAPNHAT set MA_THELOAI='"+theloai+"', TIEUDE=N'"+txtTieuDe.Text+"', LINK='"+txtLink.Text+"', MA_NGUON='"+nguon+"', GHICHU=N'"+txtGhiChu.Text+"',MA_CAP='"+cap+"',MA_PHONG='"+phong+"',MA_THOILUONG='"+tluong+"' where MATIN='"+txtMatin.Text+"'";
                _cls.UpdateDataText(strup);
                //MessageBox.Show(strup);
                MessageBox.Show("Đã sữa : " + matin, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                ReLoad();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                try
                {
                    if (_dt.Rows.Count > 0)
                    {
                        DataRowView dr = (DataRowView)dgvTarGet.SelectedItems[0];
                        string matin = dr["MATIN"].ToString();
                        string str = "delete from TT_CAPNHAT where MATIN='" +matin + "'";
                        _cls.UpdateDataText(str);
                        MessageBox.Show("Đã xóa mã tin : " + matin, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        ReLoad();

                    }
                    else
                    {
                        MessageBox.Show("Không có dòng nào " + txtTieuDe.Text, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        private void dgvTarGet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (_dt.Rows.Count > 0)
                {
                    DataRowView dr = (DataRowView)dgvTarGet.SelectedItems[0];
                    txtTieuDe.Text = dr["TIEUDE"].ToString();
                    txtGhiChu.Text = dr["GHICHU"].ToString();
                    txtLink.Text = dr["LINK"].ToString();
                    txtMatin.Text = dr["MATIN"].ToString();
                }
                else
                {
                    MessageBox.Show("Không có dòng nào "+txtTieuDe.Text, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
            catch (Exception ex)
            {
                 MessageBox.Show("Error \n" + ex.Message, "Mess", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ReLoad()
        {
            string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
            string pos = RadCboPos.SelectedValue.ToString();
            string phong = RadCboPhong.SelectedValue.ToString();
            dgvTarGet.ItemsSource = null;
            dtload.Clear();
            dtload = _cls.LoadDataText("select a.MATIN,a.NGAY,a.POS,a.NAM,a.QUY,a.MA_CAP,a.MA_PHONG,f.TEN TEN_PHONG,a.MA_LOAITIN,g.TEN TEN_LOAITIN,d.TEN TEN_CAP,a.MA_THELOAI,b.TEN TEN_THELOAI,a.MA_NGUON,c.TEN TEN_NGUON,a.MA_THOILUONG,e.TEN TEN_THOILUONG,a.TIEUDE,a.LINK,a.ND_TEN,a.GHICHU"
             + " from TT_CAPNHAT a left join TT_THELOAI b on b.MA = a.MA_THELOAI left join TT_NGUONTIN c on c.MA = a.MA_NGUON left join TT_CAP d on a.MA_CAP = d.MA left join TT_THOILUONG e on a.MA_THOILUONG = e.MA left join DM_PHONGBAN f on a.MA_PHONG=f.MA left join TT_LOAITIN g on a.MA_LOAITIN=g.MA where NAM = '" + nam + "' and POS = '" + pos + "' and MA_PHONG='" + phong + "' order by a.NGAY");
            dgvTarGet.ItemsSource = dtload.DefaultView;
            _dt = dtload.Copy();
        }

        private void RadCboTheLoai_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            try
            {
                _cls.ClsConnect();
                var dtchde = _cls.LoadDataText("select * from TT_NGUONTIN where MA_THELOAI='"+RadCboTheLoai.SelectedValue+"' order by MA");
                RadCboNguon.ItemsSource = dtchde.DefaultView;
                RadCboNguon.DisplayMemberPath = "TEN";
                RadCboNguon.SelectedValuePath = "MA";

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
            */
        }
        private void ShowGrid_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //string quy = ((int.Parse(dtpNgay.SelectedDate.Value.ToString("MM")) - 1) / 3 + 1).ToString();
                string nam = dtpNgay.SelectedDate.Value.ToString("yyyy");
                //string ng = dtpNgay.SelectedDate.Value.ToString("yyyy-MM-dd");
                //string ngay = dtpNgay.SelectedDate.Value.ToString("ddMMyyyy");
                string pos = RadCboPos.SelectedValue.ToString();
                string phong = RadCboPhong.SelectedValue.ToString();
                //string theloai = RadCboTheLoai.SelectedValue.ToString();
                //string nguon = RadCboNguon.SelectedValue.ToString();
                //string matin = ngay + pos +phong + theloai + nguon;
                //string tluong = RadCboCap.SelectedValue.ToString();
                //string cap = RadCboCap.SelectedValue.ToString();
                _cls.ClsConnect();
                dtload = _cls.LoadDataText("select a.MATIN,a.NGAY,a.POS,a.NAM,a.QUY,a.MA_CAP,a.MA_PHONG,f.TEN TEN_PHONG,a.MA_LOAITIN,g.TEN TEN_LOAITIN,d.TEN TEN_CAP,a.MA_THELOAI,b.TEN TEN_THELOAI,a.MA_NGUON,c.TEN TEN_NGUON,a.MA_THOILUONG,e.TEN TEN_THOILUONG,a.TIEUDE,a.LINK,a.ND_TEN,a.GHICHU"
                 + " from TT_CAPNHAT a left join TT_THELOAI b on b.MA = a.MA_THELOAI left join TT_NGUONTIN c on c.MA = a.MA_NGUON left join TT_CAP d on a.MA_CAP = d.MA left join TT_THOILUONG e on a.MA_THOILUONG = e.MA left join DM_PHONGBAN f on a.MA_PHONG=f.MA left join TT_LOAITIN g on a.MA_LOAITIN=g.MA where NAM = '" + nam+"' and POS = '"+pos+"' order by a.NGAY");
                if (dtload.Rows.Count>0) dgvTarGet.ItemsSource = dtload.DefaultView;
                else MessageBox.Show("Không có bản ghi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                _dt = dtload.Copy();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            _cls.DongKetNoi();
        }
        private void VBCD_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
           WpfTuyenTruyenVB f = new WpfTuyenTruyenVB();
            f.ShowDialog();
        }
        private void GetForm_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            WpfTuyenTruyenSet f = new WpfTuyenTruyenSet();
            f.ShowDialog();
        }

        private void RadCboPos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                _cls.ClsConnect();
                string sqlpb = RadCboPos.SelectedValue.ToString().Trim() == BienBll.MainPos.Trim() ? "select * from DM_PHONGBAN where MA in ('18','19','20','21','22','99') order by MA" : "select * from DM_PHONGBAN where MA in ('98','99') order by MA";
                var dtpb = _cls.LoadDataText(sqlpb);
                RadCboPhong.ItemsSource = dtpb.DefaultView;
                RadCboPhong.DisplayMemberPath = "TEN";
                RadCboPhong.SelectedValuePath = "MA";
   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
