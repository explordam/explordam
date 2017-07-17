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
using System.Diagnostics;

namespace prjct4app
{
    namespace WebServiceDetails
    {

        public class PlaceDetails
        {
            string placesApiKey = "AIzaSyBRLocticp0ff2IqxmCZh9rz2opiXa - 2Oo";
            private const string Url = "https://maps.googleapis.com/maps/api/place/details/json?placeid=";

            public async Task<RootObject> PlaceDetailsWebRequest(string placeid)
            {

                try
                {
                    var client = new HttpClient();
                    var json = await client.GetStringAsync(string.Format(Url + placeid + "&key=" + placesApiKey));
                    var details = JsonConvert.DeserializeObject<RootObject>(json.ToString());

                    //var zondag = details.result.opening_hours.periods[0];
                    //Debug.WriteLine("opent op " + zondag.open.day + " om " + zondag.open.time + " en sluit om " + zondag.close.time);
                    Debug.WriteLine(details.result.rating);

                    return details;
                }
                
                catch (System.Exception exception) //dit crasht
                {
                    return new RootObject();
                }
            }   
        }
    }
}