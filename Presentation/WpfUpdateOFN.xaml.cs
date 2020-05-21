using System;
using System.Collections;
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
using System.ComponentModel;
using System.IO;
using System.Data;
using DAL;
using BLL;


namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfImportText.xaml
    /// </summary>
    public partial class WpfUpdateOFN : Window
    {
        public WpfUpdateOFN()
        {
            InitializeComponent();
        }
        private FileStream _fw;
        private ToolBll bll = new ToolBll();
        private DataTable dt = new DataTable();
        private DataTable dttrans = new DataTable();
        private DataTable dtkt = new DataTable();
        DataTable dttb = new DataTable();
        ClsOffline cls = new ClsOffline();
        ClsServer cnn = new ClsServer();
        string Thumuc = "C:\\TEXT";
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            cls.ClsConnect();
            bll.TaoThuMuc(Thumuc);
            try
            {
                string NGAY = dtpNgay.SelectedDate.Value.ToString("dd/MM/yyyy");
                string TransCd = bll.Left(CboTranCd.SelectedValue.ToString().Trim(), 10);
                string strkt = "select * from OfflineUp where TransCd = '" + TransCd + "'" + " and NGAYOFL = '" + NGAY + "'";
                cnn.ClsConnect();
                dtkt = cnn.LoadDataText(strkt);
                if (dtkt.Rows.Count > 0)
                {
                    MessageBox.Show("Đã thực hiện xuất số liệu, không thực hiện nữa", "Thông báo", MessageBoxButton.OK,
                                    MessageBoxImage.Warning);
                    btnOk.IsEnabled = false;
                    return;
                }
                else
                {
                    #region

                    string str = "select '00'+left(a.CommuneId,4) as MAPOS,c.TransCd" +
                                 " ,right(LEFT(b.BusinessDt,10),2)+'/'+SUBSTRING(LEFT(b.BusinessDt,10),5,2)+'/'+left(b.BusinessDt,4) as NGAY " +
                                 " ,right(LEFT(b.BusinessDt,10),2)+'-'+SUBSTRING(LEFT(b.BusinessDt,10),5,2)+'-'+left(b.BusinessDt,4) as NGAYQB " +
                                 " ,'KU' as MA,'11' as MNV,a.CommuneId as KH_MADP,left(a.CommuneId,6) as MAXA,c.GroupId as MATO, a.LoanNo as SOKU " +
                                 " ,a.CustomerId as MAKH,c.FullName as TENKH,LEFT(a.LoanProgramShort,2) as CHTRINH,a.Amount as MUCVAY" +
                                 ",CONVERT(decimal(28,12),0) as TNKH,CONVERT(decimal(28,12),0) as TNQH,a.FreezeAmount as DNKH,a.OverDueAmount as DNQH, " +
                                 " b.DisbursalAmount as SOTIEN	,right(LEFT(b.MakerDt,10),2)+'/'+SUBSTRING(LEFT(b.MakerDt,10),6,2)+'/'+left(b.MakerDt,4) as NGAYGD,'GN' as MOTA" +
                                 " from vLoanDetail a, BulkDisbursal b,Customer c  where b.DisbursalAmount>0 and c.GroupId<>'NOGROUP' and a.LoanNo=b.LoanNo and a.CustomerId=c.CustomerId " +
                                 " and right(LEFT(b.BusinessDt,10),2)+'/'+SUBSTRING(LEFT(b.BusinessDt,10),5,2)+'/'+left(b.BusinessDt,4) ='" +
                                 NGAY + "'" +
                                 " union	select '00'+left(a.CommuneId,4) as MAPOS,c.TransCd " +
                                 " ,RIGHT(b.BussinessDt, 2) + '/' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '/' + LEFT(b.BussinessDt, 4) as NGAY " +
                                 " ,RIGHT(b.BussinessDt, 2) + '-' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '-' + LEFT(b.BussinessDt, 4) as NGAYQB " +
                                 " ,'KU' as MA,'12' as MNV,a.CommuneId as KH_MADP,left(a.CommuneId,6) as MAXA,c.GroupId as MATO, a.LoanNo as SOKU " +
                                 " ,a.CustomerId as MAKH,c.FullName as TENKH,LEFT(a.LoanProgramShort,2) as CHTRINH,a.Amount as MUCVAY" +
                                 ",CONVERT(decimal(28,12),0) as TNKH,CONVERT(decimal(28,12),0) as TNQH,a.FreezeAmount as DNKH,a.OverDueAmount as DNQH, " +
                                 " b.TotalPrinPaid as SOTIEN	,right(LEFT(b.MakerDt,10),2)+'/'+SUBSTRING(LEFT(b.MakerDt,10),6,2)+'/'+left(b.MakerDt,4) as NGAYGD,'TN' as MOTA " +
                                 " from vLoanDetail a, BulkPayment b,Customer c  where b.TotalPrinPaid>0 and c.GroupId<>'NOGROUP' and a.LoanNo=b.LoanNo and a.CustomerId=c.CustomerId " +
                                 " and RIGHT(b.BussinessDt, 2) + '/' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '/' + LEFT(b.BussinessDt, 4) = '" +
                                 NGAY + "'" +
                                 " union	select '00'+left(d.CommuneId,4) as MAPOS,c.TransCd " +
                                 " ,RIGHT(b.BussinessDt, 2) + '/' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '/' + LEFT(b.BussinessDt, 4) as NGAY " +
                                 " ,RIGHT(b.BussinessDt, 2) + '-' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '-' + LEFT(b.BussinessDt, 4) as NGAYQB " +
                                 " ,'TK' as MA,'13' as MNV,d.CommuneId as KH_MADP,left(d.CommuneId,6) as MAXA,c.GroupId as MATO, a.CasaNo as SOKU " +
                                 " ,a.CustomerId as MAKH,c.FullName as TENKH,'105' as CHTRINH,0 as MUCVAY" +
                                 ",CONVERT(decimal(28,12),0) as TNKH,CONVERT(decimal(28,12),0) as TNQH,0 as DNKH,0 as DNQH, " +
                                 " b.Deposit as SOTIEN,right(LEFT(b.MakerDt,10),2)+'/'+SUBSTRING(LEFT(b.MakerDt,10),6,2)+'/'+left(b.MakerDt,4) as NGAYGD,'GUITK' as MOTA " +
                                 " from Casa a, MicroSaving b,Customer c,vLoanDetail d " +
                                 " where  c.GroupId<>'NOGROUP' and a.CasaNo=b.CasaNo and a.CustomerId=c.CustomerId and c.CustomerId=a.CustomerId and a.CustomerId=d.CustomerId and b.Deposit<>0 " +
                                 " and RIGHT(b.BussinessDt, 2) + '/' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '/' + LEFT(b.BussinessDt, 4) = '" +
                                 NGAY + "'" +
                                 " union	select '00'+left(d.CommuneId,4) as MAPOS,c.TransCd " +
                                 " ,RIGHT(b.BussinessDt, 2) + '/' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '/' + LEFT(b.BussinessDt, 4) as NGAY " +
                                 " ,RIGHT(b.BussinessDt, 2) + '-' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '-' + LEFT(b.BussinessDt, 4) as NGAYQB " +
                                 " ,'TK' as MA,'14' as MNV,d.CommuneId as KH_MADP,left(d.CommuneId,6) as MAXA,c.GroupId as MATO, a.CasaNo as SOKU " +
                                 " ,a.CustomerId as MAKH,c.FullName as TENKH,'105' as CHTRINH,0 as MUCVAY" +
                                 ",CONVERT(decimal(28,12),0) as TNKH,CONVERT(decimal(28,12),0) as TNQH,0 as DNKH,0 as DNQH, " +
                                 " b.Withdrawal as SOTIEN,right(LEFT(b.MakerDt,10),2)+'/'+SUBSTRING(LEFT(b.MakerDt,10),6,2)+'/'+left(b.MakerDt,4) as NGAYGD,'RUTTK' as MOTA " +
                                 " from Casa a, MicroSaving b,Customer c,vLoanDetail d " +
                                 " where c.GroupId<>'NOGROUP' and a.CasaNo=b.CasaNo and a.CustomerId=c.CustomerId and c.CustomerId=a.CustomerId and a.CustomerId=d.CustomerId and b.Withdrawal<>0 " +
                                 " and RIGHT(b.BussinessDt, 2) + '/' + SUBSTRING(convert(varchar(10),b.BussinessDt), 5, 2) + '/' + LEFT(b.BussinessDt, 4) = '" +
                                 NGAY + "'";

                    #endregion

                    dt = cls.LoadDataText(str);
                    if (dt.Rows.Count > 0)
                    {
                        #region

                        if (dt.Rows[0]["TransCd"].ToString().Trim() ==
                            bll.Left(CboTranCd.SelectedValue.ToString().Trim(), 10) &&
                            dt.Rows[0]["NGAY"].ToString().Trim() == NGAY)
                        {
                            dgvData.ItemsSource = dt.DefaultView;
                            //MessageBox.Show(dt.Rows[0]["TransCd"].ToString().Trim() + "  " + dt.Rows[0]["NGAY"].ToString().Trim());
                            InsertToTable();
                            //ClsConnectLocal cn = new ClsConnectLocal();
                            cnn.ClsConnect();
                            string ver = "insert into OfflineUp (MAPOS,TransCd,NGAYOFL) values ('" + dt.Rows[0]["MAPOS"] +
                                         "','" + dt.Rows[0]["TransCd"] + "','" + dt.Rows[0]["NGAY"] + "')";
                            cnn.UpdateDataText(ver);
                            cnn.DongKetNoi();
                        }
                        else
                        {
                            MessageBox.Show("Không đúng điểm giao dịch hoặc sai ngày ", "Thông báo", MessageBoxButton.OK,
                                            MessageBoxImage.Warning);
                            return;
                        }

                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("không có dữ liệu, có thể xem lại ngày");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

        private void WriteText(String fileName)
        {
            var encode = Encoding.BigEndianUnicode;
            _fw = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(_fw, encode);
            foreach (DataRow row in dt.Rows)
            {
                //foreach (DataColumn col in dt.Columns)
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i + 1 < dt.Columns.Count)
                    {
                        //sw.Write(row[col].ToString() + "#");
                        sw.Write(row[i] + "#");
                    }
                    else
                    {
                        sw.Write(row[i].ToString());
                    }
                }
                sw.WriteLine();
            }
            sw.Close();
        }

        private void InsertToTable()
        {
            try
            {
                //ClsServer cnn = new ClsServer();
                //ClsConnectLocal cnn = new ClsConnectLocal();
                cnn.ClsConnect();
                int thamso = 21;
                string[] bien = new string[thamso];
                object[] giatri = new object[thamso];
                foreach (DataRow dr in dt.Rows)
                {
                    bien[0] = "@MAPOS";
                    giatri[0] = dr[0].ToString().Trim();
                    bien[1] = "@TransCd";
                    giatri[1] = dr[1].ToString().Trim();
                    bien[2] = "@NGAY";
                    giatri[2] = dr[2].ToString().Trim();
                    bien[3] = "@NGAYQB";
                    giatri[3] = dr[3].ToString().Trim();
                    bien[4] = "@MA";
                    giatri[4] = dr[4].ToString().Trim();
                    bien[5] = "@MNV";
                    giatri[5] = dr[5].ToString().Trim();
                    bien[6] = "@KH_MADP";
                    giatri[6] = dr[6].ToString().Trim();
                    bien[7] = "@MAXA";
                    giatri[7] = dr[7].ToString().Trim();
                    bien[8] = "@MATO";
                    giatri[8] = dr[8].ToString().Trim();
                    bien[9] = "@SOKU";
                    giatri[9] = dr[9].ToString().Trim();
                    bien[10] = "@MAKH";
                    giatri[10] = dr[10].ToString().Trim();
                    bien[11] = "@TENKH";
                    giatri[11] = dr[11].ToString().Trim();
                    bien[12] = "@CHTRINH";
                    giatri[12] = dr[12].ToString().Trim();
                    bien[13] = "@MUCVAY";
                    giatri[13] = dr[13];
                    bien[14] = "@TNKH";
                    giatri[14] = dr[14];
                    bien[15] = "@TNQH";
                    giatri[15] = dr[15];
                    bien[16] = "@DNKH";
                    giatri[16] = dr[16];
                    bien[17] = "@DNQH";
                    giatri[17] = dr[17];
                    bien[18] = "@SOTIEN";
                    giatri[18] = dr[18];
                    bien[19] = "@NGAYGD";
                    giatri[19] = dr[19].ToString().Trim();
                    bien[20] = "@MOTA";
                    giatri[20] = dr[20].ToString().Trim();
                    string sql = "insert into OfflineData values('" + giatri[0] + "','" + giatri[1] + "','" + giatri[2] + "','" + giatri[3] + "','" + giatri[4] + "','" + giatri[5] + "','" +
                                 giatri[6] + "','" + giatri[7] + "','" + giatri[8] + "','" + giatri[9] + "','" + giatri[10] + "',N'" + giatri[11] + "','" + giatri[12] + "'," + giatri[13] + "," +
                                 giatri[14] + "," + giatri[15] + "," + giatri[16] + "," + giatri[17] + "," + giatri[18] + ",'" + giatri[19] + "','" + giatri[20] + "')";
                   // MessageBox.Show(sql);
                    cnn.LoadDataText(sql); // insert to may 16
                }
                cnn.DongKetNoi();
                MessageBox.Show("Nhập số liệu thành công","Thông báo",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch 
            {
                MessageBox.Show("Lổi, liên hệ phòng tin học để được hướng dẫn", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dtpNgay.SelectedDate = DateTime.Now;
            try
            {
                cls.ClsConnect();
                dttb = cls.LoadDataText("SELECT TABLE_NAME as NAME FROM INFORMATION_SCHEMA.TABLES order by TABLE_NAME");//where TABLE_SCHEMA='dbo'  
                for (int i = 0; i < dttb.Rows.Count; i++)
                {
                    CboTable.Items.Add(dttb.Rows[i][0]);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {
                //ClsConnectLocal cn = new ClsConnectLocal();
                cnn.ClsConnect();
                if (BienBll.Quyen == "1")
                {
                    dttrans =
                        cnn.LoadDataText("select TransCd,TransName from TransPoint"); //where TABLE_SCHEMA='dbo'  
                }
                else
                {
                    dttrans =
                        cnn.LoadDataText("select TransCd,TransName from TransPoint where PosCd = '" + BienBll.NdMadv +
                                         "'"); //where TABLE_SCHEMA='dbo'  

                }
                for (int i = 0; i < dttrans.Rows.Count; i++)
                {
                    CboTranCd.Items.Add(dttrans.Rows[i][0] + "  |  " + dttrans.Rows[i][1]);
                }
                CboTranCd.SelectedIndex = 0;
                cnn.DongKetNoi();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
        }

        private void CboTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(CboTable.SelectedValue.ToString());
            try
            {
                string sql = "select * from " + CboTable.SelectedValue.ToString().Trim();
                cls.ClsConnect();
                dttb = cls.LoadDataText(sql);
                dgvData.ItemsSource = dttb.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            cls.DongKetNoi();
        }

    }
}