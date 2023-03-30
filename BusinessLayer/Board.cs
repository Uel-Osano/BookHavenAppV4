using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookHavenAppV4.BusinessLayer.CoverTypes;

namespace BookHavenAppV4.BusinessLayer
{
    public class Board: CoverTypes
    {
        #region: Data Members

        #endregion


        #region: Constructor

        public Board() : base()
        {
            getCoverType = CoverType.Board;
        }
        #endregion
    }
}
