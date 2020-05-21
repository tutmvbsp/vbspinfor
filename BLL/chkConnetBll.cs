using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
namespace BLL
{
    public class chkConnetBll
    {
        public string LocalIPAddress()
        {
            //chkConnetBll bll = new chkConnetBll();
            chkConnectDal dal = new chkConnectDal();
            return dal.LocalIPAddress();
        }

        public bool chkConnect(string str)
        {
            chkConnectDal dal = new chkConnectDal();
            return dal.chkConnect(str);
        }

        public string GetPublicIP() // xem IP public
        {
            chkConnectDal dal = new chkConnectDal();
            return dal.GetPublicIP();
        }
    }
}
