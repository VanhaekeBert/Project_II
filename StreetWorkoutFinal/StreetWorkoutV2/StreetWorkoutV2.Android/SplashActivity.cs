using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace StreetWorkoutV2.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.splash);
            ThreadPool.QueueUserWorkItem(o => LoadActivity());
            // Create your application here
        }
        private void LoadActivity()
        {
            Thread.Sleep(1000); // Simulate a long pause
            RunOnUiThread(() => StartActivity(typeof(MainActivity)));
        }
    }
}