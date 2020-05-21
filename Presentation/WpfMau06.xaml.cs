using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.IO;
using System.Text;
using BLL;
using DAL;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for Wpf_THONGBAO_DONG105.xaml
    /// </summary>
    public partial class WpfMau06 : Window
    {
        ClsServer cls = new ClsServer();
        ServerInfor srv = new ServerInfor();
        ToolBll str = new ToolBll();
        DataTable dt = new DataTable();
        DataTable dtNew = new DataTable();
        public WpfMau06()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                DataTable dtpos = new DataTable();
                string sql = "select PO_MA,PO_TEN from DMPOS where PO_MA='"+BienBll.NdMadv.Trim()+"' order by PO_MA";
                dtpos = cls.LoadDataText(sql);
                for (int i = 0; i < dtpos.Rows.Count; i++)
                {
                    CboPos.Items.Add(dtpos.Rows[i][0] + " | " + dtpos.Rows[i][1]);
                }
                CboPos.SelectedIndex = 0;
                DataTable dtng = new DataTable();
                dtng = cls.LoadDataText("select MAX(convert(date,NGAYKU,105)) as NGMAX from U_HSTD");
                dtpNgay.SelectedDate = DateTime.Parse(dtng.Rows[0]["NGMAX"].ToString());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error "+ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

   


        private void lblOk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dtNew = dt.GetChanges();
            if (dtNew == null || dtNew.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có thay đổi ngày nào !", "Thông báo",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
            else
            {
                dgvTarGet.ItemsSource = dtNew.DefaultView;
            }
           
        }
        private void lblTuChoi_MouseDown(object sender, MouseButtonEventArgs e)
        {
           dtNew.RejectChanges();
           dtNew = null;
           dgvTarGet.ItemsSource = null;
        }
        private void lblCapNhat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (dtNew.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtNew.Rows)
                    {
                        //MessageBox.Show(dr["NG_MATO"].ToString()+"      "+dr["A01"].ToString());
                        string strsql = "update MAU06 set TRANGTHAI1='" + dr["TRANGTHAI1"] + "' where SOKU='" + dr["SOKU"]+ "'";
                        cls.UpdateDataText(strsql);
                        //MessageBox.Show(strsql);
                        MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Bạn chưa chấp nhận hoặc không có dòng nào được chọn !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi !"+ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void lblIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dtNew = dt.GetChanges();
            if (dtNew == null || dtNew.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có thay đổi nào !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                rpt_Mau06 rpt = new rpt_Mau06();
                RPUtility.ShowRp(rpt, dtNew, this, srv.DbSourceSerVer(), srv.DbNameSerVer(), srv.DbUserSerVer(), srv.DbPassSerVer());
            }
        }

        private void lblClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void LblGetData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            string strssql = "";
            cls.ClsConnect();
            try
            {
                var mato = str.Left(CboTo.SelectedValue.ToString().Trim(), 7);
                if (dtpNgay.SelectedDate != null)
                {
                    if (Opt1.IsChecked == true)
                        strssql ="select MAPOS,TENPOS,MAKH,TENKH,MAXA,TENXA,SOKU,CHTRINH,MATO,TENTT,DUNO,convert(varchar(10),NGAY_VAY,103) NGAY_VAY,TRANGTHAI1,TRANGTHAI2,PLMD,TEN_PLMD from MAU06 where MATO='" +
                            mato + "'";
                    else if (Opt2.IsChecked==true)
                        strssql = "select MAPOS,TENPOS,MAKH,TENKH,MAXA,TENXA,SOKU,CHTRINH,MATO,TENTT,DUNO,convert(varchar(10),NGAY_VAY,103) NGAY_VAY,TRANGTHAI1,TRANGTHAI2,PLMD,TEN_PLMD from MAU06 where MATO='" +
                            mato + "' and TRANGTHAI1='true'";
                    else
                        strssql = "select MAPOS,TENPOS,MAKH,TENKH,MAXA,TENXA,SOKU,CHTRINH,MATO,TENTT,DUNO,convert(varchar(10),NGAY_VAY,103) NGAY_VAY,TRANGTHAI1,TRANGTHAI2,PLMD,TEN_PLMD from MAU06 where MATO='" +
                            mato + "' and TRANGTHAI1='false'";
                    dt = cls.LoadDataText(strssql);
                    if (dt.Rows.Count > 0)  dgvData.ItemsSource = dt.DefaultView;
                    else MessageBox.Show("Không có bản ghi nào ", "Mess");
                }
                else MessageBox.Show("Chưa chọn ngày ", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            
        }

        private void CboPos_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                CboXa.Items.Clear();
                cls.ClsConnect();
                DataTable dtxa = new DataTable();
                string sql = "select MA,TEN from DMXA where PGD_QL= " + "'" + str.Left(CboPos.SelectedValue.ToString().Trim(), 6) + "'" + " order by MA";
                dtxa = cls.LoadDataText(sql);
                for (int i = 0; i < dtxa.Rows.Count; i++)
                {
                    CboXa.Items.Add(dtxa.Rows[i][0] + " | " + dtxa.Rows[i][1]);
                }
                CboXa.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }


        }

        private void CboXa_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            try
            {
                CboTo.Items.Clear();
                cls.ClsConnect();
                DataTable dtto = new DataTable();
                string sql = "select TO_MATO,TO_TENTT from HSTO where Left(TO_MADP,6) = " + str.Left(CboXa.SelectedValue.ToString().Trim(), 6) +" and TRANGTHAI<>'C' order by TO_MATO ";
                //MessageBox.Show(sql);
                dtto = cls.LoadDataText(sql);
                for (int i = 0; i < dtto.Rows.Count; i++)
                {
                    CboTo.Items.Add(dtto.Rows[i][0] + " | " + dtto.Rows[i][1]);
                }
                CboTo.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tiếp tục" + ex.Message, "Mess");
            }


        }

        private void ChkAll_Checked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = true;
            }
        }

        private void ChkAll_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (DataRow dr in dt.Rows)
            {
                dr[0] = false;
            }

        }

    }
}
