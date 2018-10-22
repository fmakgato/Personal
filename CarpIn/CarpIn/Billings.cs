using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.OleDb;

namespace CarpIn
{
    public partial class Billings : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        string query;
        DataTable dt;
        BillingList bList;
        DataGridViewRow drow;


        BaseFont f_cb = BaseFont.CreateFont("c:\\windows\\fonts\\CalibriB.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
        BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\Calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);

        public Billings()
        {
            InitializeComponent();
            PayMode();
            LoadProvince();
            LoadCompany();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void Billings_Load(object sender, EventArgs e)
        {
            generateID();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtInvoiceNo.Text != null)
            {
                if (btnSave.Text != "Print")
                {
                    try
                    {
                        query = "INSERT INTO [Billings](InvoiceNo,InvoiceDate,[CustomerID],[CustomerName],[ContactNo],VAT,TotalAmount,GrandTotal,TotalPayment,PaymentDue,PaymentMode,PaymentAmount,PaymentDate)VALUES('" + txtInvoiceNo.Text + "','" + this.dtpInvoiceDate.Text + "','" + this.txtCustomerID.Text + "','" + this.txtCustomerName.Text + "','" + this.txtCustomerContactNo.Text + "','" + this.txtProductVAT.Text + "','" + this.txtProductTotalAmount.Text + "','" + this.txtProductGrandTotal.Text + "','" + this.txtProductTotalPayment.Text + "','" + this.txtProductPaymentDue.Text + "','" + this.cbPaymentMode.Text + "','" + this.txtPaymentAmmount.Text + "','" + this.dtpPaymentDate.Text + "');";
                        OleDbCommand cmd = new OleDbCommand(query, con);
                        con.Open();
                        myReader = cmd.ExecuteReader();
                        DialogResult dResults = MessageBox.Show(txtInvoiceNo.Text + " saved successfully. \n" + "Do you want to print?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dResults == DialogResult.Yes)
                        {
                            PrintPdf();

                            while (myReader.Read())
                            {

                            }
                            con.Close();
                            reset();
                            generateID();
                            ProductList pList = new ProductList();
                            pList.LoadGrid();
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }
        public void reset()
        {
            txtCustomerContactNo.Clear();
            txtCustomerID.Clear();
            txtCustomerName.Clear();
            txtInvoiceNo.Clear();
            txtPaymentAmmount.Clear();
            txtProductAmount.Clear();
            txtProductCode.Clear();
            txtProductColour.Clear();
            txtProductDiscount.Clear();
            txtProductGrandTotal.Clear();
            txtProductName.Clear();
            txtProductPaymentDue.Clear();
            txtProductPrice.Clear();
            txtProductQuantity.Clear();
            txtProductSize.Clear();
            txtProductTotalAmount.Clear();
            txtProductTotalPayment.Clear();
            txtProductVAT.Text = "14";
            cbPaymentMode.Text = "Select Mode";
            dgvInvoice.DataSource = null;
        }

        public void resetProduct()
        {
            txtProductCode.Clear();
            txtProductColour.Clear();
            txtProductDiscount.Text = "0";
            txtProductName.Clear();
            txtProductPrice.Text = "0";
            txtProductQuantity.Text = "0";
            txtProductSize.Clear();
            txtProductAmount.Text = "0";
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"UPDATE [Billings] SET InvoiceNo='" + txtInvoiceNo.Text + "',InvoiceDate='" + this.dtpInvoiceDate.Text + "',[CustomerID]='" + this.txtCustomerID.Text + "',[CustomerName]='" + this.txtCustomerName.Text + "',[ContactNo]='" + this.txtCustomerContactNo.Text + "',VAT='" + this.txtProductVAT.Text + "',TotalAmount='" + this.txtProductTotalAmount.Text + "',GrandTotal='" + this.txtProductGrandTotal.Text + "',TotalPayment='" + this.txtProductTotalPayment.Text + "',PaymentDue='" + this.txtProductPaymentDue.Text + "',PaymentMode='" + this.cbPaymentMode.Text + "',PaymentAmount='" + this.txtPaymentAmmount.Text + "',PaymentDate='" + dtpPaymentDate.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtInvoiceNo.Text + " info updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //upList = new UserList();
                //upList.LoadGrid();
                Close();
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
                query = @"DELETE FROM [Billings] WHERE InvoiceNo='" + txtInvoiceNo.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtInvoiceNo.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                bList = new BillingList();
                LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnCustomerBrowser_Click(object sender, EventArgs e)
        {
            CustomerList cList = new CustomerList();
            cList.MdiParent = MdiParent;
            cList.Show();
            cList.lblGet.Visible = true;
            lblCollect.Visible = true;
            btnCustomerBrowser.Text = "";
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            bool closed = false;
            foreach (Form form in MdiParent.MdiChildren)
            {
                if (form.Name == "BillingList")
                {
                    form.Activate();
                    closed = true;
                    break;
                }
            }
            if (!closed)
            {
                BillingList bList = new BillingList();
                bList.MdiParent = MdiParent;
                bList.Show();
                //Hide();
                //AppDoCurrentDomain + "\\Infoices"
            }
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
                //txtProductTotalAmount.Text = total.ToString();
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
            }
        }

        private void txtProductTotalPayment_TextChanged(object sender, EventArgs e)
        {
            double val1;
            double val2;

            bool validOne = Double.TryParse(txtProductGrandTotal.Text, out val1);
            bool validTwo = Double.TryParse(txtProductTotalPayment.Text, out val2);

            if (validOne && validTwo)
            {
                double result = val1 - val2;
                txtProductPaymentDue.Text = result.ToString();
            }
        }
        public void PayMode()
        {
            cbPaymentMode.Text = "Select Mode";
            cbPaymentMode.Items.Add("Cash");
            cbPaymentMode.Items.Add("Instalment");
            cbPaymentMode.Items.Add("Layby");
        }       
        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            
            if (txtProductCode.Text == "")
            {
                con.Open();
                addTotalAmount();
                addToGrandTotal();
                addDiscount();
                con.Close();
            }
            else
            {
                try
                {
                    query = "INSERT INTO [BillingsItems](InvoiceNo,[ProductCode],ProductName,[Size],[Colour],[Price],[Quantity],[Amount],[Discount])VALUES('" + txtInvoiceNo.Text + "','" + this.txtProductCode.Text + "','" + this.txtProductName.Text + "','" + this.txtProductSize.Text + "','" + this.txtProductColour.Text + "','" + this.txtProductPrice.Text + "','" + this.txtProductQuantity.Text + "','" + this.txtProductAmount.Text + "','" + this.txtProductDiscount.Text + "');";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    con.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("Item added to table", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myReader.Read())
                    {

                    }                    
                    addTotalAmount();
                    addToGrandTotal();
                    addDiscount();
                    resetProduct();
                    LoadGrid();
                    con.Close();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    con.Close();
                }
            }
        }
        public void LoadGrid()
        {
            try
            {
                query = "select * from [BillingsItems] where [InvoiceNo] like'" + txtInvoiceNo.Text + "%'";
                OleDbCommand cmd = new OleDbCommand(query, con);
                //con.Open();
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);

                dt = new DataTable();
                da.Fill(dt);
                dgvInvoice.DataSource = dt.AsDataView();
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

        private void btnProductUpdate_Click(object sender, EventArgs e)
        {
            addTotalAmount();
            addToGrandTotal();
            addDiscount();
            try
            {
                query = @"UPDATE [BillingsItems] SET InvoiceNo='" + txtInvoiceNo.Text + "',[ProductCode]='" + this.txtProductCode.Text + "',ProductName='" + this.txtProductName.Text + "',[Size]='" + this.txtProductSize.Text + "',[Colour]='" + this.txtProductColour.Text + "',[Price]='" + this.txtProductPrice.Text + "',[Quantity]='" + this.txtProductQuantity.Text + "',[Amount]='" + this.txtProductAmount.Text + "',[Discount]='" + this.txtProductDiscount.Text + "',VAT='" + this.txtProductVAT.Text + "',TotalAmount='" + this.txtProductTotalAmount.Text + "',GrandTotal='" + this.txtProductGrandTotal.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtInvoiceNo.Text + " info updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnProductAdd.Enabled = true;
                
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
                query = @"DELETE FROM [BillingsItems] WHERE ProductCode='" + txtProductCode.Text.ToString() + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtProductCode.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            addTotalAmount();
            addToGrandTotal();
            addDiscount();
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
        public void PrintPdf()
        {
            try
            {
                int left_margin;
                int top_margin;
                int lastwriteposition = 100;

                //Document creation

                Document doc = new Document(PageSize.A4, 25, 25, 30, 1);
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("C:\\Users\\France\\documents\\visual studio 2017\\Projects\\CarpIn\\CarpIn\\Invoices\\" + txtInvoiceNo.Text + ".pdf", FileMode.Create));
                doc.Open();
                //Inserting Image
                //iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance("Pic Name");
                //png.ScalePercent(25f);
                //png.ScaleToFit(50f, 100f);
                //png.SetAbsolutePosition(doc.PageSize.Width -35f -72f, doc.PageSize.Height -36f -213f);
                //Inserting a paragraph
                Paragraph par = new Paragraph("First Page");


                //
                drow = dgvCompany.Rows[0];
                PdfContentByte cb = writer.DirectContent;
                // Add a footer template to the document
                cb.AddTemplate(PdfFooter(cb, drow), 30, 1);

                cb.BeginText();
                // Start with the invoice type header
                writeText(cb, "Invoice".ToString(), 270, 820, f_cb, 20);
                // HEader details; invoice number, invoice date, due date and customer Id
                writeText(cb, "Invoice No", 420, 800, f_cb, 10);
                writeText(cb, txtInvoiceNo.Text.ToString(), 500, 800, f_cn, 10);
                writeText(cb, "Invoice Date", 420, 788, f_cb, 10);
                writeText(cb, dtpInvoiceDate.Text.ToString(), 500, 788, f_cn, 10);
                writeText(cb, "Due Date", 420, 776, f_cb, 10);
                writeText(cb, dtpPaymentDate.Text.ToString(), 500, 776, f_cn, 10);
                writeText(cb, "Payment", 420, 764, f_cb, 10);
                writeText(cb, cbPaymentMode.Text.ToString(), 500, 764, f_cn, 10);

                // Delivery address details headers
                left_margin = 40;
                top_margin = 720;

                writeText(cb, "Invoice To:", left_margin, top_margin, f_cb, 14);
                writeText(cb, "Customer Name", left_margin, top_margin - 18, f_cb, 10);
                writeText(cb, "Address", left_margin, top_margin - 30, f_cb, 10);
                writeText(cb, "", left_margin, top_margin - 42, f_cb, 10);
                writeText(cb, "", left_margin, top_margin - 54, f_cb, 10);
                writeText(cb, "Province", left_margin, top_margin - 66, f_cb, 10);

                //Adding the address details
                left_margin = 120;
                top_margin = 720;

                //if (txtCustomerID.Text == null)
                //{
                    writeText(cb, txtCustomerName.Text, left_margin, top_margin - 18, f_cn, 10);
                    writeText(cb, txtAddress.Text, left_margin, top_margin - 30, f_cn, 10);
                    writeText(cb, txtCustomerContactNo.Text, left_margin, top_margin - 42, f_cn, 10);
                    writeText(cb, txtTown.Text + ", " + txtCode.Text, left_margin, top_margin - 54, f_cn, 10);
                    writeText(cb, cbProvince.Text, left_margin, top_margin - 66, f_cn, 10);

                //}
                // Invoiceaccount details header
                left_margin = 350;
                writeText(cb, "Invoice Account Details", left_margin, top_margin, f_cb, 14);
                writeText(cb, "Account Name", left_margin, top_margin - 18, f_cb, 10);
                writeText(cb, "Account No.", left_margin, top_margin - 30, f_cb, 10);
                writeText(cb, "Account Type", left_margin, top_margin - 42, f_cb, 10);
                writeText(cb, "Bank", left_margin, top_margin - 54, f_cb, 10);
                writeText(cb, "Branch", left_margin, top_margin - 66, f_cb, 10);
                //
                // Invoice account details
                left_margin = 450;
                writeText(cb, drow.Cells["AccName"].Value.ToString(), left_margin, top_margin - 18, f_cn, 10);
                writeText(cb, drow.Cells["AccNo"].Value.ToString(), left_margin, top_margin - 30, f_cn, 10);
                writeText(cb, drow.Cells["AccType"].Value.ToString(), left_margin, top_margin - 42, f_cn, 10);
                writeText(cb, drow.Cells["Bank"].Value.ToString(), left_margin, top_margin - 54, f_cn, 10);
                writeText(cb, drow.Cells["BranchNo"].Value.ToString(), left_margin, top_margin - 66, f_cn, 10);

                //
                // Write out invoice terms details
                left_margin = 40;
                top_margin = 620;
                writeText(cb, "Payment Terms", left_margin, top_margin, f_cb, 10);
                writeText(cb, cbPaymentMode.Text, left_margin, top_margin - 12, f_cn, 10);
                writeText(cb, "Delivery Terms", left_margin + 200, top_margin, f_cb, 10);
                writeText(cb, "No Delivery", left_margin + 200, top_margin - 12, f_cn, 10);
                writeText(cb, "Transport Terms", left_margin + 350, top_margin, f_cb, 10);
                writeText(cb, "Door-To-Door", left_margin + 350, top_margin - 12, f_cn, 10);
                // Move down
                left_margin = 40;
                top_margin = 590;
                string sales;
                MainForm main = new MainForm();
                sales = lblUser.Text;
                writeText(cb, "Order Reference", left_margin, top_margin, f_cb, 10);
                writeText(cb, txtInvoiceNo.Text, left_margin, top_margin - 12, f_cn, 10);
                writeText(cb, "Payment Due", left_margin + 200, top_margin, f_cb, 10);
                writeText(cb, dtpPaymentDate.Text, left_margin + 200, top_margin - 12, f_cn, 10);
                writeText(cb, "Salesman", left_margin + 350, top_margin, f_cb, 10);
                writeText(cb, sales, left_margin + 350, top_margin - 12, f_cn, 10);
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
                foreach (DataGridViewRow drItem in dgvInvoice.Rows)
                {
                    writeText(cb, drItem.Cells["ProductCodeD"].Value.ToString(), left_margin + 10, top_margin, f_cn, 10);
                    writeText(cb, drItem.Cells["ProductNameD"].Value.ToString() + " " + drItem.Cells["ColourD"].Value.ToString() + " - " + drItem.Cells["SizeD"].Value.ToString(), left_margin + 60, top_margin, f_cn, 10);
                    writeText(cb, drItem.Cells["QuantityD"].Value.ToString(), left_margin + 340, top_margin, f_cn, 10);
                    writeText(cb, "R " + drItem.Cells["PriceD"].Value.ToString(), left_margin + 380, top_margin, f_cn, 10);
                    writeText(cb, "R " + drItem.Cells["DiscountD"].Value.ToString(), left_margin + 440, top_margin, f_cn, 10);
                    writeText(cb, "R " + drItem.Cells["AmountD"].Value.ToString(), left_margin + 480, top_margin, f_cn, 10);

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
                //
                /*
                cb.EndText();
                //sepatration line
                cb.SetLineWidth(0f);
                cb.MoveTo(40, 400);
                cb.LineTo(560, 400);
                cb.Stroke();

                cb.BeginText();*/
                //
                top_margin -= 20;
                left_margin = 380;
                //
                // First the headers
                writeText(cb, "Invoice Total Amount", left_margin, top_margin, f_cb, 10);
                writeText(cb, "Discount Total", left_margin, top_margin - 12, f_cb, 10);
                writeText(cb, "VAT Amount", left_margin, top_margin - 24, f_cb, 10);
                writeText(cb, "Grand Total", left_margin, top_margin - 48, f_cb, 10);
                // Move right to write out the values
                left_margin = 540;
                // Write out the invoice currency and values in regular text
                cb.SetFontAndSize(f_cn, 10);
                              
                string curr = "R ".ToString();
                left_margin = 549;
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, curr + txtProductTotalAmount.Text.ToString(), left_margin, top_margin, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, curr + txtProductDiscountTotal.Text.ToString(), left_margin, top_margin - 12, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "%" + txtProductVAT.Text.ToString(), left_margin, top_margin - 24, 0);
                cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, curr + txtProductGrandTotal.Text.ToString(), left_margin, top_margin - 48, 0);

                // End the writing of text

                cb.EndText();
                doc.Close();
                System.Diagnostics.Process.Start("C:\\Users\\France\\Documents\\Visual Studio 2017\\Projects\\CarpIn\\CarpIn\\Invoices\\" + txtInvoiceNo.Text + ".pdf");
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
        public void addToGrandTotal()
        {
            float val1;
            float val2;
            float val3;
            float total;
            float vat;
            float result;

            bool validOne = float.TryParse(txtProductTotalAmount.Text, out val1);
            bool validTwo = float.TryParse(txtProductDiscountTotal.Text, out val2);
            bool validThree = float.TryParse(txtProductVAT.Text, out val3);

            if (validOne && validTwo && validThree)
            {
                total = val1 - val2;
                vat = total / 100 * val3;
                result = total + vat;
                txtProductGrandTotal.Text = result.ToString();
            }
        }
        public void addTotalAmount()
        {
            double amount = 0;
            for (int i = 0; i < dgvInvoice.Rows.Count; i++)
            {
                amount += Convert.ToDouble(dgvInvoice.Rows[i].Cells["AmountD"].Value.ToString());
            }
            txtProductTotalAmount.Text = amount.ToString();
        }
        public void addDiscount()
        {
            double disc = 0;
            for (int i = 0; i < dgvInvoice.Rows.Count; i++)
            {
                disc += Convert.ToDouble(dgvInvoice.Rows[i].Cells["DiscountD"].Value.ToString());
            }
            txtProductDiscountTotal.Text = disc.ToString();
        }

        private void txtInvoiceNo_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("InvoiceNo LIKE '%{0}%'", txtInvoiceNo.Text);
            dgvInvoice.DataSource = dataV;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void txtProductTotalAmount_TextChanged(object sender, EventArgs e)
        {
            addDiscount();
        }
        public void LoadCompany()
        {
            try
            {
                query = "select * from [Company]"; //where [SiteName] like'" + lblSite.Text + "%'";
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
                con.Close();
            }
        }

        private void btnProductBrowser_Click(object sender, EventArgs e)
        {
            ProductList upList = new ProductList();
            upList.MdiParent = MdiParent;
            upList.Show();
            upList.lblGet.Visible = true;
            lblCollect.Visible = true;
            btnProductBrowser.Text = "";
        }
        public void generateID()
        {
            try
            {
                int num;
                query = "SELECT MAX (InvoiceNo) FROM [Billings]";
                con.Open();
                OleDbCommand cmd = new OleDbCommand(query, con);
                myReader = cmd.ExecuteReader();

                if (myReader.Read())
                {
                    string val = myReader[0].ToString();
                    if (val == "")
                    {
                        txtInvoiceNo.Text = "I0001";
                        con.Close();
                    }
                    else
                    {
                        num = Convert.ToInt32(val);
                        num = num++;
                        txtInvoiceNo.Text = num.ToString();
                        con.Close();
                    }                    
                }                
                dtpInvoiceDate.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to generate Invoice No..!" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
        }

        private void Billings_Activated(object sender, EventArgs e)
        {
            ProductList plist = new ProductList();
            CustomerList clist = new CustomerList();
            if (lblCollect.Visible == true && btnCustomerBrowser.Text == "")
            {
                DataGridViewRow data = clist.dgvCustomerList.Rows[0];
                txtCustomerID.Text = data.Cells["CustomerID"].Value.ToString();
                txtCustomerName.Text = data.Cells["CustomerName"].Value.ToString();
                txtAddress.Text = data.Cells["Address"].Value.ToString();
                txtSuburb.Text = data.Cells["Suburb"].Value.ToString();
                txtTown.Text = data.Cells["Town"].Value.ToString();
                cbProvince.Text = data.Cells["Province"].Value.ToString();
                txtCode.Text = data.Cells["ZipCode"].Value.ToString();
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
                btnCustomerBrowser.Text = "...";
                clist.Hide();
            }
            else if (lblCollect.Visible == true && btnProductBrowser.Text == "")
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
                btnProductBrowser.Text = "...";
                clist.Hide();
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

        private void dgvInvoice_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvInvoice.Rows[e.RowIndex];

                lblIDNo.Text = row.Cells["ID"].Value.ToString();
                txtInvoiceNo.Text = row.Cells["InvoiceNoD"].Value.ToString();
                txtProductCode.Text = row.Cells["ProductCodeD"].Value.ToString();
                txtProductName.Text = row.Cells["ProductNameD"].Value.ToString();
                txtProductSize.Text = row.Cells["SizeD"].Value.ToString();
                txtProductColour.Text = row.Cells["ColourD"].Value.ToString();
                txtProductPrice.Text = row.Cells["PriceD"].Value.ToString();
                txtProductQuantity.Text = row.Cells["QuantityD"].Value.ToString();
                txtProductAmount.Text = row.Cells["AmountD"].Value.ToString();
                txtProductDiscount.Text = row.Cells["DiscountD"].Value.ToString();
                txtProductVAT.Text = row.Cells["VATD"].Value.ToString();
                txtProductTotalAmount.Text = row.Cells["TotalAmountD"].Value.ToString();
                txtProductGrandTotal.Text = row.Cells["GrandTotalD"].Value.ToString();
                btnProductUpdate.Enabled = true;
                btnProductAdd.Enabled = false;
                btnProductRemove.Enabled = true;
                lblIDNo.Visible = true;
            }
        }
    }
}
