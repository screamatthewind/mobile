using Xamarin.Forms;

namespace MVVMUtopia
{
	public partial class FirstPage : ContentPage
	{
		public FirstPage()
		{
			InitializeComponent();

		}

         protected override void OnAppearing()
        {
            Label Label1 = this.FindByName<Label>("Label1");
            Label1.IsVisible = false;

            int x = 1;

        }
	}
}
