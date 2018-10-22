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
using System.Windows.Forms.Design;

namespace CarpIn
{
    public partial class UserList : Form
    {
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Application.StartupPath + "\\CARPINDATABASE.accdb");
        OleDbConnection con = new OleDbConnection(@"Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\Users\France\Documents\Visual Studio 2017\Projects\CarpIn\CarpIn\CARPINDATABASE.accdb");
        //OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDoCurrentDoBaseDirectory + "\\CARPINDATABASE.accdb");
        OleDbDataReader myReader;
        DataTable dt;
        string query;
        UserEntry userE;

        public UserList()
        {
            InitializeComponent();
            LoadGrid();
            txtbyID.Focus();
        }

        private void UserList_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        public void LoadGrid()
        {
            try
            {
               query = "select * from [Users]";
               OleDbCommand cmd = new OleDbCommand(query, con);
               con.Open();
               OleDbDataAdapter da = new OleDbDataAdapter(cmd);

               dt = new DataTable();
               da.Fill(dt);
               dgvUserList.DataSource = dt.AsDataView();
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
            }
        }

        private void dgvUserList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvUserList.Rows[e.RowIndex];
                bool closed = false;

                foreach (Form form in MdiParent.MdiChildren)
                {
                    if (form.Name == "UserEntry")
                    {
                        form.Activate();
                        UserEntry userE = new UserEntry();
                        userE.txtUserID.Text = row.Cells["UserID"].Value.ToString();
                        userE.txtName.Text = row.Cells["UserName"].Value.ToString();
                        userE.txtSurname.Text = row.Cells["Surname"].Value.ToString();
                        userE.cbGender.Text = row.Cells["Gender"].Value.ToString();
                        userE.txtSiteName.Text = row.Cells["SiteName"].Value.ToString();
                        userE.txtPassword.Text = row.Cells["Password"].Value.ToString();
                        userE.btnUpdate.Enabled = true;
                        userE.btnSave.Enabled = false;
                        userE.btnDelete.Enabled = true;
                        closed = true;
                        break;
                    }
                }
                if (!closed)
                {
                    UserEntry userE = new UserEntry();
                    userE.MdiParent = MdiParent;
                    userE.FormClosed += UserE_FormClosed;
                    userE.Show();

                    userE.txtUserID.Text = row.Cells["UserID"].Value.ToString();
                    userE.txtName.Text = row.Cells["UserName"].Value.ToString();
                    userE.txtSurname.Text = row.Cells["Surname"].Value.ToString();
                    userE.cbGender.Text = row.Cells["Gender"].Value.ToString();
                    userE.txtSiteName.Text = row.Cells["SiteName"].Value.ToString();
                    userE.txtPassword.Text = row.Cells["Password"].Value.ToString();
                    userE.btnUpdate.Enabled = true;
                    userE.btnSave.Enabled = false;
                    userE.btnDelete.Enabled = true;
                }
            }
        }

        private void UserE_FormClosed(object sender, FormClosedEventArgs e)
        {
            userE = null;
        }
        private void txtbyName_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("UserName LIKE '%{0}%'", txtbyName.Text);
            dgvUserList.DataSource = dataV;
        }

        private void txtbyID_TextChanged(object sender, EventArgs e)
        {
            DataView dataV = new DataView(dt);
            dataV.RowFilter = string.Format("UserID LIKE '%{0}%'", txtbyID.Text);
            dgvUserList.DataSource = dataV;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtbyID.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("UserID LIKE '%{0}%'", txtbyID.Text);
                dgvUserList.DataSource = dataV;
            }
            else if (txtbyName.Text != null)
            {
                DataView dataV = new DataView(dt);
                dataV.RowFilter = string.Format("UserName LIKE '%{0}%'", txtbyName.Text);
                dgvUserList.DataSource = dataV;
            }
            else
            {
                MessageBox.Show("Please enter text on Name or User ID!", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            LoadGrid();
            txtbyName.Clear();
            txtbyID.Clear();
        }
    }
}
