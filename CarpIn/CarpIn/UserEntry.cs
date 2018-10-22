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
    public partial class UserEntry : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        string query;
        int idno = 0;
        UserList upList;
        MainForm main;

        public UserEntry()
        {
            InitializeComponent();
            lblError.Visible = false;
            lblPassCorrect.Visible = false;
            txtUserID.Text = "U-000" + idno + 1 .ToString();
            
            genderadd();

        }

        private void genderadd()
        {
            cbGender.Text = "Select Gender";
            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text != null)
            {
                try
                {
                    query = "INSERT INTO [Users](UserID,UserName,Surname,Gender,SiteName,[Password])VALUES('" + this.txtUserID.Text.ToString() + "','" + this.txtName.Text + "','" + this.txtSurname.Text + "','" + this.cbGender.Text + "','" + this.txtSiteName.Text + "','" + this.txtPassword.Text + "');";
                    OleDbCommand cmd = new OleDbCommand(query, con);
                    con.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show(txtUserID.Text + " saved successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    while (myReader.Read())
                    {
                        
                    }
                    con.Close();
                    reset();
                    upList = new UserList();
                    upList.LoadGrid();
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter all the required fields!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPassValidate_Click(object sender, EventArgs e)
        {
            if (txtConfirmPassword.Text == txtPassword.Text)
            {
                lblPassCorrect.Visible = true;
                lblError.Visible = false;
            }
            else if (txtConfirmPassword.Text != txtPassword.Text)
            {
                lblError.Visible = true;
                lblPassCorrect.Visible = false;
            }
            else
            {
                lblError.Visible = false;
                lblPassCorrect.Visible = false;
            }
        }
        public void reset()
        {
            txtConfirmPassword.Clear();
            txtName.Clear();
            txtPassUserID.Clear();
            txtPassword.Clear();
            txtSiteName.Clear();
            txtSurname.Clear();
            txtUserID.Clear();
            cbGender.Text = "Select Gender";
            txtUserID.Text = "U-000" + idno + 1.ToString();
        }
        public void lastAdd()
        {
            query = "select [UserID] from [Users]";
            OleDbCommand cmd = new OleDbCommand(query, con);
            con.Open();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //da.Update(lblLastID.Text);
            //
            while (myReader.Read())
            {
            }

            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"UPDATE [Users] SET UserID='" + txtUserID.Text + "',UserName='" + txtName.Text + "',Surname='" + txtSurname.Text + "',Gender='" + cbGender.Text + "',SiteName='" + txtSiteName.Text + "',[Password]='" + txtPassword.Text + "' WHERE UserID='" + txtUserID.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtUserID.Text + " updated Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                upList = new UserList();
                upList.LoadGrid();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            bool closed = false;
            foreach (Form form in MdiParent.MdiChildren)
            {
                if (form.Name == "UserList")
                {
                    form.Activate();
                    closed = true;
                    break;
                }
            }
            if (!closed)
            {
                upList = new UserList();
                upList.MdiParent = MdiParent;
                upList.FormClosed += UpList_FormClosed;
                upList.Show();
            }
        }

        private void UpList_FormClosed(object sender, FormClosedEventArgs e)
        {
            upList = null;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = @"DELETE FROM [Users] WHERE UserID='" + txtUserID.Text + "';";
                OleDbCommand cmd = new OleDbCommand(query, con);
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {

                }
                con.Close();
                MessageBox.Show(txtUserID.Text + " deleted Successfully...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                upList = new UserList();
                upList.LoadGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            string userName = txtUserID.Text;

            if (userName != null)
            {
                txtPassUserID.Text = userName;
            }
        }
    }
}
