using BookHavenAppV4.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookHavenAppV4.Properties;
using System.Windows.Forms;

namespace BookHavenAppV4.DatabaseLayer
{
    public class UserDB
    {

        #region: Data Members
        private string table1 = "User";
        private string sqlLocal1 = "SELECT * FROM User";
        private Collection<User> users;

        private string strConn = Settings.Default.UsersConnectionString;
        protected SqlConnection cnMain;
        protected DataSet dsMain = new DataSet();
        protected SqlDataAdapter daMain;
        #endregion

        #region CRUD Enumeration
        public enum DBOperation
        {
            Add = 0,
            Edit = 1,
            Delete = 2
        }
        #endregion

        #region: Property Methods

        public Collection<User> AllUsers
        {
            get { return users; }
        }

        #endregion

        #region: Constructors

        public UserDB() : base()
        {

            try
            {
                //Open a connection & create a new dataset object
                cnMain = new SqlConnection(strConn);// A SqlConnection is an object, just like any other C# object
                dsMain = new DataSet();
            }
            catch (SystemException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error");
                return;
            }

            dsMain = new DataSet();
            users = new Collection<User>();
            FillDataSet(sqlLocal1, table1);
            uAdd2Collection(table1);
        }

        #endregion

        #region Fills dataset fresh from the db for a specific table and with a specific Query        
        public void FillDataSet(string aSQLstring, string aTable)
        {
            try
            {
                daMain = new SqlDataAdapter(aSQLstring, cnMain);// Create an instance of the data adapter (daMain). 
                cnMain.Open();// Opens a database connection with the property settings specified by the ConnectionString.  https://docs.microsoft.com/en-us/dotnet/api/system.data.sqlclient.sqlconnection.open?view=dotnet-plat-ext-5.0      
                              //https://www.c-sharpcorner.com/UploadFile/201fc1/sql-server-database-connection-in-csharp-using-adonet/              
                              //https://www.c-sharpcorner.com/UploadFile/718fc8/working-with-dataset-and-its-methods-in-ado-net/
                daMain.Fill(dsMain, aTable);//Adds or refreshes rows in the DataSet to match those in the data source. https://docs.microsoft.com/en-us/dotnet/api/system.data.common.dataadapter.fill?view=net-5.0
                                            //is used to populate a DataSet with the results of the SelectCommand of the DataAdapter.
                                            //Fill takes as its arguments a DataSet to be populated, and a DataTable object, or the name of the DataTable to be filled with the rows returned from the SelectCommand. https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/populating-a-dataset-from-a-dataadapter
                cnMain.Close();//close the connection
            }
            catch (Exception errObj)
            {
                MessageBox.Show(errObj.Message + "  " + errObj.StackTrace);//https://docs.microsoft.com/en-us/dotnet/api/system.exception.stacktrace?view=net-5.0
            }
        }
        #endregion


        #region Update the data source 

        protected bool UpdateDataSource(string sqlLocal, string table)
        { //The method has two parameters SQL statement to be used to do the update and the table which needs to be updated.
            bool success;//Declare a Boolean variable success that will signal the successful update
            try
            {

                cnMain.Open();   // open the connection

                #region Update the database table via the data adapter
                daMain.Update(dsMain, table);
                /*
                 The Update method of the DataAdapter is called to resolve changes from a DataSet
                 back to the data source. The Update method, like the Fill method, takes as arguments
                 an instance of a DataSet, and an optional DataTable object or DataTable name.
                 When you call the Update method, the DataAdapter analyzes the changes that have been made and
                 executes the appropriate command (INSERT, UPDATE, or DELETE). When the DataAdapter encounters a 
                 change to a DataRow, it uses the InsertCommand, UpdateCommand, or DeleteCommand to process the change. 
                 */

                #endregion

                cnMain.Close(); // close the connection

                #region  Fill the dataset with the SQL statement sqlLocal and the specific table table
                FillDataSet(sqlLocal, table); //refresh the dataset using the SQL statement; and the Table from which the query will emanate from
                #endregion

                success = true;// The variable success returns true (Assign true to the variable success).
            }
            catch (Exception errObj)
            {
                MessageBox.Show(errObj.Message + "  " + errObj.StackTrace);
                success = false;
            }
            finally
            {
            }
            return success;
        }
        #endregion

        #region Utility Methods
        public DataSet GetDataSet()
        {
            
            return dsMain;
        }

        private void uAdd2Collection(string table)
        {
           
            DataRow myRow = null;
            User Use;
            dsMain = new DataSet();
            foreach (DataRow myRow_loopVariable in dsMain.Tables[table].Rows)
            {

                myRow = myRow_loopVariable;
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    Use = new User(); //new inventory object with an availability type parameter
                                                    //Obtain each Inventory attribute from the specific field in the row in the table
                    ;
                    //Do the same for all other attributes
                    Use.getUserName = Convert.ToString(myRow["Username"]).TrimEnd();
                    Use.getPassword = Convert.ToString(myRow["Password"]).TrimEnd();
                    
                    
                    users.Add(Use);
                }
            }
        }
        private void uFillRow(DataRow aRow, User Use, DB.DBOperation operation)
        {
            
            if (operation == DB.DBOperation.Add)
            {
                aRow["Username"] = Use.getUserName;
            }
            aRow["Password"] = Use.getPassword;

            
        }

