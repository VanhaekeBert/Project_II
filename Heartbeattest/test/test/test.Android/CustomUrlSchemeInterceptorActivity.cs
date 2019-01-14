using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Auth;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using test.Model;
using System.Net;

namespace test.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView },Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },DataSchemes = new[] { "com.companyname.test" },DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                base.OnCreate(savedInstanceState);
                var uri = new Uri(Intent.Data.ToString());
                var auth = PolarAuthenticator.GetPolarAuth();
                var polarCode = uri.ToString().Split('&')[1].Split('=')[1];
                var token = new PolarCode();
                PolarCode.Code = polarCode;
                auth.OnPageLoading(uri);
                Finish();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}