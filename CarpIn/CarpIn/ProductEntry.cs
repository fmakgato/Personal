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
    public partial class ProductEntry : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        string query;

        ProductList pList = new ProductList();
        public ProductEntry()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void ProductEntry_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Products' table. You can move, or remove it, as needed.
            generateID();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text != null)
            {
                try
                {
                    query = "INSERT INTO [Products](ProductCode,ProductName,[Size],[Colour],[Description],[CostPrice],SellingPrice,ReorderPoint,OpeningStock,[Discount],VAT)VALUES('" + txtProductCode.Text + "','" + this.txtProductName.Text + "','" + this.txtSize.Text + "','" + this.txtColour.Text + "','" + this.txtDescription.Text + "','" + this.txtCostPrice.Text + "','" + this.txtSellingPrice.Text + "','" + this.txtReorder.Text + "','" + this.txtOpeningStock.Text + "','" + this.txtDiscount.Text + "','" + this.txtVAT.Text + "');";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    con.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show(txtProductCode.Text + " saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myReader.Read())
                    {

                    }
                    generateID();
                    con.Close();
                    reset();
                    ProductList pList = new ProductList();
                    pList.LoadGrid();
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
            txtColour.Clear();
            txtCostPrice.Clear();
            txtDescription.Clear();
            txtDiscount.Clear();
            txtOpeningStock.Clear();
            txtProductCode.Clear();
            txtProductName.Clear();
            txtReorder.Clear();
            txtSellingPrice.Clear();
            txtSize.Clear();
            txtVAT.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"UPDATE [Products] SET ProductCode='" + txtProductCode.Text + "',ProductName='" + txtProductName.Text + "',[Size]='" + txtSize.Text + "',[Colour]='" + txtColour.Text + "',[Description]='" + txtDescription.Text + "',[CostPrice]='" + txtCostPrice.Text + "',SellingPrice='" + txtSellingPrice.Text + "',ReorderPoint='" + txtReorder.Text + "',OpeningStock='" + txtOpeningStock.Text + "',[Discount]='" + txtDiscount.Text + "',VAT='" + txtVAT.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show("User info updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pList.LoadGrid();
                Close();
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
                query = @"DELETE FROM [Products] WHERE ProductCode='" + txtProductCode.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtProductCode.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                pList = new ProductList();
                pList.LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void generateID()
        {
            try
            {
                int num;
                string queryadd;
                //query = "SELECT [ProductCode] isnull(max(cast(RIGHT(ProductCode) as int)))+ 1 FROM Products";
                //queryadd = "select max ('P' + RIGHT('000' + CAST(ProductCode as string)))";
                con.Open();
                query = "select max (ProductCode) from [Products]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                myReader = cmd.ExecuteReader();
                int val = Convert.ToInt32(myReader[0]);
                if (myReader.Read())
                {
                    if (val <= -1)
                    {
                        txtProductCode.Text = "P0001";
                        con.Close();
                    }
                    else
                    {
                        num = Convert.ToInt32(myReader[0].ToString());
                        txtProductCode.Text = num.ToString() + 1;
                        con.Close();
                    } 
                }

                txtProductName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to generate Invoice No..!" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            ProductList pList = new ProductList();
            pList.MdiParent = MdiParent;
            pList.Show();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fDialog = new OpenFileDialog();
            fDialog.Filter = "JPG File(*.jpg)|*.jpg|PNG File(*.png)|*.png|All Files(*.*)|*.*";
            if (fDialog.ShowDialog() == DialogResult.OK)
            {
                string picLocation = fDialog.FileName.ToString();
                imgProduct.ImageLocation = picLocation;
                txtPath.Text = picLocation;
            }
        }
    }
}
