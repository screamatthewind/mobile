using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HttpClient1
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

            Task.Run(async () =>
            {
                GetData getData = new GetData();
                await getData.GetDataAsync();
            });
		}
	}
}
