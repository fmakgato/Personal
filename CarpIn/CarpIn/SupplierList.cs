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
    public partial class SupplierList : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        SupplierEntry sEntry;
        public SupplierList()
        {
            InitializeComponent();
            LoadGrid();
            txtbyID.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SupplierList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Suppliers' table. You can move, or remove it, as needed.
            LoadGrid();
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [Suppliers]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvSupplierList.DataSource = dt.AsDataView();
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
        private void dgvSupplierList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSupplierList.Rows[e.RowIndex];
                sEntry = new SupplierEntry();
                sEntry.MdiParent = MdiParent;
                sEntry.Show();
                sEntry.txtSupplierID.Text = row.Cells["SupplierID"].Value.ToString();
                sEntry.txtSupplierName.Text = row.Cells["SupplierName"].Value.ToString();
                sEntry.txtAddress.Text = row.Cells["Address"].Value.ToString();
                sEntry.txtTown.Text = row.Cells["Town"].Value.ToString();
                sEntry.cbProvince.Text = row.Cells["Province"].Value.ToString();
                sEntry.txtCode.Text = row.Cells["ZipCode"].Value.ToString();
                sEntry.txtContactNo.Text = row.Cells["ContactNo"].Value.ToString();
                sEntry.txtEmail.Text = row.Cells["Email"].Value.ToString();
                sEntry.txtRemarks.Text = row.Cells["Remarks"].Value.ToString();
                sEntry.btnUpdate.Enabled = true;
                sEntry.btnSave.Enabled = false;
                sEntry.btnDelete.Enabled = true;
            }
        }

        private void txtbyName_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("SupplierName LIKE '%{0}%'", txtbyName.Text);
            dgvSupplierList.DataSource = dataV;
        }

        private void txtbyContact_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("ContactNo LIKE '%{0}%'", txtbyContact.Text);
            dgvSupplierList.DataSource = dataV;
        }

        private void txtbyID_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("SupplierID LIKE '%{0}%'", txtbyID.Text);
            dgvSupplierList.DataSource = dataV;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtbyID.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("SupplierID LIKE '%{0}%'", txtbyID.Text);
                dgvSupplierList.DataSource = dataV;
            }
            else if (txtbyName.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("SupplierName LIKE '%{0}%'", txtbyName.Text);
                dgvSupplierList.DataSource = dataV;
            }
            else if (txtbyContact.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("ContactNo LIKE '%{0}%'", txtbyContact.Text);
                dgvSupplierList.DataSource = dataV;
            }
            else
            {
                MessageBox.Show("Please enter text on Name or Contact or Supplier ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadGrid();
            txtbyName.Clear();
            txtbyContact.Clear();
            txtbyID.Clear();
        }
    }
}
