using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;


namespace prjct4app
{
    namespace WebServiceDistance
    {

        public class PlaceDistance
        {
            string distanceApiKey = "AIzaSyABsr2bhPqTsWnkz03YsJOr4XSpqqj7Jn0";
            string placeId1 = "ChIJvQBNdJ80xEcRmNuMeHpljHw";
            string placeId2 = "ChIJIV5ETF8zxEcRkPnmoBJRk8Q";
            
            private const string Url = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&mode=walking&origins=place_id:";

            public async static Task<RootObjectDistance> PlaceDistanceWebRequest(Resultaat result1, Resultaat result2)
            {
                try
                {
                    
                    var client = new HttpClient();

                    var distancejson = await client.GetStringAsync(string.Format((Url + result1.placeid + "&destinations=place_id:" + result2.placeid + "&key=" + "AIzaSyABsr2bhPqTsWnkz03YsJOr4XSpqqj7Jn0")));
                    var distances = JsonConvert.DeserializeObject<RootObjectDistance>(distancejson.ToString());

                    var duration = distances.rows[0].elements[0].duration.text;
                    //System.Diagnostics.Debug.WriteLine(duration);
                    return distances;
                }
               
                 catch (System.Exception exception)
                {
                    return null;
 
                }

            }
        }
    }
}
