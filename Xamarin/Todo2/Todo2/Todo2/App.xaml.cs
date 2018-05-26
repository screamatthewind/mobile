using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Todo2;
using Xamarin.Forms;

namespace Todo2
{
    public partial class App : Application
    {
        static DatabaseUtils database;

        public App()
        {
            InitializeComponent();

            // MainPage = new Todo2.MainPage();
            Resources = new ResourceDictionary();
            Resources.Add("primaryGreen", Color.FromHex("91CA47"));
            Resources.Add("primaryDarkGreen", Color.FromHex("6FA22E"));

            var nav = new NavigationPage(new LocationsListPage());
            nav.BarBackgroundColor = (Color)App.Current.Resources["primaryGreen"];
            nav.BarTextColor = Color.White;

            MainPage = nav;
            // MainPage = new HybridWebViewPageCS();
        }

        public static DatabaseUtils Database
        {
            get
            {
                if (database == null)
                {
                    database = new DatabaseUtils(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return database;
            }
        }

        public int ResumeAtTodoId { get; set; }
        public long CurrentLocationId{ get; set; }
        public long CurrentContactId { get; set; }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
