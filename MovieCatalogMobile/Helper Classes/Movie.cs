﻿using System;
using WatTmdb.V3;
using System.Collections.Generic;
using System.Linq;

namespace MovieCatalogMobile
{
	public class Movie
	{
		public string name { get; set; }
		public string year {get;set;}
		public int mid { get; set; }
		public string imageLocation { get; set; }
		public double onlineRating { get; set; }
		public double userRating { get; set; }
		public string description { get; set; }
		public List<MovieGenre> genres = new List<MovieGenre>();
		//Use this name to sort by, it will exclude "the" in a title.
		public string sortName { get; set; }
		public string displayYear{ get; set; }

		public Movie(string toParse)
		{
			setGenreCommaSeperated(toParse);
			setSortName();
			//setDisplayYear ();
		}

		public Movie()
		{
			setSortName();
			//setDisplayYear ();
		}

		public string getGenreCommaSeperated()
		{
			string toReturn = "";

			foreach(MovieGenre genre in genres)
			{
				toReturn += genre.name + ",";
			}

			if (toReturn == "")
			{
				return toReturn;
			}
			else
			{
				return toReturn.Substring(0, toReturn.Count() - 1);
			}
		}

		public void setGenreCommaSeperated(string toBeParsed)
		{
			List<string> splitString = toBeParsed.Split(',').ToList();

			foreach(string item in splitString)
			{
				genres.Add(new MovieGenre
					{
						name = item
					});
			}
		}

		public void setSortName()
		{
			if (name != null)
			{
				if (name.Split(' ')[0].ToLower() == "the")
				{
					var tmp = name.Split(' ').ToList();
					tmp.RemoveAt(0);
					sortName = String.Join(" ", tmp);
				}

				else
				{
					sortName = name;
				}
			}
		}

		public void setDisplayYear()
		{
			if (year != null) 
			{
				var parsed = year.Split ('-');
				displayYear = parsed [0];
			}
		}
	}
}

