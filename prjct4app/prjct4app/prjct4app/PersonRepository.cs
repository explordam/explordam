using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using People.Models;
using SQLite.Net;
using SQLite.Net.Interop;
using SQLite.Net.Async;
using System.Diagnostics;

namespace prjct4app
{
    public class PersonRepository
    {
        private SQLiteAsyncConnection dbConn;

        public string StatusMessage { get; set; }
        List<string> museumplaceids;
        List<string> restaurantplaceids;
        List<string> shoppingplaceids;
        List<string> parkplaceids;
        List<string> nightclubplaceids;
        public PersonRepository(ISQLitePlatform sqlitePlatform, string dbPath)
        {
            //initialize a new SQLiteConnection 
            if (dbConn == null)
            {
                
                var connectionFunc = new Func<SQLiteConnectionWithLock>(() =>
                    new SQLiteConnectionWithLock
                    (
                        sqlitePlatform,
                        new SQLiteConnectionString(dbPath, storeDateTimeAsTicks: false)
                    ));

                dbConn = new SQLiteAsyncConnection(connectionFunc);
                dbConn.CreateTableAsync<Person>();
            }
        }

        public async Task AddNewPersonAsync(string name)
        {
            int result = 0;
            try
            {
                //basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                //insert a new person into the Person table
                result = await dbConn.InsertAsync(new Person { Name = name });
                StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
            }

        }

        public async Task<Dictionary<string, List<string>>> GetAllPeopleAsync(List<string> category)
        {
            museumplaceids = new List<string>();
            parkplaceids = new List<string>();
            restaurantplaceids = new List<string>();
            shoppingplaceids = new List<string>();
            nightclubplaceids = new List<string>();

            Dictionary <string, List<string>> dictionaryplaceids = new Dictionary<string, List<string>>();

            dictionaryplaceids.Add("museums", museumplaceids);
            dictionaryplaceids.Add("shoppingmalls", shoppingplaceids);
            dictionaryplaceids.Add("parks", parkplaceids);
            dictionaryplaceids.Add("restaurants", restaurantplaceids);
            dictionaryplaceids.Add("nightclubs", nightclubplaceids);
            foreach (var test in category)
            {
                
                

                //return a list of people saved to the Person table in the database
                string query = string.Format("SELECT * FROM people WHERE category = {0}", test);
                List<Person> people = await dbConn.QueryAsync<Person>(query);
                Debug.WriteLine(people.Count.ToString());

                if (test == "'museum'" || test == "'artgallery'")
                {
                    foreach (var i in people)
                    {
                        string placeid = i.Name;

                        museumplaceids.Add(placeid);
                    }
                }

                else if (test == "'park'")
                {
                    foreach (var i in people)
                    {
                        string placeid = i.Name;

                        parkplaceids.Add(placeid);
                    }
                }

                else if (test == "'restaurant'" || test == "'cafe'" || test == "'bar'")
                {
                    foreach (var i in people)
                    {
                        string placeid = i.Name;

                        restaurantplaceids.Add(placeid);
                    }
                }

                else if (test == "'shoppingmall'")
                {
                    foreach (var i in people)
                    {
                        string placeid = i.Name;

                        shoppingplaceids.Add(placeid);
                    }
                }

                else if (test == "'nightclub'")
                {
                    foreach (var i in people)
                    {
                        string placeid = i.Name;

                        nightclubplaceids.Add(placeid);
                    }
                }

            }
            return dictionaryplaceids;
        }
    }
}
