using ModernHttpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ModernHttpClient1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Task.Run(async () =>
            {
               string s = await GetResponseString("https://www.numberhelper.com/api/mobile/all/miles/38.729827/-75.278149/100");
                // GetJsonAsync<String>("https://www.google.com").Wait();

                int i = 1;
            });
        }

        public async Task<string> GetResponseString(string url)
        {
            var client = new HttpClient(new NativeMessageHandler());
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + "eyJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJlYWNiMjQxZS04ZDdhLTExZTctOWZiYS0wYTAwMjcwMDAwMDYiLCJpc3MiOiI3My4xMjguMTUwLjg5IiwiZXhwIjoxNTA3MzIzNDU2LCJpYXQiOjE1MDYxMTM4NTZ9.eNlnxA4Bg9-8Q1fwmhwz7tZ03MUKVDIQ6gEVbI_RNn0");

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

