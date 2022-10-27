using QuanLyQuanAn_FINAL.DAO;
using QuanLyQuanAn_FINAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WinForms;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Account = QuanLyQuanAn_FINAL.DAO.Account;
using ComboBox = System.Windows.Forms.ComboBox;
using DevExpress.XtraRichEdit.Import.Html;

namespace QuanLyQuanAn_FINAL
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();
        BindingSource accountList = new BindingSource();
        public Account loginAccount;
        public fAdmin()
        {
            InitializeComponent();
            Load();
        }

        new


        #region methods

                void Load()
        {
            dgvFood.DataSource = foodList;
            dgvAccount.DataSource = accountList;

            LoadDateTimePickerBill();
            LoadListBillByDate(dateTimePicker1.Value, dateTimePicker2.Value);
            LoadCategoryIntoCombobox(cbCategory);
            LoadListFood();
            LoadAccount();
            LoadListTableFood();
            LoadListCatrgory();
            AddFoodBinding();
            AddAccountBinding();
            AddCategoryBinding();
            AddTableBinding();
        }

        void AddAccountBinding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            cmbAccountType.DataBindings.Add(new Binding("Text", dgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));
        }
        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = FoodDAO.Instance.SearchFoodByName(name);


            return listFood;
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

        void AddFoodBinding()
        {
            txbName.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbID.DataBindings.Add(new Binding("Text", dgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmPrice.DataBindings.Add(new Binding("Value", dgvFood.DataSource, "Price", true, DataSourceUpdateMode.Never));
        }

        void AddCategoryBinding()
        {
            txbCategoryName.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbIDcategory.DataBindings.Add(new Binding("Text", dgvCategory.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }
        void AddTableBinding()
        {
            txbTableName.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "Name", true, DataSourceUpdateMode.Never));
            txbIDTable.DataBindings.Add(new Binding("Text", dgvTable.DataSource, "ID", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        
        
        void LoadListTableFood()
        {
            dgvTable.DataSource = TableDAO.Instance.GetListTable();
        }
        void LoadListCatrgory()
        {
            dgvCategory.DataSource = CategoryDAO.Instance.GetListCategory1();
        }
        void AddAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.InsertAccount(userName, displayName, type))
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }

            LoadAccount();
        }

        void EditAccount(string userName, string displayName, int type)
        {
            if (AccountDAO.Instance.UpdateAccount(userName, displayName, type))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }

            LoadAccount();
        }

        void DeleteAccount(string userName)
        {
            if (loginAccount.UserName.Equals(userName))
            {
                MessageBox.Show("Tài khoản hiện không xóa được");
                return;
            }
            if (AccountDAO.Instance.DeleteAccount(userName))
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }

            LoadAccount();
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

        private void txbID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);

                    cbCategory.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbCategory.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbCategory.SelectedIndex = index;
                }
            }
            catch { }
        }
        private void txbSearching_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            foodList.DataSource = SearchFoodByName(txbSearching.Text);
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbName.Text;
            int categoryID = (cbCategory.SelectedItem as Category).ID;
            float price = (float)nmPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm thành công");
                LoadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
                    
            }
            else
            {
                MessageBox.Show("Nhập sai");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            string name = txbName.Text;
            int categoryID = (cbCategory.SelectedItem as Category).ID;
            float price = (float)nmPrice.Value;
            int id = Convert.ToInt32(txbID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa thành công");
                LoadListFood();
                if (updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Nhập sai");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }


        

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = cmbAccountType.SelectedIndex;

            AddAccount(userName, displayName, type);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;

            DeleteAccount(userName);
        }

        private void btnUpdateAccount_Click(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string displayName = txbDisplayName.Text;
            int type = (int)cmbAccountType.SelectedIndex;

            EditAccount(userName, displayName, type);
        }       

       

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;

            if (CategoryDAO.Instance.InsertCategory(name))
            {
                MessageBox.Show("Thêm thành công");
                LoadListCatrgory();               
            }
            else
            {
                MessageBox.Show("Nhập sai");
            }
        }

        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDcategory.Text);

            if (CategoryDAO.Instance.DeleteCategory(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListCatrgory();               
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string name = txbCategoryName.Text;  
            int id = Convert.ToInt32(txbIDcategory.Text);

            if (CategoryDAO.Instance.UpdateCategory(name, id))
            {
                MessageBox.Show("Sửa thành công");
                LoadListCatrgory();
                
            }
            else
            {
                MessageBox.Show("Nhập sai");
            }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;

            if (TableDAO.Instance.InsertTable(name))
            {
                MessageBox.Show("Thêm thành công");
                LoadListTableFood();
            }
            else
            {
                MessageBox.Show("Nhập sai");
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDTable.Text);

            if (TableDAO.Instance.DeleteTable(id))
            {
                MessageBox.Show("Xóa thành công");
                LoadListTableFood();
            }
            else
            {
                MessageBox.Show("Lỗi");
            }
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            string name = txbTableName.Text;
            int id = Convert.ToInt32(txbIDcategory.Text);

            if (TableDAO.Instance.UpdateTable(name, id))
            {
                MessageBox.Show("Sửa thành công");
                LoadListTableFood();

            }
            else
            {
                MessageBox.Show("Nhập sai");
            }
        }
    }
}
