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
using System.IO;

namespace MovieCatalogMobile
{
	[Activity (Label = "Your Catalog")]			
	public class CatalogActivity : Activity
	{
		CustomListAdapter listAdapter;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.catalogactivitylayout);
			Stream inputFile = Assets.Open ("SampleMoviesList.xml");
			listAdapter = new CustomListAdapter (this, inputFile);

			var listview = FindViewById<ListView>(Resource.Id.lvMovies);

			listview.Adapter = listAdapter;

			listview.ItemClick += listViewClicked;
		}

		protected void listViewClicked(object sender, EventArgs e)
		{
			int index = (e as Android.Widget.AdapterView.ItemClickEventArgs).Position;
			var textName = FindViewById<TextView> (Resource.Id.txtCatalogName);
			var textYear = FindViewById<TextView> (Resource.Id.txtCatalogYear);
			var textRating = FindViewById<TextView> (Resource.Id.txtCatalogOnlineRate);
			var textRatingUsr = FindViewById<TextView> (Resource.Id.txtCatalogUserRate);

			CustomListAdapter adapter = (sender as ListView).Adapter as CustomListAdapter;

			textName.SetText ("Name: "+(adapter.GetItemAtPosition(index) as Movie).name, TextView.BufferType.Normal);
			textYear.SetText ("Year: " + (adapter.GetItemAtPosition (index) as Movie).displayYear, TextView.BufferType.Normal);
			textRating.SetText ("Online Rating: " + (adapter.GetItemAtPosition (index) as Movie).onlineRating + "/10", TextView.BufferType.Normal);
			textRatingUsr.SetText ("User Rating: " + (adapter.GetItemAtPosition (index) as Movie).userRating + "/10", TextView.BufferType.Normal);
		}
	}
}