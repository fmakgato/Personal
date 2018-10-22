namespace CarpIn
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNewCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNewSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNewProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNewStock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.tsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDataCustomer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDataSupplier = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDataProduct = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDataStock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsQuotations = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBilling = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBillingInvoice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsBillingQuote = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSettingCompany = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSettingRegisterUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSettingUsersList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblSite = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFile,
            this.tsData,
            this.tsBilling,
            this.tsSettings,
            this.tsLogin});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(745, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "MainMenuStrip";
            // 
            // tsFile
            // 
            this.tsFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripNew,
            this.tsOpen,
            this.tsPrint,
            this.tsExit});
            this.tsFile.Name = "tsFile";
            this.tsFile.Size = new System.Drawing.Size(37, 20);
            this.tsFile.Text = "&File";
            // 
            // ToolStripNew
            // 
            this.ToolStripNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNewCustomer,
            this.tsNewSupplier,
            this.tsNewProduct,
            this.tsNewStock});
            this.ToolStripNew.Name = "ToolStripNew";
            this.ToolStripNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.ToolStripNew.Size = new System.Drawing.Size(146, 22);
            this.ToolStripNew.Text = "&New";
            // 
            // tsNewCustomer
            // 
            this.tsNewCustomer.Name = "tsNewCustomer";
            this.tsNewCustomer.Size = new System.Drawing.Size(126, 22);
            this.tsNewCustomer.Text = "Customer";
            this.tsNewCustomer.Click += new System.EventHandler(this.tsNewCustomer_Click);
            // 
            // tsNewSupplier
            // 
            this.tsNewSupplier.Name = "tsNewSupplier";
            this.tsNewSupplier.Size = new System.Drawing.Size(126, 22);
            this.tsNewSupplier.Text = "Supplier";
            this.tsNewSupplier.Click += new System.EventHandler(this.tsNewSupplier_Click);
            // 
            // tsNewProduct
            // 
            this.tsNewProduct.Name = "tsNewProduct";
            this.tsNewProduct.Size = new System.Drawing.Size(126, 22);
            this.tsNewProduct.Text = "Product";
            this.tsNewProduct.Click += new System.EventHandler(this.tsNewProduct_Click);
            // 
            // tsNewStock
            // 
            this.tsNewStock.Name = "tsNewStock";
            this.tsNewStock.Size = new System.Drawing.Size(126, 22);
            this.tsNewStock.Text = "Stock";
            this.tsNewStock.Click += new System.EventHandler(this.tsNewStock_Click);
            // 
            // tsOpen
            // 
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.tsOpen.Size = new System.Drawing.Size(146, 22);
            this.tsOpen.Text = "&Open";
            // 
            // tsPrint
            // 
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.tsPrint.Size = new System.Drawing.Size(146, 22);
            this.tsPrint.Text = "&Print";
            this.tsPrint.Click += new System.EventHandler(this.tsPrint_Click);
            // 
            // tsExit
            // 
            this.tsExit.Name = "tsExit";
            this.tsExit.Size = new System.Drawing.Size(146, 22);
            this.tsExit.Text = "&Exit";
            this.tsExit.Click += new System.EventHandler(this.tsExit_Click);
            // 
            // tsData
            // 
            this.tsData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsDataCustomer,
            this.tsDataSupplier,
            this.tsDataProduct,
            this.tsDataStock,
            this.tsInvoice,
            this.tsQuotations});
            this.tsData.Name = "tsData";
            this.tsData.Size = new System.Drawing.Size(43, 20);
            this.tsData.Text = "&Data";
            // 
            // tsDataCustomer
            // 
            this.tsDataCustomer.Name = "tsDataCustomer";
            this.tsDataCustomer.Size = new System.Drawing.Size(128, 22);
            this.tsDataCustomer.Text = "&Customer";
            this.tsDataCustomer.Click += new System.EventHandler(this.tsDataCustomer_Click);
            // 
            // tsDataSupplier
            // 
            this.tsDataSupplier.Name = "tsDataSupplier";
            this.tsDataSupplier.Size = new System.Drawing.Size(128, 22);
            this.tsDataSupplier.Text = "&Supplier";
            this.tsDataSupplier.Click += new System.EventHandler(this.tsDataSupplier_Click);
            // 
            // tsDataProduct
            // 
            this.tsDataProduct.Name = "tsDataProduct";
            this.tsDataProduct.Size = new System.Drawing.Size(128, 22);
            this.tsDataProduct.Text = "&Product";
            this.tsDataProduct.Click += new System.EventHandler(this.tsDataProduct_Click);
            // 
            // tsDataStock
            // 
            this.tsDataStock.Name = "tsDataStock";
            this.tsDataStock.Size = new System.Drawing.Size(128, 22);
            this.tsDataStock.Text = "&Stock";
            this.tsDataStock.Click += new System.EventHandler(this.tsDataStock_Click);
            // 
            // tsInvoice
            // 
            this.tsInvoice.Name = "tsInvoice";
            this.tsInvoice.Size = new System.Drawing.Size(128, 22);
            this.tsInvoice.Text = "&Invoice";
            this.tsInvoice.Click += new System.EventHandler(this.tsInvoice_Click);
            // 
            // tsQuotations
            // 
            this.tsQuotations.Name = "tsQuotations";
            this.tsQuotations.Size = new System.Drawing.Size(128, 22);
            this.tsQuotations.Text = "&Quotation";
            this.tsQuotations.Click += new System.EventHandler(this.tsQuotations_Click);
            // 
            // tsBilling
            // 
            this.tsBilling.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsBillingInvoice,
            this.tsBillingQuote});
            this.tsBilling.Name = "tsBilling";
            this.tsBilling.Size = new System.Drawing.Size(52, 20);
            this.tsBilling.Text = "&Billing";
            // 
            // tsBillingInvoice
            // 
            this.tsBillingInvoice.Name = "tsBillingInvoice";
            this.tsBillingInvoice.Size = new System.Drawing.Size(112, 22);
            this.tsBillingInvoice.Text = "&Invoice";
            this.tsBillingInvoice.Click += new System.EventHandler(this.tsBillingInvoice_Click);
            // 
            // tsBillingQuote
            // 
            this.tsBillingQuote.Name = "tsBillingQuote";
            this.tsBillingQuote.Size = new System.Drawing.Size(112, 22);
            this.tsBillingQuote.Text = "&Quote";
            this.tsBillingQuote.Click += new System.EventHandler(this.tsBillingQuote_Click);
            // 
            // tsSettings
            // 
            this.tsSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSettingCompany,
            this.tsSettingRegisterUser,
            this.tsSettingUsersList});
            this.tsSettings.Enabled = false;
            this.tsSettings.Name = "tsSettings";
            this.tsSettings.Size = new System.Drawing.Size(56, 20);
            this.tsSettings.Text = "&Setting";
            // 
            // tsSettingCompany
            // 
            this.tsSettingCompany.Name = "tsSettingCompany";
            this.tsSettingCompany.Size = new System.Drawing.Size(152, 22);
            this.tsSettingCompany.Text = "&Company";
            this.tsSettingCompany.Click += new System.EventHandler(this.tsSettingCompany_Click);
            // 
            // tsSettingRegisterUser
            // 
            this.tsSettingRegisterUser.Name = "tsSettingRegisterUser";
            this.tsSettingRegisterUser.Size = new System.Drawing.Size(152, 22);
            this.tsSettingRegisterUser.Text = "&Register User";
            this.tsSettingRegisterUser.Click += new System.EventHandler(this.tsSettingRegisterUser_Click);
            // 
            // tsSettingUsersList
            // 
            this.tsSettingUsersList.Name = "tsSettingUsersList";
            this.tsSettingUsersList.Size = new System.Drawing.Size(152, 22);
            this.tsSettingUsersList.Text = "&Users List";
            this.tsSettingUsersList.Click += new System.EventHandler(this.tsSettingUsersList_Click);
            // 
            // tsLogin
            // 
            this.tsLogin.Name = "tsLogin";
            this.tsLogin.Size = new System.Drawing.Size(49, 20);
            this.tsLogin.Text = "&Login";
            this.tsLogin.Click += new System.EventHandler(this.tsLogin_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.Color.Transparent;
            this.lblDate.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(140, 31);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "&Date&Time";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(561, 0);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(184, 31);
            this.lblUser.TabIndex = 3;
            this.lblUser.Text = "User: France";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblSite);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.lblUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(745, 46);
            this.panel1.TabIndex = 5;
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.BackColor = System.Drawing.Color.Transparent;
            this.lblSite.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblSite.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSite.ForeColor = System.Drawing.Color.White;
            this.lblSite.Location = new System.Drawing.Point(338, 0);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(223, 31);
            this.lblSite.TabIndex = 4;
            this.lblSite.Text = "Site: Mokomene";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 390);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(745, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel1.Text = "Status ";
            // 
            // lblStatus
            // 
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(63, 17);
            this.lblStatus.Text = "Status Info";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(745, 412);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CARPIN";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem tsNewCustomer;
        private System.Windows.Forms.ToolStripMenuItem tsNewSupplier;
        private System.Windows.Forms.ToolStripMenuItem tsNewProduct;
        private System.Windows.Forms.ToolStripMenuItem tsNewStock;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.MenuStrip menuStrip1;
        public System.Windows.Forms.ToolStripMenuItem tsFile;
        public System.Windows.Forms.ToolStripMenuItem ToolStripNew;
        public System.Windows.Forms.ToolStripMenuItem tsOpen;
        public System.Windows.Forms.ToolStripMenuItem tsPrint;
        public System.Windows.Forms.ToolStripMenuItem tsExit;
        public System.Windows.Forms.ToolStripMenuItem tsData;
        public System.Windows.Forms.ToolStripMenuItem tsDataCustomer;
        public System.Windows.Forms.ToolStripMenuItem tsDataSupplier;
        public System.Windows.Forms.ToolStripMenuItem tsDataProduct;
        public System.Windows.Forms.ToolStripMenuItem tsBilling;
        public System.Windows.Forms.ToolStripMenuItem tsDataStock;
        public System.Windows.Forms.ToolStripMenuItem tsBillingInvoice;
        public System.Windows.Forms.ToolStripMenuItem tsBillingQuote;
        public System.Windows.Forms.ToolStripMenuItem tsSettings;
        public System.Windows.Forms.ToolStripMenuItem tsSettingCompany;
        public System.Windows.Forms.ToolStripMenuItem tsSettingRegisterUser;
        public System.Windows.Forms.ToolStripMenuItem tsInvoice;
        public System.Windows.Forms.ToolStripMenuItem tsQuotations;
        public System.Windows.Forms.ToolStripMenuItem tsLogin;
        public System.Windows.Forms.ToolStripMenuItem tsSettingUsersList;
        public System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
    }
}

