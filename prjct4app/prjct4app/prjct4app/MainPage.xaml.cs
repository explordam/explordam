using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prjct4app
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await DisplayAlert("Clicked", "page: " + button.Text + " bestaat nog niet", "Ok");
        }

        async void StartPageVisitor(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            if (button.Text == "Start")
            {
                await Navigation.PushModalAsync(new Datepage());
            }

            else if (button.Text == "Info")
            {
                await Navigation.PushModalAsync(new InfoPage());
            }

            //else if (button.Text == "Opties")
            //{
            //    await navigation.pushasync(new ());
            //}

            else if (button.Text == "Exit")
            {
                var closer = DependencyService.Get<ICloseApp>();
                if (closer != null)
                    closer.CloseApp();
            }
        }
    }
}