using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;
using StreetWorkoutV2.Model;

namespace StreetWorkoutV2.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView },Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },DataSchemes = new[] { "com.nmct.SICWorkout" },DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var uri = new Uri(Intent.Data.ToString());
            var auth = PolarManager.GetPolarAuth();
            var polarCode = uri.ToString().Split('&')[1].Split('=')[1];
            var token = new PolarAuth();
            PolarAuth.Code = polarCode;
            auth.OnPageLoading(uri);
            Finish();
        }
    }
}