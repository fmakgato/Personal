using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpIn
{
    public partial class MainForm : Form
    {
        Dashboard dash = new Dashboard();
        public MainForm()
        {
            InitializeComponent();
            Login log = new Login();
            log.MdiParent = this;
            log.Show();

            //this.Hide();
            timer1.Start();
            //lblUser.Text = UserName;
        }
        public void tsNewCustomer_Click(object sender, EventArgs e)
        {
            CustomerEntry cust = new CustomerEntry();
            cust.MdiParent = this;
            cust.Show();
            dash.Hide();

        }

        public void tsNewSupplier_Click(object sender, EventArgs e)
        {
            SupplierEntry sup = new SupplierEntry();
            sup.MdiParent = this;
            sup.Show();
            dash.Hide();
        }

        public void tsNewProduct_Click(object sender, EventArgs e)
        {
            ProductEntry prod = new ProductEntry();
            prod.MdiParent = this;
            prod.Show();
            dash.Hide();
        }

        public void tsNewStock_Click(object sender, EventArgs e)
        {
            StockEntry stock = new StockEntry();
            stock.MdiParent = this;
            stock.Show();
            dash.Hide();
        }

        public void tsExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void tsBillingInvoice_Click(object sender, EventArgs e)
        {
            Billings bill = new Billings();
            bill.MdiParent = this;
            bill.Show();
            dash.Hide();
        }

        public void tsBillingQuote_Click(object sender, EventArgs e)
        {
            Quotations quote = new Quotations();
            quote.MdiParent = this;
            quote.Show();
            dash.Hide();
        }

        public void tsSettingCompany_Click(object sender, EventArgs e)
        {
            CompanyEntry comp = new CompanyEntry();
            comp.MdiParent = this;
            comp.Show();
            dash.Hide();
        }

        public void tsSettingRegisterUser_Click(object sender, EventArgs e)
        {
            UserEntry users = new UserEntry();
            users.MdiParent = this;
            users.Show();
            dash.Hide();
        }

        public void tsDataCustomer_Click(object sender, EventArgs e)
        {
            CustomerList cList = new CustomerList();
            cList.MdiParent = this;
            cList.Show();
            dash.Hide();
        }

        public void tsDataSupplier_Click(object sender, EventArgs e)
        {
            SupplierList sList = new SupplierList();
            sList.MdiParent = this;
            sList.Show();
            dash.Hide();
        }

        public void tsDataProduct_Click(object sender, EventArgs e)
        {
            ProductList pList = new ProductList();
            pList.MdiParent = this;
            pList.Show();
            dash.Hide();
        }

        public void tsDataStock_Click(object sender, EventArgs e)
        {
            StockList skList = new StockList();
            skList.MdiParent = this;
            skList.Show();
            dash.Hide();
        }

        public void tsInvoice_Click(object sender, EventArgs e)
        {
            BillingList bList = new BillingList();
            bList.MdiParent = this;
            bList.Show();
            dash.Hide();
        }

        public void tsQuotations_Click(object sender, EventArgs e)
        {
            QuotationsList qList = new QuotationsList();
            qList.MdiParent = this;
            qList.Show();
            dash.Hide();
        }

        public void tsLogin_Click(object sender, EventArgs e)
        {
            if (tsLogin.Text == "&Login")
            {
                Login log = new Login();
                log.MdiParent = this;
                log.Show();
            }
            else
            {
                tsLogin.Text = "&Login";
                tsSettings.Enabled = false;
                tsFile.Enabled = false;
                tsData.Enabled = false;
                tsBilling.Enabled = false;
            }
        }

        public void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dateTime = DateTime.Now;
            this.lblDate.Text = dateTime.ToString();
        }

        public void tsSettingUsersList_Click(object sender, EventArgs e)
        {
            UserList users = new UserList();
            users.MdiParent = this;
            users.Show();
        }
        private void tsPrint_Click(object sender, EventArgs e)
        {
            CreatePdf cPdf = new CreatePdf();
            cPdf.printPdf();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (tsLogin.Text == "&Login" && lblUser.Visible == true)
            {
                tsSettings.Enabled = true;
                tsFile.Enabled = true;
                tsData.Enabled = true;
                tsBilling.Enabled = true;
                tsLogin.Text = "&Logout";
                lblStatus.Text = "Logged In";
            }
            else
            {
                lblUser.Visible = true;
                lblStatus.Text = "Login to activate controls";
            }
        }
    }
}
