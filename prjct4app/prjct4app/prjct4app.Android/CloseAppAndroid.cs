using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;


[assembly: Dependency(typeof(prjct4app.Droid.CloseAppAndroid))]

namespace prjct4app.Droid
{
    public class CloseAppAndroid : ICloseApp
    {
        public void CloseApp()
        {

            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }

}