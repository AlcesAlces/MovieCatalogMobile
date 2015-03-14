﻿using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Linq;

namespace MovieCatalogMobile
{
	public class FileHandlers
	{
		public static bool checkUserFileExists()
		{
			return File.Exists(getUserPath());
		}

		public static string getUserPath()
		{
			string saveLoc = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
			saveLoc += @"/userinfo.xml";
			return saveLoc;
		}

		//checks the integrity of the user file.
		public static void verifyUserFile()
		{
			//User file does exist
			if(checkUserFileExists())
			{
				//Verify that the contents of the user's file follow the guidelines.
			}

			else
			{
				//Creates a blank template for the user's file.
				createUserFile();
			}
		}

		public static void createUserFile()
		{
			XmlTextWriter writer = new XmlTextWriter(getUserPath(), System.Text.Encoding.UTF8);
			writer.WriteStartDocument(true);
			writer.Formatting = Formatting.Indented;
			writer.Indentation = 2;
			writer.WriteStartElement("usersettings");
			//Where all the movie data is going to be stored.
			writer.WriteStartElement("movies");
			writer.WriteEndElement();
			//where all of the user settings are going to be stored.
			writer.WriteStartElement("settings");
			writer.WriteEndElement();
			//write end of usersettings
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Close();
		}

		public static void addMovie(Movie movie)
		{
			XmlDocument xml = new XmlDocument();
			xml.Load(getUserPath());

			XmlNode usersettings = xml.SelectSingleNode("usersettings");
			XmlNode movies = usersettings.SelectSingleNode("movies");

			//New movie node
			XmlNode newSub = xml.CreateNode(XmlNodeType.Element, "movie", null);

			List<XmlNode> subList = new List<XmlNode>();

			XmlNode nameSub = xml.CreateNode(XmlNodeType.Element, "name", null);
			nameSub.InnerText = movie.name.ToString();
			subList.Add(nameSub);
			XmlNode genSub = xml.CreateNode(XmlNodeType.Element, "genres", null);
			genSub.InnerText = movie.getGenreCommaSeperated();
			subList.Add(genSub);
			XmlNode yearSub = xml.CreateNode(XmlNodeType.Element, "year", null);
			yearSub.InnerText = movie.year.ToString();
			subList.Add(yearSub);
			XmlNode imageSub = xml.CreateNode(XmlNodeType.Element, "image", null);
			imageSub.InnerText = movie.imageLocation;
			subList.Add(imageSub);
			XmlNode movieidSub = xml.CreateNode(XmlNodeType.Element, "movieid", null);
			movieidSub.InnerText = movie.mid.ToString();
			subList.Add(movieidSub);
			XmlNode ratingSub = xml.CreateNode(XmlNodeType.Element, "rating", null);
			ratingSub.InnerText = movie.onlineRating.ToString();
			subList.Add(ratingSub);
			XmlNode descSub = xml.CreateNode(XmlNodeType.Element, "description", null);
			descSub.InnerText = movie.description;
			subList.Add(descSub);
			XmlNode posterSub = xml.CreateNode(XmlNodeType.Element, "posternum", null);
			posterSub.InnerText = "0";
			subList.Add(posterSub);
			XmlNode userratingSub = xml.CreateNode(XmlNodeType.Element, "userrating", null);
			try
			{
				userratingSub.InnerText = movie.userRating.ToString();
			}
			catch(Exception e)
			{
				userratingSub.InnerText = "0";
			}
			subList.Add(userratingSub);

			foreach(XmlNode node in subList)
			{
				newSub.AppendChild(node);
			}

			movies.AppendChild(newSub);
			xml.Save(getUserPath());
		}

		//Get all of the movies in the user's XML file.
		public static List<Movie> allMoviesInXml()
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(getUserPath());

			XmlNodeList nodes = doc.DocumentElement.SelectNodes("/usersettings/movies/movie");

			List<Movie> moviesToReturn = new List<Movie>();

			foreach(XmlNode node in nodes)
			{
				moviesToReturn.Add(new Movie(node.SelectSingleNode("genres").InnerText.ToString())
					{
						imageLocation = node.SelectSingleNode("image").InnerText.ToString(),
						mid = Int32.Parse(node.SelectSingleNode("movieid").InnerText.ToString()),
						name = node.SelectSingleNode("name").InnerText.ToString(),
						onlineRating = Double.Parse(node.SelectSingleNode("rating").InnerText.ToString()),
						userRating = Double.Parse(node.SelectSingleNode("userrating").InnerText.ToString()),
						year = node.SelectSingleNode("year").InnerText.ToString(),
						description = node.SelectSingleNode("description").InnerText.ToString()
					});
				moviesToReturn[moviesToReturn.Count - 1].setSortName();
				moviesToReturn[moviesToReturn.Count - 1].setDisplayYear();
			}

			moviesToReturn = moviesToReturn.OrderBy(x => x.sortName).ToList();

			return moviesToReturn;
		}
		 
		public static bool isMovieDuplicate(int movieid)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(getUserPath());

			XmlNodeList nodes = doc.DocumentElement.SelectNodes("/usersettings/movies/movie");

			foreach(XmlNode node in nodes)
			{
				if(node.SelectSingleNode("movieid").InnerText == movieid.ToString())
				{
					return true;
				}
			}

			return false;
		}

		public static void removeMovies(List<Movie> movies)
		{

			XmlDocument doc = new XmlDocument();
			doc.Load(getUserPath());

			XmlNodeList nodes = doc.DocumentElement.SelectNodes("/usersettings/movies/movie");

			foreach(Movie movie in movies)
			{
				foreach(XmlNode node in nodes)
				{
					if(node.SelectSingleNode("movieid").InnerText == movie.mid.ToString())
					{
						node.ParentNode.RemoveChild(node);
					}
				}
			}

			doc.Save(getUserPath());
		}

		public static bool verifyXml(string location)
		{
			bool success = true;
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.Load(location);

				XmlNodeList nodes = doc.DocumentElement.SelectNodes("/usersettings/movies/movie");

				List<Movie> moviesToReturn = new List<Movie>();

				foreach(XmlNode node in nodes)
				{
					moviesToReturn.Add(new Movie(node.SelectSingleNode("genres").InnerText.ToString())
						{
							imageLocation = node.SelectSingleNode("image").InnerText.ToString(),
							mid = Int32.Parse(node.SelectSingleNode("movieid").InnerText.ToString()),
							name = node.SelectSingleNode("name").InnerText.ToString(),
							onlineRating = Double.Parse(node.SelectSingleNode("rating").InnerText.ToString()),
							userRating = Double.Parse(node.SelectSingleNode("userrating").InnerText.ToString()),
							year = node.SelectSingleNode("year").InnerText.ToString(),
							description = node.SelectSingleNode("description").InnerText.ToString()
						});
					moviesToReturn[moviesToReturn.Count - 1].setSortName();
					moviesToReturn[moviesToReturn.Count - 1].setDisplayYear();
				}
			}
			catch
			{
				success = false;
			}

			return success;
		}

		public static void saveNewFileByLocation(string location)
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(location);
			doc.Save(getUserPath());
		}
	}
}

