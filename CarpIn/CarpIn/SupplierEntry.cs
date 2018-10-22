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
    public partial class SupplierEntry : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        string query;
        SupplierList sList;
        public SupplierEntry()
        {
            InitializeComponent();
            LoadProvince();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }
        public void LoadProvince()
        {
            cbProvince.Text = "Select Province";
            cbProvince.Items.Add("Eastern Cape");
            cbProvince.Items.Add("Free State");
            cbProvince.Items.Add("Gauteng");
            cbProvince.Items.Add("KwaZulu Natal");
            cbProvince.Items.Add("Limpopo");
            cbProvince.Items.Add("Mpumalanga");
            cbProvince.Items.Add("North West");
            cbProvince.Items.Add("Northern Cape");
            cbProvince.Items.Add("Western Cape");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSupplierID.Text != null)
            {
                try
                {
                    query = "INSERT INTO [Suppliers](SupplierID,SupplierName,Address,Town,Province,[ZipCode],ContactNo,[Email],[Remarks])VALUES('" + this.txtSupplierID.Text.ToString() + "','" + this.txtSupplierName.Text + "','" + this.txtAddress.Text + "','" + this.txtTown.Text + "','" + this.cbProvince.Text + "','" + this.txtCode.Text + "','" + this.txtContactNo.Text + "','" + this.txtEmail.Text + "','" + this.txtRemarks.Text + "');";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    con.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show(txtSupplierID.Text + " saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myReader.Read())
                    {

                    }
                    con.Close();
                    reset();
                    SupplierList sList = new SupplierList();
                    sList.LoadGrid();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter all the required fields!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void reset()
        {
            txtAddress.Clear();
            txtCode.Clear();
            txtContactNo.Clear();
            txtEmail.Clear();
            txtRemarks.Clear();
            txtSupplierID.Clear();
            txtSupplierName.Clear();
            txtTown.Clear();
            cbProvince.Text = "Select Province";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"DELETE FROM [Suppliers] WHERE SupplierID='" + txtSupplierID.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtSupplierID.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sList = new SupplierList();
                sList.LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            SupplierList sList = new SupplierList();
            sList.MdiParent = MdiParent;
            sList.Show();
        }
    }
}
