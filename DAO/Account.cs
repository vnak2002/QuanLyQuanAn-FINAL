using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn_FINAL.DAO
{
    public class Account
    {
        private static Account instance;

        public static Account Instance
        {
            get { if (instance == null) instance = new Account(); return instance; }
            private set { instance = value; }
        }

        public DataRow Item { get; }
        public int Type { get; internal set; }
        public string DisplayName { get; internal set; }
        public string UserName { get; internal set; }

        private Account() { }

        public Account(DataRow item)
        {
            Item = item;
        }

        public bool Login(string userName, string passWord)
        {
            string query = "USP_Login @userName , @passWord";

            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { userName, passWord });

            return result.Rows.Count > 0;
        }
    }
}
