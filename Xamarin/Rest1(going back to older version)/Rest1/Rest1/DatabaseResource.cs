using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using Xamarin.Forms;
using System.Linq;
using System.Net;
using System.IO;

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
            var request = HttpWebRequest.Create("https://www.numberhelper.com/api/mobile/all/miles/38.729827/-75.278149/500");
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Console.Out.WriteLine("Response contained empty body...");
                    }
                    else
                    {
                        Console.Out.WriteLine("Response Body: \r\n {0}", content);
                    }

                    // Assert.NotNull(content);
                }
            }

            return null;

            //    var client = new RestClient("http://192.168.1.104:8080");
            //    var request = new RestRequest("api/mobile/all/miles/38.729827/-75.278149/500", Method.GET);
            //    request.Timeout = 10000;

            //    // async with deserialization
            //    IRestResponse<List<LocationContacts>> response = client.Execute<List<LocationContacts>>(request);

            //    if (!response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            //    {
            //        // DisplayAlert ("Error", response.ErrorMessage, "OK");
            //        // callback(null);
            //        return null;
            //    }

            //    lock (collisionLock)
            //    {
            //        foreach (LocationContacts locationContacts in response.Data)
            //        {
            //            Location location = locationContacts.location;
            //            database.Insert(location);

            //            List<Contact> contacts = locationContacts.contacts;
            //            foreach (Contact contact in contacts)
            //            {
            //                database.Insert(contact);
            //            }
            //        }
            //    }

            //    IEnumerable<Location> locations = null;

            //    lock (collisionLock)
            //    {
            //        locations = database.Query<Location>("SELECT * FROM Location ORDER BY locationName").AsEnumerable();
            //    }

            //    return locations;

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
            lock (collisionLock)
            {
                database.DropTable<Location>();
                database.DropTable<Contact>();

                database.CreateTable<Location>();
                database.CreateTable<Contact>();
            }

            this.locations = null;
            this.locations = new ObservableCollection<Location>(database.Table<Location>());

            this.contacts = null;
            this.contacts = new ObservableCollection<Contact>(database.Table<Contact>());
        }
    }
}
