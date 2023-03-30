using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BookHavenAppV4.PresentationLayer;

namespace BookHavenAppV4
{
    public partial class Dashhboard : Form
    {
        public LoginForm loginForm;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
             int nHeightEllipse

          );

        public Dashhboard()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            PnlNav.Height = BtnDashboard.Height;
            PnlNav.Top = BtnDashboard.Top;
            PnlNav.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);

            ;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            PnlNav.Height = BtnDashboard.Height;
            PnlNav.Top = BtnDashboard.Top;
            PnlNav.Left = BtnDashboard.Left;
            BtnDashboard.BackColor= Color.FromArgb(46, 51, 73);
        }

       

        private void BtnBooks_Click(object sender, EventArgs e)
        {
            PnlNav.Height = BtnBooks.Height;
            PnlNav.Top = BtnBooks.Top;
            PnlNav.Left = BtnBooks.Left;
            BtnBooks.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnContacts_Click(object sender, EventArgs e)
        {
            PnlNav.Height = BtnContacts.Height;
            PnlNav.Top = BtnContacts.Top;
            PnlNav.Left = BtnContacts.Left;
            BtnContacts.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnInvoice_Click(object sender, EventArgs e)
        {
            PnlNav.Height = BtnInvoice.Height;
            PnlNav.Top = BtnInvoice.Top;
            PnlNav.Left = BtnInvoice.Left;
            BtnInvoice.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            BtnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnBooks_Leave(object sender, EventArgs e)
        {
            BtnBooks.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnContacts_Leave(object sender, EventArgs e)
        {
            BtnContacts.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnInvoice_Leave(object sender, EventArgs e)
        {
            BtnInvoice.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
