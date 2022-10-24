using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn_FINAL.DTO
{
    public class BillInfo
    {
        public BillInfo(int id,int billid,int foodid,int count)
        {
            this.Id = id;
            this.billID = billid;
            this.FoodID = foodid;
            this.count = count;
        }

        public BillInfo(DataRow row)
        {
            this.Id = (int)row["id"];
            this.billID = (int)row["idbill"];
            this.FoodID = (int)row["idfood"];
            this.count = (int)row["count"];
        }


        private int id;
        public int Id { get { return id; }  set { id = value; } }

        private int billID;

        public int BillID { get { return billID; } set {billID = value; } }

        private int foodID;
        public int FoodID { get { return foodID; } set {foodID = value; } }

        private int count;
        public int Count { get { return count; } set { count = value; } }
    }
}
