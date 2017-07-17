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
    public partial class InteressePage : ContentPage
    {
        private DateTime date;
        private int begintijd;
        private int eindtijd;
        private bool kunst;
        private bool natuur;
        private bool architectuur;
        private bool restaurant;
        private bool overige;
        

        public InteressePage(DateTime date, TimeSpan begintijd, TimeSpan eindtijd)
        {
            InitializeComponent();
            this.date = date;
            this.begintijd = (begintijd.Hours*100) + (begintijd.Minutes);
            this.eindtijd = (eindtijd.Hours*100) + (eindtijd.Minutes);
           
        }



        public async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "Doorgaan")
            {
               

                await Navigation.PushModalAsync(new Resultpage(date, begintijd, eindtijd, Museum.IsToggled, Restaurant.IsToggled, Park.IsToggled, Nightclub.IsToggled, Shopping.IsToggled));
                
                
            }
        }
    }
}