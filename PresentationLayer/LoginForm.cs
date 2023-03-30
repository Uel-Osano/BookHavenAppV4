using BookHavenAppV4.BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookHavenAppV4.PresentationLayer
{
    public partial class LoginForm : Form
    {
        #region: Data Members
        public RegisterForm registerForm;
        public UserController userController;
        public Dashhboard dashhboard;
        public bool loginFormClosed = false;
        SqlCommand cmd = new SqlCommand();
        #endregion

        #region: Cosntructors
        public LoginForm()
        {
            InitializeComponent();
        }
        #endregion


        private void label6_Click(object sender, EventArgs e)
        {
            if (registerForm == null)
            {
                CreateNewRegisterForm();
            }
            if (registerForm.loginFormClosed)
            {
                CreateNewRegisterForm();
            }
            registerForm.Show();

        }


        #region Create a New ChildForm 
        public void CreateNewRegisterForm()
        {
            this.Hide();
            registerForm = new RegisterForm(userController);
            registerForm.ShowDialog();
            
        }

        #endregion

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\uelos\\OneDrive\\Desktop\\BookHavenAppV4\\Users.mdf;Integrated Security=True");
            con.Open();

            string login = "SELECT * FROM [User] WHERE Username= '" + txtUsername.Text + "' AND Password= '" + txtPassword.Text+ "'";
            cmd = new SqlCommand(login, con);
            SqlDataReader DR = cmd.ExecuteReader();

            if(DR.Read() == true)
            {
                dashhboard= new Dashhboard();
                new Dashhboard().Show();
                
                dashhboard.txtUser.Text = txtUsername.Text;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password, Please Try Again", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtUsername.Focus();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void chbxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chbxShowPass.Checked)
            {
                ;
                txtPassword.PasswordChar = '\0';
            }

            else
            {
                
                txtPassword.PasswordChar = '●';
            }
        }
    }
}
