using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public  class chkConnectDal
    {
        public string LocalIPAddress()
        {
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                    break;
                }
            }
            return localIP;
        }
        //
        public bool chkConnect(string str)
        {
            bool ok = true;
            try
            {
                ////Constructing connection string from the inputs
                StringBuilder Con = new StringBuilder("Data Source=");
                //Con.Append(txtServer.Text + @"\SQLEXPRESS");
                ////Con.Append(LocalIPAddress() + @"\SQLEXPRESS");
                //Con.Append(";Initial Catalog=");
                //Con.Append(txtDatabase.Text + @";Persist Security Info=True; User ID=");
                //Con.Append(txtusername.Text + @";Password=");
                //Con.Append(txtPassWord.Text);
                //Con.Append(";Integrated Security=SSPI;");
                //string strCon = Con.ToString();
                Con.Append(str + @"\SQLVBSP2008;Initial Catalog=VBSPINFOR;Persist Security Info=True;User ID=sa;Password=vbsp@123");
                //\SQLVBSP2008;Initial Catalog=VBSPINFOR;Persist Security Info=True;User ID=sa;Password=vbsp@123
                string strCon = Con.ToString();
               //MessageBox.Show(strCon);
                //Console.Write(strCon);
                updateConfigFile(strCon);
                //Create new sql connection
                //SqlConnection Db = new SqlConnection();
                //to refresh connection string each time else it will use previous connection string
                ConfigurationManager.RefreshSection("connectionStrings");
                // Db.ConnectionString = ConfigurationManager.ConnectionStrings["con"].ToString();
                SqlConnection con = new SqlConnection();
               // string ketnoi = ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
                con.ConnectionString = ConfigurationManager.ConnectionStrings["strcon"].ConnectionString; ; //ConfigurationManager.ConnectionStrings["strcon"].ConnectionString;
                con.Open();
                   
                //To check new connection string is working or not
                // SqlDataAdapter da = new SqlDataAdapter("select * from user", Db);
                //SqlDataAdapter da = new SqlDataAdapter("select * from user");//incase earlier Visualstudios
                //DataTable dt = new DataTable();
                //da.Fill(dt);
                //cboSource.DataSource = dt;
                //cboSource.DisplayMember = "TenNsd";

            }
            catch
            {
                ok = false;
            }
            return ok;
        }

        // doan nay thay doi noi dung file app.config khi run time
        public void updateConfigFile(string con)
        {
            //updating config file
            XmlDocument XmlDoc = new XmlDocument();
            //Loading the Config file
            XmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            foreach (XmlElement xElement in XmlDoc.DocumentElement)
            {
                if (xElement.Name == "connectionStrings")
                {
                    //setting the coonection string
                    xElement.FirstChild.Attributes[2].Value = con;
                }
            }
            //writing the connection string in config file
            XmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

        }
        public string GetPublicIP() // xem IP public
        {
            String direction = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                direction = stream.ReadToEnd();
            }

            //Search for the ip in the html
            int first = direction.IndexOf("Address: ") + 9;
            int last = direction.LastIndexOf("</body>");
            direction = direction.Substring(first, last - first);

            return direction;
        }
    }
}
