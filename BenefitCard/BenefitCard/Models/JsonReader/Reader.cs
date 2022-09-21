using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BenefitCard.Models.JsonReader
{
    class Reader
    {
        public List<Facility> facilities = new List<Facility>();
        public void Read()
        {
			using (TextReader tr = new StreamReader(Environment.CurrentDirectory + @"\Models\Data\places.json"))
			// using (TextReader tr = new StreamReader("places.json"))
			{
				JsonTextReader reader = new JsonTextReader(tr);
				while (reader.Read())
				{
					if (reader.Value != null)
					{
						if (reader.TokenType == JsonToken.PropertyName && (reader.Value.ToString() == "places"))
						{
							reader.Read(); // startarray
							reader.Read(); // startobject
							while (reader.TokenType == JsonToken.StartObject)
							{
								facilities.Add(ReadFacility(reader));
								reader.Read();
							}
						}
					}
				}
			}
		}

        Facility ReadFacility(JsonTextReader tr)
        {
            Facility f = new Facility();

            tr.Read(); // name
            tr.Read();
            f.Name = tr.Value.ToString();

            tr.Read(); // url
            tr.Read();
            f.Url = tr.Value.ToString();

            tr.Read(); // address
            f.Address = ReadAdress(tr);
            f.Activities = ReadActivities(tr);
			//f.Coordinates = new Coordinates() { Latitude = 0, Longtitude = 0 }; ----------Not needed
            return f;
        }

		/*
		#region Helper functions to get Coordinates
		/// <summary>
		/// HelperFunction - get facilities
		/// </summary>
		/// <returns></returns>
		public Coordinates GetCoordinates(Facility f)
		{
			Coordinates c = new Coordinates();

			var client = new WebClient();
			using (var stream = client.OpenRead(GetLocationAPI(f.Address)))
			{
				using (var reader = new StreamReader(stream))
				{
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						if (line.Contains("northeast"))
						{
							line = reader.ReadLine();
							c.Latitude = GetNumber(line);

							line = reader.ReadLine();
							c.Longtitude = GetNumber(line);

							return c;
						}

					}
					return c;
				}
			}
		}
		/// <summary>
		/// Helper function for GetCoordinates 
		/// </summary>
		/// <param name="ad"></param>
		/// <returns></returns>
		static string GetLocationAPI(Address ad)
		{
			string url = @"https://maps.googleapis.com/maps/api/geocode/json?address=";
			url += ad.Street.Replace(' ', '+');
			url += ",";
			url += ad.City.Replace(' ', '+');
			url += @"&key=AIzaSyCHQFxLKLWMvOQR5cCjKxkWED2YH98V2G8";
			return url;
		}
		/// <summary>
		/// Helper funciton for GetCoordinates
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		static decimal GetNumber(string s)
		{
			decimal num = 0;
			long counter = 0;
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= 48 && s[i] <= 57)
				{
					while (s[i] >= 48 && s[i] <= 57)
					{
						num += s[i] - 48;
						num *= 10;
						i++;
					}

					while (++i < s.Length && s[i] >= 48 && s[i] <= 57)
					{
						num += s[i] - 48;
						num *= 10;

						counter++;
					}

					for (int j = -1; j < counter; j++)
					{
						num /= 10;
					}
				}
			}

			return num;
		}
		#endregion
		*/
		Address ReadAdress(JsonTextReader tr)
        {
            Address a = new Address();

            tr.Read(); // startObject

            tr.Read(); //street
            tr.Read();
            a.Street = tr.Value.ToString();

            tr.Read(); // zipcode
            tr.Read();
            a.ZipCode = Int32.Parse(tr.Value.ToString());

            tr.Read(); // city
            tr.Read();
            a.City = tr.Value.ToString();

            tr.Read(); // EndObject

            return a;
        }

        List<string> ReadActivities(JsonTextReader tr)
        {
            List<string> a = new List<string>();

            tr.Read(); // activities
            tr.Read(); // startarray

            tr.Read();
            while (tr.TokenType != JsonToken.EndArray)
            {
                a.Add(tr.Value.ToString());
                tr.Read();
            }

            tr.Read(); // endobject

            return a;
        }
    }
}