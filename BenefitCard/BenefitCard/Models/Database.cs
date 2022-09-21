using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BenefitCard.Models.JsonReader;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace BenefitCard.Models
{
    public class Database
    {

		public Dictionary<int, Facility> Facilities { get; set; }
      //  public DbSet<Facility> Facilities { get; set; }

		public Dictionary<string, List<Facility>> Activities { get; set; }
       // public DbSet<Tuple<string, List<Facility>>> Activities { get; set; }


        public Database()
        {
            LoadDatabase();
        }

        public void LoadDatabase()
        {
			Facilities = new Dictionary<int, Facility>();
			Activities = new Dictionary<string, List<Facility>>();


            int counter = 1;
            //- konzultovat s dušanem
            Reader reader = new Reader();
            reader.Read();

            foreach (Facility f in reader.facilities)
            {
                f.Id = counter++;
                Facilities.Add(f.Id, f);

                foreach (string activity in f.Activities)
                {
                    AddActivity(activity, f);
                }
            }
        }

        void AddActivity(string activity, Facility f)
        {

			if (Activities.ContainsKey(activity))
			{
				Activities[activity].Add(f);
			}
			else
			{
				List<Facility> l = new List<Facility>();
				l.Add(f);
				Activities.Add(activity, l);
			}

			//_______________________OLD
			/*
            foreach (Tuple<string, List<Facility>> t in Activities)
            {
                if (t.Item1 == activity)
                {
                    found = true;
                    t.Item2.Add(f);
                }
            }
			

            if (found == false)
            {
                List<Facility> l = new List<Facility>();
                l.Add(f);

                Activities.Add(new Tuple<string, List<Facility>>(activity, l));
            }
			*/
        }
    }
}