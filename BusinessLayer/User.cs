using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHavenAppV4.BusinessLayer
{
    public class User
    {

        #region: Data Members:
        public string UserName;
        public string PassWord;
        #endregion

        #region Property Methods
        public string getUserName
        {
            get { return UserName; }
            set { UserName = value; }
            
        }

        public string getPassword
        {
            get { return PassWord; }
            set { PassWord = value; }

        }
        #endregion
        #region: Constructors

        public User() { }

        #endregion
    }
}
