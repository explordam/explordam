using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using SQLite.Net.Interop;

namespace prjct4app
{
    public partial class App : Application
    {
        public static PersonRepository PersonRepo { get; private set; }

        public App(string dbPath, ISQLitePlatform sqlitePlatform)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());

            PersonRepo = new PersonRepository(sqlitePlatform, dbPath);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
