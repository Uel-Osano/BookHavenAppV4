using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using BookHavenAppV4.BusinessLayer;
using BookHavenAppV4.DatabaseLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using MongoDB.Driver;
using MongoDB.Bson; 

namespace BookHavenAppV4.PresentationLayer
{
    public partial class RegisterForm : Form
    {
        #region: Data Members

        public User user;
        public UserController userController;
        public bool loginFormClosed = false;
        public LoginForm loginForm;
        MongoClient dbClient = new MongoClient("mongodb+srv://uelosano:4XIDw5g4Nv6FPTzo@cluster0.3rdqxmz.mongodb.net/?retryWrites=true&w=majority");

           
        #endregion

        #region: Constructors
        public RegisterForm(UserController aController)
        {
            InitializeComponent();
            userController = aController;
        }
        #endregion

        private void btnRegister_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\uelos\\OneDrive\\Desktop\\BookHavenAppV4\\Users.mdf;Integrated Security=True");
                con.Open();

            if (txtUsername.Text == "" && txtPassword.Text == "" && txtConPassword.Text == "")
            {
                MessageBox.Show("Username and Password fields are empty. Please fill all fields.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            else if (txtPassword.Text == txtConPassword.Text)
            {
                
                

                    SqlCommand cmd = new SqlCommand("Insert Into [User] Values (@Username, @Password)", con);
                    cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                

                con.Close();

                txtUsername.Text = "";
                txtPassword.Text = "";
                txtConPassword.Text = "";

                MessageBox.Show("You were saved successfully.");
                
            }

            else
            {
                MessageBox.Show("Passwords do not match. Please re-enter.", "Registration failed");
                txtPassword.Text = "";
                txtConPassword.Text = "";
                txtPassword.Focus();
            }
            
        }

        private void uPopulateObject()
        {
            user = new User();
            user.getUserName = txtUsername.Text;
            user.getPassword = txtPassword.Text;

            
        }

        private void label6_Click(object sender, EventArgs e)
        {
            if (loginForm == null)
            {
                CreateNewLoginForm();
            }
            if (loginForm.loginFormClosed)
            {
                CreateNewLoginForm();
            }
            this.Close();
            loginForm.Show();
        }

        private void CreateNewLoginForm()
        {
            this.Hide();
            loginForm = new LoginForm();
            loginForm.ShowDialog();

        }

        private void chbxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if(chbxShowPass.Checked) 
            {
                txtPassword.PasswordChar = '\0';
                txtConPassword.PasswordChar = '\0';
            }

            else
            {
                txtPassword.PasswordChar = '●';
                txtConPassword.PasswordChar = '●';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtConPassword.Text = "";
            txtUsername.Focus();
        }
    }
}
