using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookHavenAppV4.BusinessLayer
{
    public class Inventory
    {
        #region: Data Members

        public string Name;
        public string SKU;
        public string Language;
        public CoverTypes Cover;
        public decimal OriginalQuantity;
        public decimal ReorderPoint;
        public decimal IkhokhaFee;
        public decimal CostExclIkhokha;
        public decimal CostInclIkhokha;
        public decimal ProfitExclIkhokha;
        public decimal ProfitInclIkhokha;
        public decimal PercentProfitExclIkhokha;
        public decimal PercentProfitInclIkhokha;
        public decimal ProfitPercentage;
        public decimal VAT;
        public decimal RetailPrice;
        public decimal TotalSold;
        public decimal OrdersToBeFulfilled;
        public decimal StockToBeReceived;
        public decimal CurrentQuantity;
        public string ISBN;
        #endregion

        #region: Property Methods

        

        public decimal getOriginalQuantity
        {
            get { return OriginalQuantity; }
            set { OriginalQuantity = value; }
        }
        public decimal getReorderPoint 
        {
            get { return ReorderPoint; }
            set { ReorderPoint = value; }
        }
        public decimal getIkhokhaFee
        {
            get { return IkhokhaFee; }
            set { IkhokhaFee = value; }
        }
        public decimal getCostExclIkhokha
        {
            get { return CostExclIkhokha; }
            set { CostExclIkhokha = value; }
        }
        public decimal getCostInclIkhokha
        {
            get { return CostInclIkhokha; }
            set { CostInclIkhokha = value; }
        }
        public decimal getProfitExclIkhokha
        {
            get { return ProfitExclIkhokha; }
            set { ProfitExclIkhokha = value; }
        }
        public decimal getProfitInclIkhokha
        {
            get { return ProfitInclIkhokha; }
            set { ProfitInclIkhokha = value; }
        }
        public decimal getPercentProfitExclIkhokha
        {
            get { return PercentProfitExclIkhokha; }
            set { PercentProfitExclIkhokha = value; }
        }
        public decimal getPercentProfitInclIkhokha
        {
            get { return PercentProfitInclIkhokha; }
            set { PercentProfitInclIkhokha = value; }
        }
        public decimal getProfitPercentage
        {
            get { return ProfitPercentage; }
            set { ProfitPercentage = value; }
        }
        public decimal getVAT
        {
            get { return VAT; }
            set { VAT = value; }
        }
        public decimal getRetailPrice
        {
            get { return RetailPrice; }
            set { RetailPrice = value; }
        }
        public decimal getTotalSold
        {
            get { return TotalSold; }
            set {TotalSold = value; }
        }
        public decimal getOrdersToBeFulfilled
        {
            get { return OrdersToBeFulfilled; }
            set { OrdersToBeFulfilled = value; }
        }
        public decimal getStockToBeReceived
        {
            get { return StockToBeReceived; }
            set { StockToBeReceived = value; }
        }
        public decimal getCurrentQuantity
        {
            get { return CurrentQuantity; }
            set { CurrentQuantity = value; }
        }
        public string getISBN
        {
            get { return ISBN; }
            set { ISBN = value; }
        }
        public string name
        {
            get { return Name; }
            set { Name = value; }
        }

        public string getSKU
        {
            get { return SKU; }
            set { SKU = value; }
        }
        public string getLanguage
        {
            get { return Language; }
            set { Language = value; }
        }


        

        #endregion

        #region: Constructor

        public Inventory(CoverTypes.CoverType coverType) /*class constructor; determines the inventory availability upon being invoked
                                                          this constructor has the parameter availType
                                                          of type Availability.AvailType*/
        {
            switch (coverType) //this switch statement finds the specific availability of the inventory
            {
                case CoverTypes.CoverType.Soft:
                    Cover = new Soft();
                    break;
                case CoverTypes.CoverType.Board:
                    Cover = new Board();
                    break;
            }
        }

        #endregion
    }
}
