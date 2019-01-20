using FormsControls.Base;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
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
using ZXing.Net.Mobile.Forms;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : AnimationPage
    {

        string _result;
        public DashboardPage()
        {
            InitializeComponent();
            //Debug.WriteLine(Application.Current.Properties["Naam"]);
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundDashboard_alt.png");
            imgCal.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Vuur.png");
            imgWater.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Beker.png");
            imgStartWorkout.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.StartWorkout.png");
            imgStartWorkout_Cover.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.StartWorkout_Cover.png");
            imgQr.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.qrcode.png");
            imgMuscle.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.spier.png");
            imgDevice.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.toestel.png");
            lblWelcome.Text = "Welkom " + Preferences.Get("Naam", "");
            one_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_1.png");
            two_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_2.png");
            four_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_4.png");
            if (Preferences.Get("WaterDoel", 0) != null)
            {
                if (Preferences.Get("WaterGedronken", 0) != null)
                {
                    lblWaterGedronken.Text = Preferences.Get("WaterGedronken", 0).ToString();
                }
                lblWaterTotaal.Text = " / " + Preferences.Get("WaterDoel", 0).ToString() + " ";
            }

            //WaterPopUpFrame.GestureRecognizers.Add(new TapGestureRecognizer
            //{

            //});

            innerPopWater.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    popWater.IsEnabled = true;
                    popWater.IsVisible = true;
                    TotalWater.Text = "0";
                })
            });
            popWater.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    popWater.IsEnabled = false;
                    popWater.IsVisible = false;

                })
            });

 
            // -------------------------------------------------------------------
            // --------------------------TAPGESTURES -----------------------------
            // -------------------------------------------------------------------
            btnMuscle.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new PickerPage("Spiergroep"));
                })
            });
            btnDevice.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new PickerPage("Toestel"));
                })
            });

            click_one_glass.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 250;
                    TotalWater.Text = Water_update.ToString();
                })
            });

            click_two_glass.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 500;
                    TotalWater.Text = Water_update.ToString();
                })
            });

            click_four_glass.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 1000;
                    TotalWater.Text = Water_update.ToString();
                })
            });
            // -------------------------------------------------------------------
            // -------------------------------------------------------------------
            // -------------------------------------------------------------------
        }


       


        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void SubmitWater_Clicked(object sender, EventArgs e)
        {
            popWater.IsEnabled = false;
            lblWaterGedronken.Text = "500";
            popWater.IsVisible = false;
        }

        private async void SubmitWaterInput_Clicked(object sender, EventArgs e)
        {
            Preferences.Set("WaterGedronken", Preferences.Get("WaterGedronken", 0) + int.Parse(TotalWater.Text.ToString()));
            lblWaterGedronken.Text = Preferences.Get("WaterGedronken", 0).ToString();
            JObject water = new JObject();
            water["Naam"] = Preferences.Get("Naam", "");
            water["WaterGedronken"] = Preferences.Get("WaterGedronken", 0);
            DBManager.PutWater(water);
            popWater.IsEnabled = false;
            popWater.IsVisible = false;
        }

        
    }
}