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
using System.IO;
using System.Drawing.Imaging;

namespace CarpIn
{
    public partial class CustomerEntry : Form
    {        
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        string query;
        int idno = 0;
        CustomerList cList;
        public CustomerEntry()
        {
            InitializeComponent();
            genderadd();
            LoadProvince();
        }
        private void genderadd()
        {
            cbGender.Text = "Select Gender";
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CustomerEntry_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Customers' table. You can move, or remove it, as needed.
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {/*
            byte[] byPicture = null;
            FileStream fStream = new FileStream(lblPath.Text, FileMode.Open, FileAccess.Read);
            BinaryReader bReader = new BinaryReader(fStream);
            byPicture = bReader.ReadBytes((int)fStream.Length);*/
            if (txtCustomerName.Text != null && txtContactNo.Text != null)
            {
                try
                {
                    query = "INSERT INTO [Customers]([CustomerID],[CustomerName],[Gender],[Address],[Town],[Province],[ZipCode],[ContactNo],[Email],[Remarks])VALUES('" + this.txtCustomerID.Text.ToString() + "','" + this.txtCustomerName.Text + "','" + this.cbGender.Text + "','" + this.txtAddress.Text + "','" + this.txtTown.Text + "','" + this.cbProvince.Text + "','" + this.txtZipCode.Text + "','" + this.txtContactNo.Text + "','" + this.txtEmail.Text + "','" + this.txtRemarks.Text + "');";
                    //query = "INSERT INTO [Customers]([CustomerID],[CustomerName],[Gender],[Address],[Town],[Province],[ZipCode],[ContactNo],[Email],[Remarks],[Image])VALUES('" + this.txtCustomerID.Text.ToString() + "','" + this.txtCustomerName.Text + "','" + this.cbGender.Text + "','" + this.txtAddress.Text + "','" + this.txtTown.Text + "','" + this.cbProvince.Text + "','" + this.txtZipCode.Text + "','" + this.txtContactNo.Text + "','" + this.txtEmail.Text + "','" + this.txtRemarks.Text + "',@IMG);";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    con.Open();
                    //cmd.Parameters.Add(new OleDbParameter("@IMG", byPicture));
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show(txtCustomerName.Text + " saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myReader.Read())
                    {

                    }
                    con.Close();
                    reset();
                    UserList upList = new UserList();
                    upList.LoadGrid();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter all the required fields!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                con.Close();
            }
        }
        public void reset()
        {
            txtAddress.Clear();
            txtContactNo.Clear();
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtEmail.Clear();
            txtRemarks.Clear();
            txtSuburb.Clear();
            txtTown.Clear();
            txtZipCode.Clear();
            imgCustomer.Image = null;
            txtPath.Text = "Path:";
            cbGender.Text = "Select Gender";
            cbProvince.Text = "Select Province";
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Filter = "JPG File(*.jpg)|*.jpg|PNG File(*.png)|*.png|All Files(*.*)|*.*";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                string picLocation = fDialog.FileName.ToString();
                imgCustomer.ImageLocation = picLocation;
                txtPath.Text = picLocation;
            }
        }
        public void LoadImage()
        {
            query = "select [Image] from [Customers] where CustomerID='" + txtCustomerID.Text + "'";
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
                imgCustomer.Image = bm;
                String strPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + txtPath.Text;
                bm.Save(strPath, ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"UPDATE [Customers] SET [CustomerName]='" + txtCustomerName.Text + "',[Gender]='" + cbGender.Text + "',[Address]='" + txtAddress.Text + "',[Suburb]='" + txtSuburb.Text + "',[Town]='" + txtTown.Text + "',[Province]='" + cbProvince.Text + "',[ZipCode]='" + txtZipCode.Text + "',[ContactNo]='" + txtContactNo.Text + "',[Email]='" + txtEmail.Text + "',[Remarks]='" + txtRemarks.Text + "' where [CustomerID]='" + txtCustomerID.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtCustomerID.Text + " info updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cList = new CustomerList();
                cList.LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"DELETE FROM [Customers] WHERE CustomerID='" + txtCustomerID.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtCustomerID.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cList = new CustomerList();
                cList.LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            CustomerList cList = new CustomerList();
            cList.MdiParent = MdiParent;
            cList.Show();
        }
    }
}
