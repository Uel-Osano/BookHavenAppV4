using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHavenAppV4.BusinessLayer
{
    public class CoverTypes
    {
        #region: Data Members
        public enum CoverType //enum that stores the inventory's availablity
        {
            Soft = 0,
            Board = 1,
        }
        protected CoverType coverType;
        #endregion

        #region: Property Methods

        public CoverType getCoverType
        {
            get { return coverType; }
            set { coverType = value; }
        }

        #endregion

        #region: Constructors

        public CoverTypes()
        {
            coverType = CoverTypes.CoverType.Soft; //initialises the CoverTyoe as Soft as the default
            
        }

        #endregion
    }
}
