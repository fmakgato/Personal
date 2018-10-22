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
    public partial class BillingList : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        Billings bList;
        public BillingList()
        {
            InitializeComponent();
            LoadGrid();
            txtbyNo.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BillingList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Billings' table. You can move, or remove it, as needed.
            LoadGrid();
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [Billings]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvInvoiceList.DataSource = dt.AsDataView();
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

        private void dgvInvoiceList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvInvoiceList.Rows[e.RowIndex];

                bList = new Billings();
                bList.MdiParent = MdiParent;
                bList.Show();
                bList.txtInvoiceNo.Text = row.Cells["InvoiceNo"].Value.ToString();
                bList.dtpInvoiceDate.Text = row.Cells["InvoiceDate"].Value.ToString();
                bList.txtCustomerID.Text = row.Cells["CustomerID"].Value.ToString();
                bList.txtCustomerName.Text = row.Cells["CustomerName"].Value.ToString();
                bList.txtAddress.Text = row.Cells["Address"].Value.ToString();
                bList.txtSuburb.Text = row.Cells["Suburb"].Value.ToString();
                bList.txtTown.Text = row.Cells["Town"].Value.ToString();
                bList.cbProvince.Text = row.Cells["Province"].Value.ToString();
                bList.txtCode.Text = row.Cells["ZipCode"].Value.ToString();
                bList.txtCustomerContactNo.Text = row.Cells["ContactNo"].Value.ToString();
                bList.txtProductVAT.Text = row.Cells["VAT"].Value.ToString();
                bList.txtProductTotalAmount.Text = row.Cells["TotalAmount"].Value.ToString();
                bList.txtProductGrandTotal.Text = row.Cells["GrandTotal"].Value.ToString();
                bList.txtProductTotalPayment.Text = row.Cells["TotalPayment"].Value.ToString();
                bList.txtProductPaymentDue.Text = row.Cells["PaymentDue"].Value.ToString();
                bList.cbPaymentMode.Text = row.Cells["PaymentMode"].Value.ToString();
                bList.txtPaymentAmmount.Text = row.Cells["PaymentAmount"].Value.ToString();
                bList.dtpPaymentDate.Text = row.Cells["PaymentDate"].Value.ToString();
                bList.btnUpdate.Enabled = true;
                bList.btnSave.Text = "Print";
                bList.btnDelete.Enabled = true;

            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadGrid();
            txtbyName.Clear();
            txtbyNo.Clear();
            dtpbyDate.ResetText();
        }

        private void txtbyName_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("CustomerName LIKE '%{0}%'", txtbyName.Text);
            dgvInvoiceList.DataSource = dataV;
        }

        private void dtpbyDate_ValueChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("select * from [Billings] where InvoiceDate between '{0}' and '{1}'", DateTime.Now, dtpbyDate.Text);
            //dataV.RowFilter = string.Format("InvoiceDate LIKE '%{0}%'", dtpbyDate.Text);
            dgvInvoiceList.DataSource = dataV;
        }

        private void txtbyNo_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("InvoiceNo LIKE '%{0}%'", txtbyNo.Text);
            dgvInvoiceList.DataSource = dataV;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtbyNo.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("InvoiceNo LIKE '%{0}%'", txtbyNo.Text);
                dgvInvoiceList.DataSource = dataV;
            }
            else if (txtbyName.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("CustomerName LIKE '%{0}%'", txtbyName.Text);
                dgvInvoiceList.DataSource = dataV;
            }
            else if (dtpbyDate.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("InvoiceDate LIKE '%{0}%'", dtpbyDate.Text);
                dgvInvoiceList.DataSource = dataV;
            }
            else
            {
                MessageBox.Show("Please enter text on Name or Date or Invoice No!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
