using StreetWorkout.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkout
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

<<<<<<< HEAD
            MainPage = new MainPage();
=======
            MainPage = new NavigationPage(new IntroPage());
>>>>>>> de0c5a84f4cc5fa827e59cdce9c4341fa9747e94
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
