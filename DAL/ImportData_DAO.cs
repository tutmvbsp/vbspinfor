using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL; //Thêm 2 cái này để dùng class trong project. Nhờ anh khi nào up vào QBIM thì mở 2 dòng này lên em với
//using BLL; //Em xài class Mẫu trong Project này luôn

namespace Presentation
{
    public class ImportData_DAO
    {
        ClsOracle cls = new ClsOracle();
        private static ImportData_DAO instance; // Crl + R + E
        public static ImportData_DAO Instance
        {
            get
            {
                if (instance == null) instance = new ImportData_DAO(); return instance;
            }

            private set
            {
                instance = value;
            }
        }

        private ImportData_DAO() { }
        public static string connectionST = @"Data Source = 10.31.204.2, 1433;Initial Catalog=OfflineQB;Integrated Security=False;User ID='sa';Password='123456'";
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionST))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query, conn);
                    comm.CommandTimeout = 1800;
                    if (parameter != null)
                    {
                        string[] listpara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listpara)
                        {
                            if (item.Contains('@'))
                            {
                                comm.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(comm);
                    adapter.Fill(data);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Thông báo");
            }
            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionST))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand(query, conn);
                    comm.CommandTimeout = 3600;
                    if (parameter != null)
                    {
                        string[] listpara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listpara)
                        {
                            if (item.Contains('@'))
                            {
                                comm.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }
                    data = comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Thông báo");
            }
            return data;
        }

        public DataTable GetDMCONVERT()
        {
            string query = "EXECUTE USP_GETDMCONVERT";
            DataTable dt = ExecuteQuery(query);
            return dt;
        }
        public DataTable GetDMHUYEN()
        {
            string query = "EXECUTE USP_GETDMHUYEN";
            DataTable dt = ExecuteQuery(query);
            return dt;
        }
        public void ConvertToLETHUY(string TYPE, string HOSO_NAME, DateTime TuNgay, DateTime DenNgay, string MAPOS, string TEN_MAPOS, string TEN_NGAYBC)
        {
            string txtdate, dd, mm, yy;
            txtdate = DenNgay.ToString();
            dd = txtdate.Substring(0, 2);
            mm = txtdate.Substring(3, 2);
            yy = txtdate.Substring(8, 2);
            if (!Check_Hoso_SQL(TYPE, HOSO_NAME, MAPOS, TEN_MAPOS, TEN_NGAYBC, TuNgay, DenNgay))
            {
                DataTable dt = GetDataFromOracle(TYPE, HOSO_NAME, MAPOS, TEN_MAPOS, TEN_NGAYBC, TuNgay, DenNgay);
                // THÊM ĐÂY 1 IF
                if (dt.Rows.Count > 0)
                {
                    Delete_Hoso_SQL(TYPE, HOSO_NAME, MAPOS, TEN_MAPOS, TEN_NGAYBC, TuNgay, DenNgay);
                    CopyDataTableToSQL(dt, HOSO_NAME);
                    ExecuteNonQuery("EXECUTE USP_HT_DM_CONVERT_HIST_INSERT @TENDM , @NgayHS , @MaPos", new object[] { HOSO_NAME, DenNgay, MAPOS });
                    MessageBox.Show("ĐÃ CHUYỂN DỮ LIỆU THÀNH CÔNG", "Thông báo");
                }
                else
                    MessageBox.Show("Dữ liệu rỗng", "Thông báo");
            }
            else
                MessageBox.Show("Dữ liệu SQL đã có cho POS này", "Thông báo");
        }
        
        private DataTable GetDataFromOracle(string TYPE, string HOSO_NAME, string MAPOS, string TEN_MAPOS, string TEN_NGAYBC, DateTime TuNgay, DateTime DenNgay)
        {
            cls.ClsConnect();
            string sql_kt = "";
            switch (TYPE)
            {
                case "01":  //Đây là những hồ sơ hệ thống DMHUYEN, DMXA...
                    sql_kt = "SELECT * FROM " + HOSO_NAME;
                    break;
                case "02":  //02, 03, 04 giống nhau. Là những hồ sơ dữ liệu lớn: HSKU, CASA, HSKH, HSTO, ...
                    sql_kt = "SELECT * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS;
                    break;
                case "03":  //02, 03, 04 giống nhau. Là những hồ sơ dữ liệu lớn: HSKU, CASA, HSKH, HSTO, ...
                    sql_kt = "SELECT * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " = '" + DenNgay.ToString("dd/MMM/yyyy") + "'";
                    break;
                case "04":  //02, 03, 04 giống nhau. Là những hồ sơ dữ liệu lớn: HSKU, CASA, HSKH, HSTO, ...
                    sql_kt = "SELECT * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " = '" + DenNgay.ToString("dd/MMM/yyyy") + "'";
                    break;
                case "05":    //05 là hồ sơ dữ liệu nhỏ: SBV_VBSP, GL_VBSP, HSBT...
                    sql_kt = "SELECT * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " >= '" + TuNgay.ToString("dd/MMM/yyyy") + "'" + " AND " + TEN_NGAYBC + " <= '" + DenNgay.ToString("dd/MMM/yyyy") + "'";
                    break;
            }
            DataTable dt = cls.LoadDataText(sql_kt);
            cls.DongKetNoi();
            return dt;
        }
        private Boolean Check_Hoso_SQL(string TYPE, string HOSO_NAME, string MAPOS, string TEN_MAPOS, string TEN_NGAYBC, DateTime TuNgay, DateTime DenNgay)
        {
            Boolean kt = false;
            string sql_kt = "";
            switch (TYPE)
            {
                case "01":  //Đây là những hồ sơ hệ thống DMHUYEN, DMXA... Không cần kiểm tra
                    break;
                case "02":  //Là những hồ sơ dữ liệu lớn: HSKH, HSTO, HSSV ...
                    sql_kt = "SELECT TOP 1 * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS;
                    break;
                case "03":  //Là những hồ sơ dữ liệu lớn: HSKU, CASA, ...
                case "04":  //Là những hồ sơ dữ liệu lớn: HSSV_DAILY, CASA_DAILY...
                    sql_kt = "SELECT TOP 1 * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " = '" + DenNgay.ToString("yyyy-MM-dd") + "'";
                    break;
                case "05":    //05 là hồ sơ dữ liệu nhỏ: SBV_VBSP, GL_VBSP, HSBT...
                    sql_kt = "SELECT TOP 1 * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " >= '" + TuNgay.ToString("yyyy-MM-dd") + "'" + " AND " + TEN_NGAYBC + " <= '" + DenNgay.ToString("yyyy-MM-dd") + "'";
                    break;
            }

            DataTable dt_kt = ExecuteQuery(sql_kt);
            if (dt_kt.Rows.Count > 0)
            {
                DialogResult dlR = MessageBox.Show("Đã có dữ liệu của hồ sơ này. Bạn vẫn muốn tiếp tục tạo lại?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlR == DialogResult.No)
                {
                    kt = true;
                }
                else
                {
                    kt = false;
                }
            }
            return kt;
        }
        private void Delete_Hoso_SQL(string TYPE, string HOSO_NAME, string MAPOS, string TEN_MAPOS, string TEN_NGAYBC, DateTime TuNgay, DateTime DenNgay)
        {
            string sql_kt = "";
            switch (TYPE)
            {
                case "01":  //Đây là những hồ sơ hệ thống DMHUYEN, DMXA...
                    sql_kt = "DELETE FROM " + HOSO_NAME;
                    break;
                case "02":  //HSKH, HSTO, HSSV ...
                    sql_kt = "DELETE FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS;
                    break;
                case "03":  //HSKU, CASA 
                    sql_kt = "DELETE FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " <= '" + DenNgay.ToString("yyyy-MM-dd") + "'";
                    break;
                case "04":  //HSCV_DAILY, CASA_DAILY xóa là xóa hết
                    sql_kt = "DELETE FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS;
                    break;
                case "05":    //05 là hồ sơ dữ liệu nhỏ: SBV_VBSP, GL_VBSP, HSBT...
                    sql_kt = "DELETE FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " >= '" + TuNgay.ToString("yyyy-MM-dd") + "'" + " AND " + TEN_NGAYBC + " <= '" + DenNgay.ToString("yyyy-MM-dd") + "'";
                    break;
            }
            ExecuteNonQuery(sql_kt);
        }

        public void CopyDataTableToSQL(DataTable dt, string ssqltable)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionST))
                {
                    conn.Open();
                    SqlBulkCopy bulkcopy = new SqlBulkCopy(conn);
                    bulkcopy.BulkCopyTimeout = 1800;
                    bulkcopy.DestinationTableName = ssqltable;
                    bulkcopy.WriteToServer(dt);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Thông báo");
            }
        }

        public DataTable View_Hoso_SQL(string TYPE, string HOSO_NAME, string MAPOS, string TEN_MAPOS, string TEN_NGAYBC, DateTime TuNgay, DateTime DenNgay)
        {
            string sql_kt = "";
            switch (TYPE)
            {
                case "01":  //Đây là những hồ sơ hệ thống DMHUYEN, DMXA... Không cần kiểm tra
                    sql_kt = "SELECT TOP 100 * FROM " + HOSO_NAME;
                    break;
                case "02":  //Là những hồ sơ dữ liệu lớn: HSKH, HSTO, HSSV ...
                    sql_kt = "SELECT TOP 100 * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS;
                    break;
                case "03":  //Là những hồ sơ dữ liệu lớn: HSKU, CASA, ...
                case "04":  //Là những hồ sơ dữ liệu lớn: HSSV_DAILY, CASA_DAILY...
                    sql_kt = "SELECT TOP 100 * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " = '" + DenNgay.ToString("yyyy-MM-dd") + "'";
                    break;
                case "05":    //05 là hồ sơ dữ liệu nhỏ: SBV_VBSP, GL_VBSP, HSBT...
                    sql_kt = "SELECT TOP 100 * FROM " + HOSO_NAME + " WHERE " + TEN_MAPOS + "=" + MAPOS + " AND " + TEN_NGAYBC + " >= '" + TuNgay.ToString("yyyy-MM-dd") + "'" + " AND " + TEN_NGAYBC + " <= '" + DenNgay.ToString("yyyy-MM-dd") + "'";
                    break;
            }

            DataTable dt_kt = ExecuteQuery(sql_kt);
            return dt_kt;
        }

    }
}
