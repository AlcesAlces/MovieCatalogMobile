
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
using Android.Util;
using System.IO;

namespace MovieCatalogMobile
{
	[Activity (Label = "SettingsActivity")]			
	public class SettingsActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.settingsactivitylayout);
			// Create your application here
			Button button1 = FindViewById<Button> (Resource.Id.btnSettingsLoad);
			button1.Click += loadXmlClicked;
		}

		protected void loadXmlClicked(object sender, EventArgs e)
		{
			string dir = Android.OS.Environment.ExternalStorageDirectory.Path;
			List<string> files = Directory.GetFiles (dir).Where(x => x.Split('.').Last() == "xml").ToList();

			List<string> filesToCheck = new List<string>();

			if (files.Count == 0)
			{
				Toast.MakeText(this, "No files found on your SD card! Ensure the XML file is in the root directory on your SD card.", ToastLength.Long).Show();
			}
			else
			{
				foreach (string item in files)
				{
					if (FileHandlers.verifyXml(item))
					{
						//It's the real deal!
						filesToCheck.Add(item);
					}
				}
			}

			if (filesToCheck.Count > 1)
			{
				//Multiple valid files to deal with
			}
			else if (filesToCheck.Count == 1)
			{
				//Exactly 1 file found, verify that the user wants to copy it down.
				FileHandlers.saveNewFileByLocation(files[0]);
			}
		}
	}
}

