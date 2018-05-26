using Plugin.Geolocator;
using GeolocatorSample;
using Plugin.Geolocator.Abstractions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Rest1
{
    class Coordinates
    {
        public async void getLocation()
		{
			try
			{
				var hasPermission = await Utils.CheckPermissions(Permission.Location);
				if (!hasPermission)
					return;

				var locator = CrossGeolocator.Current;
				// locator.DesiredAccuracy = DesiredAccuracy.Value;
				// labelGPS.Text = "Getting gps...";

				var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(60), null, false);

				if (position == null)
				{
					// labelGPS.Text = "null gps :(";
					return;
				}
				//labelGPS.Text = string.Format("Time: {0} \nLat: {1} \nLong: {2} \nAltitude: {3} \nAltitude Accuracy: {4} \nAccuracy: {5} \nHeading: {6} \nSpeed: {7}",
				//	position.Timestamp, position.Latitude, position.Longitude,
				//	position.Altitude, position.AltitudeAccuracy, position.Accuracy, position.Heading, position.Speed);

                var address = await locator.GetAddressesForPositionAsync(position, "RJHqIE53Onrqons5CNOx~FrDr3XhjDTyEXEjng-CRoA~Aj69MhNManYUKxo6QcwZ0wmXBtyva0zwuHB04rFYAPf7qqGJ5cHb03RCDw1jIW8l");
				if (address == null || address.Count() == 0)
				{
					// LabelAddress.Text = "Unable to find address";
				}

				var a = address.FirstOrDefault();

                int i = 1;

			}
			catch (Exception ex)
			{
				// await DisplayAlert("Uh oh", "Something went wrong, but don't worry we captured for analysis! Thanks.", "OK");
			}
			finally
			{
				// ButtonGetGPS.IsEnabled = true;
			}
		}
    }
}
