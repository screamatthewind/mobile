using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Linq;

namespace Rest1
{
    class DatabaseResource
    {
        private SQLiteConnection database;
        public static object collisionLock = new object();

        public ObservableCollection<Location> locations { get; set; }
        public ObservableCollection<Contact> contacts { get; set; }

        public DatabaseResource()
        {
            database = DependencyService.Get<IDatabaseConnection>().DbConnection();
        }

        public IEnumerable<Location> RefreshData()
        {
            // var client = new RestClient("https://api.twilio.com");
            // var request = new RestRequest("2008-08-01", Method.GET);

            // var client = new RestClient("http://192.168.1.104:8080");
            // var client = new RestClient("https://www.numberhelper.com");
            var client = new RestClient("http://www.numberhelper.com:8080/");

            var request = new RestRequest("api/mobile/all/miles/38.729827/-75.278149/500", Method.GET);

            request.Timeout = 10000;

            IRestResponse response = client.Execute(request);

            // async with deserialization
            // IRestResponse<List<LocationContacts>> response = client.Execute<List<LocationContacts>>(request);

            if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                // DisplayAlert ("Error", response.ErrorMessage, "OK");
                // callback(null);
                return null;
            }

            //lock (collisionLock)
            //{
            //    foreach (LocationContacts locationContacts in response.Data)
            //    {
            //        Location location = locationContacts.location;
            //        database.Insert(location);

            //        List<Contact> contacts = locationContacts.contacts;
            //        foreach (Contact contact in contacts)
            //        {
            //            database.Insert(contact);
            //        }
            //    }
            //}

            //IEnumerable<Location> locations = null;

            //lock (collisionLock)
            //{
            //        locations = database.Query<Location>("SELECT * FROM Location ORDER BY locationName").AsEnumerable();
            //}

            return locations;

        }

        public IEnumerable<Location> getLocations()
        {
            lock (collisionLock)
            {
                IEnumerable<Location> locations = database.Query<Location>("SELECT * FROM Location").AsEnumerable();

                return locations;
            }
        }

        public void ResetDatabase()
        {
            lock(collisionLock)
            {
                database.DropTable<Location>();
                database.DropTable<Contact>();

                database.CreateTable<Location>();
                database.CreateTable<Contact>();
            }

            this.locations = null;
            this.locations = new ObservableCollection<Location> (database.Table<Location>());

            this.contacts = null;
            this.contacts = new ObservableCollection<Contact> (database.Table<Contact>());
        }            
    }
}
