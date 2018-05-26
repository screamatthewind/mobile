using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Rest1
{
    public partial class MainPage : ContentPage
	{
        private DatabaseResource dbResource;

        public MainPage()
		{
            InitializeComponent();

            //Coordinates coordinates = new Coordinates();
            //coordinates.getLocation();

            this.dbResource = new DatabaseResource();

            this.dbResource.ResetDatabase();
            IEnumerable<Location> locations = this.dbResource.RefreshData();
            LoadLocationsView(locations);
        }

        private void LoadLocationsView(IEnumerable<Location> locations)
        {
    //        var tableItems = new List<string> ();

    //        foreach (Location location in locations)
    //        {
				//tableItems.Add (location.locationName);
    //        }

            var locationListView = new ListView {
                ItemsSource = locations,
                RowHeight = 50,
            };

            locationListView.ItemTemplate = new DataTemplate(typeof(TextCell));
            locationListView.ItemTemplate.SetBinding(TextCell.TextProperty, "locationName");

            this.Content = new StackLayout {
                Children = {
                locationListView
                }
            };


			//listView.ItemSelected += async (sender, e) => {
			//	if (e.SelectedItem == null)
			//		return;
			//	listView.SelectedItem = null; // deselect row
   //             await Navigation.PushModalAsync (new DetailPage (e.SelectedItem));
			//};

			//Content = new StackLayout { 
			//	Padding = new Thickness (5, Device.OnPlatform (20, 0, 0), 5, 0),
			//	Children = {
			//		new Label {
			//			HorizontalTextAlignment = TextAlignment.Center,
			//			Text = "Xamarin.Forms built-in ListView"
			//		},
			//		listView
			//	}
			//};
        }

         //var nMgr = (NotificationManager)GetSystemService (NotificationService);
         //   var notification = new Notification (Resource.Drawable.iconBlue, currentTime);
         //   var pendingIntent = PendingIntent.GetActivity (this, 0, new Intent (this, typeof(HomeScreen)), 0);
         //   notification.SetLatestEventInfo (this, "Reminder Service Notification", currentTime, pendingIntent);
         //   nMgr.Notify (0, notification);


        void OnButtonClicked(object sender, System.EventArgs e)
        {
            Button button = (Button)sender;
            // RefreshData();
        }


    }
}
