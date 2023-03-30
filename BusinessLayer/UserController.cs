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
    public class UserController
    {
        #region Data Members
        public UserDB userDB;//make reference 
        private Collection<User> users;
        private User use;
        #endregion

        #region Properties
        public Collection<User> AllUsers
        {
            get
            {
                return users;
            }
        }
        #endregion

        #region Constructor
        public UserController()
        {
            //***instantiate the UserDB object to communicate with the database
            userDB = new UserDB();
            users = userDB.AllUsers;
        }
        #endregion

        #region Database Communication.
        public void DataMaintenance(User aUser, DB.DBOperation operation)
        {
            int index = 0;
            //perform a given database operation to the dataset in meory; 
            userDB.uDataSetChange(aUser, operation);//calling method to do the insert
            switch (operation)
            {
                case DB.DBOperation.Add:
                    //*** Add the employee to the Collection
                    users.Add(aUser);
                    break;
                case DB.DBOperation.Edit:
                    index = uFindIndex(aUser);
                    users[index] = aUser;  // replace employee at this index with the updated employee
                                               //  employees.Add(anEmp);
                    break;
                case DB.DBOperation.Delete:
                    index = uFindIndex(aUser);  // find the index of the specific employee in collection
                    users.RemoveAt(index);  // remove that employee form the collection
                    break;
            }
        }

        //***Commit the changes to the database
        public bool FinalizeChanges(User user)
        {
            //***call the EmployeeDB method that will commit the changes to the database
            return userDB.uUpdateDataSource(user);

        }
        #endregion

        #region Search Methods
        //This method  (function) searched through all the employess to finds onlly those with the required role
        public Collection<User> FindByRole(Collection<User> us)
        {
            Collection<User> matches = new Collection<User>();

            foreach (User u in us)
            {
                
                    matches.Add(u);
                
            }
            return matches;
        }

        public User Find(string Username)
        {
            int index = 0;
            bool found = (users[index].getUserName == Username);  //check if it is the first record
            int count = users.Count;
            while (!(found) && (index < users.Count - 1))  //if not "this" record and you are not at the end of the list 
            {
                index = index + 1;
                found = (users[index].getUserName == Username);   // this will be TRUE if found
            }
            return users[index];  // this is the one!  
        }

        public int uFindIndex(User aUser)
        {
            int counter = 0;
            bool found = false;
            found = (aUser.getUserName == users[counter].getUserName);   //using a Boolean Expression to initialise found
            while (!(found) & counter < users.Count - 1)
            {
                counter += 1;
                found = (aUser.getUserName == users[counter].getUserName);
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
