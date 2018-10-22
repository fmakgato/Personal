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
    public partial class Login : Form
    {
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        
        public Login()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                query = "SELECT * FROM [Users] WHERE [UserName]='" + txtUsername.Text + "' AND Password='" + txtPassword.Text + "'";
                OleDbCommand cmd = new OleDbCommand(query, con);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                con.Open();
                myReader = cmd.ExecuteReader();
                if (dt.Rows.Count == 1)
                {
                    Dashboard dash = new Dashboard();
                    MainForm main = new MainForm();
                    dash.MdiParent = MdiParent;
                    main.Activate();
                    MessageBox.Show("Username and Password correct", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dash.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username and password..!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                con.Close();
            }
            catch (Exception)
            {

                MessageBox.Show("An error occured!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lblLinkChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (txtUsername.Text != "")
            {
                UserList uList = new UserList();
                uList.MdiParent = MdiParent;
                uList.Show();
                uList.txtbyName.Text = txtUsername.Text;
            }
        }
    }
}
