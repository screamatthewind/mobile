using App1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Todo2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactDetailPage : ContentPage
	{
		public ContactDetailPage ()
		{
			InitializeComponent ();
		}

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ContactDetailPage
            {
                BindingContext = new Contact()
            });
        }

        async void OnSaveClicked(object sender, EventArgs e)
		{
			var contact = (Contact)BindingContext;
			await App.Database.SaveContactAsync(contact);
			await Navigation.PopAsync();
		}

		async void OnDeleteClicked(object sender, EventArgs e)
		{
			var contact = (Contact)BindingContext;
			await App.Database.DeleteContactAsync(contact);
			await Navigation.PopAsync();
		}

		async void OnCancelClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		void OnSpeakClicked(object sender, EventArgs e)
		{
			var contact = (Contact)BindingContext;
			DependencyService.Get<ITextToSpeech>().Speak(contact.contactType + " " + contact.contactName + " " + contact.phoneNumber);
		}

    }
}