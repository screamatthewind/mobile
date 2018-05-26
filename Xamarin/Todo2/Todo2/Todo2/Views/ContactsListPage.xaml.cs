using App1;
using System;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactsListPage : ContentPage
    {
        public ContactsListPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            long currentLocationId = ((App)App.Current).CurrentLocationId;
            listView.ItemsSource = await App.Database.getContactsByLocationIdAsync(currentLocationId);
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactDetailPage
            {
                BindingContext = new Contact()
            });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((App)App.Current).CurrentContactId = (e.SelectedItem as Contact).Id;
            Debug.WriteLine("setting CurrentContactId = " + (e.SelectedItem as Contact).Id);

            await Navigation.PushAsync(new ContactDetailPage
            {
                BindingContext = e.SelectedItem as Contact
            });
        }
    }
}