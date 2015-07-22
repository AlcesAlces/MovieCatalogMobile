using System;
using Android.Widget;
using Android.App;
using System.Collections.Generic;
using Android.Views;
using System.IO;

namespace MovieCatalogMobile
{

	//Based off of a tutorial found at:
	// http://redth.codes/2010/10/12/monodroid-custom-listadapter-for-your-listview/

	public class CustomListAdapter : BaseAdapter
	{

		Activity context;
		public List<Movie> items;

		public CustomListAdapter(Activity context) : base()
		{
			this.context = context;

			this.items = FileHandlers.allMoviesInXml ();
		}

		public override int Count
		{
			get { return items.Count; }
		}

		public override Java.Lang.Object GetItem(int position)
		{
			return position;
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var item = items[position];

			var view = (convertView ??
				context.LayoutInflater.Inflate(
					Resource.Layout.row,
					parent,
					false)) as LinearLayout;

			var nameText = view.FindViewById(Resource.Id.txtRowTitle) as TextView;
			var yearText = view.FindViewById(Resource.Id.txtRowYear) as TextView;

			nameText.SetText (item.name, TextView.BufferType.Normal);
			yearText.SetText (item.displayYear, TextView.BufferType.Normal);

			return view;
		}

		public Movie GetItemAtPosition(int position)
		{
			return items[position];
		}
	}
}

