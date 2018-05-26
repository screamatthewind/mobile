using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Xamarin.Forms;

namespace Rest1
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => {
                return true;
            };

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

			// MainPage = new Rest1.PersonPage();
			MainPage = new Rest1.MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
