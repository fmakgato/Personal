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
using System.Windows.Shapes;
using System.Data.OleDb;
using System.Data;

namespace SMDA //final product
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            HideRegister();

            PasswordBox mypass = new PasswordBox();
            mypass.PasswordChar = '*';
            mypass.Password = txtPassword.Text;
        }
        public void HideRegister()
        {
            txtConfirmPass.Visibility = Visibility.Hidden;
            txtIDRegister.Visibility = Visibility.Hidden;
            txtPasswordRegister.Visibility = Visibility.Hidden;
            txtUserRegister.Visibility = Visibility.Hidden;
            label5.Visibility = Visibility.Hidden;
            label4.Visibility = Visibility.Hidden;
            label3.Visibility = Visibility.Hidden;
            label2.Visibility = Visibility.Hidden;
            gpRegister.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
            btnRegisterPanel.Visibility = Visibility.Hidden;
            lblError.Visibility = Visibility.Hidden;
        }
        public void clearall()
        {
            txtConfirmPass.Clear();
            txtIDRegister.Clear();
            txtPassword.Clear();
            txtPasswordRegister.Clear();
            txtUsername.Clear();
            txtUserRegister.Clear();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text != "" && txtPassword.Text != "")
            {
                try
                {
                    string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
                    string query = "select * from Users where Username='" + txtPassword.Text + "' and password='" + txtPassword.Text + "' ;";
                    OleDbConnection connet = new OleDbConnection(scon);
                    OleDbCommand cmd = new OleDbCommand(query, connet);
                    OleDbDataReader myReader;

                    connet.Open();
                    myReader = cmd.ExecuteReader();
                    int count = 0;

                    while (myReader.Read())
                    {
                        count = count + 1;
                    }
                    if (count == 1)
                    {
                        MainWindow myMain = new MainWindow();
                        myMain.Show();
                        myMain.BindGrid();
                        myMain.IsEnabled = true;
                        myMain.Visibility = Visibility.Visible;
                        Hide();
                    }
                    else
                    {
                        MessageBox.Show("Please enter correct username and password!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter correct username and password!", "Message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            txtConfirmPass.Visibility = Visibility.Visible;
            txtIDRegister.Visibility = Visibility.Visible;
            txtPasswordRegister.Visibility = Visibility.Visible;
            txtUserRegister.Visibility = Visibility.Visible;
            label5.Visibility = Visibility.Visible;
            label4.Visibility = Visibility.Visible;
            label3.Visibility = Visibility.Visible;
            label2.Visibility = Visibility.Visible;
            gpRegister.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            btnRegisterPanel.Visibility = Visibility.Visible;
            clearall();
            txtUserRegister.Focus();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            clearall();
            HideRegister();
        }

        private void btnRegisterPanel_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserRegister.Text != "" && txtPasswordRegister.Text != "" && txtConfirmPass.Text != "")
            {
                if (txtConfirmPass.Text != txtPasswordRegister.Text)
                {
                    try
                    {
                        lblError.Visibility = Visibility.Visible;
                        txtConfirmPass.Focus();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        string query = "INSERT INTO Users (Username,EmployeeID,Password) Values('" + txtUserRegister.Text.ToString() + "','" + txtIDRegister.Text.ToString() + "','" + txtConfirmPass.Text.ToString() + ")";
                        string scon = "Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\SMADB.accdb";
                        OleDbConnection connet = new OleDbConnection(scon);
                        OleDbCommand cmd = new OleDbCommand(query, connet);
                        connet.Open();

                        cmd.ExecuteNonQuery();
                        
                        MessageBox.Show("Member added succesfully!!!!", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        connet.Close();
                        clearall();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }

                }
            }
            else
            {
                MessageBox.Show("Please enter all required fields (*)", "Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
