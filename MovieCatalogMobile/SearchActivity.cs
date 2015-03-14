
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
		}

		protected void searchButtonClick(object sender, EventArgs e)
		{

		}
	}
}

