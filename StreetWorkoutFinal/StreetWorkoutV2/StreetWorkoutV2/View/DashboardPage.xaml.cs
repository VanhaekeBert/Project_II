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
            //lblWelcome.Text = "Welkom " + Application.Current.Properties["Naam"].ToString();
            one_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_1.png");
            two_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_2.png");
            four_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_4.png");
            //if (Application.Current.Properties["WaterDoel"].ToString() != null)
            //{
            //    if (Application.Current.Properties["WaterGedronken"].ToString() != null)
            //    {
            //        lblWaterGedronken.Text = Application.Current.Properties["WaterGedronken"].ToString();
            //    }
            //    lblWaterTotaal.Text = " / " + Application.Current.Properties["WaterDoel"].ToString() + " ";
            //}



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
            int water_current = int.Parse(lblWaterGedronken.Text);
            int water_added = int.Parse(TotalWater.Text);
            int water_now = water_current + water_added;
            Application.Current.Properties["WaterGedronken"] = water_now.ToString();
            await Application.Current.SavePropertiesAsync();
            lblWaterGedronken.Text = Application.Current.Properties["WaterGedronken"].ToString();
            popWater.IsEnabled = false;
            popWater.IsVisible = false;
        }

        
    }
}