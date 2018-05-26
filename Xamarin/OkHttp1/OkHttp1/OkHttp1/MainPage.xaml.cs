using Square.OkHttp3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace OkHttp1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            GetResponseString("hello").Wait();
        }

        public async Task<string> GetResponseString(string url)
        {
            OkHttpClient client = new OkHttpClient();
            Request request = new Request.Builder()
            .Url("https://www.google.com")
            .Build();

            try
            {

                // Synchronous blocking call
                Response response = await client.NewCall(request).ExecuteAsync();
                string body = response.Body().String();

                return body;
            }
            catch (Exception ex)
            {
                int i = 1;
                return null;
            }
        }
    }
}