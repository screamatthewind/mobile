using System.IO;
using Android.OS;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using ModernHttpClient;

namespace HttpClientAndroid1
{
    [Activity(Label = "HttpClientAndroid1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
		public static readonly string WisdomUrl = "https://www.numberhelper.com";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			SetContentView (Resource.Layout.Main);

			Button button = FindViewById<Button> (Resource.Id.button1);

            button.Click += async delegate {
			System.Net.Http.HttpClient client = new System.Net.Http.HttpClient (new NativeMessageHandler());
			var stream = await client.GetStreamAsync (MainActivity.WisdomUrl);
				await new NetHttp (this).HttpSample (new NativeMessageHandler());
			};
        }

        public void RenderStream (Stream stream) {
			var reader = new System.IO.StreamReader (stream);

			var intent = new Intent (this, typeof(ShowStream));
			intent.PutExtra ("string", reader.ReadToEnd ());
			StartActivity (intent);
		}
    }
}

