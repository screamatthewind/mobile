using System;

using Xamarin.Forms;

namespace CustomRenderer
{
	public class HybridWebViewPageCS : ContentPage
	{
        HybridWebView hybridWebView;

		public HybridWebViewPageCS ()
		{
            hybridWebView = new HybridWebView {
                Uri = "http://10.0.0.83:8080/#/loginX",
				// Uri = "https://www.numberhelper.com:8080/#/loginX",
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			// hybridWebView.RegisterAction (data => DisplayAlert ("Alert", "Hello " + data, "OK"));
			hybridWebView.RegisterAction (data => showData (data));

			Padding = new Thickness (0, 20, 0, 0);
			Content = hybridWebView;
        }

        public void showData(string data)
        {
            hybridWebView.Token = data;
            // hybridWebView.Uri = "http://10.0.0.83:8080/api/location/all";
            // hybridWebView.Uri = "http://10.0.0.83:8080/#/mobile";
        }
	}
}
