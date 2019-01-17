using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                    user.Naam = Application.Current.Properties["Naam"].ToString();
                    string text = JsonConvert.SerializeObject(user);
                    JObject data = JsonConvert.DeserializeObject<JObject>(text);
                    Debug.WriteLine(user.Leeftijd);
                    await DBManager.PutUserData(user.Naam, "Naam", data);
                    JObject api = await DBManager.GetUserData(Application.Current.Properties["Naam"].ToString(), "Naam");
                    if (api["API"].ToString() == "FitBit")
                    {
                        lblFBverbonden.Text = "Verbonden";
                        lblPverbonden.Text = "Niet Verbonden";
                    }
                    else if (api["API"].ToString() == "Polar")
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
            };
            var tapGestureRecognizerP = new TapGestureRecognizer();
            tapGestureRecognizerP.Tapped += (s, e) => {
                Task.Run(async () =>
                {
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
                    JObject api = await DBManager.GetUserData(Application.Current.Properties["Naam"].ToString(), "Naam");
                    if (api["API"].ToString() == "FitBit")
                    {
                        lblFBverbonden.Text = "Verbonden";
                        lblPverbonden.Text = "Niet Verbonden";
                    }
                    else if (api["API"].ToString() == "Polar")
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
            };
            var tapGestureRecognizerWWR = new TapGestureRecognizer();
            tapGestureRecognizerWWR.Tapped += (s, e) => {
                Navigation.PushAsync(new WachtwoordResetPage());
            };
            FraFB.GestureRecognizers.Add(tapGestureRecognizerFB);
            FraP.GestureRecognizers.Add(tapGestureRecognizerP);
            FraWWR.GestureRecognizers.Add(tapGestureRecognizerWWR);
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
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