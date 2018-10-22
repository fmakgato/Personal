using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Imaging;
using System.IO;

namespace CarpIn
{
    public partial class CompanyEntry : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        CompanyList cList = new CompanyList();
        public CompanyEntry()
        {
            InitializeComponent();
            LoadProvince();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void CompanyEntry_Load(object sender, EventArgs e)
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
                dgvCompany.DataSource = dt.AsDataView();
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                query = "INSERT INTO [Company]([CompanyName],[Address],[Suburb],[Town],[ZipCode],[Province],[ContactNo],[Email],[SiteName],[TaxRef],[CIN],[AccName],[AccNo],[AccType],[Bank],[BranchNo])" +
                    "VALUES('" + txtCompanyName.Text + "','" + txtAddress.Text + "','" + txtSuburb.Text + "','" + txtTown.Text + "','" + txtCode.Text + "','" + cbProvince.Text + "','" + txtContactNo.Text + "','" + txtEmail.Text + "','" + txtWebsite.Text + "','" + txtSiteName.Text + "','" + txtTaxRef.Text + "','" + txtCIN.Text + "','" + txtAccName.Text + "','" + txtAccNo.Text + "','" + txtAccType.Text + "','" + txtBank.Text + "','" + txtBranchNo.Text + "')";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show(txtCompanyName.Text + " saved sucessfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                while (myReader.Read())
                {

                }
                con.Close();
                cList.LoadGrid();
                LoadGrid();
                resetAll();

            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void resetAll()
        {
            txtAddress.Clear();
            txtCIN.Clear();
            txtCompanyName.Clear();
            txtContactNo.Clear();
            txtEmail.Clear();
            txtSiteName.Clear();
            txtWebsite.Clear();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            resetAll();
        }
        private void btnGetData_Click(object sender, EventArgs e)
        {
            CompanyList cList = new CompanyList();
            cList.MdiParent = MdiParent;
            cList.Show();
            Hide();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"UPDATE [Company] SET [CompanyName]='" + txtCompanyName.Text + "',[Address]='" + txtAddress.Text + "',[Suburb]='" + txtSuburb.Text + "',[Town]='" + txtTown.Text + "',[ZipCode]='" + txtCode.Text + "',[Province]='" + cbProvince.Text + "',[ContactNo]='" + txtContactNo.Text + "',[Email]='" + txtEmail.Text + "',[SiteName]='" + txtSiteName.Text + "',[TaxRef]='" + txtWebsite.Text + "',[CIN]='" + txtCIN.Text + "',[AccName]='" + txtAccName.Text + "',[AccNo]='" + txtAccNo.Text + "',[AccType]='" + txtAccType.Text + "',[Bank]='" + txtBank.Text + "',[BranchNo]='" + txtBranchNo.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtCompanyName.Text + " info updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cList.LoadGrid();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"DELETE FROM [Company] WHERE CompanyName='" + txtCompanyName.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtCompanyName.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cList = new CompanyList();
                cList.LoadGrid();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Filter = "JPG File(*.jpg)|*.jpg|PNG File(*.png)|*.png|All Files(*.*)|*.*";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                string picLocation = fDialog.FileName.ToString();
                imgCompany.ImageLocation = picLocation;
                txtPath.Text = picLocation;
            }
            SaveImage();
        }

        private void dgvCompany_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvCompany.Rows[e.RowIndex];
                txtCompanyName.Text = row.Cells["CompanyName"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtSuburb.Text = row.Cells["Suburb"].Value.ToString();
                txtTown.Text = row.Cells["Town"].Value.ToString();
                txtCode.Text = row.Cells["ZipCode"].Value.ToString();
                cbProvince.Text = row.Cells["Province"].Value.ToString();
                txtContactNo.Text = row.Cells["ContactNo"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtWebsite.Text = row.Cells["Web"].Value.ToString();
                txtTaxRef.Text = row.Cells["TaxRef"].Value.ToString();
                txtSiteName.Text = row.Cells["SiteName"].Value.ToString();
                txtCIN.Text = row.Cells["CIN"].Value.ToString();
                txtAccName.Text = row.Cells["AccName"].Value.ToString();
                txtAccNo.Text = row.Cells["AccNo"].Value.ToString();
                txtAccType.Text = row.Cells["AccType"].Value.ToString();
                txtBank.Text = row.Cells["Bank"].Value.ToString();
                txtBranchNo.Text = row.Cells["BranchNo"].Value.ToString();
                btnUpdate.Enabled = true;
                btnSave.Enabled = false;
                btnDelete.Enabled = true;
            }
        }
        public void SaveImage()
        {
            //imgCompany.Image.Save(Application.StartupPath + "Images\\Logo", ImageFormat.Jpeg);
            imgCompany.ImageLocation = txtPath.Text;
            Bitmap bm = new Bitmap(imgCompany.ImageLocation);
            imgCompany.Image = bm;
            String strPath = Application.StartupPath + "\\Images\\Logo";
            bm.Save(strPath, ImageFormat.Jpeg);
        }
    }
}
