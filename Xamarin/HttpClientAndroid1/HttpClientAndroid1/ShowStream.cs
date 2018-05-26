using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace HttpClientAndroid1
{
	[Activity (Label = "Resulting Stream")]			
	public class ShowStream : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.ShowStream);

			var textview = FindViewById<TextView> (Resource.Id.html_textView);
			textview.Text = this.Intent.GetStringExtra ("string") ?? Resources.GetString (Resource.String.no_data);
		}
	}
}