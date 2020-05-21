using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
namespace DAL
{
    public class ClsServer
    {
        SqlConnection connect = new SqlConnection();
        OleDbConnection conn = new OleDbConnection();
       //private string str = "strsrv";
        private string str = "strlocal";
       //private string str = "strstrang";
        public bool KiemTraKetNoi()
        {
            bool ok = true;
            string ketnoi = ConfigurationManager.ConnectionStrings[str].ConnectionString;
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
                string ketnoi = ConfigurationManager.ConnectionStrings[str].ConnectionString;
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
        //Load Procedure co tham so nhan gia tri time out 1800
        public DataTable LoadLdbf(string sql, string[] name, object[] value, int nparameter)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 1800;
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
        // update ldbf time out 1800
        public int UpdateLdbf(string sql, string[] name, object[] value, int nparameter)
        {
            SqlCommand command = new SqlCommand(sql, connect);
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 1800;
            for (int i = 0; i < nparameter; i++)
            {
                command.Parameters.AddWithValue(name[i], value[i]);
            }
            return command.ExecuteNonQuery();
        }

        //Load text khong co tham so , doc file foxpro
        public DataTable OleDbDataText(string sql)
        {
            OleDbCommand command = new OleDbCommand(sql, conn);
            command.CommandType = CommandType.Text;
            command.CommandTimeout = 1800;
            OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        public void OleConnect(string dataSource)
        {
            if (conn.State == ConnectionState.Closed)
            {
                string ketnoi = ConfigurationManager.ConnectionStrings[str].ConnectionString;
                conn.ConnectionString = @"Provider=vfpoledb;Data Source=" + dataSource +";Collating Sequence=machine;";
                if (conn.State == ConnectionState.Closed)

                    try
                    {
                        conn.Open();
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

        public void OleDongKetNoi()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
            }
        }

    }
}
