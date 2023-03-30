using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHavenAppV4.BusinessLayer
{
    public class Soft: CoverTypes
    {
        #region: Data Members

        #endregion


        #region: Constructor

        public Soft() : base()
        {
            getCoverType = CoverType.Soft;
        }
        #endregion
    }
}
