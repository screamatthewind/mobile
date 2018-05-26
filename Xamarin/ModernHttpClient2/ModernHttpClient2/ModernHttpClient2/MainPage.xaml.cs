using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ModernHttpClient2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
               string s = await GetResponseString("https://www.numberhelper.com");
                // GetJsonAsync<String>("https://www.google.com").Wait();

                int i = 1;
            });
        }

        public async Task<string> GetResponseString(string url)
        {
            var client = new HttpClient(new NativeMessageHandler());
            var result = await client.GetAsync(url);
            return await result.Content.ReadAsStringAsync();
        }

        static HttpClient Client = new HttpClient(new NativeMessageHandler());

        async Task<T> GetJsonAsync<T>(string url)
        {
            T item = default(T);
            var response = await Client.SendAsync(new HttpRequestMessage(HttpMethod.Get, new Uri(url)));
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                // item = JsonConvert.DeserializeObject<T>(json);
            }
            return item;
        }
    }
}

