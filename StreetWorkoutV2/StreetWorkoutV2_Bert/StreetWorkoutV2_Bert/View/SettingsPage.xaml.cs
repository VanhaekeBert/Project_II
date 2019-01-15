using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            Task.Run(async () =>
            {
                string api = await DBManager.CheckForAPI(Application.Current.Properties["Naam"].ToString());
                if (api == "FitBit")
                {
                    lblFBverbonden.Text = "Verbonden";
                    lblPverbonden.Text = "Niet Verbonden";
                }
                else if (api == "Polar")
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
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.BackgroundSettings_2x.png");
            var tapGestureRecognizerFB = new TapGestureRecognizer();
            tapGestureRecognizerFB.Tapped += (s, e) => {
                Task.Run(async () =>
                {
                    FitBitUser user = await FitBitManager.FitBitAsync();
                    user.Naam = Application.Current.Properties["Naam"].ToString();
                    await DBManager.UpdateAPIFB(user);
                    string api = await DBManager.CheckForAPI(Application.Current.Properties["Naam"].ToString());
                    if (api == "FitBit")
                    {
                        lblFBverbonden.Text = "Verbonden";
                        lblPverbonden.Text = "Niet Verbonden";
                    }
                    else if (api == "Polar")
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
                    PolarUser user = await PolarManager.GetUserData(PolarManager.PolarAsync());
                    user.Naam = Application.Current.Properties["Naam"].ToString();
                    Debug.WriteLine(user.Leeftijd);
                    await DBManager.UpdateAPIP(user);
                });
                Task.Run(async () =>
                {
                    string api = await DBManager.CheckForAPI(Application.Current.Properties["Naam"].ToString());
                if (api == "FitBit")
                    {
                        lblFBverbonden.Text = "Verbonden";
                        lblPverbonden.Text = "Niet Verbonden";
                    }
                    else if (api == "Polar")
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

        private async Task Sign_Out_Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["Naam"] = "";
            await Application.Current.SavePropertiesAsync();
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
        }
    }
}