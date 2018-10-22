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
    public partial class StockEntry : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        string query;
        StockList sList;
        public StockEntry()
        {
            InitializeComponent();
            autoNumbe();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StockEntry_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Stock' table. You can move, or remove it, as needed.

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtStockID.Text != null)
            {
                try
                {
                    string queryadd = "update [Products] set [OpeningStock]=OpeningStock + '" + Convert.ToInt32(txtProductQuantity.Text) + "' where ProductCode='" + txtProductCode.Text + "' ";
                    query = "INSERT INTO [Stock](StockID,StockDate,SupplierID,SupplierName,ProductCode,[ProductName],[Quantity],[PricePerUnit],[TotalAmount],GrandTotal,TotalPayment,TotalDue)VALUES('" + this.txtStockID.Text + "','" + this.dtpStockDate.Text + "','" + this.txtSupplierID.Text + "','" + this.txtSupplierName.Text + "','" + this.txtProductCode.Text + "','" + this.txtProductName.Text + "','" + this.txtProductQuantity.Text + "','" + this.txtProductPrice.Text + "','" + this.txtProductTotalAmount.Text + "','" + this.txtGrandTotal.Text + "','" + this.txtTotalPayment.Text + "','" + this.txtTotalDue.Text + "');";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    OleDbCommand cmd1 = new OleDbCommand(queryadd, con);
                    con.Open();
                    myReader = cmd.ExecuteReader();
                    myReader = cmd1.ExecuteReader();
                    MessageBox.Show(txtStockID.Text + " saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myReader.Read())
                    {

                    }
                    con.Close();
                    reset();
                    StockList sList = new StockList();
                    sList.LoadGrid();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("Please enter all the required fields!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void reset()
        {
            txtGrandTotal.Clear();
            txtProductCode.Clear();
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductQuantity.Clear();
            txtProductTotalAmount.Clear();
            txtStockID.Clear();
            txtSupplierID.Clear();
            txtSupplierName.Clear();
            txtTotalDue.Clear();
            txtTotalPayment.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"UPDATE [Stock] SET StockID='" + txtStockID.Text + "',StockDate='" + dtpStockDate.Text + "',SupplierID='" + txtSupplierID.Text + "',SupplierName='" + txtSupplierName.Text + "',ProductCode='" + txtProductCode.Text + "',[ProductName]='" + txtProductName.Text + "',[Quantity]='" + txtProductQuantity.Text + "',[PricePerUnit]='" + txtProductPrice.Text + "',[TotalAmount]='" + txtProductTotalAmount.Text + "',GrandTotal='" + txtGrandTotal.Text + "',TotalPayment='" + txtTotalPayment.Text + "',TotalDue='" + txtTotalDue.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show("Stock " + txtStockID.Text + " updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ProductList pList = new ProductList();
                pList.LoadGrid();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtProductQuantity_TextChanged(object sender, EventArgs e)
        {
            double val1;
            double val2;
            double val3;

            bool validOne = Double.TryParse(txtProductPrice.Text, out val1);
            bool validTwo = Double.TryParse(txtProductQuantity.Text, out val2);
            bool validThree = Double.TryParse(txtTotalPayment.Text, out val3);

            if (validOne && validTwo && validThree)
            {
                double result = val1 * val2;
                double total = result - val3;
                txtProductTotalAmount.Text = result.ToString();
                txtTotalDue.Text = total.ToString();

            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void txtProductTotalAmount_TextChanged(object sender, EventArgs e)
        {
            float val1;

            bool validOne = float.TryParse(txtProductTotalAmount.Text, out val1);

            if (validOne)
            {
                float result = val1;
                txtGrandTotal.Text = result.ToString();
            }
        }

        private void txtTotalPayment_TextChanged(object sender, EventArgs e)
        {
            double val1;
            double val2;

            bool validOne = Double.TryParse(txtGrandTotal.Text, out val1);
            bool validTwo = Double.TryParse(txtTotalPayment.Text, out val2);

            if (validOne && validTwo)
            {
                double result = val1 - val2;
                txtTotalDue.Text = result.ToString();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"DELETE FROM [Stock] WHERE StockID='" + txtStockID.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtStockID.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                sList = new StockList();
                sList.LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            StockList sList = new StockList();
            sList.MdiParent = MdiParent;
            sList.Show();
        }
        public void autoNumbe()
        {
            query = "select MAX(StockID) from [Stock]";
            OleDbCommand cmd = new OleDbCommand(query, con);
            con.Open();
            myReader = cmd.ExecuteReader();
            while (myReader.Read())
            {

            }
            txtStockID.Text = myReader.ToString();
            con.Close();
        }

        private void btnProductRemove_Click(object sender, EventArgs e)
        {
            
        }
    }
}
