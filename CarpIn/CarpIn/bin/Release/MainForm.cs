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
        public MainForm()
        {
            InitializeComponent();
            Login log = new Login();
            log.MdiParent = this;
            //log.Show();

            this.Hide();
            timer1.Start();
            //lblUser.Text = UserName;
        }
        public void tsNewCustomer_Click(object sender, EventArgs e)
        {
            CustomerEntry cust = new CustomerEntry();
            cust.MdiParent = this;
            cust.Show();
        }

        public void tsNewSupplier_Click(object sender, EventArgs e)
        {
            SupplierEntry sup = new SupplierEntry();
            sup.MdiParent = this;
            sup.Show();
        }

        public void tsNewProduct_Click(object sender, EventArgs e)
        {
            ProductEntry prod = new ProductEntry();
            prod.MdiParent = this;
            prod.Show();
        }

        public void tsNewStock_Click(object sender, EventArgs e)
        {
            StockEntry stock = new StockEntry();
            stock.MdiParent = this;
            stock.Show();
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
        }

        public void tsBillingQuote_Click(object sender, EventArgs e)
        {
            Quotations quote = new Quotations();
            quote.MdiParent = this;
            quote.Show();
        }

        public void tsSettingCompany_Click(object sender, EventArgs e)
        {
            CompanyEntry comp = new CompanyEntry();
            comp.MdiParent = this;
            comp.Show();
        }

        public void tsSettingRegisterUser_Click(object sender, EventArgs e)
        {
            UserEntry users = new UserEntry();
            users.MdiParent = this;
            users.Show();
        }

        public void tsDataCustomer_Click(object sender, EventArgs e)
        {
            CustomerList cList = new CustomerList();
            cList.MdiParent = this;
            cList.Show();
        }

        public void tsDataSupplier_Click(object sender, EventArgs e)
        {
            SupplierList sList = new SupplierList();
            sList.MdiParent = this;
            sList.Show();
        }

        public void tsDataProduct_Click(object sender, EventArgs e)
        {
            ProductList pList = new ProductList();
            pList.MdiParent = this;
            pList.Show();
        }

        public void tsDataStock_Click(object sender, EventArgs e)
        {
            StockList skList = new StockList();
            skList.MdiParent = this;
            skList.Show();
        }

        public void tsInvoice_Click(object sender, EventArgs e)
        {
            BillingList bList = new BillingList();
            bList.MdiParent = this;
            bList.Show();
        }

        public void tsQuotations_Click(object sender, EventArgs e)
        {
            QuotationsList qList = new QuotationsList();
            qList.MdiParent = this;
            qList.Show();
        }

        public void tsLogin_Click(object sender, EventArgs e)
        {
            if (tsLogin.Text == "&Login")
            {
                Login log = new Login();
                log.MdiParent = this;
                log.Show();

                tsLogin.Text = "&Logout";
                tsSettings.Enabled = true;
            }
            else
            {
                tsLogin.Text = "&Login";
                tsSettings.Enabled = false;
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
        public void OpenUsers()
        {
            UserEntry userE = new UserEntry();
            userE.MdiParent = this;
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
    }
}
