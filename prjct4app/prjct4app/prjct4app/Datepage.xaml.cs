using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prjct4app
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Datepage : ContentPage
    {
        DateTime date;
        TimeSpan begintijd;
        TimeSpan eindtijd;

        public Datepage()
        {
            InitializeComponent();
            DatumSelector.MinimumDate = DateTime.Now.ToLocalTime();
            DatumSelector.Date = DateTime.Now.ToLocalTime();
        }
        //async void OnButtonClicked(object sender, EventArgs args)
        //{
        //    Button button = (Button)sender;
        //    await DisplayAlert("Clicked", "page: " + button.Text + " bestaat nog niet", "Ok");
        //}

        async void StartPageVisitor(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            if (button.Text == "Doorgaan")
            {
                if (BeginTijd.Time.CompareTo(EindTijd.Time) >= 0)
                {
                    await DisplayAlert("Verkeerde tijden", "Aankomst later dan Vertrek", "Ok");
                }
                else { await Navigation.PushModalAsync(new InteressePage(DatumSelector.Date, BeginTijd.Time, EindTijd.Time)); }


                //alles hierboven ingecomment en deze lijn eronder toegevoegd

                //TimeSpan(int hours, int minutes, int seconds);
                

            }
            //await DisplayAlert("Clicked", "page: " + button.Text + " bestaat nog niet", "Ok");
        }

    }
}