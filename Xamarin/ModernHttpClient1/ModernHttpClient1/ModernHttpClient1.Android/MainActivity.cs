using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading;
using System.Threading.Tasks;
using ModernHttpClient;

namespace ModernHttpClient1.Droid
{
    [Activity(Label = "ModernHttpClient1", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static readonly string WisdomUrl = "https://www.numberhelper.com";

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new ModernHttpClient1.App());

            CancellationTokenSource _cts;
            _cts = new CancellationTokenSource();

                Task.Run(async () =>            {
                try
                {
                    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient(new NativeMessageHandler());
                    var stream = await client.GetStreamAsync(MainActivity.WisdomUrl);
                        int i = 1;
                }
                catch (Exception ex)
                {
                        int i = 1;
                }

            }, _cts.Token);

        }
    }
}

