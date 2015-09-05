
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
using SocketIOClient;

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
            Button button2 = FindViewById<Button>(Resource.Id.btnSettingsLoginSync);
            button2.Click += loginAndSync;
            Button button3 = FindViewById<Button>(Resource.Id.btnSettingsDelete);
            button3.Click += RemoveLocalXML;
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

        protected void RemoveLocalXML(object sender, EventArgs e)
        {
            FileHandlers.RemoveLocalXML();
        }

        protected async void loginAndSync(object sender, EventArgs e)
        {
            TextView userTxt = FindViewById<TextView> (Resource.Id.txtSettingsUser);
            TextView passTxt = FindViewById<TextView>(Resource.Id.txtSettingsPassword);
            string user = userTxt.Text;
            string pass = passTxt.Text;
            pass = Hashing.stringToHash(pass).ToLower();
            user = user.ToLower();

            Global.socket = new Client(Global.connectionString);

            //System.Net.WebRequest.DefaultWebProxy = null;

            Global.socket.On("connect", (fn) =>
                {

                });

            Global.socket.Connect();

            while (!Global.socket.IsConnected);

            string userId = "";
            try
            {
                userId = await MovieCatalogLibrary.DatabaseHandling.MongoInteraction.VerifyCredentials(user, pass, Global.socket);
            }
            catch(Exception ex)
            {
                if(ex.Message == "TIMEOUT")
                {
                    return;
                }
            }

            if (userId != String.Empty)
            {
                //User is logged in
                Global.userName = user;
                Global.uid = userId;

                if (await MovieCatalogLibrary.DatabaseHandling.MongoInteraction.GetUserSyncStatus(Global.userName))
                {
                    //TODO: uncoment this and fix it too lol
                    //MovieCatalogLibrary.DatabaseHandling.MongoXmlLinker.SyncUserFiles(Global.uid);
                }
            }

            else
            {
                //Incorrect information
            }

            try
            {
                await MovieCatalogLibrary.DatabaseHandling.MongoXmlLinker.SyncUserFiles(Global.uid, Global.socket, MovieCatalogLibrary.FileHandler.platformType.Android);
            }
            catch(Exception ex)
            {
                int i = 0;
            }
        }
	}
}

