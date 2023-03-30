using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHavenAppV4
{
    public class Book
    {
        #region: Data Members

        private string ItemID, Name;

        #endregion

        #region: Property Methods

        public string itemID
        {
            get { return ItemID; }
            set { ItemID = value; }
        }

        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        
        #endregion

        #region Constructor
        public Book()
        {
            ItemID = "";
            Name = "";
            
        }

        public Book(string iID, string iName, string iDescription)
        {
            ItemID = iID;
            Name = iName;
            
        }
        #endregion
    }
}