        private int uFindRow(User Use, string table) // Method that finds the specific inventory
        {
            int rowIndex = 0;
            DataRow myRow;
            int returnVal = -1;
            foreach (DataRow myRow_loopVariable in dsMain.Tables[table].Rows)
            {
                myRow = myRow_loopVariable;
                if (!(myRow.RowState == DataRowState.Deleted))
                {
                    if (Use.getUserName == Convert.ToString(dsMain.Tables[table].Rows[rowIndex]["Username"])) //checks if current Inventory ItemID equals to desired Inventory ItemID
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
        public void uDataSetChange(User Use, DB.DBOperation operation)
        {
            DataRow aRow = null;
            string dataTable = table1; // default table1 i.e. Available
            dataTable = table1;
            switch (operation)
            {
                case DB.DBOperation.Add: //if the operation is add
                    aRow = dsMain.Tables[dataTable].NewRow();// creates a new row
                    uFillRow(aRow, Use, operation); //fills the newly created row with the inventory attributes acording to the operation
                    dsMain.Tables[dataTable].Rows.Add(aRow); //adds the newly created row/inventory item to the table
                    break;
                case DB.DBOperation.Edit: //if the operation is edit
                    aRow = dsMain.Tables[dataTable].Rows[uFindRow(Use, dataTable)]; //finds the row using the findrow method (which uses itemID)
                    uFillRow(aRow, Use, operation); //fills the found row with the inventory attributes according to the operation
                    break;
                case DB.DBOperation.Delete: //if the operation is delete
                    aRow = dsMain.Tables[dataTable].Rows[uFindRow(Use, dataTable)]; //finds the inventory row
                    aRow.Delete(); //deletes this row/inventory item
                    break;
            }
        }
        #endregion

        #region Build Parameters/Create Commands/Update database
        private void uBuild_INSERT_Parameters(User Use)
        {
            SqlParameter param = default(SqlParameter); //Creates variable param of type SqlParameter and sets it to default

            param = new SqlParameter("@Username", SqlDbType.NVarChar, 100, "Username");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Password", SqlDbType.NVarChar, 100, "Password");
            daMain.InsertCommand.Parameters.Add(param);


        }

        private void uBuild_UPDATE_Parameters(User Use) //this method is for updating inventory
        {
            //these are the Parameters to communicate with SQLUPDATE
            SqlParameter param = default(SqlParameter);

            param = new SqlParameter("@Username", SqlDbType.NVarChar, 100, "Username");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);

            //This will be replicated for all fields other than ID and CUSID because those are the onlt attributes that cannot be changes (Key attributes)
            // they are what uniquely identify a customer
            // Only way to "Change" them is to create a new customer 
            param = new SqlParameter("@Password", SqlDbType.NVarChar, 100, "Password");
            param.SourceVersion = DataRowVersion.Current;
            daMain.UpdateCommand.Parameters.Add(param);


        }



        private void uBuild_DELETE_Parameters() //delete function method
        {
            //these are the parameters to communicate with SQLDELETE
            SqlParameter param;
            param = new SqlParameter("@Username", SqlDbType.NVarChar, 100, "Username");
            param.SourceVersion = DataRowVersion.Original;
            daMain.DeleteCommand.Parameters.Add(param);
        }

        private void uCreate_INSERT_Command(User Use) //Create a new Inventory item
        {
            
                
                    daMain.InsertCommand = new SqlCommand("INSERT into User (Username, Password) VALUES (@Username, @Password)", cnMain);
                    
            
            uBuild_INSERT_Parameters(Use);
        }

        private void uCreate_UPDATE_Command(User Use) //Update existing inventory
        {
            
                    daMain.UpdateCommand = new SqlCommand("UPDATE User SET Username =@Username, Password =@Password" + "WHERE Username = @Username", cnMain);
                    
            
            uBuild_UPDATE_Parameters(Use);
        }

        private string uCreate_DELETE_Command(User Use) //Delete an existing inventory item
        {
            string errorString = null;
            //this switch statment finds the inventory's availability type then deletes the values from the appropiate table

            daMain.DeleteCommand = new SqlCommand("DELETE FROM User WHERE Username = @Username", cnMain);


            try
            {
                uBuild_DELETE_Parameters();
            }
            catch (Exception errObj)
            {
                errorString = errObj.Message + "  " + errObj.StackTrace; //error message that describes the error
            }
            return errorString;
        }
        public bool uUpdateDataSource(User Use)
        {
            bool accepted = true;
            uCreate_INSERT_Command(Use);
            uCreate_UPDATE_Command(Use);
            uCreate_DELETE_Command(Use);

            accepted = UpdateDataSource(sqlLocal1, table1);


            return accepted;
        }

        #endregion

    }
}
