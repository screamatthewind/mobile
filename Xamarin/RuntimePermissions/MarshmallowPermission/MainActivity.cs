using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System.Threading.Tasks;
using Android;
using Android.Support.V4.Content;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using Geolocator.Plugin;
using System;
using Android.Content.PM;
using Android.Views;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using System.Net;
using System.IO;
using System.Net.Security;
using ModernHttpClient;

namespace MarshmallowPermission
{
	[Activity (Label = "Marshmallow Permission", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : AppCompatActivity
	{
        const int RequestLocationId = 0;

        readonly string[] PermissionsLocation =
            {
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessFineLocation,
                Manifest.Permission.Internet
            };

        TextView textLocation;
        Button buttonGetLocation, buttonGetLocationCompat;
        View layout;
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Main);
           
            layout = FindViewById<LinearLayout>(Resource.Id.main_layout);
            textLocation = FindViewById<TextView>(Resource.Id.label);

            buttonGetLocation = FindViewById<Button> (Resource.Id.myButton);
            buttonGetLocationCompat = FindViewById<Button>(Resource.Id.myButtonCompat);

            buttonGetLocation.Click += async (sender, e) => await TryGetLocationAsync();
            buttonGetLocationCompat.Click += async (sender, e) => await GetLocationCompatAsync();
		}

        async Task TryGetLocationAsync()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                GetLocationData();
                await GetLocationAsync();
                return;
            }

            await GetLocationPermissionAsync();
        }

        async Task GetLocationAsync()
        {
  
            textLocation.Text = "Getting Location";
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                var position = await locator.GetPositionAsync(20000);

                textLocation.Text = string.Format("Lat: {0}  Long: {1}", position.Latitude, position.Longitude);
            }
            catch (Exception ex)
            {

                textLocation.Text = "Unable to get location: " + ex.ToString();
            }
        }

        private bool HasPermissionCompat()
        {

            for(int i=0;i<PermissionsLocation.Length;i++)
            {
                if (ContextCompat.CheckSelfPermission(this, PermissionsLocation[i]) != (int)Permission.Granted)
                    return false;
            }

            return true;
        }

        private bool HasPermission()
        {

            for(int i=0;i<PermissionsLocation.Length;i++)
            {
                if (CheckSelfPermission(PermissionsLocation[i]) != (int)Permission.Granted)
                    return false;
            }

            return true;
        }

        async Task GetLocationPermissionAsync()
        {
            // const string permission = Manifest.Permission.AccessFineLocation;

            // if (CheckSelfPermission(permission) == (int)Permission.Granted)
            if (HasPermission())
            {
                GetLocationData();
                await GetLocationAsync();
                return;
            }

            await new NetHttp(this).HttpSample(new NativeMessageHandler());

            var httpClient = new HttpClient(new ModernHttpClient.NativeMessageHandler());


            //if (ShouldShowRequestPermissionRationale(permission))
            //{
            //    //Explain to the user why we need to read the contacts
            //    Snackbar.Make(layout, "Location access is required to show coffee shops nearby.",
            //        Snackbar.LengthIndefinite)
            //        .SetAction("OK", v => RequestPermissions(PermissionsLocation, RequestLocationId))
            //        .Show();
               
            //    return;
            //}

            RequestPermissions(PermissionsLocation, RequestLocationId); 

        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, int[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == (int)Permission.Granted)
                        {
                            //Permission granted
                            var snack = Snackbar.Make(layout, "Location permission is available, getting lat/long.",
                                            Snackbar.LengthShort);
                            snack.Show();
                            
                            GetLocationData();
                            await GetLocationAsync();
                        }
                        else
                        {
                            //Permission Denied :(
                            //Disabling location functionality
                            var snack = Snackbar.Make(layout, "Location permission is denied.", Snackbar.LengthShort);
                            snack.Show();
                        }
                    }
                    break;
            }
        }

        async Task GetLocationCompatAsync()
        {
            // const string permission = Manifest.Permission.AccessFineLocation;

            // if (ContextCompat.CheckSelfPermission(this, permission) == (int)Permission.Granted)
            if (HasPermissionCompat())
            {
                GetLocationData();
                await GetLocationAsync();
                return;
            }

            //if (ActivityCompat.ShouldShowRequestPermissionRationale(this, permission))
            //{
            //    //Explain to the user why we need to read the contacts
            //    Snackbar.Make(layout, "Location access is required to show coffee shops nearby.",
            //        Snackbar.LengthIndefinite)
            //        .SetAction("OK", v => RequestPermissions(PermissionsLocation, RequestLocationId))
            //        .Show();

            //    return;
            //}

            RequestPermissions(PermissionsLocation, RequestLocationId); 
        }

        public  bool hasPermissions()
        {
           if ((int)Build.VERSION.SdkInt < 23)
            {
                    for (int i=0; i<PermissionsLocation.Length; i++)
                {
                    if (ContextCompat.CheckSelfPermission(this, PermissionsLocation[i]) != (int)Permission.Granted)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        void GetLocationData()
        {

            try
            {
            int PERMISSION_ALL = 0;
            if (!hasPermissions())
            {
                RequestPermissions(PermissionsLocation, PERMISSION_ALL);
            }

            RequestPermissions(new string[] { Manifest.Permission.Internet}, PERMISSION_ALL);

            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback((sender, certificate, chain, policyErrors) => { return true; });


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
            } catch (Exception e)
            {
                int i = 1;

            }
        }

    }
}
