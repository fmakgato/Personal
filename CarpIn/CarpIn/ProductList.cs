using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpIn
{
    public partial class ProductList : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        ProductEntry pEntry;
        MainForm main;
        public ProductList()
        {
            InitializeComponent();
            LoadGrid();
            txtbyProductCode.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProductList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Products' table. You can move, or remove it, as needed.
            LoadGrid();
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [Products]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvProductList.DataSource = dt.AsDataView();
                myReader = cmd.ExecuteReader();
                //
                while (myReader.Read())
                {
                }

                con.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblGet.Visible == true)
            {
                if (e.RowIndex >= 0)
                {
                    LoadBill();
                }
            }
            else
            {
                if (e.RowIndex >= 0)
                {
                    LoadCustom();
                    DataGridViewRow row = dgvProductList.Rows[e.RowIndex];
                    pEntry = new ProductEntry();
                    pEntry.MdiParent = MdiParent;
                    pEntry.Show();
                    pEntry.txtProductCode.Text = row.Cells["ProductCode"].Value.ToString();
                    pEntry.txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                    pEntry.txtSize.Text = row.Cells["Size"].Value.ToString();
                    pEntry.txtColour.Text = row.Cells["Colour"].Value.ToString();
                    pEntry.txtDescription.Text = row.Cells["Description"].Value.ToString();
                    pEntry.txtCostPrice.Text = row.Cells["CostPrice"].Value.ToString();
                    pEntry.txtSellingPrice.Text = row.Cells["SellingPrice"].Value.ToString();
                    pEntry.txtReorder.Text = row.Cells["ReorderPoint"].Value.ToString();
                    pEntry.txtDiscount.Text = row.Cells["Discount"].Value.ToString();
                    pEntry.txtOpeningStock.Text = row.Cells["OpeningStock"].Value.ToString();
                    pEntry.txtVAT.Text = row.Cells["VAT"].Value.ToString();
                    pEntry.imgProduct.ImageLocation = row.Cells["Image"].Value.ToString();
                    pEntry.btnUpdate.Enabled = true;
                    pEntry.btnSave.Enabled = false;
                    pEntry.btnDelete.Enabled = true;
                }
            }
        }
        private void LoadCustom()
        {
            bool closed = false;
            foreach (Form form in MdiParent.MdiChildren)
            {
                if (form.Name == "ProductEntry")
                {
                    form.Activate();
                    closed = true;
                    break;
                }
            }
            if (!closed)
            {
                pEntry = new ProductEntry();
                pEntry.MdiParent = MdiParent;
                pEntry.FormClosed += PEntry_FormClosed; ;
                pEntry.Show();
            }
        }

        private void PEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            pEntry = null;
        }

        private void LoadBill()
        {
            bool closed = false;
            foreach (Form form in MdiParent.MdiChildren)
            {
                if (form.Name == "Billings")
                {
                    form.Activate();
                    closed = true;
                    break;
                }
            }
            if (!closed)
            {                
                bill.MdiParent = MdiParent;
                bill.FormClosed += Bill_FormClosed; ;
                bill.Show();
            }
        }

        private void Bill_FormClosed(object sender, FormClosedEventArgs e)
        {
            bill = null;
        }

        Billings bill = new Billings();
        public void dialogInfo()
        {
            if (dgvProductList.SelectedRows.Count >= 0)
            {
                DataGridViewRow row = dgvProductList.Rows[dgvProductList.SelectedRows.Count];
                pEntry.txtProductCode.Text = row.Cells["ProductCode"].Value.ToString();
                pEntry.txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                pEntry.txtSize.Text = row.Cells["Size"].Value.ToString();
                pEntry.txtColour.Text = row.Cells["Colour"].Value.ToString();
                pEntry.txtDescription.Text = row.Cells["Description"].Value.ToString();
                pEntry.txtCostPrice.Text = row.Cells["CostPrice"].Value.ToString();
                pEntry.txtSellingPrice.Text = row.Cells["SellingPrice"].Value.ToString();
                pEntry.txtDiscount.Text = row.Cells["Discount"].Value.ToString();
                pEntry.txtOpeningStock.Text = row.Cells["OpeningStock"].Value.ToString();
                pEntry.txtVAT.Text = row.Cells["VAT"].Value.ToString();
                pEntry.imgProduct.ImageLocation = row.Cells["Image"].Value.ToString();
                pEntry.btnUpdate.Enabled = true;
                pEntry.btnSave.Enabled = false;
                pEntry.btnDelete.Enabled = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadGrid();
            txtbyName.Clear();
            txtbyPrice.Clear();
            txtbyProductCode.Clear();
        }

        private void txtbyName_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("ProductName LIKE '%{0}%'", txtbyName.Text);
            dgvProductList.DataSource = dataV;
        }

        private void txtbyPrice_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("Size LIKE '%{0}%'", txtbyPrice.Text);
            dgvProductList.DataSource = dataV;
        }

        private void txtbyProductCode_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("ProductCode LIKE '%{0}%'", txtbyProductCode.Text);
            dgvProductList.DataSource = dataV;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtbyProductCode.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("ProductCode LIKE '%{0}%'", txtbyProductCode.Text);
                dgvProductList.DataSource = dataV;
            }
            else if (txtbyName.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("ProductName LIKE '%{0}%'", txtbyName.Text);
                dgvProductList.DataSource = dataV;
            }
            else if (txtbyPrice.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("Size LIKE '%{0}%'", txtbyPrice.Text);
                dgvProductList.DataSource = dataV;
            }
            else
            {
                MessageBox.Show("Please enter text on Name or Size or Product Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
