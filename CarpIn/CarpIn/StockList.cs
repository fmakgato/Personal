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
    public partial class StockList : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        StockEntry sEntry;
        public StockList()
        {
            InitializeComponent();
            LoadGrid();
            txtbyCode.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StockList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Stock' table. You can move, or remove it, as needed.
            LoadGrid();
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [Stock]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvStockList.DataSource = dt.AsDataView();
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
        private void dgvStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvStockList.Rows[e.RowIndex];
                sEntry = new StockEntry();
                sEntry.MdiParent = MdiParent;
                sEntry.Show();
                sEntry.txtStockID.Text = row.Cells["StockID"].Value.ToString();
                sEntry.dtpStockDate.Text = row.Cells["StockDate"].Value.ToString();
                sEntry.txtSupplierID.Text = row.Cells["SupplierID"].Value.ToString();
                sEntry.txtSupplierName.Text = row.Cells["SupplierName"].Value.ToString();
                sEntry.txtProductCode.Text = row.Cells["ProductCode"].Value.ToString();
                sEntry.txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                sEntry.txtProductQuantity.Text = row.Cells["Quantity"].Value.ToString();
                sEntry.txtProductPrice.Text = row.Cells["PricePerUnit"].Value.ToString();
                sEntry.txtProductTotalAmount.Text = row.Cells["TotalAmount"].Value.ToString();
                sEntry.txtGrandTotal.Text = row.Cells["GrandTotal"].Value.ToString();
                sEntry.txtTotalPayment.Text = row.Cells["TotalPayment"].Value.ToString();
                sEntry.txtTotalDue.Text = row.Cells["TotalDue"].Value.ToString();
                sEntry.btnUpdate.Enabled = true;
                sEntry.btnSave.Enabled = false;
                sEntry.btnDelete.Enabled = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadGrid();
            txtbyName.Clear();
            txtbyCode.Clear();
            dtpDate.ResetText();
        }

        private void txtbyName_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("SupplierName LIKE '%{0}%'", txtbyName.Text);
            dgvStockList.DataSource = dataV;
        }

        private void txtbyCode_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("StockID LIKE '%{0}%'", txtbyCode.Text);
            dgvStockList.DataSource = dataV;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtbyCode.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("StockID LIKE '%{0}%'", txtbyCode.Text);
                dgvStockList.DataSource = dataV;
            }
            else if (txtbyName.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("SupplierName LIKE '%{0}%'", txtbyName.Text);
                dgvStockList.DataSource = dataV;
            }
            else if (dtpDate.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("StockDate LIKE '%{0}%'", dtpDate.Text);
                dgvStockList.DataSource = dataV;
            }
            else
            {
                MessageBox.Show("Please enter text on Name or Prise or Product Code!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("StockDate LIKE '%{0}%'", dtpDate.Text);
            dgvStockList.DataSource = dataV;
        }
    }
}
