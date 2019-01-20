using Newtonsoft.Json.Linq;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using Xamarin.Forms.Xaml;
using System.IO;
using FormsControls.Base;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : AnimationPage
    {
        public Color JObject { get; }
        List<OefeningDB> weekOef = new List<OefeningDB>();
        List<OefeningDB> maandOef = new List<OefeningDB>();

        public AccountPage()
        {
            InitializeComponent();

            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundAccount.png");
            imgPencil.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.pencil.png");
            imgSelector.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.ImageSelect.png");
            lblUsername.Text = Preferences.Get("Naam", "");
            NameChangeEntry.Text = Preferences.Get("Naam", "");
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
                    if (int.Parse(DateTime.Now.ToString("MM")) == oefening.Datum.Month)
                    {
                        maandOef.Add(oefening);
                    }
                }
                LblOefWeek.Text = weekOef.Count().ToString();
                LblOefMaand.Text = maandOef.Count().ToString();
                int kcalweek = 0;
                foreach (OefeningDB oefening in weekOef)
                {
                    kcalweek += oefening.Kcal;
                }
                int kcalmaand = 0;
                foreach (OefeningDB oefening in weekOef)
                {
                    kcalmaand += oefening.Kcal;
                }
                LblKcalWeek.Text = kcalweek.ToString();
                LblKcalMaand.Text = kcalmaand.ToString();
                MakeEntriesOef();
                MakeEntriesKcal();
            }
            this.BackgroundColor = Color.FromHex("2B3049");


            //Profile picture ophalen
            TapGestureRecognizer ImageHandler = new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
                    if (stream != null)
                    {

                        imgProfile.Source = ImageSource.FromStream(() => stream);


                    }
                })
            };

            imgSelector.GestureRecognizers.Add(ImageHandler);
            imgProfile.GestureRecognizers.Add(ImageHandler);

            NameChanger.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {
                    NameChangeEntry.Placeholder = Preferences.Get("Naam", "");
                    NameChangeEntry.IsVisible = true;
                    NameChangeEntry.IsEnabled = true;
                    NameChangeEntry.Focus();
                    lblUsername.IsVisible = false;
                    imgPencil.IsVisible = false;
                })
            });

            NameChangeEntry.Unfocused += async (sender, e) =>
            {
                NameChangeEntry.IsVisible = false;
                NameChangeEntry.IsEnabled = false;
                lblUsername.Text = NameChangeEntry.Text;
                lblUsername.IsVisible = true;
                imgPencil.IsVisible = true;
            };
            weightInput.Text = Preferences.Get("Gewicht", "");
            ageInput.Text = Preferences.Get("Leeftijd", "");
            heightInput.Text = Preferences.Get("Lengte", "");
            waterInput.Text = Preferences.Get("WaterDoel", 0).ToString();
        }

        private void MakeEntriesKcal()
        {
            List<string> listKleuren = new List<string> {
                "#EE9F44","#EE9944","#EE9344","#EE8E44","#EE8844","#EE8244","#EE7D44","#EE8244"
            };
            List<string> listLabels = new List<string>();
            for (int i = 6; i >= 0; i--)
            {
                listLabels.Add(DateTime.Now.AddDays(-i).DayOfWeek.ToString().Substring(0, 3));
            }
            List<string> listValues = new List<string>();
            foreach (string date in listLabels)
            {
                int i = 0;
                foreach (OefeningDB oefening in weekOef)
                {
                    if (date == DateTime.Parse(oefening.Datum.ToString()).DayOfWeek.ToString().Substring(0, 3))
                    {
                        i += oefening.Kcal;
                    }
                }
                listValues.Add(i.ToString());
            }


            List<Entry> entriesKcal2 = new List<Entry> { };
            for (int i = 0; i < 7; i++)
            {
                float value = float.Parse(listValues[i]);

                entriesKcal2.Add(new Entry(value)
                {
                    Color = SKColor.Parse(listKleuren[i]),
                    Label = listLabels[i],
                    ValueLabel = listValues[i]
                });
            }
            chartKcal.Chart = new LineChart()
            {
                Entries = entriesKcal2,
                BackgroundColor = SKColors.Transparent,
                PointSize = 22,
                LabelTextSize = 22,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                LabelColor = SKColor.Parse("#FFFFFF")
            };
        }
        private void MakeEntriesOef()
        {
            List<string> listKleuren = new List<string> {
                "#FF4A4A","#F74848","#F74848","#F74848","#E64343","#E64343","#E64343","#E64343"
            };
            List<string> listLabels = new List<string>();
            for (int i = 6; i >= 0; i--)
            {
                listLabels.Add(DateTime.Now.AddDays(-i).DayOfWeek.ToString().Substring(0, 3));
            }
            List<string> listValues = new List<string>();
            foreach (string date in listLabels)
            {
                int i = 0;
                foreach (OefeningDB oefening in weekOef)
                {
                    if (date == DateTime.Parse(oefening.Datum.ToString()).DayOfWeek.ToString().Substring(0, 3))
                    {
                        i++;
                    }
                }
                listValues.Add(i.ToString());
            }

            List<Entry> entriesOef = new List<Entry> { };
            for (int i = 0; i < listLabels.Count(); i++)
            {
                float value = float.Parse(listValues[i]);

                entriesOef.Add(new Entry(value)
                {
                    Color = SKColor.Parse(listKleuren[i]),
                    Label = listLabels[i],
                    ValueLabel = listValues[i]
                });
            }
            chartOef.Chart = new LineChart()
            {
                Entries = entriesOef,
                BackgroundColor = SKColors.Transparent,
                PointSize = 22,
                LabelTextSize = 22,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                LabelColor = SKColor.Parse("#FFFFFF"),
            };
        }

        private void MakeEntriesWater()
        {
            List<string> listKleuren = new List<string> {
                "#44C5EE","#44B9EE","#44AFEE","#44A2EE","#4499EE","#448EEE","#447DEE","#447DEE"
            };
            List<string> listLabels = new List<string>();
            for (int i = 6; i >= 0; i--)
            {
                listLabels.Add(DateTime.Now.AddDays(-i).DayOfWeek.ToString().Substring(0, 3));
            }

            List<string> listValues = new List<string>
            {
                "72","62","20","30","50","80","60","70"
            };

            List<Entry> entriesWater = new List<Entry> { };
            for (int i = 0; i < listLabels.Count(); i++)
            {
                float value = float.Parse(listValues[i]);

                entriesWater.Add(new Entry(value)
                {
                    Color = SKColor.Parse(listKleuren[i]),
                    Label = listLabels[i],
                    ValueLabel = listValues[i]
                });
            }
            chartWater.Chart = new LineChart()
            {
                Entries = entriesWater,
                BackgroundColor = SKColors.Transparent,
                PointSize = 22,
                LabelTextSize = 22,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                LabelColor = SKColor.Parse("#FFFFFF"),
            };
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsRunning = true;
            JObject user = new JObject();
            JObject water = new JObject();
            user["Lengte"] = heightInput.Text.ToString();
            user["Gewicht"] = weightInput.Text.ToString();
            user["Leeftijd"] = ageInput.Text.ToString();
            water["WaterDoel"] = int.Parse(waterInput.Text);
            water["Naam"] = Preferences.Get("Naam", "");
            //user["Naam"] = NameChangeEntry.Text.ToString();
            Preferences.Set("Lengte", user["Lengte"].ToString());
            Preferences.Set("Gewicht", user["Gewicht"].ToString());
            Preferences.Set("Leeftijd", user["Leeftijd"].ToString());
            Preferences.Set("WaterDoel", int.Parse(water["WaterDoel"].ToString()));
            DBManager.PutUserData(Preferences.Get("Naam", ""), "Naam", user);
            DBManager.PutWater(water);
            //Preferences.Set("Naam", user["Naam"].ToString());
            LoadingIndicator.IsRunning = false;
            var vUpdatedPage = new AccountPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            await Navigation.PopAsync();
        }
    }
}