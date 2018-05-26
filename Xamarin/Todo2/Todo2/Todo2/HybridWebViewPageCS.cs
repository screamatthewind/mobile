using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Todo2;
using Xamarin.Forms;

namespace App1
{
    public class HybridWebViewPageCS : ContentPage
    {
        HybridWebView hybridWebView;

        public HybridWebViewPageCS()
        {
            hybridWebView = new HybridWebView
            {
                // Uri = "http://192.168.1.104:8080/#/loginX",
                // Uri = "http://10.0.0.83:8080/#/loginX",
                Uri = "https://www.numberhelper.com/#/loginX",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            // hybridWebView.RegisterAction (data => DisplayAlert ("Alert", "Hello " + data, "OK"));
            hybridWebView.RegisterAction(data => handleDataAsync(data));

            Padding = new Thickness(0, 20, 0, 0);
            Content = hybridWebView;
        }

        public async void handleDataAsync(string data)
        {

            try
            {
                object json = SimpleJson.DeserializeObject(data);

                JsonDeserializer jsonDeserializer = new JsonDeserializer();
                IDictionary<string, object> dictionary = json as IDictionary<string, object>;

                var dataType = dictionary["dataType"];
                if (dataType.Equals("locationContacts"))
                {
                    var locationContactsData = dictionary["data"];
                    List<LocationContacts> locationContactsList = (List<LocationContacts>)jsonDeserializer.ConvertValue(typeof(List<LocationContacts>), locationContactsData);

                    App.Database.ResetLocationContactDatabase();

                    foreach (LocationContacts locationContacts in locationContactsList)
                    {
                        Location location = locationContacts.location;
                        await App.Database.SaveLocationAsync(location);

                        List<Contact> contacts = locationContacts.contacts;
                        foreach (Contact contact in contacts)
                        {
                            await App.Database.SaveContactAsync(contact);
                        }
                    }

                    List<Location> locations = await App.Database.getLocationsAsync();
                    int i = 1;
                }
                else if (dataType.Equals("token"))
                {
                    string token = (string)dictionary["data"];
                    hybridWebView.Token = token;
                }
            }
            catch (SerializationException e)
            {
                int i = 1;
            }

        }
    }
}
