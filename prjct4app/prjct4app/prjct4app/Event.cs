namespace prjct4app
{
    public class Event
    {
        string name;
        int begintijd;
        int eindtijd;
        int duration;
        string locatie;
        string type;


        public Event(string name, int begintijd, int eindtijd, string locatie, string type)
        {
            this.name = name;
            this.begintijd = begintijd;
            this.eindtijd = eindtijd;
            this.locatie = locatie;
            this.type = type;
        }
    }
}