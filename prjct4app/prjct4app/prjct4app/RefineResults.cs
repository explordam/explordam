using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using prjct4app.WebServiceDetails;


namespace prjct4app
{
    class RefineResults
    {
        
        int aankomsttijd { get; set; }
        int vertrektijd { get; set; }
        int day { get; set; }
        int totaletijd { get; set; }
        int huidigetijd { get; set; }
        RootObject rootobject { get; set; }
        bool museum;
        bool park;
        bool shopping;
        bool restaurant;
        bool nightclub;
        int limit = 10;
        
        PlaceDetails placedetails = new PlaceDetails();

        public RefineResults(int aankomsttijd, int vertrektijd, int day, bool museum, bool restaurant, bool park, bool nightclub, bool shopping)
        {   

            this.day = day;
            this.aankomsttijd = aankomsttijd;
            this.vertrektijd = vertrektijd;
            this.totaletijd = IntToTime(vertrektijd - aankomsttijd);
            this.museum = museum;
            this.park = park;
            this.restaurant = restaurant;
            this.nightclub = nightclub;
            this.shopping = shopping;
         
        }

        public async Task FilterAsync(List<Resultaat> resultaatlijst, Dictionary<string, List<string>> placeids)
        {
            
            int maxgrootte = 5;
            int maxtries = 100;
            int tries = 0;
            int huidigetijd = aankomsttijd;
            Random random = new Random();

            while (resultaatlijst.Count < maxgrootte && huidigetijd < 2400 && tries < maxtries)
            {
                int nieuwetijd = TimePlusInt(huidigetijd, IntToTime(200));
                List<string> newplaceids = new List<string>();
                string placeid = "";
                bool added = false;

                try
                {
                    if (huidigetijd < 1000)
                    {
                        if (park)
                        {
                            newplaceids.AddRange(placeids["parks"]);
                        }

                    }

                    else if (huidigetijd >= 1000 && huidigetijd < 1400)
                    {
                        if (museum)
                        {
                            newplaceids.AddRange(placeids["museums"]);
                        }

                        if (park)
                        {
                            newplaceids.AddRange(placeids["parks"]);
                        }

                        if (shopping)
                        {
                            newplaceids.AddRange(placeids["shoppingmalls"]);
                        }

                    }

                    else if (huidigetijd >= 1400 && huidigetijd < 1800 && shopping)
                    {
                        if (museum)
                        {
                            newplaceids.AddRange(placeids["museums"]);
                        }

                        if (park)
                        {
                            newplaceids.AddRange(placeids["parks"]);
                        }

                        if (shopping)
                        {
                            newplaceids.AddRange(placeids["shoppingmalls"]);
                        }

                    }

                    else if (huidigetijd >= 1800 && huidigetijd < 2200 && restaurant)
                    {
                        
                        if (restaurant)
                        {
                            newplaceids.AddRange(placeids["restaurants"]);
                        }

                        else if (park)
                        {
                            newplaceids.AddRange(placeids["parks"]);
                        }

                        placeid = newplaceids[random.Next(0, newplaceids.Count - 1)];
                        rootobject = await placedetails.PlaceDetailsWebRequest(placeid);

                    }

                    else if (huidigetijd >= 2200 && nightclub)
                    {
                        if (nightclub)
                        {
                            newplaceids.AddRange(placeids["nightclubs"]);        
                        }
                        
                    }

                    placeid = newplaceids[random.Next(0, newplaceids.Count - 1)];
                    rootobject = await placedetails.PlaceDetailsWebRequest(placeid);


                    if (true) //(huidigetijd >= Convert.ToInt32(rootobject.result.opening_hours.periods[day].open.time) && (nieuwetijd <= Convert.ToInt32(rootobject.result.opening_hours.periods[day].close.time) || Convert.ToInt32(rootobject.result.opening_hours.periods[day].close.time) == 0))
                    {
                        if (resultaatlijst.Count > 0)
                        {
                            int afstand = await resultaatlijst[resultaatlijst.Count-1].afstandresultaatAsync(rootobject);
                            if (afstand < 60)
                            {
                                resultaatlijst.Add(new Resultaat(rootobject, TimeToString(huidigetijd), TimeToString(nieuwetijd)));
                                added = true;
                                huidigetijd = nieuwetijd;
                            }

                        }

                        else
                        {
                            resultaatlijst.Add(new Resultaat(rootobject, TimeToString(huidigetijd), TimeToString(nieuwetijd)));
                            added = true;
                            huidigetijd = nieuwetijd;
                        }
                        
                    }

                    Debug.WriteLine(Convert.ToInt32(rootobject.result.opening_hours.periods[0].open.time).ToString() + " " + huidigetijd.ToString());
                    Debug.WriteLine(Convert.ToInt32(rootobject.result.opening_hours.periods[0].close.time).ToString() + " " + nieuwetijd.ToString());
                    
                }

                catch {  }

                if (added)
                {
                    if (placeids["parks"].Contains(placeid))
                    {
                        park = false;
                    }

                    else if (placeids["museums"].Contains(placeid))
                    {
                        museum = false;
                    }

                    else if (placeids["restaurants"].Contains(placeid))
                    {
                        restaurant = false;
                    }

                    else if (placeids["shoppingmalls"].Contains(placeid))
                    {
                        shopping = false;
                    }

                    else if (placeids["nightclubs"].Contains(placeid))
                    {
                        nightclub = false;
                    }
                }

                else { tries++;}
                

                if (tries % limit == 0)
                {
                    huidigetijd = nieuwetijd;
                }

            }

        }
        
        private int IntToTime(int Int)
        {
            int Time = 0;
            while (Int >= 100)
            {
                Time += 100;
                Int -= 100;
            }
            
            Time += (Int * 60/ 100);

            return Time;
        }

        private int TimePlusInt(int Time ,int Int)
        {
            int newtime = 0;
            while (Int >= 100)
            {
                Time += 100;
                Int -= 100;
            }
            while (Int >= 60)
            {
                Time += 100;
                Int -= 60;
            }

            Time += Int;

            while (Time >= 100)
            {
                newtime += 100;
                Time -= 100;
            }
            while (Time >= 60)
            {
                newtime += 100;
                Time -= 60;
            }

            newtime += Time;

            if (newtime > 2400)
            {
                newtime -= 2400;
            }

            return newtime;
        }

        private string TimeToString(int time)
        {
            int Time = time;
            string result = "0000";
            string newresult = "";

            if (time.ToString().Length == 1)
            {
                result = "000" + time.ToString();
            }

            else if (time.ToString().Length == 2)
            {
                result = "00" + time.ToString();
            }

            else if (time.ToString().Length == 3)
            {
                result = "0" + time.ToString();

            }

            else if (time.ToString().Length == 4)
            {
                result = time.ToString();
            }


            for (int c = 0; c < result.Length; c++)
            {
                newresult = newresult + result[c];
                
                if (c == 1)
                {
                    newresult += ":"; 
                }

            }

            return newresult;

        }
    }
}