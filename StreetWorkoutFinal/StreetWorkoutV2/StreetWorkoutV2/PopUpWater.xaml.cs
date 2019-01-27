using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Pages;
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

namespace StreetWorkoutV2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpWater : PopupPage
    {
		public PopUpWater ()
		{
			InitializeComponent ();
            imgGlassOne.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_1.png");
            imgGlassTwo.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_2.png");
            imgGlassFour.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_4.png");
            imgNoConnectionWater.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.connection.png");

            // lblWaterGedronken.Text = Preferences.Get("WaterDrunk", 0).ToString();
            stackGlassOne.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await stackGlassOne.FadeTo(0.3, 75);
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 250;
                    TotalWater.Text = Water_update.ToString();
                    await stackGlassOne.FadeTo(1, 75);

                })
            });

            stackGlassTwo.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await stackGlassTwo.FadeTo(0.3, 75);
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 500;
                    TotalWater.Text = Water_update.ToString();
                    await stackGlassTwo.FadeTo(1, 75);

                })
            });

            stackGlassFour.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await stackGlassFour.FadeTo(0.3, 75);
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 1000;
                    TotalWater.Text = Water_update.ToString();
                    await stackGlassFour.FadeTo(1, 75);

                })
            });
            popNoConnectionWater.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    popNoConnectionWater.IsVisible = false;
                })
            });
        }
        private async void SubmitWaterInput_Clicked(object sender, EventArgs e)
        {
           LoadingIndicator.IsRunning = true;

            if (Connection.CheckConnection())
            {

                Preferences.Set("WaterDrunk", Preferences.Get("WaterDrunk", 0) + int.Parse(TotalWater.Text.ToString()));
            MessagingCenter.Send(this, "PassCurrentWater", Preferences.Get("WaterDrunk", 0).ToString());

            JObject water = new JObject();
            water["Name"] = Preferences.Get("Name", "");
            water["WaterDrunk"] = Preferences.Get("WaterDrunk", 0);
         
                await DBManager.PutWaterData(water);
                JArray waterlist = await DBManager.GetWaterData(Preferences.Get("Name", ""));
                var waterTojson = JsonConvert.SerializeObject(waterlist);
                Preferences.Set("Water", waterTojson.ToString());
                Debug.WriteLine(Preferences.Get("Water", ""));

                MessagingCenter.Send(this, "PassWaterDrunk", Preferences.Get("Water", ""));
                LoadingIndicator.IsRunning = false;
                PopupNavigation.PopAsync();
            }
            else
            {
                 popNoConnectionWater.IsVisible = true;
            }            
   
        }
    }
}