using BookHavenAppV4.DatabaseLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookHavenAppV4.BusinessLayer
{
    public class InventoryController
    {

        #region Data Members
        private InventoryDB inventoryDB;//make reference 
        private Collection<Inventory> inventories;
        private Inventory inv;
        #endregion

        #region Properties
        public Collection<Inventory> AllInventory        
        {
            get
            {
                return inventories;
            }
        }
        #endregion

        #region Constructor
        public InventoryController()
        {
            //***instantiate the InventoryDB object to communicate with the database
            inventoryDB = new InventoryDB();
            inventories = inventoryDB.AllInventory;
        }
        #endregion

        #region Database Communication.
        public void DataMaintenance(Inventory anInv, DB.DBOperation operation)
        {
            int index = 0;
            //perform a given database operation to the dataset in meory; 
            inventoryDB.iDataSetChange(anInv, operation);//calling method to do the insert
            switch (operation)
            {
                case DB.DBOperation.Add:
                    //*** Add the inventory to the Collection
                    inventories.Add(anInv);
                    break;
                case DB.DBOperation.Edit:
                    index = FindIndex(anInv);
                    inventories[index] = anInv;  // replace invetory at this index with the updated inventory
                                               //  inventories.Add(anInv);
                    break;
                case DB.DBOperation.Delete:
                    index = FindIndex(anInv);  // find the index of the specific inventory in collection
                    inventories.RemoveAt(index);  // remove that inventory item form the collection
                    break;
            }
        }

        //***Commit the changes to the database
        public bool FinalizeChanges(Inventory inventory)
        {
            //***call the InventoryDB method that will commit the changes to the database
            return inventoryDB.iUpdateDataSource(inventory);

        }
        #endregion

        #region Search Methods
        //This method  (function) searched through all the inventoryy to finds onlly those with the required cover
        public Collection<Inventory> FindByRole(Collection<Inventory> invs, CoverTypes.CoverType coverVal)
        {
            Collection<Inventory> matches = new Collection<Inventory>();

            foreach (Inventory inv in invs)
            {
                if (inv.Cover.getCoverType == coverVal)
                {
                    matches.Add(inv);
                }
            }
            return matches;
        }

        public Inventory Find(string Name)
        {
            int index = 0;
            bool found = (inventories[index].name == Name);  //check if it is the first record
            int count = inventories.Count;
            while (!(found) && (index < inventories.Count - 1))  //if not "this" record and you are not at the end of the list 
            {
                index = index + 1;
                found = (inventories[index].name == Name);   // this will be TRUE if found
            }
            return inventories[index];  // this is the one!  
        }

        public int FindIndex(Inventory anInv)
        {
            int counter = 0;
            bool found = false;
            found = (anInv.name == inventories[counter].name);   //using a Boolean Expression to initialise found
            while (!(found) & counter < inventories.Count - 1)
            {
                counter += 1;
                found = (anInv.name == inventories[counter].name);
            }
            if (found)
            {
                return counter;
            }
            else
            {
                return -1;
            }
        }
        #endregion

    }
}
