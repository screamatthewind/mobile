using App1;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationsListPage : ContentPage
    {
        public LocationsListPage()
        {
            InitializeComponent();

			listView.ItemTemplate = new DataTemplate(typeof(TextCell)); // has context actions defined
			listView.ItemTemplate.SetBinding(TextCell.TextProperty, "locationName");
			listView.ItemTemplate.SetBinding(TextCell.DetailProperty, "city");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).CurrentLocationId = -1;
            listView.ItemsSource = await App.Database.getLocationsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactsListPage
            {
                BindingContext = new Location()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((App)App.Current).CurrentLocationId = (e.SelectedItem as Location).Id;
            Debug.WriteLine("setting CurrentLocationId = " + (e.SelectedItem as Location).Id);

            await Navigation.PushAsync(new ContactsListPage
            {
                BindingContext = e.SelectedItem as Contact
            });
        }
    }
}