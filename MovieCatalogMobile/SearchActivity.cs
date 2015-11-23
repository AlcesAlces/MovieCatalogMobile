
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

namespace MovieCatalogMobile
{
	[Activity (Label = "SearchActivity")]			
	public class SearchActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.searchactivitylayout);
            Button btn = FindViewById<Button>(Resource.Id.btnSearchGo);
            btn.Click += searchButtonClick;
		}

		protected void searchButtonClick(object sender, EventArgs e)
		{
            EditText et = FindViewById<EditText>(Resource.Id.etSearchInput);
            string toSearch = et.Text;
            var second = new Intent(this, typeof(CatalogActivity));
            second.PutExtra("search", toSearch);
            StartActivity(second);
		}
	}
}

