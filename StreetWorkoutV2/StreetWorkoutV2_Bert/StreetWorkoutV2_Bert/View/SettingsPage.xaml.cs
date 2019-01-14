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
                    user.Naam = "FitBit";
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
                    PolarUser user = PolarManager.PolarAsync();
                    user.Naam = Application.Current.Properties["Naam"].ToString();
                Task.Run(async () =>
                {
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
            FraFB.GestureRecognizers.Add(tapGestureRecognizerFB);
            FraP.GestureRecognizers.Add(tapGestureRecognizerP);
        }

        private async Task Sign_Out_Button_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties["Naam"] = "";
            await Application.Current.SavePropertiesAsync();
            await Navigation.PushAsync(new LoginPage());
        }
    }
}