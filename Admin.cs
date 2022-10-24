using QuanLyQuanAn_FINAL.DAO;
using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanAn_FINAL
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }

        new

        #region methods

                void Load()
        {
            LoadDateTimePickerBill();
            LoadListBillByDate(dateTimePicker1.Value, dateTimePicker2.Value);
            LoadListFood();
        }
        void LoadDateTimePickerBill()
        {
            DateTime today = DateTime.Now;
            dateTimePicker1.Value = new DateTime(today.Year, today.Month, 1);
            dateTimePicker2.Value = dateTimePicker1.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        void LoadListFood()
        {
            dgvFood.DataSource = FoodDAO.Instance.GetListFood();
        }
        #endregion

        #region events
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dateTimePicker1.Value, dateTimePicker2.Value);
        }
        #endregion

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

    }
}
