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
    public partial class QuotationsList : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        Quotations qEntry;
        public QuotationsList()
        {
            InitializeComponent();
            LoadGrid();
            txtbyQuoteNo.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void QuotationsList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Quotations' table. You can move, or remove it, as needed.
            LoadGrid();
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [Quotations]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvQuoteList.DataSource = dt.AsDataView();
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
        private void dgvQuoteList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvQuoteList.Rows[e.RowIndex];
                qEntry = new Quotations();
                qEntry.MdiParent = MdiParent;
                qEntry.Show();
                qEntry.txtQuoteNo.Text = row.Cells["QuoteNo"].Value.ToString();
                qEntry.dtpQuoteDate.Text = row.Cells["QuoteDate"].Value.ToString();
                qEntry.txtCustomerID.Text = row.Cells["CustomerID"].Value.ToString();
                qEntry.txtCustomerName.Text = row.Cells["CustomerName"].Value.ToString();
                qEntry.txtAddress.Text = row.Cells["Address"].Value.ToString();
                qEntry.txtSuburb.Text = row.Cells["Suburb"].Value.ToString();
                qEntry.txtTown.Text = row.Cells["Town"].Value.ToString();
                qEntry.cbProvince.Text = row.Cells["Province"].Value.ToString();
                qEntry.txtCode.Text = row.Cells["ZipCode"].Value.ToString();
                qEntry.txtCustomerContactNo.Text = row.Cells["ContactNo"].Value.ToString();               
                qEntry.txtProductVAT.Text = row.Cells["VAT"].Value.ToString();
                qEntry.txtProductTotalAmount.Text = row.Cells["TotalAmount"].Value.ToString();
                qEntry.txtProductGrandTotal.Text = row.Cells["GrandTotal"].Value.ToString();
                qEntry.btnUpdate.Enabled = true;
                qEntry.btnSave.Text = "Print";
                qEntry.btnDelete.Enabled = true;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadGrid();
            txtbyName.Clear();
            txtbyQuoteNo.Clear();
            dtpDate.ResetText();
        }

        private void txtbyName_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("CustomerName LIKE '%{0}%'", txtbyName.Text);
            dgvQuoteList.DataSource = dataV;
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("QuoteDate LIKE '%{0}%'", dtpDate.Text);
            dgvQuoteList.DataSource = dataV;
        }

        private void txtbyQuoteNo_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("QuoteNo LIKE '%{0}%'", txtbyQuoteNo.Text);
            dgvQuoteList.DataSource = dataV;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtbyQuoteNo.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("QuoteNo LIKE '%{0}%'", txtbyQuoteNo.Text);
                dgvQuoteList.DataSource = dataV;
            }
            else if (txtbyName.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("CustomerName LIKE '%{0}%'", txtbyName.Text);
                dgvQuoteList.DataSource = dataV;
            }
            else if (dtpDate.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("QuoteDate LIKE '%{0}%'", dtpDate.Text);
                dgvQuoteList.DataSource = dataV;
            }
            else
            {
                MessageBox.Show("Please enter text on Name or Date or Quote No!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
