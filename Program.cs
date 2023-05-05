using BookHavenAppV4.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB;
using MongoDB.Driver;
using BookHavenAppV4;
using BookHavenAppV4.BusinessLayer;
using MongoDB.Bson;

namespace BookHavenAppV4
{
    public class Program
    {
        MongoCRUD db = new MongoCRUD("AddressBook");
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
         void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            const string connectionUri = "mongodb+srv://uelosano:4XIDw5g4Nv6FPTzo@cluster0.3rdqxmz.mongodb.net/?retryWrites=true&w=majority";

            var settings = MongoClientSettings.FromConnectionString(connectionUri);

            // Set the ServerApi field of the settings object to Stable API version 1
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);

            // Create a new client and connect to the server
            var client = new MongoClient(settings);

            // Send a ping to confirm a successful connection
            try
            {
                var result = client.GetDatabase("admin").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Application.Run(new LoginForm());
        }
    }
}
