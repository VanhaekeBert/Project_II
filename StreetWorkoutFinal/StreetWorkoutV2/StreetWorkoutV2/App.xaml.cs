using FormsControls.Base;
using StreetWorkoutV2.View;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StreetWorkoutV2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            if (Preferences.Get("Name", "") != "")
            {
                Preferences.Set("From", true);
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
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
