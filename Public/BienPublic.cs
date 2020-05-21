using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Public
{
    public class BienPublic
    {
        private string _dbName;
        private string _dbSource;
        private string _user;
        private string _pass;

        public string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }

        public string DbSource
        {
            get { return _dbSource; }
            set { _dbSource = value; }
        }

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }

        public string Pass
        {
            get { return _pass; }
            set { _pass = value; }
        }


    }
}
