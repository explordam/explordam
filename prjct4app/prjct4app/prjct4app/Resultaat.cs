using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Collections.ObjectModel;
using prjct4app.WebServiceDetails;

namespace prjct4app
{
    public class Resultaat
    {
        public string Naam { get; set; }
        public string Adress { get; set; }
        public string Logo { get; set; }
        public string BeginTijd { get; set; }
        public string EindTijd { get; set; }
        public string placeid { get; set; }
        public string afstandvolgende { get; set; }
        public string URL { get; set; }
        

        public Resultaat(RootObject source, string begintijd, string eindtijd)
        {
            this.placeid = source.result.place_id;
            Debug.WriteLine(source.result.place_id);
            this.Naam = source.result.name;
            this.Adress = source.result.formatted_address;
            this.Logo = source.result.icon;
            this.URL = source.result.url;

            this.BeginTijd = begintijd;
            this.EindTijd = eindtijd;
            //this.begintijdeindtijd(source);

            
        }

        public void begintijdeindtijd(RootObject source)
        {
            try
            {
                this.BeginTijd = source.result.opening_hours.periods[2].open.time;
                this.EindTijd = source.result.opening_hours.periods[2].close.time;
            }
            catch (System.Exception exception) //dit crasht
            {
                this.BeginTijd = "****";
                this.EindTijd = "****";
            }
        }

        public async Task<int> afstandresultaatAsync(RootObject anderresultaat)
        {
            Debug.WriteLine("yolocase0");
            var afstand = await WebServiceDistance.PlaceDistance.PlaceDistanceWebRequest(this, anderresultaat);
            Debug.WriteLine("yolocase1");
            this.afstandvolgende = afstand.rows[0].elements[0].duration.text;
            Debug.WriteLine("yolocase2");
            string[] afstandstrings = afstandvolgende.Split(' ');
            Debug.WriteLine("yolocase3");
            if (afstandstrings.Length > 2)
            {
                Debug.WriteLine("yoloboy");
                return 9999;
            }

            else
            {
                Debug.WriteLine("yoloboy" + afstandstrings[0]);
                Debug.WriteLine("yoloboy" + Convert.ToInt32(afstandstrings[0]));
                return Convert.ToInt32(afstandstrings[0]);
            }
        }
    }
}
