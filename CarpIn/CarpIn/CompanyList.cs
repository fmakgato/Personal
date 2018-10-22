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
    public partial class CompanyList : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        CompanyEntry cEntry;
        public CompanyList()
        {
            InitializeComponent();
            LoadGrid();
            txtbyID.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void CompanyList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Company' table. You can move, or remove it, as needed.
            LoadGrid();
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [Company]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvCompanyList.DataSource = dt.AsDataView();
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

        private void dgvCompanyList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCompanyList.Rows[e.RowIndex];
                cEntry = new CompanyEntry();
                cEntry.MdiParent = MdiParent;
                cEntry.Show();
                cEntry.txtCompanyName.Text = row.Cells["CompanyName"].Value.ToString();
                cEntry.txtAddress.Text = row.Cells["Address"].Value.ToString();
                cEntry.txtSuburb.Text = row.Cells["Suburb"].Value.ToString();
                cEntry.txtTown.Text = row.Cells["Town"].Value.ToString();
                cEntry.txtCode.Text = row.Cells["ZipCode"].Value.ToString();
                cEntry.cbProvince.Text = row.Cells["Province"].Value.ToString();
                cEntry.txtContactNo.Text = row.Cells["ContactNo"].Value.ToString();
                cEntry.txtEmail.Text = row.Cells["Email"].Value.ToString();
                cEntry.txtWebsite.Text = row.Cells["TaxRef"].Value.ToString();
                cEntry.txtSiteName.Text = row.Cells["SiteName"].Value.ToString();
                cEntry.txtCIN.Text = row.Cells["CIN"].Value.ToString();
                cEntry.txtWebsite.Text = row.Cells["AccName"].Value.ToString();
                cEntry.txtWebsite.Text = row.Cells["AccNo"].Value.ToString();
                cEntry.txtWebsite.Text = row.Cells["AccType"].Value.ToString();
                cEntry.txtWebsite.Text = row.Cells["Bank"].Value.ToString();
                cEntry.txtWebsite.Text = row.Cells["BranchNo"].Value.ToString();
                cEntry.btnUpdate.Enabled = true;
                cEntry.btnSave.Enabled = false;
                cEntry.btnDelete.Enabled = true;
                Hide();
            }
        }

        private void txtbyName_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("CompanyName LIKE '%{0}%'", txtbyName.Text);
            dgvCompanyList.DataSource = dataV;
        }

        private void txtbyContact_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("ContactNo LIKE '%{0}%'", txtbyContact.Text);
            dgvCompanyList.DataSource = dataV;
        }

        private void txtbyID_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("SiteName LIKE '%{0}%'", txtbyID.Text);
            dgvCompanyList.DataSource = dataV;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtbyID.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("SiteName LIKE '%{0}%'", txtbyID.Text);
                dgvCompanyList.DataSource = dataV;
            }
            else if (txtbyName.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("CompanyName LIKE '%{0}%'", txtbyName.Text);
                dgvCompanyList.DataSource = dataV;
            }
            else if (txtbyContact.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("ContactNo LIKE '%{0}%'", txtbyContact.Text);
                dgvCompanyList.DataSource = dataV;
            }
            else
            {
                MessageBox.Show("Please enter text on Name or Contact or Site!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
