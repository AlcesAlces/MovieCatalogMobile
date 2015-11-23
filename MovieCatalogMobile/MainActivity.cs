using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MovieCatalogMobile
{
	[Activity (Label = "MovieCatalogMobile", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			FileHandlers.verifyUserFile ();
			// Get our button from the layout resource,
			// and attach an event to it
			Button button1 = FindViewById<Button> (Resource.Id.btnMainCollection);
			Button button3 = FindViewById<Button> (Resource.Id.btnMainSettings);
            Button button2 = FindViewById<Button>(Resource.Id.btnMainSearch);
			
			button1.Click += catalogClicked;
            button2.Click += searchClicked;
			button3.Click += settingsClicked;
		}

		protected void catalogClicked(object sender, EventArgs e)
		{
			StartActivity (typeof(CatalogActivity));
		}

		protected void settingsClicked(object sender, EventArgs e)
		{
			StartActivity (typeof(SettingsActivity));
		}

        protected void searchClicked(object sender, EventArgs e)
        {
            StartActivity(typeof(SearchActivity));
        }
	}
}