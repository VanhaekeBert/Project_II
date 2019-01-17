﻿using FormsControls.Base;
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
            Task.Run(async () =>
            {
                JObject data = await DBManager.GetUserData(Application.Current.Properties["Naam"].ToString(), "Naam");
                if (data["API"].ToString() == "FitBit")
                {
                    lblFBverbonden.Text = "Verbonden";
                    lblPverbonden.Text = "Niet Verbonden";
                }
                else if (data["API"].ToString() == "Polar")
                {
                    lblFBverbonden.Text = "Niet Verbonden";
                    lblPverbonden.Text = "Verbonden";
                }
                else
                {
                    lblFBverbonden.Text = "Niet Verbonden";
                    lblPverbonden.Text = "Niet Verbonden";
                }
            });
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundSettings_2x.png");

            var tapGestureRecognizerFB = new TapGestureRecognizer();
            tapGestureRecognizerFB.Tapped += (s, e) => {
                Task.Run(async () =>
                {
                    FitBitUser user = await FitBitManager.FitBitAsync();
                    //LoadingIndicator.IsRunning = false;
                    var vUpdatedPage = new SettingsPage();
                    Navigation.InsertPageBefore(vUpdatedPage, this);
                    await Navigation.PopAsync();
                    user.Naam = Application.Current.Properties["Naam"].ToString();
                    string text = JsonConvert.SerializeObject(user);
                    JObject data = JsonConvert.DeserializeObject<JObject>(text);
                    await DBManager.PutUserData(user.Naam, "Naam", data);
                });
            };

            var tapGestureRecognizerP = new TapGestureRecognizer();
            tapGestureRecognizerP.Tapped += (s, e) => {
                var auth = PolarManager.GetPolarAuth();
                auth.AllowCancel = true;
                var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
                presenter.Login(auth);
                presenter.Completed += (p, ee) =>
                {
                    Task.Run(async () =>
                    {
                        PolarUser user = await PolarManager.GetPolarToken();
                        user.Naam = Application.Current.Properties["Naam"].ToString();

                        string text = JsonConvert.SerializeObject(user);
                        JObject data = JsonConvert.DeserializeObject<JObject>(text);
                        Debug.WriteLine(user.Leeftijd);
                        await DBManager.PutUserData(user.Naam, "Naam", data);
                    });
                };
            };


            FraWWR.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await Navigation.PushAsync(new WachtwoordResetPage());
                })
            });

            FraAD.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {


                    await Navigation.PushPopupAsync(new PopUpAccountDelete());
                })
            });


            FraFB.GestureRecognizers.Add(tapGestureRecognizerFB);
            FraP.GestureRecognizers.Add(tapGestureRecognizerP);
        }

        private async void Logout(object sender, EventArgs e)
        {
            Application.Current.Properties["Naam"] = null;
            await Application.Current.SavePropertiesAsync();
            await Navigation.PushAsync(new LoginPage());
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}