using System;
using System.Data;
using System.Windows;

namespace Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class WpfLeThuy : Window
    {
        public WpfLeThuy()
        {
            InitializeComponent();
            GetDMHOSO();
            dateTuNgay.SelectedDate = DateTime.Now;
            dateDenNgay.SelectedDate = DateTime.Now;
        }

        #region Load mấy thứ linh tinh
        private void GetDMHOSO()
        {
            DataTable dt = ImportData_DAO.Instance.GetDMCONVERT();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cboNameHoso.Items.Add(dt.Rows[i][0] + " | " + dt.Rows[i][1] + " | " + dt.Rows[i][4] + " | " + dt.Rows[i][2] + " | " + dt.Rows[i][3]);
            }
        }
        private void GetDMHUYEN()
        {
            cboMaPos.Items.Clear();
            DataTable dt = ImportData_DAO.Instance.GetDMHUYEN();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cboMaPos.Items.Add(dt.Rows[i][0] + " | " + dt.Rows[i][1]);
            }
        }
        private void SetEnable(bool TuNgay, bool DenNgay, bool MaPos)
        {
            dateTuNgay.IsEnabled = TuNgay;
            dateDenNgay.IsEnabled = DenNgay;
            cboMaPos.IsEnabled = MaPos;
            if (MaPos)
            {
                GetDMHUYEN();
            }
        }
        private void cboNameHoso_DropDownClosed(object sender, EventArgs e)
        {
            if (cboNameHoso.SelectedItem != null)
            {
                string[] arrStr = cboNameHoso.SelectedValue.ToString().Trim().Split('|');
                //MessageBox.Show(arrStr[2].Trim(), "TB");
                switch (arrStr[2].Trim())
                {
                    case "01":
                        SetEnable(false, false, false);
                        break;
                    case "02":
                        SetEnable(false, false, true);
                        break;
                    case "03":
                    case "04":
                        SetEnable(false, true, true);
                        break;
                    default:
                        SetEnable(true, true, true);
                        break;
                }
            }
        }

        #endregion

        // Bắt đầu đoạn chính
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            // thêm đây đoạn kiểm tra giá trị nữa
            if (CheckTienComNull())
            {
                string[] arrStr = cboNameHoso.SelectedValue.ToString().Trim().Split('|');
                string[] arrMAPOS = cboMaPos.SelectedValue.ToString().Trim().Split('|');
                //MessageBox.Show(arrStr[2].Trim(), "TB");
                ImportData_DAO.Instance.ConvertToLETHUY(arrStr[2].Trim(), arrStr[0].Trim(), dateTuNgay.SelectedDate.Value, dateDenNgay.SelectedDate.Value, arrMAPOS[0].Trim(), arrStr[3].Trim(), arrStr[4].Trim());
                dgView.ItemsSource = ImportData_DAO.Instance.View_Hoso_SQL(arrStr[2].Trim(), arrStr[0].Trim(), arrMAPOS[0].Trim(), arrStr[3].Trim(), arrStr[4].Trim(), dateTuNgay.SelectedDate.Value, dateDenNgay.SelectedDate.Value).DefaultView;
            }
            else
                MessageBox.Show("Cập nhật hồ sơ thất bại!", "Thông báo");
        }

        private bool CheckTienComNull()
        {
            bool chk = false;
            if (cboNameHoso.SelectedValue == null)
            {
                MessageBox.Show("Tên hồ sơ không được để trống!", "Thông báo");
                cboNameHoso.Focus();
                return chk = false;
            }
            else
            {
                chk = true;
            }
            if (dateTuNgay.SelectedDate == null)
            {
                MessageBox.Show("Ngày bắt đầu không được để trống!", "Thông báo");
                dateTuNgay.Focus();
                return chk = false;
            }
            else
            {
                chk = true;
            }
            if (dateDenNgay.SelectedDate == null)
            {
                MessageBox.Show("Ngày kết thúc không được để trống!", "Thông báo");
                dateDenNgay.Focus();
                return chk = false;
            }
            else
            {
                chk = true;
            }

            if (cboMaPos.SelectedValue == null)
            {
                MessageBox.Show("Mã PGD không được để trống!", "Thông báo");
                cboMaPos.Focus();
                return chk = false;
            }
            else
            {
                chk = true;
            }
            return chk;
        }

    }
}
