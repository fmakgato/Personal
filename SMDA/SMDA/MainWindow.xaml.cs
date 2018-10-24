using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;

namespace SMDA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dt;
        bool sSurname = false;
        bool sProduct = false;
        bool sID = false;
        bool sPolicy = false;

        public MainWindow()
        {
            InitializeComponent();


            BindGrid();
            LoadCombos();
            lblConnected.Visibility = Visibility.Hidden;
            Login mylogin = new Login();
            mylogin.Show();
        }
        public void BindGrid()
        {
            string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
            OleDbConnection connet = new OleDbConnection(scon);
            OleDbCommand cmd = new OleDbCommand();
            if (connet.State != ConnectionState.Open)
                connet.Open();
            cmd.Connection = connet;
            cmd.CommandText = "select * from Members";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvAll.ItemsSource = dt.AsDataView();

            if (dt.Rows.Count > 0)
            {
                lblCount.Visibility = Visibility.Hidden;
                gvAll.Visibility = Visibility.Visible;
            }
            else
            {
                lblCount.Visibility = Visibility.Visible;
                gvAll.Visibility = Visibility.Hidden;
            }
        }
        private void gridSummary()
        {
            string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
            OleDbConnection connet = new OleDbConnection(scon);
            OleDbCommand cmd = new OleDbCommand();
            if (connet.State != ConnectionState.Open)
                connet.Open();
            cmd.Connection = connet;
            cmd.CommandText = "select * from Members";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvSummary.ItemsSource = dt.AsDataView();
        }
        private void gridPersonal()
        {
            string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
            OleDbConnection connet = new OleDbConnection(scon);
            OleDbCommand cmd = new OleDbCommand();
            if (connet.State != ConnectionState.Open)
                connet.Open();
            cmd.Connection = connet;
            cmd.CommandText = "select * from Members";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvPersonal.ItemsSource = dt.AsDataView();
        }
        private void gridChild()
        {
            string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
            OleDbConnection connet = new OleDbConnection(scon);
            OleDbCommand cmd = new OleDbCommand();
            if (connet.State != ConnectionState.Open)
                connet.Open();
            cmd.Connection = connet;
            cmd.CommandText = "select ChildSurname, ChildFirstName, ChildMiddleName, ChildGender from Members";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvChild.ItemsSource = dt.AsDataView();
        }
        private void LoadCombos()
        {
            combGender1.SelectedIndex = 0;
            combGender1.Items.Add("Select Gender");
            combGender1.Items.Add("Male");
            combGender1.Items.Add("Female");

            combGender2.SelectedIndex = 0;
            combGender2.Items.Add("Select Gender");
            combGender2.Items.Add("Male");
            combGender2.Items.Add("Female");

            combGender3.SelectedIndex = 0;
            combGender3.Items.Add("Select Gender");
            combGender3.Items.Add("Male");
            combGender3.Items.Add("Female");

            //Adding data to Product
            combProduct1.SelectedIndex = 0;
            combProduct1.Items.Add("Select Product");
            combProduct1.Items.Add("Allangrey");
            combProduct1.Items.Add("Asupole");
            combProduct1.Items.Add("Avbob");
            combProduct1.Items.Add("Liberty");
            combProduct1.Items.Add("Liberty(Investment)");
            combProduct1.Items.Add("Momentum");
            combProduct1.Items.Add("Old Mutual");
            combProduct1.Items.Add("Old Mutual(Investment)");
            combProduct1.Items.Add("Sanlam");

            combSProduct.SelectedIndex = 0;
            combSProduct.Items.Add("Select Product");
            combSProduct.Items.Add("Allangrey");
            combSProduct.Items.Add("Asupole");
            combSProduct.Items.Add("Avbob");
            combSProduct.Items.Add("Liberty");
            combSProduct.Items.Add("Liberty(Investment)");
            combSProduct.Items.Add("Momentum");
            combSProduct.Items.Add("Old Mutual");
            combSProduct.Items.Add("Old Mutual(Investment)");
            combSProduct.Items.Add("Sanlam");

            combDProduct.SelectedIndex = 0;
            combDProduct.Items.Add("Select Product");
            combDProduct.Items.Add("Allangrey");
            combDProduct.Items.Add("Asupole");
            combDProduct.Items.Add("Avbob");
            combDProduct.Items.Add("Liberty");
            combDProduct.Items.Add("Liberty(Investment)");
            combDProduct.Items.Add("Momentum");
            combDProduct.Items.Add("Old Mutual");
            combDProduct.Items.Add("Old Mutual(Investment)");
            combDProduct.Items.Add("Sanlam");
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            BindGrid();
            lblConnected.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
            OleDbConnection connet = new OleDbConnection(scon);
            connet.Close();
            lblConnected.Visibility = Visibility.Hidden;
        }

        private void btnNext1_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tabControl.SelectedIndex + 1;
            if (newIndex >= 0)
                newIndex = tabControl.SelectedIndex + 1;
            tabControl.SelectedIndex = newIndex;
        }

        private void btnPrevious1_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tabControl.SelectedIndex - 1;
            if (newIndex >= 0)
                newIndex = tabControl.SelectedIndex - 1;
            tabControl.SelectedIndex = newIndex;
        }

        private void btnNext2_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tabControl.SelectedIndex + 1;
            if (newIndex >= 0)
                newIndex = tabControl.SelectedIndex + 1;
            tabControl.SelectedIndex = newIndex;
        }

        private void btnPrevious2_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tabControl.SelectedIndex - 1;
            if (newIndex >= 0)
                newIndex = tabControl.SelectedIndex - 1;
            tabControl.SelectedIndex = newIndex;
        }

        private void btnSSave_Click(object sender, RoutedEventArgs e)
        {
            string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
            OleDbConnection connet = new OleDbConnection(scon);
            OleDbCommand cmd = new OleDbCommand();
            connet.Open();
            if (btnSSave.Content.ToString() != "Update")
            {
                try
                {
                    string query = "INSERT INTO Members (Surname,FirstName,MiddleName,IDNumber,DateOfBirth,MaidenName,Gender,MaritalStatus,HomeLanguage,Country,PostalAddress,PostalCode1,PhysicalAddress,PostalCode2,Employer,Occupation,WorkAddress,PostalCode3,Premium,PolicyNo,Product,TelWork,TelHome1,Mobile,Email,SpauseSurname,SpauseFirstName,SpauseMiddleName,SpauseIDNumber,SpauseDateOfBirth,SpauseMaidenName,SpauseGender,SpauseCountry,SpauseTelWork,SpauseTelHome,SpauseMobile,SpauseEmail,ChildSurname,ChildFirstName,ChildMiddleName,ChildGender) Values('" + txtSurname1.Text + "','" + txtFirst1.Text + "','" + txtMiddle1.Text + "','" + txtID1.Text.ToString() + "','" + Date1.Text + "','" + txtMaiden1.Text + "','" + combGender1.Text + "','" + txtMarital1.Text + "','" + txtLanguage1.Text + "','" + txtCountry1.Text + "','" + txtPostal1.Text + "','" + txtCode1.Text + "','" + txtPhysical1.Text + "','" + txtCode2.Text + "','" + txtEmployer1.Text + "','" + txtOccupation1.Text + "','" + txtWorkA1.Text + "','" + txtCode3.Text + "','" + txtPremium1.Text + "','" + txtPolicy.Text + "','" + combProduct1.Text + "','" + txtTWork1.Text + "','" + txtTHome1.Text + "','" + txtMobile1.Text + "','" + txtEmail.Text + "','" + txtSurname2.Text + "','" + txtFirst2.Text + "','" + txtMiddle2.Text + "','" + txtID2.Text + "','" + Date2.Text + "','" + txtMaiden2.Text + "','" + combGender2.Text + "','" + txtCountry2.Text + "','" + txtTWork2.Text + "','" + txtTHome2.Text + "','" + txtMobile2.Text + "','" + txtEmail2.Text + "','" + txtSurname3.Text + "','" + txtFirst3.Text + "','" + txtMiddle3.Text + "','" + combGender3.Text + "')";
                    cmd = new OleDbCommand(query, connet);
                    cmd.ExecuteNonQuery();
                    connet.Close();
                    MessageBox.Show("Member added succesfully!!!!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    BindGrid();
                    ClearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                try
                {
                    string query = "UPDATE Members SET Surname='" + txtSurname1.Text + "',FirstName='" + txtFirst1.Text + "',MiddleName='" + txtMiddle1.Text + "',IDNumber='" + txtID1.Text.ToString() + "',DateOfBirth='" + Date1.Text + "',MaidenName='" + txtMaiden1.Text + "',Gender='" + combGender1.Text + "',MaritalStatus='" + txtMarital1.Text + "',HomeLanguage='" + txtLanguage1.Text + "',Country='" + txtCountry1.Text + "',PostalAddress='" + txtPostal1.Text + "',PostalCode1='" + txtCode1.Text + "',PhysicalAddress='" + txtPhysical1.Text + "',PostalCode2='" + txtCode2.Text + "',Employer='" + txtEmployer1.Text + "',Occupation='" + txtOccupation1.Text + "',WorkAddress='" + txtWorkA1.Text + "',PostalCode3='" + txtCode3.Text + "',Premium='" + txtPremium1.Text + "',PolicyNo='" + txtPolicy.Text + "',Product='" + combProduct1.Text + "',TelWork='" + txtTWork1.Text + "',TelHome='" + txtTHome1.Text + "',Mobile='" + txtMobile1.Text + "',Email='" + txtEmail.Text + "',SpauseSurname='" + txtSurname2.Text + "',SpauseFirstName='" + txtFirst2.Text + "',SpauseMiddleName='" + txtMiddle2.Text + "',SpauseIDNumber='" + txtID2.Text + "',SpauseDateOfBirth='" + Date2.Text + "',SpauseMaidenName='" + txtMaiden2.Text + "',SpauseGender='" + combGender2.Text + "',SpauseCountry='" + txtCountry2.Text + "',SpauseTelWork='" + txtTWork2.Text + "',SpauseTelHome='" + txtTHome2.Text + "',SpauseMobile='" + txtMobile2.Text + "',SpauseEmail='" + txtEmail2.Text + "',ChildSurname='" + txtSurname3.Text + "',ChildFirstName='" + txtFirst3.Text + "',ChildMiddleName='" + txtMiddle3.Text + "',ChildGender='" + combGender3.Text + "'WHERE IDNumber='" + txtID1.Text + " '";
                    cmd = new OleDbCommand(query, connet);
                    cmd.ExecuteNonQuery();
                    connet.Close();
                    MessageBox.Show("Data updated Successfully...", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                    BindGrid();
                    ClearAll();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void btnSEdit_Click(object sender, RoutedEventArgs e)
        {
            //Edit records
            if (gvAll.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)gvAll.SelectedItems[0];

                txtSurname1.Text = row["Surname"].ToString();
                txtFirst1.Text = row["FirstName"].ToString();
                txtMiddle1.Text = row["MiddleName"].ToString();
                txtID1.Text = row["IDNumber"].ToString();
                Date1.Text = row["DateOfBirth"].ToString();
                txtMaiden1.Text = row["MaidenName"].ToString();
                combGender1.Text = row["Gender"].ToString();
                txtMarital1.Text = row["MaritalStatus"].ToString();
                txtLanguage1.Text = row["HomeLanguage"].ToString();
                txtCountry1.Text = row["Country"].ToString();
                txtPostal1.Text = row["PostalAddress"].ToString();
                txtCode1.Text = row["PostalCode1"].ToString();
                txtPhysical1.Text = row["PhysicalAddress"].ToString();
                txtCode2.Text = row["PostalCode2"].ToString();
                txtEmployer1.Text = row["Employer"].ToString();
                txtOccupation1.Text = row["Occupation"].ToString();
                txtWorkA1.Text = row["WorkAddress"].ToString();
                txtCode3.Text = row["PostalCode3"].ToString();
                txtPremium1.Text = row["Premium"].ToString();
                txtPolicy.Text = row["PolicyNo"].ToString();
                combProduct1.Text = row["Product"].ToString();
                txtTWork1.Text = row["TelWork"].ToString();
                txtTHome1.Text = row["TelHome"].ToString();
                txtMobile1.Text = row["Mobile"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtSurname2.Text = row["SpauseSurname"].ToString();
                txtFirst2.Text = row["SpauseFirstName"].ToString();
                txtMiddle2.Text = row["SpauseMiddleName"].ToString();
                txtID2.Text = row["SpauseIDNumber"].ToString();
                Date2.Text = row["SpauseDateOfBirth"].ToString();
                txtMaiden2.Text = row["SpauseMaidenName"].ToString();
                txtFax.Text = row["Fax"].ToString();
                txtEmail2.Text = row["SpauseEmail"].ToString();
                txtCountry2.Text = row["SpauseCountry"].ToString();
                txtTWork2.Text = row["SpauseTelWork"].ToString();
                txtTHome2.Text = row["SpauseTelHome"].ToString();
                txtMobile2.Text = row["SpauseMobile"].ToString();
                btnSSave.Content = "Update";
                txtNumber.Text = row["ID"].ToString();
                txtSurname3.Text = row["ChildSurname"].ToString();
                txtFirst3.Text = row["ChildFirstName"].ToString();
                txtMiddle3.Text = row["ChildMiddleName"].ToString();
                combGender3.Text = row["ChildGender"].ToString();

                //back to first tab
                int newIndex = tabControl.SelectedIndex = 0;
            }
            else if (txtID1.Text != "")
            {
                DatagridPersonal.IsEnabled = true;
                DatagridSummary.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Please Select Any Member From List...", "Message", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        public void ClearAll()
        {
            btnSSave.Content = "Save";
            txtSurname1.Clear();
            txtFirst1.Clear();
            txtMiddle1.Clear();
            txtID1.Clear();
            Date1.Text = null;
            txtMaiden1.Clear();
            combGender1.SelectedIndex = 0;
            txtMarital1.Clear();
            txtLanguage1.Clear();
            txtCountry1.Clear();
            txtPostal1.Clear();
            txtCode1.Clear();
            txtPhysical1.Clear();
            txtCode2.Clear();
            txtEmployer1.Clear();
            txtOccupation1.Clear();
            txtWorkA1.Clear();
            txtCode3.Clear();
            txtPremium1.Clear();
            txtPolicy.Clear();
            combGender2.SelectedIndex = 0;
            txtTWork1.Clear();
            txtTHome1.Clear();
            txtMobile1.Clear();
            txtEmail.Clear();
            txtSurname2.Clear();
            txtFirst2.Clear();
            txtMiddle2.Clear();
            txtID2.Clear();
            Date2.Text = null;
            txtMaiden2.Clear();
            txtCountry2.Clear();
            txtTWork2.Clear();
            txtTHome2.Clear();
            txtMobile2.Clear();
            combGender3.SelectedIndex = 0;
            combProduct1.SelectedIndex = 0;
            txtSurname3.Clear();
            txtNumber.Clear();
            txtFirst3.Clear();
            txtMiddle3.Clear();
            txtFax.Clear();
            txtEmail2.Clear();
            combGender3.SelectedIndex = 0;
            combDProduct.SelectedIndex = 0;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
            OleDbConnection connet = new OleDbConnection(scon);
            connet.Open();

            if (sSurname == true)
            {
                sSurname = true;
                OleDbCommand cmd = new OleDbCommand("select * from Members where Surname like'" + txtSSurname.Text + "%'", connet);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                gvSearch.ItemsSource = dt.AsDataView();
            }
            else if (sProduct == true)
            {
                if (combSProduct.Text != "Select Product")
                {
                    sProduct = true;
                    OleDbCommand cmd = new OleDbCommand("select * from Members where Product like'" + combSProduct.SelectedValue + "%'", connet);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    dt = new DataTable();
                    da.Fill(dt);
                    gvSearch.ItemsSource = dt.AsDataView();
                }
            }
            else if (sID == true)
            {
                sID = true;
                OleDbCommand cmd = new OleDbCommand("select * from Members where IDNumber like'" + txtSID.Text + "%'", connet);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                gvSearch.ItemsSource = dt.AsDataView();
            }
            else if (sPolicy == true)
            {
                sPolicy = true;
                OleDbCommand cmd = new OleDbCommand("select * from Members where PolicyNo like'" + txtSPolicy.Text + "%'", connet);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                gvSearch.ItemsSource = dt.AsDataView();
            }
            else
            {
                MessageBox.Show("No data found!!!", "Message", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void txtSPolicy_TextChanged(object sender, TextChangedEventArgs e)
        {
            sSurname = false;
            sProduct = false;
            sID = false;
            sPolicy = true;
        }

        private void combSProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sSurname = false;
            sProduct = true;
            sID = false;
            sPolicy = false;
        }

        private void txtSID_TextChanged(object sender, TextChangedEventArgs e)
        {
            sSurname = false;
            sProduct = false;
            sID = true;
            sPolicy = false;
        }

        private void txtSSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            sSurname = true;
            sProduct = false;
            sID = false;
            sPolicy = false;
        }

        private void btnSview_Click(object sender, RoutedEventArgs e)
        {
            //Edit records
            if (gvSearch.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)gvSearch.SelectedItems[0];

                txtSurname1.Text = row["Surname"].ToString();
                txtFirst1.Text = row["FirstName"].ToString();
                txtMiddle1.Text = row["MiddleName"].ToString();
                txtID1.Text = row["IDNumber"].ToString();
                Date1.Text = row["DateOfBirth"].ToString();
                txtMaiden1.Text = row["MaidenName"].ToString();
                combGender1.Text = row["Gender"].ToString();
                txtMarital1.Text = row["MaritalStatus"].ToString();
                txtLanguage1.Text = row["HomeLanguage"].ToString();
                txtCountry1.Text = row["Country"].ToString();
                txtPostal1.Text = row["PostalAddress"].ToString();
                txtCode1.Text = row["PostalCode1"].ToString();
                txtPhysical1.Text = row["PhysicalAddress"].ToString();
                txtCode2.Text = row["PostalCode2"].ToString();
                txtEmployer1.Text = row["Employer"].ToString();
                txtOccupation1.Text = row["Occupation"].ToString();
                txtWorkA1.Text = row["WorkAddress"].ToString();
                txtCode3.Text = row["PostalCode3"].ToString();
                txtPremium1.Text = row["Premium"].ToString();
                txtPolicy.Text = row["PolicyNo"].ToString();
                combProduct1.Text = row["Product"].ToString();
                txtTWork1.Text = row["TelWork"].ToString();
                txtTHome1.Text = row["TelHome"].ToString();
                txtMobile1.Text = row["Mobile"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtSurname2.Text = row["SpauseSurname"].ToString();
                txtFirst2.Text = row["SpauseFirstName"].ToString();
                txtMiddle2.Text = row["SpauseMiddleName"].ToString();
                txtID2.Text = row["SpauseIDNumber"].ToString();
                Date2.Text = row["SpauseDateOfBirth"].ToString();
                txtMaiden2.Text = row["SpauseMaidenName"].ToString();
                txtFax.Text = row["Fax"].ToString();
                txtEmail2.Text = row["SpauseEmail"].ToString();
                txtCountry2.Text = row["SpauseCountry"].ToString();
                txtTWork2.Text = row["SpauseTelWork"].ToString();
                txtTHome2.Text = row["SpauseTelHome"].ToString();
                txtMobile2.Text = row["SpauseMobile"].ToString();
                btnSSave.Content = "Update";
                txtNumber.Text = row["ID"].ToString();
                txtSurname3.Text = row["ChildSurname"].ToString();
                txtFirst3.Text = row["ChildFirstName"].ToString();
                txtMiddle3.Text = row["ChildMiddleName"].ToString();
                combGender3.Text = row["ChildGender"].ToString();

                DatagridPersonal.IsEnabled = false;
                DatagridSummary.IsEnabled = false;

                txtSID.Clear();
                txtSSurname.Clear();
                txtSPolicy.Clear();
                combSProduct.SelectedIndex = 0;
                gvSearch = null;
                //back to first tab
                int newIndex = tabControl.SelectedIndex = 0;
                clearSearch();
            }
            else
            {
                MessageBox.Show("Please Select Any Member From List...");
            }
        }
        private void clearSearch()
        {
            txtSID.Clear();
            txtSPolicy.Clear();
            txtSSurname.Clear();
            combSProduct.SelectedIndex = 0;
        }

        private void btnPreviousRecord_Click(object sender, RoutedEventArgs e)
        {
            //needs work
            int newIndex = gvAll.SelectedItems.Count - 1;
            if (newIndex >= 0)
                newIndex = gvAll.SelectedItems.Count - 1;
            gvAll.SelectedIndex = newIndex;

            DataRowView row = (DataRowView)gvSearch.SelectedItems[0];

            txtSurname1.Text = row["Surname"].ToString();
            txtFirst1.Text = row["FirstName"].ToString();
            txtMiddle1.Text = row["MiddleName"].ToString();
            txtID1.Text = row["IDNumber"].ToString();
            Date1.Text = row["DateOfBirth"].ToString();
            txtMaiden1.Text = row["MaidenName"].ToString();
            combGender1.Text = row["Gender"].ToString();
            txtMarital1.Text = row["MaritalStatus"].ToString();
            txtLanguage1.Text = row["HomeLanguage"].ToString();
            txtCountry1.Text = row["Country"].ToString();
            txtPostal1.Text = row["PostalAddress"].ToString();
            txtCode1.Text = row["PostalCode1"].ToString();
            txtPhysical1.Text = row["PhysicalAddress"].ToString();
            txtCode2.Text = row["PostalCode2"].ToString();
            txtEmployer1.Text = row["Employer"].ToString();
            txtOccupation1.Text = row["Occupation"].ToString();
            txtWorkA1.Text = row["WorkAddress"].ToString();
            txtCode3.Text = row["PostalCode3"].ToString();
            txtPremium1.Text = row["Premium"].ToString();
            txtPolicy.Text = row["PolicyNo"].ToString();
            combProduct1.Text = row["Product"].ToString();
            txtTWork1.Text = row["TelWork"].ToString();
            txtTHome1.Text = row["TelHome"].ToString();
            txtMobile1.Text = row["Mobile"].ToString();
            txtEmail.Text = row["Email"].ToString();
            txtSurname2.Text = row["SpauseSurname"].ToString();
            txtFirst2.Text = row["SpauseFirstName"].ToString();
            txtMiddle2.Text = row["SpauseMiddleName"].ToString();
            txtID2.Text = row["SpauseIDNumber"].ToString();
            Date2.Text = row["SpauseDateOfBirth"].ToString();
            txtMaiden2.Text = row["SpauseMaidenName"].ToString();
            txtFax.Text = row["Fax"].ToString();
            txtEmail2.Text = row["SpauseEmail"].ToString();
            txtCountry2.Text = row["SpauseCountry"].ToString();
            txtTWork2.Text = row["SpauseTelWork"].ToString();
            txtTHome2.Text = row["SpauseTelHome"].ToString();
            txtMobile2.Text = row["SpauseMobile"].ToString();
            btnSSave.Content = "Update";
            txtNumber.Text = row["ID"].ToString();
            txtSurname3.Text = row["ChildSurname"].ToString();
            txtFirst3.Text = row["ChildFirstName"].ToString();
            txtMiddle3.Text = row["ChildMiddleName"].ToString();
            combGender3.Text = row["ChildGender"].ToString();

            DatagridPersonal.IsEnabled = false;
            DatagridSummary.IsEnabled = false;

        }

        private void btnNextRecord_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //needs work
                if (gvAll.SelectedItems.Count > 0)
                {
                    DataRowView row = (DataRowView)gvSearch.SelectedItems;

                    txtSurname1.Text = row["Surname"].ToString();
                    txtFirst1.Text = row["FirstName"].ToString();
                    txtMiddle1.Text = row["MiddleName"].ToString();
                    txtID1.Text = row["IDNumber"].ToString();
                    Date1.Text = row["DateOfBirth"].ToString();
                    txtMaiden1.Text = row["MaidenName"].ToString();
                    combGender1.Text = row["Gender"].ToString();
                    txtMarital1.Text = row["MaritalStatus"].ToString();
                    txtLanguage1.Text = row["HomeLanguage"].ToString();
                    txtCountry1.Text = row["Country"].ToString();
                    txtPostal1.Text = row["PostalAddress"].ToString();
                    txtCode1.Text = row["PostalCode1"].ToString();
                    txtPhysical1.Text = row["PhysicalAddress"].ToString();
                    txtCode2.Text = row["PostalCode2"].ToString();
                    txtEmployer1.Text = row["Employer"].ToString();
                    txtOccupation1.Text = row["Occupation"].ToString();
                    txtWorkA1.Text = row["WorkAddress"].ToString();
                    txtCode3.Text = row["PostalCode3"].ToString();
                    txtPremium1.Text = row["Premium"].ToString();
                    txtPolicy.Text = row["PolicyNo"].ToString();
                    combProduct1.Text = row["Product"].ToString();
                    txtTWork1.Text = row["TelWork"].ToString();
                    txtTHome1.Text = row["TelHome"].ToString();
                    txtMobile1.Text = row["Mobile"].ToString();
                    txtEmail.Text = row["Email"].ToString();
                    txtSurname2.Text = row["SpauseSurname"].ToString();
                    txtFirst2.Text = row["SpauseFirstName"].ToString();
                    txtMiddle2.Text = row["SpauseMiddleName"].ToString();
                    txtID2.Text = row["SpauseIDNumber"].ToString();
                    Date2.Text = row["SpauseDateOfBirth"].ToString();
                    txtMaiden2.Text = row["SpauseMaidenName"].ToString();
                    txtFax.Text = row["Fax"].ToString();
                    txtEmail2.Text = row["SpauseEmail"].ToString();
                    txtCountry2.Text = row["SpauseCountry"].ToString();
                    txtTWork2.Text = row["SpauseTelWork"].ToString();
                    txtTHome2.Text = row["SpauseTelHome"].ToString();
                    txtMobile2.Text = row["SpauseMobile"].ToString();
                    btnSSave.Content = "Update";
                    txtNumber.Text = row["ID"].ToString();
                    txtSurname3.Text = row["ChildSurname"].ToString();
                    txtFirst3.Text = row["ChildFirstName"].ToString();
                    txtMiddle3.Text = row["ChildMiddleName"].ToString();
                    combGender3.Text = row["ChildGender"].ToString();

                    DatagridPersonal.IsEnabled = false;
                    DatagridSummary.IsEnabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void combDProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combDProduct.Text != "Select Product")
            {
                string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
                OleDbConnection connet = new OleDbConnection(scon);
                connet.Open();
                sProduct = true;
                OleDbCommand cmd = new OleDbCommand("select * from Members where Product like'" + combDProduct.SelectedValue + "%'", connet);
                OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                gvAll.ItemsSource = dt.AsDataView();
                lblCount.Visibility = Visibility.Hidden;
            }
            else
            {
                lblCount.Visibility = Visibility.Visible;
            }
        }

        private void btnViewData_Click(object sender, RoutedEventArgs e)
        {
            //Edit records
            if (gvAll.SelectedItems.Count > 0)
            {
                DataRowView row = (DataRowView)gvAll.SelectedItems[0];

                txtSurname1.Text = row["Surname"].ToString();
                txtFirst1.Text = row["FirstName"].ToString();
                txtMiddle1.Text = row["MiddleName"].ToString();
                txtID1.Text = row["IDNumber"].ToString();
                Date1.Text = row["DateOfBirth"].ToString();
                txtMaiden1.Text = row["MaidenName"].ToString();
                combGender1.Text = row["Gender"].ToString();
                txtMarital1.Text = row["MaritalStatus"].ToString();
                txtLanguage1.Text = row["HomeLanguage"].ToString();
                txtCountry1.Text = row["Country"].ToString();
                txtPostal1.Text = row["PostalAddress"].ToString();
                txtCode1.Text = row["PostalCode1"].ToString();
                txtPhysical1.Text = row["PhysicalAddress"].ToString();
                txtCode2.Text = row["PostalCode2"].ToString();
                txtEmployer1.Text = row["Employer"].ToString();
                txtOccupation1.Text = row["Occupation"].ToString();
                txtWorkA1.Text = row["WorkAddress"].ToString();
                txtCode3.Text = row["PostalCode3"].ToString();
                txtPremium1.Text = row["Premium"].ToString();
                txtPolicy.Text = row["PolicyNo"].ToString();
                combProduct1.Text = row["Product"].ToString();
                txtTWork1.Text = row["TelWork"].ToString();
                txtTHome1.Text = row["TelHome"].ToString();
                txtMobile1.Text = row["Mobile"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtSurname2.Text = row["SpauseSurname"].ToString();
                txtFirst2.Text = row["SpauseFirstName"].ToString();
                txtMiddle2.Text = row["SpauseMiddleName"].ToString();
                txtID2.Text = row["SpauseIDNumber"].ToString();
                Date2.Text = row["SpauseDateOfBirth"].ToString();
                txtMaiden2.Text = row["SpauseMaidenName"].ToString();
                txtFax.Text = row["Fax"].ToString();
                txtEmail2.Text = row["SpauseEmail"].ToString();
                txtCountry2.Text = row["SpauseCountry"].ToString();
                txtTWork2.Text = row["SpauseTelWork"].ToString();
                txtTHome2.Text = row["SpauseTelHome"].ToString();
                txtMobile2.Text = row["SpauseMobile"].ToString();
                btnSSave.Content = "Update";
                txtNumber.Text = row["ID"].ToString();
                txtSurname3.Text = row["ChildSurname"].ToString();
                txtFirst3.Text = row["ChildFirstName"].ToString();
                txtMiddle3.Text = row["ChildMiddleName"].ToString();
                combGender3.Text = row["ChildGender"].ToString();

                DatagridPersonal.IsEnabled = false;
                DatagridSummary.IsEnabled = false;
                //back to first tab
                int newIndex = tabControl.SelectedIndex = 0;
            }
        }

        private void btnSReset_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void logOff_Click(object sender, RoutedEventArgs e)
        {
            Login mylogin = new Login();
            mylogin.Show();
            Close();

        }
    }
}
