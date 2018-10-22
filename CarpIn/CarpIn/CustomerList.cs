using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpIn
{
    public partial class CustomerList : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        CustomerEntry cEntry;
        Billings bill = new Billings();
        public CustomerList()
        {
            InitializeComponent();
            LoadGrid();
            txtbyID.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CustomerList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.cARPINDATABASEDataSet.Customers);
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Customers' table. You can move, or remove it, as needed.
            LoadGrid();
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [Customers]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvCustomerList.DataSource = dt.AsDataView();
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
        public void dgvCustomerList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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
                    DataGridViewRow row = dgvCustomerList.Rows[e.RowIndex];
                    cEntry = new CustomerEntry();
                    cEntry.MdiParent = MdiParent;
                    cEntry.Show();
                    cEntry.txtCustomerID.Text = row.Cells["CustomerID"].Value.ToString();
                    cEntry.txtCustomerName.Text = row.Cells["CustomerName"].Value.ToString();
                    cEntry.cbGender.Text = row.Cells["Gender"].Value.ToString();
                    cEntry.txtAddress.Text = row.Cells["Address"].Value.ToString();
                    cEntry.txtSuburb.Text = row.Cells["Suburb"].Value.ToString();
                    cEntry.txtTown.Text = row.Cells["Town"].Value.ToString();
                    cEntry.cbProvince.Text = row.Cells["Province"].Value.ToString();
                    cEntry.txtZipCode.Text = row.Cells["ZipCode"].Value.ToString();
                    cEntry.txtContactNo.Text = row.Cells["ContactNo"].Value.ToString();
                    cEntry.txtEmail.Text = row.Cells["Email"].Value.ToString();
                    cEntry.txtRemarks.Text = row.Cells["Remarks"].Value.ToString();
                    //cEntry.imgCustomer.ImageLocation = row.Cells["Image"].Value.ToString();
                    cEntry.btnUpdate.Enabled = true;
                    cEntry.btnSave.Enabled = false;
                    cEntry.btnDelete.Enabled = true;
                    Hide();
                }
            }
        }
        public void LoadImage()
        {
            query = "select [Image] from [Customers] where CustomerID='" + cEntry.txtCustomerID.Text + "'";
            Byte[] byPicture;
            OleDbCommand cmd = new OleDbCommand(query, con);
            try
            {
                con.Open();
                byPicture = (Byte[])cmd.ExecuteScalar();
                con.Close();
                MemoryStream ms = new MemoryStream();
                Bitmap bm;
                ms.Write(byPicture, 78, byPicture.Length);
                bm = new Bitmap(ms);
                cEntry.imgCustomer.Image = bm;
                String strPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + cEntry.txtPath.Text;
                bm.Save(strPath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtbyName_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("CustomerName LIKE '%{0}%'", txtbyName.Text);
            dgvCustomerList.DataSource = dataV;
        }

        private void txtbyContact_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("ContactNo LIKE '%{0}%'", txtbyContact.Text);
            dgvCustomerList.DataSource = dataV;
        }

        private void txtbyID_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("CustomerID LIKE '%{0}%'", txtbyID.Text);
            dgvCustomerList.DataSource = dataV;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtbyID.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("CustomerID LIKE '%{0}%'", txtbyID.Text);
                dgvCustomerList.DataSource = dataV;
            }
            else if (txtbyName.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("CustomerName LIKE '%{0}%'", txtbyName.Text);
                dgvCustomerList.DataSource = dataV;
            }
            else if (txtbyContact.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("ContactNo LIKE '%{0}%'", txtbyContact.Text);
                dgvCustomerList.DataSource = dataV;
            }
            else
            {
                MessageBox.Show("Please enter text on Name or Contact or Customer ID!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                bill.FormClosed += Bill_FormClosed;
                bill.Show();
            }
        }

        private void Bill_FormClosed(object sender, FormClosedEventArgs e)
        {
            bill = null;
        }
        private void LoadCustom()
        {
            bool closed = false;
            foreach (Form form in MdiParent.MdiChildren)
            {
                if (form.Name == "CustomerEntry")
                {
                    form.Activate();
                    closed = true;
                    break;
                }
            }
            if (!closed)
            {
                cEntry = new CustomerEntry();
                cEntry.MdiParent = MdiParent;
                cEntry.FormClosed += CEntry_FormClosed;
                cEntry.Show();
            }
        }

        private void CEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            cEntry = null;
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
