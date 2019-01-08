using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Auth;

namespace test.Droid
{
    [Activity(Label = "test", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            global::Xamarin.Auth.Presenters.XamarinAndroid.AuthenticationConfiguration.Init(this, savedInstanceState);
            LoadApplication(new App());


        }
        //private async void LoginToFacebook(bool allowCancel)
        //{
        //    var facebookService = new FacebookService();
        //    var account = await facebookService.LoginAsync(this, allowCancel);

        //    if (account == null)
        //    {
        //        ShowMessage("Not Authenticated");
        //        return;
        //    }

        //    // in this step the username is always null, but i followed the original sample
        //    ShowMessage(string.Format("Authenticated {0}", account.Username));

        //    var userInfo = await facebookService.GetUserInfoAsync(account);
        //    ShowMessage(!string.IsNullOrEmpty(userInfo) ? string.Format("Logged as {0}", userInfo) : "Wasn´t possible to get the name.");
        //}

        //public void ShowMessage(string message)
        //{
        //    var builder = new AlertDialog.Builder(this);
        //    builder.SetMessage(message);
        //    builder.SetPositiveButton("Ok", (o, e) => { });
        //    builder.Create().Show();
        //}

        //protected override void OnCreate(Bundle bundle)
        //{
        //    base.OnCreate(bundle);
        //    SetContentView(Resource.Layout.Main);

        //    var facebook = FindViewById<Button>(Resource.Id.FacebookButton);
        //    facebook.Click += delegate
        //    {
        //        LoginToFacebook(true);
        //    };

        //    var facebookNoCancel = FindViewById<Button>(Resource.Id.FacebookButtonNoCancel);
        //    facebookNoCancel.Click += delegate
        //    {
        //        LoginToFacebook(false);
        //    };
        //}





    }
}