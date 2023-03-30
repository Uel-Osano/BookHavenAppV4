using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookHavenAppV4.DatabaseLayer.InventoryDB;
using BookHavenAppV4.BusinessLayer;

namespace BookHavenAppV4.DatabaseLayer
{
    public class InventoryDB : DB
    {

        #region: Data Members
        private string table1 = "Inventory";
        private string sqlLocal1 = "SELECT * FROM Inventory";
        private Collection<Inventory> inventories;
        #endregion

        #region: Property Methods

        public Collection<Inventory> AllInventory
        {
            get { return inventories; }
        }

        #endregion

        #region: Constructors

        public InventoryDB() : base()
        {
            inventories = new Collection<Inventory>();
            FillDataSet(sqlLocal1, table1);
            iAdd2Collection(table1);
        }

        #endregion

        #region Utility Methods
        public DataSet GetDataSet()
        {
            return dsMain;
        }

        private void iAdd2Collection(string table)
        {
            DataRow myRow = null;
            Inventory Inv;
            Soft soft;
            Board board;


            CoverTypes.CoverType coverType = CoverTypes.CoverType.Soft;
            switch (table)
            {
                case "Soft":
                    coverType = CoverTypes.CoverType.Soft;
                    break;
                case "Board":
                    coverType = CoverTypes.CoverType.Board;
                    break;
            }

            foreach (DataRow myRow_loopVariable in dsMain.Tables[table].Rows)
            {
                myRow = myRow_loopVariable;
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    Inv = new Inventory(coverType); //new inventory object with an availability type parameter
                                                    //Obtain each Inventory attribute from the specific field in the row in the table
                    ;
                    //Do the same for all other attributes
                    Inv.name = Convert.ToString(myRow["Name"]).TrimEnd();
                    Inv.getSKU = Convert.ToString(myRow["SKU"]).TrimEnd();
                    Inv.getLanguage = Convert.ToString(myRow["Language"]);
                    Inv.Cover.getCoverType = (CoverTypes.CoverType)Convert.ToByte(myRow["Cover"]);
                    Inv.getOriginalQuantity = Convert.ToDecimal(myRow["OriginalQuantity"]);
                    Inv.getReorderPoint = Convert.ToDecimal(myRow["ReorderPoint"]);
                    Inv.getIkhokhaFee = Convert.ToDecimal(myRow["IkhokhaFee"]);
                    Inv.getCostExclIkhokha = Convert.ToDecimal(myRow["CostExclIkhokha"]);
                    Inv.getCostInclIkhokha = Convert.ToDecimal(myRow["CostIncludingIkhokha"]);
                    Inv.getProfitExclIkhokha = Convert.ToDecimal(myRow["ProfitExcludingIkhokha"]);
                    Inv.getProfitInclIkhokha = Convert.ToDecimal(myRow["ProfitIncludingIkhokha"]);
                    Inv.getPercentProfitExclIkhokha = Convert.ToDecimal(myRow["PercentProfitExcludingIkhokha"]);
                    Inv.getPercentProfitInclIkhokha = Convert.ToDecimal(myRow["PercentProfitIncludingIkhokha"]);
                    Inv.getProfitPercentage = Convert.ToDecimal(myRow["ProfitPercentage"]);
                    Inv.getVAT = Convert.ToDecimal(myRow["VAT"]);
                    Inv.getRetailPrice = Convert.ToDecimal(myRow["RetailPrice"]);
                    Inv.getTotalSold = Convert.ToDecimal(myRow["TotalSold"]);
                    Inv.getOrdersToBeFulfilled = Convert.ToDecimal(myRow["OrdersToBeFulfilled"]);
                    Inv.getStockToBeReceived = Convert.ToDecimal(myRow["StockToBeReceived"]);
                    Inv.getCurrentQuantity = Convert.ToDecimal(myRow["CurrentQuantity"]);
                    Inv.getISBN = Convert.ToString(myRow["ISBN"]).TrimEnd();
                    ;
                    //Depending on what its availability . (Whether its Available or Not Available)
                    switch (Inv.Cover.getCoverType)
                    {

                        case CoverTypes.CoverType.Soft:
                            soft = (Soft)Inv.Cover;
                            break;
                        case CoverTypes.CoverType.Board:
                            board = (Board)Inv.Cover;
                            break;
                    }
                    inventories.Add(Inv);
                }
            }
        }
        private void iFillRow(DataRow aRow, Inventory Inv, DB.DBOperation operation)
        {
            Soft soft;
            Board board;
            if (operation == DB.DBOperation.Add)
            {
                aRow["Name"] = Inv.name;
            }
            aRow["Name"] = Inv.name;
            aRow["SKU"] = Inv.getSKU;
            aRow["language"] = Inv.getLanguage;
            aRow["Cover"] = (byte)Inv.Cover.getCoverType; //byte because its availability is an enum of only one byte length
            aRow["OriginalQuantity"] = Inv.getOriginalQuantity;
            aRow["ReorderPoint"] = Inv.getReorderPoint;
            aRow["IkhokhaFee"] = Inv.getIkhokhaFee;
            aRow["CostExclIkhokha"] = Inv.getCostExclIkhokha;
            aRow["CostInclIkhokha"] = Inv.getCostInclIkhokha;
            aRow["ProfitExclIkhokha"] = Inv.getProfitExclIkhokha;
            aRow["ProfitInclIkhokha"] = Inv.getProfitInclIkhokha;
            aRow["PercentProfitExclIkhokha"] = Inv.getPercentProfitExclIkhokha;
            aRow["PercentProfitInclIkhokha"] = Inv.getPercentProfitInclIkhokha;
            aRow["ProfitPercentage"] = Inv.getProfitPercentage;
            aRow["VAT"] = Inv.getVAT;
            aRow["OrdersToBeFulfilled"] = Inv.getOrdersToBeFulfilled;
            aRow["StockToBeReceived"] = Inv.getStockToBeReceived;
            aRow["CurrentQuantity"] = Inv.getCurrentQuantity;
            aRow["ISBN"] = Inv.getISBN;

            //*** For each availability type add the specific data variables
            switch (Inv.Cover.getCoverType)
            {
                case CoverTypes.CoverType.Soft:
                    soft = (Soft)Inv.Cover;
                    break;
                case CoverTypes.CoverType.Board:
                    board = (Board)Inv.Cover;
                    break;
            }
        }

