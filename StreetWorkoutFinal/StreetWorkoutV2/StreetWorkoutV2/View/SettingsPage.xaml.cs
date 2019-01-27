using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : AnimationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            popNoConnection.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    popNoConnection.IsVisible = false;
                })
            });
            //MessagingCenter.Subscribe<SettingsPage, string>(this, "PassFitbitConnected", (sender, arg) =>
            //{
            //    lblFBverbonden.Text = arg;

            //});
            //if (Preferences.Get("API", "") == "FitBit")
            //{
            //    lblFBverbonden.Text = "Verbonden";
            //    lblPverbonden.Text = "Niet Verbonden";
            //}
            //else if (Preferences.Get("API", "") == "Polar")
            //{
            //    lblFBverbonden.Text = "Niet Verbonden";
            //    lblPverbonden.Text = "Verbonden";
            //}
            //else
            //{
            //    lblFBverbonden.Text = "Niet Verbonden";
            //    lblPverbonden.Text = "Niet Verbonden";
            //}
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundSettings_2x.png");

            //var tapGestureRecognizerFB = new TapGestureRecognizer();
            //tapGestureRecognizerFB.Tapped += (s, e) =>
            //{
            //    Task.Run(async () =>
            //    {
            //        FitBitUser user = await FitBitManager.FitBitAsync();

            //        user.Name = Preferences.Get("Name", "").ToString();
            //        string text = JsonConvert.SerializeObject(user);
            //        JObject data = JsonConvert.DeserializeObject<JObject>(text);
            //        Preferences.Set("Age", data["Age"].ToString());
            //        Preferences.Set("Length", data["Length"].ToString());
            //        Preferences.Set("Weigth", data["Weigth"].ToString());
            //        Preferences.Set("API", data["API"].ToString());
            //        MessagingCenter.Send(this, "PassFitbitConnected", "Verbonden");

            //        DBManager.PutUserData(user.Name, "Name", data);
            //    });
            //};

            //var tapGestureRecognizerP = new TapGestureRecognizer();
            //tapGestureRecognizerP.Tapped += (s, e) =>
            //{
            //    var auth = PolarManager.GetPolarAuth();
            //    auth.AllowCancel = true;
            //    var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            //    presenter.Login(auth);
            //    presenter.Completed += (p, ee) =>
            //    {
            //        Task.Run(async () =>
            //        {
            //            PolarUser user = await PolarManager.GetPolarToken();
            //            user.Name = Preferences.Get("Name", "");
            //            string text = JsonConvert.SerializeObject(user);
            //            JObject data = JsonConvert.DeserializeObject<JObject>(text);
            //            Preferences.Set("Age", data["Age"].ToString());
            //            Preferences.Set("Length", data["Length"].ToString());
            //            Preferences.Set("Weigth", data["Weigth"].ToString());
            //            Preferences.Set("API", data["API"].ToString());
            //            DBManager.PutUserData(user.Name, "Name", data);
            //        });
            //    };
            //};


            //---------------------------------------------------------------------------------------//
            //----------------------------------Gesture Recognizers----------------------------------//
            //---------------------------------------------------------------------------------------//

            framePasswordReset.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await framePasswordReset.FadeTo(0.5, 100);
                    framePasswordReset.FadeTo(1, 75);
                    await Navigation.PushAsync(new PasswordResetPage());
                })
            });

            frameDeleteAccount.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    if (Connection.CheckConnection())
                    {
                        await frameDeleteAccount.FadeTo(0.5, 100);
                        frameDeleteAccount.FadeTo(1, 75);
                        await Navigation.PushPopupAsync(new PopUpAccountDelete());
                    }
                    else
                    {
                        popNoConnection.IsVisible = true;
                    }
                })
            });


            //FraFB.GestureRecognizers.Add(tapGestureRecognizerFB);
            //FraP.GestureRecognizers.Add(tapGestureRecognizerP);
        }

        //---------------------------------------------------------------------------------------//
        //------------------------Uitloggen en alle lokale data leegmaken------------------------//
        //---------------------------------------------------------------------------------------//
        private async void Logout(object sender, EventArgs e)
        {
            Preferences.Set("Name", null);
            Preferences.Set("ApiName", null);
            Preferences.Set("Email", null);
            Preferences.Set("Age", null);
            Preferences.Set("Length", null);
            Preferences.Set("Weight", null);
            Preferences.Set("WaterGoal", null);
            Preferences.Set("WaterDrunk", null);
            Preferences.Set("Exercises", null);
            Preferences.Set("Water", null);
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }

        //---------------------------------------------------------------------------------------//
        //----------------------------Uitschakelen van de backbutton-----------------------------//
        //---------------------------------------------------------------------------------------//
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}