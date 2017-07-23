using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using People.Models;
using System.Collections.ObjectModel;
using prjct4app.WebServiceDetails;

namespace prjct4app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Resultpage : ContentPage
    {
        private DateTime date;
        private int begintijd { get; set; }
        private int eindtijd { get; set; }
        private List<string> intresses;
        private RefineResults refine { get; set; }
        

        

        public Resultpage(DateTime date, int begintijd, int eindtijd, bool museum, bool restaurant, bool park, bool nightclub, bool shopping)
        {
            this.begintijd = begintijd;
            this.eindtijd = eindtijd;
            System.Diagnostics.Debug.WriteLine(nightclub);
            intresses = new List<string>();
            if (museum) { intresses.Add("'museum'"); intresses.Add("'artgallery'"); }
            if (park) { intresses.Add("'park'"); }
            if (shopping) { intresses.Add("'shoppingmall'"); }
            if (restaurant) { intresses.Add("'restaurant'"); intresses.Add("'cafe'"); intresses.Add("'bar'"); }
            if (nightclub) { intresses.Add("'nightclub'"); }
            refine = new RefineResults(begintijd, eindtijd, DaytoInt.daytoint(date.DayOfWeek), museum, restaurant, park, nightclub, shopping);

            InitializeComponent();
            OnStart();

        }

        public async void visitwebsite(object sender)
        {
            Button button = (Button)sender;
            Device.OpenUri(new Uri(button.ClassId));
        }

        public async void OnStart()
        {
            List<Resultaat> UneListeDuResults = new List<Resultaat>();
            PlaceDetails DetailGetter = new PlaceDetails();
            Dictionary<string, List<string>> placeids;
           

            System.Diagnostics.Debug.WriteLine("case1");
            placeids = await App.PersonRepo.GetAllPeopleAsync(intresses);
            System.Diagnostics.Debug.WriteLine("case2");
            await refine.FilterAsync(UneListeDuResults, placeids);
            System.Diagnostics.Debug.WriteLine("case3");
            System.Diagnostics.Debug.WriteLine("case4");
            ObservableCollection<Resultaat> UneListeDuResultsCollection = new ObservableCollection<Resultaat>(UneListeDuResults);
            peopleList.ItemsSource = UneListeDuResultsCollection;
        }
    }
}