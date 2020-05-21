using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class ClsConnectLocal
    {
        SqlConnection connect = new SqlConnection();
        //
        public bool KiemTraKetNoi()
        {
            bool ok = true;
            string ketnoi = ConfigurationManager.ConnectionStrings["strlocal"].ConnectionString;
            connect.ConnectionString = ketnoi;//ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
            if (connect.State == ConnectionState.Closed)

                try
                {
                    connect.Open();
                }
                catch //(Exception e)
                {
                    ok = false;
                }
                finally
                {
                    connect.Close();
                    connect.Dispose();
                }
            return ok;
        }
        //
        public void ClsConnect()
        {
            if (connect.State == ConnectionState.Closed)
            {
                string ketnoi = ConfigurationManager.ConnectionStrings["strlocal"].ConnectionString;
                connect.ConnectionString = ketnoi; //ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
                if (connect.State == ConnectionState.Closed)

                    try
                    {
                        connect.Open();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                //finally
                //{
                //    connect.Close();
                //    connect.Dispose();
                //}
            }

        }



        // --------------------------
        public void DongKetNoi()
        {
            if (connect.State == ConnectionState.Open)
            {
                connect.Close();
                connect.Dispose();
            }
        }
        // Load Procedure khong co  tham so
        public DataTable LoadDataProc(string sql)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //adapter.Fill(ds, tablename);
            //dt = ds.Tables[tablename];
            adapter.Fill(dt);
            return dt;
        }
        //Load text khong co tham so
        public DataTable LoadDataText(string sql)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        //Load text co tham so
        public DataTable LoadDataTextPara(string sql, string[] name, object[] value, int nparameter)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.Text;
            //command.CommandTimeout = 240;
            for (int i = 0; i < nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //adapter.Fill(ds, tablename);
            //dt = ds.Tables[tablename];
            adapter.Fill(dt);
            return dt;
        }
        //Load Procedure co tham so
        public DataTable LoadDataProcPara(string sql, string[] name, object[] value, int nparameter)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 150;
            for (int i = 0; i < nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            //DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            //adapter.Fill(ds, tablename);
            //dt = ds.Tables[tablename];
            adapter.Fill(dt);
            return dt;
        }
        public int UpdateDataText(string sql)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 1800;
            return command.ExecuteNonQuery();
        }

        public int UpdateDataProc(string sql)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.StoredProcedure;
            return command.ExecuteNonQuery();
        }
        public int UpdateDataProcPara(string sql, string[] name, object[] value, int nparameter)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 150;
            for (int i = 0; i < nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            return command.ExecuteNonQuery();
        }
        //Load Procedure co tham so
        public DataTable ProcPara(string sql, string[] name, object[] value, int nparameter, string tablename)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 60;
            for (int i = 0; i < nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            adapter.Fill(ds, tablename);
            dt = ds.Tables[tablename];
            //adapter.Fill(dt);
            return dt;
        }
    }
}
