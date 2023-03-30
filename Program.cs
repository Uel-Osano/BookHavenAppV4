using BookHavenAppV4.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB;
using MongoDB.Driver;
using BookHavenAppV4;
using BookHavenAppV4.BusinessLayer;

namespace BookHavenAppV4
{
    public class Program
    {
        MongoCRUD db = new MongoCRUD("AddressBook");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
         void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
