using BookHavenAppV4.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookHavenAppV4.DatabaseLayer
{
    public class DB
    {
        #region Variable declaration
        private string strConn = Settings.Default.UsersConnectionString;
        protected SqlConnection cnMain;
        protected DataSet dsMain;
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

        #region Constructor

        public DB()
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
    }
}
