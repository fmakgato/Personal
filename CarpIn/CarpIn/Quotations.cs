using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp;
using System.Diagnostics;
using System.Data.OleDb;
using iTextSharp.text.pdf;

namespace CarpIn
{
    public partial class Quotations : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        QuotationsList qList;
        MainForm main;
        ProductList pList = new ProductList();
        DataGridViewRow drow;

        BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\CalibriB.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\Calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        public Quotations()
        {
            InitializeComponent();
            LoadProvince();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Quotations_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Company' table. You can move, or remove it, as needed.
            this.companyTableAdapter.Fill(this.cARPINDATABASEDataSet.Company);
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.QuotationsItems' table. You can move, or remove it, as needed.
            this.quotationsItemsTableAdapter.Fill(this.cARPINDATABASEDataSet.QuotationsItems);
            // TODO: This line of code loads data into the 'cARPINDATABASEDataSet.Company' table. You can move, or remove it, as needed.
            this.companyTableAdapter.Fill(this.cARPINDATABASEDataSet.Company);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtQuoteNo.Text != null)
            {
                if (btnSave.Text != "Print")
                {
                    try
                    {
                        query = "INSERT INTO [Quotations]([QuoteNo],QuoteDate,CustomerID,CustomerName,ContactNo,VAT,[TotalAmount],[GrandTotal]) VALUES('" + this.txtQuoteNo.Text + "','" + this.dtpQuoteDate.Text.ToString() + "','" + this.txtCustomerID.Text + "','" + this.txtCustomerName.Text + "','" + this.txtCustomerContactNo.Text + "','" + this.txtProductVAT.Text + "','" + this.txtProductTotalAmount.Text + "','" + this.txtProductGrandTotal.Text + "');";
                        OleDbCommand cmd = new OleDbCommand(query, con);
                        con.Open();
                        myReader = cmd.ExecuteReader();
                        MessageBox.Show(txtQuoteNo.Text + " saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        DialogResult dResults = MessageBox.Show(txtQuoteNo.Text + " saved successfully. \n" + "Do you want to print?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dResults == DialogResult.Yes)
                        {
                            PrintPdf();

                            while (myReader.Read())
                            {

                            }
                            con.Close();
                            reset();
                            QuotationsList qList = new QuotationsList();
                            qList.LoadGrid();
                        }

                    }
                    catch (Exception ex)
                    {

                        con.Close();
                    } 
                }
                else
                {
                    PrintPdf();
                }
            }
            else
            {
                MessageBox.Show("Please enter all the required fields!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void btnQuoteAddTo_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text == "")
            {
                addTotalAmount();
                addToGrandTotal();
                addDiscount();
            }
            else
            {
                try
                {
                    query = "INSERT INTO [QuotationsItems](QuoteNo,[ProductCode],ProductName,[Size],[Colour],[Price],Quantity,[Amount],[Discount],VAT,TotalAmount,GrandTotal)VALUES('" + this.txtQuoteNo.Text + "','" + this.txtProductCode.Text + "','" + this.txtProductName.Text + "','" + this.txtProductSize.Text + "','" + this.txtProductColour.Text + "','" + this.txtProductPrice.Text + "','" + this.txtProductQuantity.Text + "','" + this.txtProductAmount.Text + "','" + this.txtProductDiscount.Text + "','" + this.txtProductVAT.Text + "','" + this.txtProductTotalAmount.Text + "','" + this.txtProductGrandTotal.Text + "');";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    con.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("Item added to table", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myReader.Read())
                    {

                    }
                    con.Close();
                    resetProduct();
                    LoadGrid();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                } 
            }
        }
        public void reset()
        {
            txtCustomerContactNo.Clear();
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtProductAmount.Clear();
            txtProductCode.Clear();
            txtProductColour.Clear();
            txtProductDiscount.Clear();
            txtProductGrandTotal.Clear();
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductQuantity.Clear();
            txtProductSize.Clear();
            txtProductTotalAmount.Clear();
            txtProductVAT.Clear();
            txtQuoteNo.Clear();
        }
        public void addToGrandTotal()
        {
            float val1;
            float val2;
            float val3;
            float total;
            float vat;
            float result;

            bool validOne = float.TryParse(txtProductTotalAmount.Text, out val1);
            bool validTwo = float.TryParse(txtProductDiscount.Text, out val2);
            bool validThree = float.TryParse(txtProductVAT.Text, out val3);

            if (validOne && validTwo && validThree)
            {
                total = val1 - val2;
                vat = total / 100 * val3;
                result = total + vat;
                txtProductGrandTotal.Text = result.ToString();
            }
        }
        public void addDiscount()
        {
            double disc = 0;
            for (int i = 0; i < dgvQuotations.Rows.Count; i++)
            {
                disc += Convert.ToDouble(dgvQuotations.Rows[i].Cells["Discount"].Value.ToString());
            }
            txtProductDiscount.Text = disc.ToString();
        }
        public void addTotalAmount()
        {
            double amount = 0;
            for (int i = 0; i < dgvQuotations.Rows.Count; i++)
            {
                amount += Convert.ToDouble(dgvQuotations.Rows[i].Cells["Amount"].Value.ToString());
            }
            txtProductTotalAmount.Text = amount.ToString();
        }
        public void resetProduct()
        {
            txtProductCode.Clear();
            txtProductColour.Clear();
            txtProductDiscount.Clear();
            txtProductName.Clear();
            txtProductPrice.Clear();
            txtProductQuantity.Clear();
            txtProductSize.Clear();
            txtProductTotalAmount.Clear();
            txtProductAmount.Clear();
            btnProductAdd.Enabled = true;
        }
        private void txtProductQuantity_TextChanged(object sender, EventArgs e)
        {
            double val1;
            double val2;

            bool validOne = Double.TryParse(txtProductPrice.Text, out val1);
            bool validTwo = Double.TryParse(txtProductQuantity.Text, out val2);

            if (validOne && validTwo)
            {
                double result = val1 * val2;
                txtProductAmount.Text = result.ToString();
            }
        }

        private void txtProductDiscount_TextChanged(object sender, EventArgs e)
        {
            float val1;
            float val2;
            float val3;

            bool validOne = float.TryParse(txtProductAmount.Text, out val1);
            bool validTwo = float.TryParse(txtProductDiscount.Text, out val2);
            bool validThree = float.TryParse(txtProductVAT.Text, out val3);

            if (validOne && validTwo && validThree)
            {
                float total = val1 - val2;
                float vat = total / 100 * val3;
                float result = total + vat;
                txtProductTotalAmount.Text = total.ToString();
                txtProductGrandTotal.Text = result.ToString();
            }
        }

        private void txtProductAmount_TextChanged(object sender, EventArgs e)
        {
            float val1;
            float val2;
            float val3;

            bool validOne = float.TryParse(txtProductAmount.Text, out val1);
            bool validTwo = float.TryParse(txtProductDiscount.Text, out val2);
            bool validThree = float.TryParse(txtProductVAT.Text, out val3);

            if (validOne && validTwo && validThree)
            {
                float total = val1 - val2;
                float vat = total / 100 * val3;
                float result = total + vat;
                txtProductTotalAmount.Text = total.ToString();
                txtProductGrandTotal.Text = result.ToString();
            }
            else
            {
                float total = val1 - val2;
                float vat = total / 100 * val3;
                float result = total + vat;
                txtProductTotalAmount.Text = txtProductAmount.Text;
                txtProductGrandTotal.Text = result.ToString();
            }
        }

        private void txtProductVAT_TextChanged(object sender, EventArgs e)
        {
            float val1;
            float val2;
            float val3;

            bool validOne = float.TryParse(txtProductAmount.Text, out val1);
            bool validTwo = float.TryParse(txtProductDiscount.Text, out val2);
            bool validThree = float.TryParse(txtProductVAT.Text, out val3);

            if (validOne && validTwo && validThree)
            {
                float total = val1 - val2;
                float vat = total / 100 * val3;
                float result = total + vat;
                txtProductTotalAmount.Text = total.ToString();
                txtProductGrandTotal.Text = result.ToString();
            }
        }

        private void btnProductBrowse_Click(object sender, EventArgs e)
        {
            ProductList upList = new ProductList();
            upList.MdiParent = MdiParent;
            upList.Show();
            upList.lblGet.Visible = true;
            lblCollect.Visible = true;
        }

        private void PList_FormClosed(object sender, FormClosedEventArgs e)
        {
            pList = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"DELETE FROM [Quotations] WHERE QuoteNo='" + txtQuoteNo.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtQuoteNo.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                qList = new QuotationsList();
                qList.LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnCustomerBrowse_Click(object sender, EventArgs e)
        {
            CustomerList cList = new CustomerList();
            cList.MdiParent = MdiParent;
            cList.Show();
            cList.lblGet.Visible = true;
            lblCollect.Visible = true;
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            QuotationsList qList = new QuotationsList();
            qList.MdiParent = MdiParent;
            qList.Show();
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [QuotationsItems]";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvQuotations.DataSource = dt.AsDataView();
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
                con.Close();
            }
        }

        private void dgvQuotations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvQuotations.Rows[e.RowIndex];

                txtQuoteNo.Text = row.Cells["QuoteNo"].Value.ToString();
                txtProductCode.Text = row.Cells["ProductCode"].Value.ToString();
                txtProductName.Text = row.Cells["ProductName"].Value.ToString();
                txtProductSize.Text = row.Cells["Size"].Value.ToString();
                txtProductColour.Text = row.Cells["Colour"].Value.ToString();
                txtProductPrice.Text = row.Cells["Price"].Value.ToString();
                txtProductQuantity.Text = row.Cells["Quantity"].Value.ToString();
                txtProductAmount.Text = row.Cells["Amount"].Value.ToString();
                txtProductDiscount.Text = row.Cells["Discount"].Value.ToString();
                txtProductVAT.Text = row.Cells["VAT"].Value.ToString();
                txtProductTotalAmount.Text = row.Cells["TotalAmount"].Value.ToString();
                txtProductGrandTotal.Text = row.Cells["GrandTotal"].Value.ToString();
                btnProductUpdate.Enabled = true;
                btnProductAdd.Enabled = false;
                btnProductRemove.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"UPDATE [Quotations] SET QuoteNo='" + txtQuoteNo.Text + "',QuoteDate='" + this.dtpQuoteDate.Text + "',[CustomerID]='" + this.txtCustomerID.Text + "',[CustomerName]='" + this.txtCustomerName.Text + "',[ContactNo]='" + this.txtCustomerContactNo.Text + "',[ProductCode]='" + this.txtProductCode.Text + "',ProductName='" + this.txtProductName.Text + "',[Size]='" + this.txtProductSize.Text + "',[Colour]='" + this.txtProductColour.Text + "',[Price]='" + this.txtProductPrice.Text + "',[Quantity]='" + this.txtProductQuantity.Text + "',[Amount]='" + this.txtProductAmount.Text + "',[Discount]='" + this.txtProductDiscount.Text + "',VAT='" + this.txtProductVAT.Text + "',TotalAmount='" + this.txtProductTotalAmount.Text + "',GrandTotal='" + this.txtProductGrandTotal.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtQuoteNo.Text + " info updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                qList = new QuotationsList();
                qList.LoadGrid();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnProductUpdate_Click(object sender, EventArgs e)
        {
            addTotalAmount();
            addToGrandTotal();
            addDiscount();
            try
            {
                query = @"UPDATE [QuotationsItems] SET QuoteNo='" + txtQuoteNo.Text + "',[ProductCode]='" + this.txtProductCode.Text + "',ProductName='" + this.txtProductName.Text + "',[Size]='" + this.txtProductSize.Text + "',[Colour]='" + this.txtProductColour.Text + "',[Price]='" + this.txtProductPrice.Text + "',[Quantity]='" + this.txtProductQuantity.Text + "',[Amount]='" + this.txtProductAmount.Text + "',[Discount]='" + this.txtProductDiscount.Text + "',VAT='" + this.txtProductVAT.Text + "',TotalAmount='" + this.txtProductTotalAmount.Text + "',GrandTotal='" + this.txtProductGrandTotal.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtQuoteNo.Text + " info updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnProductAdd.Enabled = true;
                LoadGrid();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnProductRemove_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"DELETE FROM [QuotationsItems] WHERE ProductCode='" + txtProductCode.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtProductCode.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnProductAdd.Enabled = true;
                resetProduct();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnProductReset_Click(object sender, EventArgs e)
        {
            resetProduct();
        }

        private void txtProductPrice_TextChanged(object sender, EventArgs e)
        {
            double val1;
            double val2;

            bool validOne = Double.TryParse(txtProductPrice.Text, out val1);
            bool validTwo = Double.TryParse(txtProductQuantity.Text, out val2);

            if (validOne && validTwo)
            {
                double result = val1 * val2;
                txtProductAmount.Text = result.ToString();
            }
        }

        private void Quotations_Activated(object sender, EventArgs e)
        {
            ProductList plist = new ProductList();
            CustomerList clist = new CustomerList();
            if (lblCollect.Visible == true && btnCustomerBrowse.Focus())
            {
                DataGridViewRow data = clist.dgvCustomerList.Rows[0];
                txtCustomerID.Text = data.Cells["CustomerID"].Value.ToString();
                txtCustomerName.Text = data.Cells["CustomerName"].Value.ToString();
                txtAddress.Text = data.Cells["Address"].Value.ToString() + ", " + data.Cells["Suburb"].Value.ToString() + ", " + data.Cells["Town"].Value.ToString() + ", " + data.Cells["ZipCode"].Value.ToString();
                txtCustomerContactNo.Text = data.Cells["ContactNo"].Value.ToString();
                lblCollect.Visible = false;
                bool closed = false;
                foreach (Form form in MdiParent.MdiChildren)
                {
                    if (form.Name == "CustomerList")
                    {
                        form.Hide();
                        closed = true;
                        break;
                    }
                }
                clist.Hide();
            }
            else if (lblCollect.Visible == true && btnProductBrowse.Focus())
            {
                DataGridViewRow data = plist.dgvProductList.Rows[0];
                txtProductCode.Text = data.Cells["ProductCode"].Value.ToString();
                txtProductName.Text = data.Cells["ProductName"].Value.ToString();
                txtProductColour.Text = data.Cells["Colour"].Value.ToString();
                txtProductSize.Text = data.Cells["Size"].Value.ToString();
                txtProductPrice.Text = data.Cells["SellingPrice"].Value.ToString();
                lblCollect.Visible = false;
                bool closed = false;

                foreach (Form form in MdiParent.MdiChildren)
                {
                    if (form.Name == "ProductList")
                    {
                        form.Hide();
                        closed = true;
                        break;
                    }
                }
                clist.Hide();
            }
        }
        public void PrintPdf()
        {
            try
            {
                int left_margin;
                int top_margin;
                int lastwriteposition = 100;

                //Document creation

                Document doc = new Document(PageSize.A4, 25, 25, 30, 1);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\France\\documents\\visual studio 2017\\Projects\\CarpIn\\CarpIn\\Quotes\\" + txtQuoteNo.Text + ".pdf", FileMode.Create));
                doc.Open();
                //Inserting Image
                //iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance("Pic Name");
                //png.ScalePercent(25f);
                //png.ScaleToFit(50f, 100f);
                //png.SetAbsolutePosition(doc.PageSize.Width -35f -72f, doc.PageSize.Height -36f -213f);
                //Inserting a paragraph
                Paragraph par = new Paragraph("First Page");


                //
                drow = dgvQuotations.Rows[0];
                PdfContentByte cb = writer.DirectContent;
                // Add a footer template to the document
                cb.AddTemplate(PdfFooter(cb, drow), 30, 1);

                cb.BeginText();
                // Start with the invoice type header
                writeText(cb, "Quotation".ToString(), 270, 820, f_cb, 20);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "Quote No", 420, 800, f_cb, 10);
                writeText(cb, txtQuoteNo.Text.ToString(), 500, 800, f_cn, 10);
                writeText(cb, "Quote Date", 420, 788, f_cb, 10);
                writeText(cb, dtpQuoteDate.Text.ToString(), 500, 788, f_cn, 10);
                writeText(cb, "Quote Valid", 420, 776, f_cb, 10);
                writeText(cb, "60 Days", 500, 776, f_cn, 10);

                // Delivery address details headers
                left_margin = 40;
                top_margin = 720;

                writeText(cb, "Quote To:", left_margin, top_margin, f_cb, 14);
                writeText(cb, "Customer Name", left_margin, top_margin - 18, f_cb, 10);
                writeText(cb, "Address", left_margin, top_margin - 30, f_cb, 10);
                writeText(cb, "", left_margin, top_margin - 42, f_cb, 10);
                writeText(cb, "", left_margin, top_margin - 54, f_cb, 10);
                writeText(cb, "Province", left_margin, top_margin - 66, f_cb, 10);

                //Adding the address details
                left_margin = 120;
                top_margin = 720;

                //
                writeText(cb, txtCustomerName.Text, left_margin, top_margin - 18, f_cn, 10);
                writeText(cb, txtAddress.Text, left_margin, top_margin - 30, f_cn, 10);
                writeText(cb, txtCustomerContactNo.Text, left_margin, top_margin - 42, f_cn, 10);
                writeText(cb, txtTown.Text + ", " + txtCode.Text, left_margin, top_margin - 54, f_cn, 10);
                writeText(cb, cbProvince.Text, left_margin, top_margin - 66, f_cn, 10);

                //
                // Write out invoice terms details
                drow = dgvCompany.Rows[0];
                left_margin = 40;
                top_margin = 620;
                writeText(cb, "Account Name", left_margin, top_margin, f_cb, 10);
                writeText(cb, drow.Cells["AccName"].Value.ToString(), left_margin, top_margin - 12, f_cn, 10);
                writeText(cb, "Account No", left_margin + 200, top_margin, f_cb, 10);
                writeText(cb, drow.Cells["AccNo"].Value.ToString(), left_margin + 200, top_margin - 12, f_cn, 10);
                writeText(cb, "Account Type", left_margin + 350, top_margin, f_cb, 10);
                writeText(cb, drow.Cells["AccType"].Value.ToString(), left_margin + 350, top_margin - 12, f_cn, 10);
                // Move down
                left_margin = 40;
                top_margin = 590;
                string sales;
                MainForm main = new MainForm();
                sales = lblUser.Text;
                writeText(cb, "Bank", left_margin, top_margin, f_cb, 10);
                writeText(cb, drow.Cells["Bank"].Value.ToString(), left_margin, top_margin - 12, f_cn, 10);
                writeText(cb, "Branch", left_margin + 200, top_margin, f_cb, 10);
                writeText(cb, drow.Cells["BranchNo"].Value.ToString(), left_margin + 200, top_margin - 12, f_cn, 10);

                cb.EndText();
                //sepatration line
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 570);
                cb.LineTo(560, 570);
                cb.Stroke();

                cb.BeginText();

                top_margin = 550;
                left_margin = 40;

                // Line headers
                writeText(cb, "Item", left_margin + 10, top_margin, f_cb, 10);
                writeText(cb, "Description", left_margin + 60, top_margin, f_cb, 10);
                writeText(cb, "Qty", left_margin + 340, top_margin, f_cb, 10);
                writeText(cb, "Unit Price", left_margin + 380, top_margin, f_cb, 10);
                writeText(cb, "Discount", left_margin + 440, top_margin, f_cb, 10);
                writeText(cb, "Amount", left_margin + 480, top_margin, f_cb, 10);



                //
                //

                // First item line position starts here
                top_margin = 538;

                // Loop thru the table of items and set the linespacing to 12 points.
                // Note that we use the -= operator, the coordinates goes from the bottom of the page!

                //DataGridViewRow row = dgvInvoice.Rows[0];
                foreach (DataGridViewRow drItem in dgvQuotations.Rows)
                {
                    writeText(cb, drItem.Cells["ProductCode"].Value.ToString(), left_margin + 10, top_margin, f_cn, 10);
                    writeText(cb, drItem.Cells["ProductName"].Value.ToString() + " " + drItem.Cells["Colour"].Value.ToString() + " - " + drItem.Cells["Size"].Value.ToString(), left_margin + 60, top_margin, f_cn, 10);
                    writeText(cb, drItem.Cells["Quantity"].Value.ToString(), left_margin + 340, top_margin, f_cn, 10);
                    writeText(cb, "R " + drItem.Cells["Price"].Value.ToString(), left_margin + 380, top_margin, f_cn, 10);
                    writeText(cb, "R " + drItem.Cells["Discount"].Value.ToString(), left_margin + 440, top_margin, f_cn, 10);
                    writeText(cb, "R " + drItem.Cells["Amount"].Value.ToString(), left_margin + 480, top_margin, f_cn, 10);

                    // This is the line spacing, if you change the font size, you might want to change this as well
                    top_margin -= 12;

                    // Implement a page break function, checking if the write position has reached the lastwriteposition
                    if (top_margin <= lastwriteposition)
                    {
                        // We need to end the writing before we change the page
                        cb.EndText();
                        // Make the page break
                        doc.NewPage();
                        // Start the writing again
                        cb.BeginText();
                        // Assign the new write location on page two!
                        // Here you might want to implement a new header function for the new page
                        top_margin = 780;
                    }
                }

                //
                top_margin -= 20;
                left_margin = 380;
                //
                // First the headers
                writeText(cb, "Invoice Total Amount", left_margin, top_margin, f_cb, 10);
                writeText(cb, "VAT Amount", left_margin, top_margin - 12, f_cb, 10);
                writeText(cb, "Grand Total", left_margin, top_margin - 24, f_cb, 10);
                // Move right to write out the values
                left_margin = 540;
                // Write out the invoice currency and values in regular text
                cb.SetFontAndSize(f_cn, 10);

                string curr = "R ".ToString();
                left_margin = 549;
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, curr + txtProductTotalAmount.Text.ToString(), left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "%" + txtProductVAT.Text.ToString(), left_margin, top_margin - 12, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, curr + txtProductGrandTotal.Text.ToString(), left_margin, top_margin - 24, 0);

                // End the writing of text

                cb.EndText();
                doc.Close();
                System.Diagnostics.Process.Start("C:\\Users\\France\\Documents\\Visual Studio 2017\\Projects\\CarpIn\\CarpIn\\Quotes\\" + txtQuoteNo.Text + ".pdf");
                Close();
            }
            catch (Exception rror)
            {
                MessageBox.Show(rror.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //lblMsg.Text = rror.Message;
            }
        }
        private PdfTemplate PdfFooter(PdfContentByte cb, DataGridViewRow drFoot)
        {
            // Create the template and assign height
            PdfTemplate tmpFooter = cb.CreateTemplate(580, 70);
            // Move to the bottom left corner of the template
            tmpFooter.MoveTo(1, 1);
            // Place the footer content
            tmpFooter.Stroke();
            // Begin writing the footer
            tmpFooter.BeginText();
            // Set the font and size
            tmpFooter.SetFontAndSize(f_cb, 8);

            // write the address headers
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Company", 10, 50, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Address", 10, 42, 0);

            drFoot = dgvCompany.Rows[0];
            // Write out details from the payee table
            tmpFooter.SetFontAndSize(f_cn, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["CompanyName"].Value.ToString(), 65, 50, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["Address"].Value.ToString(), 65, 42, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["Suburb"].Value.ToString(), 65, 34, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["Town"].Value.ToString(), 65, 26, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["ZipCode"].Value.ToString() + ", " + drFoot.Cells["Province"].Value.ToString(), 65, 18, 0);

            // Bold text for ther headers
            tmpFooter.SetFontAndSize(f_cb, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Phone", 215, 50, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Email", 215, 42, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Web", 215, 34, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "CIN", 400, 50, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tax Ref", 400, 42, 0);

            // Regular text for infomation fields
            tmpFooter.SetFontAndSize(f_cn, 8);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["ContactNo"].Value.ToString(), 265, 50, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["Email"].Value.ToString(), 265, 42, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["web"].Value.ToString(), 265, 34, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["CIN"].Value.ToString(), 455, 50, 0);
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, drFoot.Cells["TaxRef"].Value.ToString(), 455, 42, 0);

            // End text
            tmpFooter.EndText();
            // Stamp a line above the page footer
            cb.SetLineWidth(0f);
            cb.MoveTo(30, 60);
            cb.LineTo(570, 60);
            cb.Stroke();
            // Return the footer template
            return tmpFooter;
        }
        private void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }

        private void txtQuoteNo_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("QuoteNo LIKE '%{0}%'", txtQuoteNo.Text);
            dgvQuotations.DataSource = dataV;
        }
    }
}
