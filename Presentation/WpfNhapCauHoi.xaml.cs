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
using System.IO;
using MessageBox = System.Windows.MessageBox;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for WpfLaiTon.xaml
    /// </summary>
    public partial class WpfNhapCauHoi : Window
    {
        public WpfNhapCauHoi()
        {
            InitializeComponent();
        }

        ToolBll s = new ToolBll();
        DataTable dt = new DataTable();
        private readonly ClsServer cls = new ClsServer();
        private string filename = "C:\\TEXT\\CAUHOI.TXT";
        private string chon = "";
        private FileStream _fw;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
   
        }



  

        private void BtnThem_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                dapan();
                if (TxtSo.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Chưa nhập số câu !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxtSo.Focus();
                }
                else
                {
                    cls.ClsConnect();
                    var dtthem = cls.LoadDataText("select * from cauhoi where TT=" + TxtSo.Text.Trim());
                    if (dtthem.Rows.Count > 0)
                        MessageBox.Show("Câu " + TxtSo.Text.Trim() + " đã tồn tại", "Thông báo", MessageBoxButton.OK,
                            MessageBoxImage.Information);
                    else
                    {
                        string sqladd =
                            "insert into CAUHOI(LOAI,CAUHOI,A,B,C,D,DAPAN,TT,POS,NG_NHAP,CANCU)" +
                            " Values('" + CboChuDe.SelectionBoxItem + "',N'" + TxtCauHoi.Text + "',N'" +
                            TxtA.Text.Trim() + "',N'" +
                            TxtB.Text.Trim() + "',N'" + TxtC.Text.Trim() + "',N'" + TxtD.Text.Trim() +
                            "','" + chon + "','" + TxtSo.Text.Trim() + "','" +
                            BienBll.NdMadv.Trim() + "',N'" + BienBll.NdTen.Trim() + "',N'" + TxtCanCu.Text + "')";
                        // MessageBox.Show(sqladd);
                        cls.UpdateDataText(sqladd);
                        MessageBox.Show("Insert OK", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            cls.DongKetNoi();
        }


        private void BtnSua_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                dapan();
                if (TxtSo.Text.Trim().Length == 0)
                {
                    MessageBox.Show("Nhập số câu hỏi và Click vào 'Câu hỏi số' đê hiện câu hỏi !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    TxtSo.Focus();
                }
                else
                {
                    cls.ClsConnect();
                        string sqladd =
                            "update CAUHOI set LOAI='"+ CboChuDe.SelectionBoxItem+ "',CAUHOI="+ "N'" + TxtCauHoi.Text + "'" +
                            ",A=N'"+ TxtA.Text.Trim() + "',B=N'"+ TxtB.Text.Trim() + "',C=N'"+ TxtC.Text.Trim() + 
                            "',D=N'"+ TxtD.Text.Trim() + "',DAPAN='"+ chon + 
                            "',POS='"+ BienBll.NdMadv.Trim() +
                            "',NG_NHAP=N'" + BienBll.NdTen.Trim() + "',CANCU=N'" + TxtCanCu.Text + "' where TT="+ TxtSo.Text.Trim();
                        // MessageBox.Show(sqladd);
                        cls.UpdateDataText(sqladd);
                        MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            cls.DongKetNoi();
        }

        private void BtnXoa_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (
                    MessageBox.Show("Có thực sự muốn xóa ? ", "Question", MessageBoxButton.YesNo,
                        MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    MessageBox.Show("Select No");
                }
                else
                {
                    cls.ClsConnect();
                    cls.UpdateDataText("delete from cauhoi where TT=" + TxtSo.Text.Trim());
                    MessageBox.Show("Đã xóa câu " + TxtSo.Text.Trim() + " thành công!", "Thông báo",MessageBoxButton.OK, MessageBoxImage.Warning);
                    ClearAll();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            cls.DongKetNoi();
        }

        private void LoadData_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            ClearAll();
        }

        private void ClearAll()
        {
            TxtA.Text = "";
            TxtB.Text = "";
            TxtC.Text = "";
            TxtD.Text = "";
            TxtC.Text = "";
            TxtCauHoi.Text = "";
            TxtCanCu.Text = "";
        }

        private void LblCheck_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                var dtCheck = cls.LoadDataText("select * from cauhoi where TT=" + TxtSo.Text.Trim());
                if (dtCheck.Rows.Count>0)
                    MessageBox.Show("Câu "+TxtSo.Text.Trim() + " đã tồn tại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("Câu " + TxtSo.Text.Trim() + " chưa tồn tại, Hãy nhập vào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lổi "+ex.Message,"Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);

            }
            cls.DongKetNoi();
        }

        private void LblDisplay_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                cls.ClsConnect();
                string sql = "select * from CAUHOI where TT="+TxtSo.Text.Trim();
                dt = cls.LoadDataText(sql);
                if (dt.Rows.Count > 0)
                {
                    WriteText(filename);
                    StreamReader sr = new StreamReader(filename);
                    string readLine = sr.ReadLine();
                    if (readLine != null)
                    {
                        string[] arrStr = readLine.Split('#');
                        TxtCauHoi.Text = arrStr[1];
                        TxtA.Text =arrStr[2];
                        TxtB.Text =arrStr[3];
                        TxtC.Text =arrStr[4];
                        TxtD.Text =arrStr[5];
                        TxtCanCu.Text = arrStr[14];
                        if (arrStr[6] == "A") RationA.IsChecked = true;
                        else if (arrStr[6] == "B") RationB.IsChecked = true;
                        else if (arrStr[6] == "C") RationC.IsChecked = true;
                        else RationD.IsChecked = true;
                        if (arrStr[0].Trim() == "KT") CboChuDe.SelectedIndex = 0;
                        else if (arrStr[0].Trim() == "TD") CboChuDe.SelectedIndex = 1;
                        else if (arrStr[0].Trim() == "TH") CboChuDe.SelectedIndex = 2;
                        else if (arrStr[0].Trim() == "KS") CboChuDe.SelectedIndex = 3;
                        else if (arrStr[0].Trim() == "TC") CboChuDe.SelectedIndex = 4;
                        // CboChuDe.SelectedIndex = CboChuDe.Items.IndexOf(arrStr[0]);
                        // CboChuDe.SelectedItem = arrStr[0];
                    }
                }
                else MessageBox.Show("Cấu số  " + TxtSo.Text.Trim()+" chưa có !", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            cls.DongKetNoi();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
           Close();
        }
        private void WriteText(String fileName)
        {
            System.Text.Encoding encode = System.Text.Encoding.BigEndianUnicode;
            _fw = new System.IO.FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            StreamWriter sw = new StreamWriter(_fw, encode);
            //TextWriter sw = new StreamWriter(expFile);
            foreach (DataRow row in dt.Rows)
            {
                //foreach (DataColumn col in dt.Columns)
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (i + 1 < dt.Columns.Count)
                    {
                        //sw.Write(row[col].ToString() + "#");
                        //sw.Write(row[i].ToString() + "#");
                        sw.Write(row[i].ToString() + "#");
                    }
                    else
                    {
                        sw.Write(row[i].ToString());
                    }
                }
                sw.WriteLine();
            }
            sw.Close();
            // MessageBox.Show("Export text OK");
        }

        private void dapan()
        {
            if (RationA.IsChecked == true) chon = "A";
            else if (RationB.IsChecked == true) chon = "B";
            else if (RationC.IsChecked == true) chon = "C";
            else chon = "D";
        }
    }
}
