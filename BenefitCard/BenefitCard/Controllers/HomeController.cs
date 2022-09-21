using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BenefitCard.Models;
using System.Net;
using System.IO;

namespace BenefitCard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

		Database database = new Database();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

		public IActionResult ListActivities(int lat, int lat2)
		{

			var activities = new List<string>();
			foreach (var item in database.Activities)
			{
				activities.Add(item.Key);
			}

            activities.Sort();
			return View(activities);
		}

		[HttpPost] ///TADY MUSÍ BÝT JEŠTĚ COORDINACE USERA A POČET KILOMETRŮ V OKRUHU JAKEM CHCE HLEDAT
		public IActionResult ListPlaces(string[] choosenActivities, int distanceRestriction)
		{
			if (choosenActivities == null || choosenActivities.Length == 0)
				return ErrorChooseEmpty();

			//V tomhle jsou ty facility
			List<Facility> facilities = new List<Facility>();

			


			foreach (string UserActivity in choosenActivities)
			{
				if (database != null) //mělo by se vyřešit kdy je databaze null - dodělat pak
				{

					foreach(var activity in database.Activities.Keys)
					{
						//chceks for activities that suits user defined activity
						if (CheckSubstring(UserActivity, activity))
						{
							//adds all the facilities providing chosen activity
							foreach (var facility in database.Activities[activity])
							{
								facilities.Add(facility);
							}
						}
					}

				}
			}

			//NAHRADIT COORDINATES USERA
			//SortByDistance(ref facilities, new Coordinates() {Latitude = 0, Longtitude = 0 });
			//Tady se musí sesortit facilities by distance

			//RestrictByDistance(ref facilities, distanceRestriction);

			return View("TableActivities", facilities);
		}

		#region HELPERS
		//Not needed
		void SortByDistance(ref List<Facility> facilities, Coordinates userCoor)
		{
			foreach(var facility in facilities)
			{
				var client = new WebClient();
				using (var stream = client.OpenRead(GetDistancesApi(facility, userCoor)))
				{
					using (var reader = new StreamReader(stream))
					{
						string line;
						while ((line = reader.ReadLine()) != null)
						{
							if (line.Contains("distance"))
							{
								line = reader.ReadLine();
								facility.distance = GetNumber(line);
							}
						}
					}
				}
			}

			facilities.Sort();
		}


		void RestrictByDistance(ref List<Facility> facilities, int restriction)
		{
			foreach(Facility facility in facilities)
			{
				if (facility.distance > restriction)
				{
					facilities.Remove(facility);
				}
			}
		}
		static int GetNumber(string s)
		{
			int num = 0;
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
					num /= 10;
				}
			}

			return num;
		}

		static string GetDistancesApi(Facility facility, Coordinates userCOor)
		{
			string url = @"https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&mode=walking&origins=";
			url += userCOor.Latitude;
			url += ',';
			url += userCOor.Longtitude;
			url += "&destinations=";

            var facCOor = facility.GetCoordinates();
			url += facCOor.Latitude;
			url += ',';
			url += facCOor.Longtitude;
			url += @"&key=AIzaSyCHQFxLKLWMvOQR5cCjKxkWED2YH98V2G8";

			return url;
		}


		/// <summary>
		/// HelperFunction - finding substrings
		/// </summary>
		private bool CheckSubstring(string subString, string mainString)
		{
			int M = subString.Length;
			int N = mainString.Length;

			for (int i = 0; i <= N - M; i++)
			{
				int j;

				for (j = 0; j < M; j++)
					if (mainString[i + j] != subString[j])
						break;

				if (j == M)
					return true;
			}
			return false;
		}
		#endregion
		public IActionResult ShowDetail(int id = 0)
		{

			if (!database.Facilities.ContainsKey(id))
			{
				// return ERROR VIEW
				return Error();
			}
			else
			{
				var facility = database.Facilities[id];

				var userCoor = new Coordinates() { Latitude = 41.43206M, Longtitude = 41.43206M };

				var client = new WebClient();
				using (var stream = client.OpenRead(GetDistancesApi(facility, userCoor)))
				{
					using (var reader = new StreamReader(stream))
					{
						string line;
						while ((line = reader.ReadLine()) != null)
						{
							if (line.Contains("distance"))
							{
								line = reader.ReadLine();
								facility.distance = GetNumber(line);
							}
						}
					}
				}


				return View(facility);
			}
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult ErrorChooseEmpty()
		{
			return View("ErrorChooseEmpty");
		}

		public IActionResult ShowMap()
		{
			return View();
		}
	}
}
