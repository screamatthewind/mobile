using SimpleToolbarsForms.Views;
using Xamarin.Forms;

namespace SimpleToolbarsForms
{
	public class App : Application
	{
		public App()
		{
			MainPage = new TabbedPage(new MainView());
		}
	}
}