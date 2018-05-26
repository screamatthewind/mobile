using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Android;
using Android.Support.Design.Widget;
using Android.Support.V4.Content;
using Android.Support.V4.App;

namespace Rest1.Droid
{
    [Activity(Label = "Rest1", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            TryGetLocationAsync();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new Rest1.App());
        }

        async Task TryGetLocationAsync()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
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
        string[] PermissionsLocation =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation
        };

        const int RequestLocationId = 0;

        async Task GetLocationPermissionAsync()
        {
            const string permission = Manifest.Permission.AccessFineLocation;
            if (ContextCompat.CheckSelfPermission(this, permission) == (int)Permission.Granted)
            {
                await GetLocationAsync();
                return;
            }

            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, permission))
            {
                //Explain to the user why we need to read the contacts
                Snackbar.Make(layout, "Location access is required to show coffee shops nearby.", Snackbar.LengthIndefinite)
                        .SetAction("OK", v => ActivityCompat.RequestPermissions(this, PermissionsLocation, RequestLocationId))
                        .Show();

                return;
            }

            ActivityCompat.RequestPermissions(this, PermissionsLocation, RequestLocationId);
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                    {
                        if (grantResults[0] == Permission.Granted)
                        {
                            //Permission granted
                            var snack = Snackbar.Make(layout, "Location permission is available, getting lat/long.", Snackbar.LengthShort);
                            snack.Show();

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
    }
}

