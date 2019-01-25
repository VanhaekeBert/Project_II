using FormsControls.Base;
using Newtonsoft.Json;
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
            if (!Preferences.Get("Connection", true))
            {
                popNoConnection.IsVisible = true;
                Preferences.Set("Connection", true);
            }
            popNoConnection.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    popNoConnection.IsVisible = false;
                })
            });
            popNoConnectionQR.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    popNoConnectionQR.IsVisible = false;
                })
            });
            popNoConnectionWater.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    popNoConnectionWater.IsVisible = false;
                })
            });
            MessagingCenter.Subscribe<AccountPage, string>(this, "PassWaterGoal", (sender,arg) =>
            {
                lblWaterTotaal.Text =arg;

            });
            MessagingCenter.Subscribe<AccountPage, string>(this, "PassName", (sender,arg) =>
            {
                lblWelcome.Text = "Welkom " + arg;

            });

            MessagingCenter.Subscribe<ExerciseCompletePage, string>(this, "PassOefeningen", (sender, arg) =>
            {
                List<OefeningDB> listweekOef = new List<OefeningDB>();
                if (arg != "[]")
                {
                    var rawOefeningen = Preferences.Get("Oefeningen", "").ToString().Replace("[", "").Replace("]", "").Split('}');
                    List<OefeningDB> oefeningen = new List<OefeningDB>();
                    for (int i = 0; i < rawOefeningen.Count(); i++)
                    {
                        if (i == 0)
                        {
                            oefeningen.Add(JsonConvert.DeserializeObject<OefeningDB>(rawOefeningen[i].ToString() + "}"));
                        }
                        else if (i != (rawOefeningen.Count() - 1))
                        {
                            oefeningen.Add(JsonConvert.DeserializeObject<OefeningDB>(rawOefeningen[i].ToString().Remove(0, 1) + "}"));
                        }
                    }
                    foreach (OefeningDB oefening in oefeningen)
                    {
                        if (Enumerable.Range((int.Parse(DateTime.Now.ToString("dd")) - 6), (int.Parse(DateTime.Now.ToString("dd")) + 1)).Contains(oefening.Datum.Day))
                        {
                            listweekOef.Add(oefening);
                        }
                    }
                }
                LblLogs.Text = listweekOef.Count().ToString();
            });

                List<OefeningDB> weekOef = new List<OefeningDB>();
            if (Preferences.Get("Oefeningen", "") != "[]")
            {
                var rawOefeningen = Preferences.Get("Oefeningen", "").ToString().Replace("[", "").Replace("]", "").Split('}');
                List<OefeningDB> oefeningen = new List<OefeningDB>();
                for (int i = 0; i < rawOefeningen.Count(); i++)
                {
                    if (i == 0)
                    {
                        oefeningen.Add(JsonConvert.DeserializeObject<OefeningDB>(rawOefeningen[i].ToString() + "}"));
                    }
                    else if (i != (rawOefeningen.Count() - 1))
                    {
                        oefeningen.Add(JsonConvert.DeserializeObject<OefeningDB>(rawOefeningen[i].ToString().Remove(0, 1) + "}"));
                    }
                }
                foreach (OefeningDB oefening in oefeningen)
                {
                    if (Enumerable.Range((int.Parse(DateTime.Now.ToString("dd")) - 6), (int.Parse(DateTime.Now.ToString("dd")) + 1)).Contains(oefening.Datum.Day))
                    {
                        weekOef.Add(oefening);
                    }
                }
            }
            LblLogs.Text = weekOef.Count().ToString();
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundDashboard_alt.png");
            imgLog.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.LogIcon.png");
            imgWater.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Beker.png");
            imgStartWorkout.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.StartWorkout.png");
            imgStartWorkout_Cover.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.StartWorkout_Cover.png");
            imgQr.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.qrcode.png");
            imgMuscle.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.spier.png");
            imgDevice.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.toestel.png");
            lblWelcome.Text = "Welkom " + Preferences.Get("ApiNaam", "");
            one_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_1.png");
            two_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_2.png");
            four_glass.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Glass_4.png");
            if (Preferences.Get("WaterDoel", 0) != null)
            {
                if (Preferences.Get("WaterGedronken", 0) != null)
                {
                    lblWaterGedronken.Text = Preferences.Get("WaterGedronken", 0).ToString();
                }
                lblWaterTotaal.Text = Preferences.Get("WaterDoel", 0).ToString();
            }


            frameWater.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await frameWater.FadeTo(0.5, 100);
                    frameWater.FadeTo(1, 75);
                    popWater.IsEnabled = true;
                    popWater.IsVisible = true;
                    
                })
            });

            frameLog.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await frameLog.FadeTo(0.5, 100);
                    frameLog.FadeTo(1, 75);
                    await Navigation.PushAsync(new LogbookPage());
                    

                })
            });

            innerPopWater.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command( () =>
                {
                    popWater.IsEnabled = true;
                    popWater.IsVisible = true;
                    TotalWater.Text = "0";
                })
            });
            popWater.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command( () =>
                {
                    TotalWater.Text = "0";

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
                    await btnMuscle.FadeTo(0.3, 75);
                    await btnMuscle.FadeTo(1, 75);
                    await Navigation.PushAsync(new PickerPage("Spiergroep"));
                    

                })
            });
            btnDevice.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await btnDevice.FadeTo(0.3, 75);
                    await btnDevice.FadeTo(1, 75);
                    await Navigation.PushAsync(new PickerPage("Toestel"));

                })
            });

            btnQR.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    if (Connection.CheckConnection())
                    {
                        await btnQR.FadeTo(0.3, 75);
                        await btnQR.FadeTo(1, 75);
                        await Navigation.PushAsync(new QrPage());
                    }
                    else
                    {
                        popNoConnectionQR.IsVisible = true;
                    }
                })
            });

            click_one_glass.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await click_one_glass.FadeTo(0.3, 75);
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 250;
                    TotalWater.Text = Water_update.ToString();
                    await click_one_glass.FadeTo(1, 75);

                })
            });

            click_two_glass.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await click_two_glass.FadeTo(0.3, 75);
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 500;
                    TotalWater.Text = Water_update.ToString();
                    await click_two_glass.FadeTo(1, 75);

                })
            });

            click_four_glass.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await click_four_glass.FadeTo(0.3, 75);
                    int Water_now = int.Parse(TotalWater.Text);
                    int Water_update = Water_now + 1000;
                    TotalWater.Text = Water_update.ToString();
                    await click_four_glass.FadeTo(1, 75);

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

        private async void SubmitWaterInput_Clicked(object sender, EventArgs e)
        {
           await SubmitWaterInput.FadeTo(0.3, 75);

            Preferences.Set("WaterGedronken", Preferences.Get("WaterGedronken", 0) + int.Parse(TotalWater.Text.ToString()));
            lblWaterGedronken.Text = Preferences.Get("WaterGedronken", 0).ToString();
            JObject water = new JObject();
            water["Naam"] = Preferences.Get("Naam", "");
            water["WaterGedronken"] = Preferences.Get("WaterGedronken", 0);
            if (Connection.CheckConnection())
            {
                await DBManager.PutWater(water);
                JArray waterlist = await DBManager.GetWater(Preferences.Get("Naam", ""));
                var waterTojson = JsonConvert.SerializeObject(waterlist);
                Preferences.Set("Water", waterTojson.ToString());
            }
            else
            {
                popNoConnectionWater.IsVisible = true;
            }
            MessagingCenter.Send(this, "PassWaterGedronken", Preferences.Get("Water", ""));
            await SubmitWaterInput.FadeTo(1, 75);
            TotalWater.Text = "0";
            popWater.IsEnabled = false;
            popWater.IsVisible = false;
        }

        
    }
}