        private int iFindRow(Inventory Inv, string table) // Method that finds the specific inventory
        {
            int rowIndex = 0;
            DataRow myRow;
            int returnVal = -1;
            foreach (DataRow myRow_loopVariable in dsMain.Tables[table].Rows)
            {
                myRow = myRow_loopVariable;
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    if (Inv.name == Convert.ToString(dsMain.Tables[table].Rows[rowIndex]["Name"])) //checks if current Inventory ItemID equals to desired Inventory ItemID
                    {
                        returnVal = rowIndex; // if the they do equal it then returns that inventory/ inventory row
                    }
                }
                rowIndex += 1; //if the dont equal the method goes to the next inventory item and keeps looking
            }
            return returnVal;
        }
        #endregion

        #region CRUD Database Operations 
        public void iDataSetChange(Inventory Inv, DB.DBOperation operation)
        {
            DataRow aRow = null;
            string dataTable = table1; // default table1 i.e. Available
            dataTable = table1;
            switch (operation)
            {
                case DB.DBOperation.Add: //if the operation is add
                    aRow = dsMain.Tables[dataTable].NewRow();// creates a new row
                    iFillRow(aRow, Inv, operation); //fills the newly created row with the inventory attributes acording to the operation
                    dsMain.Tables[dataTable].Rows.Add(aRow); //adds the newly created row/inventory item to the table
                    break;
                case DB.DBOperation.Edit: //if the operation is edit
                    aRow = dsMain.Tables[dataTable].Rows[iFindRow(Inv, dataTable)]; //finds the row using the findrow method (which uses itemID)
                    iFillRow(aRow, Inv, operation); //fills the found row with the inventory attributes according to the operation
                    break;
                case DB.DBOperation.Delete: //if the operation is delete
                    aRow = dsMain.Tables[dataTable].Rows[iFindRow(Inv, dataTable)]; //finds the inventory row
                    aRow.Delete(); //deletes this row/inventory item
                    break;
            }
        }
        #endregion

        #region Build Parameters/Create Commands/Update database
        private void iBuild_INSERT_Parameters(Inventory Inv)
        {
            SqlParameter param = default(SqlParameter); //Creates variable param of type SqlParameter and sets it to default
            
            param = new SqlParameter("@Name", SqlDbType.NVarChar, 100, "Name");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@SKU", SqlDbType.NVarChar, 100, "SKU");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Language", SqlDbType.NVarChar, 100, "Language");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Cover", SqlDbType.TinyInt, 1, "Cover");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@OriginalQuantity", SqlDbType.Decimal, 18, "OriginalQuantity");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@ReorderPoint", SqlDbType.Decimal, 18, "ReorderPoint");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@IkhokhaFee", SqlDbType.Decimal, 18, "IkhokhaFee");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@CostInclIkhokha", SqlDbType.Decimal, 18, "CostInclIkhokha");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@CostExclIkhokha", SqlDbType.Decimal, 18, "CostExclIkhokha");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@ProfitExclIkhokha", SqlDbType.Decimal, 18, "ProfitExclIkhokha");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@ProfitInclIkhokha", SqlDbType.Decimal, 18, "ProfitInclIkhokha");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PercentProfitExclIkhokha", SqlDbType.Decimal, 18, "PercentProfitExclIkhokha");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PercentProfitInclIkhokha", SqlDbType.Decimal, 18, "PercentProfitInclIkhokha");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@ProfitPercentage", SqlDbType.Decimal, 18, "ProfitPercentage");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@VAT", SqlDbType.Decimal, 18, "VAT");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@RetailPrice", SqlDbType.Decimal, 18, "RetailPrice");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@TotalSold", SqlDbType.Decimal, 18, "TotalSold");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@OrdersToBeFulfilled", SqlDbType.Decimal, 18, "OrdersToBeFulfilled");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@StockToBeReceived", SqlDbType.Decimal, 18, "StockToBeReceived");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@CurrentQuantity", SqlDbType.Decimal, 18, "CurrentQuantity");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@ISBN", SqlDbType.NVarChar, 100, "ISBN");
            daMain.InsertCommand.Parameters.Add(param);

        }

        private void iBuild_UPDATE_Parameters(Inventory Inv) //this method is for updating inventory
        {
            //these are the Parameters to communicate with SQLUPDATE
            SqlParameter param = default(SqlParameter);

            param = new SqlParameter("@Name", SqlDbType.NVarChar, 100, "Name");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            //This will be replicated for all fields other than ID and CUSID because those are the onlt attributes that cannot be changes (Key attributes)
            // they are what uniquely identify a customer
            // Only way to "Change" them is to create a new customer 
            param = new SqlParameter("@SKU", SqlDbType.NVarChar, 100, "SKU");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Language", SqlDbType.NVarChar, 100, "Language");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Cover", SqlDbType.TinyInt, 1, "Cover");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@OriginalQuantity", SqlDbType.Decimal, 18, "OriginalQuantity"); 
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@ReorderPoint", SqlDbType.Decimal, 18, "ReorderPoint");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@IkhokhaFee", SqlDbType.Decimal, 18, "IkhokhaFee");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@CostInclIkhokha", SqlDbType.Decimal, 18, "CostInclIkhokha");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@CostExclIkhokha", SqlDbType.Decimal, 18, "CostExclIkhokha");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@ProfitExclIkhokha", SqlDbType.Decimal, 18, "ProfitExclIkhokha");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@ProfitInclIkhokha", SqlDbType.Decimal, 18, "ProfitInclIkhokha");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PercentProfitExclIkhokha", SqlDbType.Decimal, 18, "PercentProfitExclIkhokha");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PercentProfitInclIkhokha", SqlDbType.Decimal, 18, "PercentProfitInclIkhokha");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@ProfitPercentage", SqlDbType.Decimal, 18, "ProfitPercentage");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@VAT", SqlDbType.Decimal, 18, "VAT");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@RetailPrice", SqlDbType.Decimal, 18, "RetailPrice");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@TotalSold", SqlDbType.Decimal, 18, "TotalSold");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@OrdersToBeFulfilled", SqlDbType.Decimal, 18, "OrdersToBeFulfilled");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@StockToBeReceived", SqlDbType.Decimal, 18, "StockToBeReceived");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@CurrentQuantity", SqlDbType.Decimal, 18, "CurrentQuantity");
            param.SourceVersion = DataRowVersion.Original;
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@ISBN", SqlDbType.NVarChar, 100, "ISBN");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

        }
    


        private void iBuild_DELETE_Parameters() //delete function method
        {
            //these are the parameters to communicate with SQLDELETE
            SqlParameter param;
            param = new SqlParameter("@Name", SqlDbType.NVarChar, 100, "Name");
            param.SourceVersion = DataRowVersion.Original;
            daMain.DeleteCommand.Parameters.Add(param);
        }

        private void iCreate_INSERT_Command(Inventory Inv) //Create a new Inventory item
        {
            switch (Inv.Cover.getCoverType)
            {
                case CoverTypes.CoverType.Soft:
                    daMain.InsertCommand = new SqlCommand("INSERT into Inventory (Name, SKU, Language, Cover, OriginalQuantity, ReorderPoint, IkhokhaFee, CostInclIkhokha, CostExclIkhokha, ProfitExclIkhokha, ProfitInclIkhokha, PercentProfitExclIkhokha, PercentProfitInclIkhokha, ProfitPercentage, VAT, RetailPrice, TotalSold, OrdersToBeFulfilled, StockToBeReceived, CurrentQuantity, ISBN) VALUES (@Name, @SKU, @Language, @Cover, @OriginalQuantity, @ReorderPoint, @IkhokhaFee, @CostInclIkhokha, @CostExclIkhokha, @ProfitExclIkhokha, @ProfitInclIkhokha, @PercentProfitExclIkhokha, @PercentProfitInclIkhokha, @ProfitPercentage, @VAT, @RetailPrice, @TotalSold, @OrdersToBeFulfilled, @StockToBeReceived, @CurrentQuantity, @ISBN)", cnMain);
                    break;
                case CoverTypes.CoverType.Board:
                    daMain.InsertCommand = new SqlCommand("INSERT into Inventory (Name, SKU, Language, Cover, OriginalQuantity, ReorderPoint, IkhokhaFee, CostInclIkhokha, CostExclIkhokha, ProfitExclIkhokha, ProfitInclIkhokha, PercentProfitExclIkhokha, PercentProfitInclIkhokha, ProfitPercentage, VAT, RetailPrice, TotalSold, OrdersToBeFulfilled, StockToBeReceived, CurrentQuantity, ISBN) VALUES (@Name, @SKU, @Language, @Cover, @OriginalQuantity, @ReorderPoint, @IkhokhaFee, @CostInclIkhokha, @CostExclIkhokha, @ProfitExclIkhokha, @ProfitInclIkhokha, @PercentProfitExclIkhokha, @PercentProfitInclIkhokha, @ProfitPercentage, @VAT, @RetailPrice, @TotalSold, @OrdersToBeFulfilled, @StockToBeReceived, @CurrentQuantity, @ISBN)", cnMain);
                    break;
            }
            iBuild_INSERT_Parameters(Inv);
        }

        private void iCreate_UPDATE_Command(Inventory Inv) //Update existing inventory
        {
            switch (Inv.Cover.getCoverType)
            {
                case CoverTypes.CoverType.Soft:
                    daMain.UpdateCommand = new SqlCommand("Update Inventory SET Name =@Name, SKU =@SKU, Language =@Language, Cover =@Cover, OriginalQuantity =@OriginalQuantity, ReorderPoint =@ReorderPoint, IkhokhaFee =@IkhokhaFee, CostInclIkhokha =@CostInclIkhokha, CostExclIkhokha =@CostExclIkhokha, ProfitExclIkhokha = @ProfitExclIkhokha, ProfitInclIkhokha =@ProfitInclIkhokha, PercentProfitExclIkhokha =@PercentProfitExclIkhokha, PercentProfitInclIkhokha =@PercentProfitInclIkhokha, ProfitPercentage, VAT =@VAT, RetailPrice =@RetailPrice, TotalSold =@TotalSold, OrdersToBeFulfilled =@OrdersToBeFulfilled, StockToBeReceived =@StockToBeReceived, CurrentQuantity =@CurrentQuantity, ISBN =@ISBN" + "WHERE Name = @Name", cnMain);
                    break;
                case CoverTypes.CoverType.Board:
                    daMain.UpdateCommand = new SqlCommand("Update Inventory SET Name =@Name, SKU =@SKU, Language =@Language, Cover =@Cover, OriginalQuantity =@OriginalQuantity, ReorderPoint =@ReorderPoint, IkhokhaFee =@IkhokhaFee, CostInclIkhokha =@CostInclIkhokha, CostExclIkhokha =@CostExclIkhokha, ProfitExclIkhokha = @ProfitExclIkhokha, ProfitInclIkhokha =@ProfitInclIkhokha, PercentProfitExclIkhokha =@PercentProfitExclIkhokha, PercentProfitInclIkhokha =@PercentProfitInclIkhokha, ProfitPercentage, VAT =@VAT, RetailPrice =@RetailPrice, TotalSold =@TotalSold, OrdersToBeFulfilled =@OrdersToBeFulfilled, StockToBeReceived =@StockToBeReceived, CurrentQuantity =@CurrentQuantity, ISBN =@ISBN" + "WHERE Name = @Name", cnMain); break;
            }
            iBuild_UPDATE_Parameters(Inv);
        }

        private string iCreate_DELETE_Command(Inventory Inv) //Delete an existing inventory item
        {
            string errorString = null;
            //this switch statment finds the inventory's availability type then deletes the values from the appropiate table
            
                    daMain.DeleteCommand = new SqlCommand("DELETE FROM Inventory WHERE Name = @Name", cnMain);
                    
            
            try
            {
                iBuild_DELETE_Parameters();
            }
            catch (Exception errObj)
            {
                errorString = errObj.Message + "  " + errObj.StackTrace; //error message that describes the error
            }
            return errorString;
        }
        public bool iUpdateDataSource(Inventory Inv)
        {
            bool accepted = true;
            iCreate_INSERT_Command(Inv);
            iCreate_UPDATE_Command(Inv);
            iCreate_DELETE_Command(Inv);
            
                    accepted = UpdateDataSource(sqlLocal1, table1);
                    
              
            return accepted;
        }

        #endregion

    }
}

