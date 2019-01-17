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

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : AnimationPage
    {
        public Color JObject { get; }
        List<JObject> weekOef = new List<JObject>();
        List<JObject> maandOef = new List<JObject>();

        public AccountPage()
        {
            InitializeComponent();
            // BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundAccount_2x.png");
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundAccount.png");
            Potlood.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.pencil.png");
            imgSelector.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.ImageSelect.png");
            Username.Text = Application.Current.Properties["Naam"].ToString();
            NameChangeEntry.Text = Application.Current.Properties["Naam"].ToString();
            Task.Run(async () =>
            {
                List<JObject> oefeningen = await DBManager.GetOefeningenData(Application.Current.Properties["Naam"].ToString());
                foreach (JObject oefening in oefeningen)
                {
                    if (Enumerable.Range((int.Parse(DateTime.Now.ToString("dd")) - 6), (int.Parse(DateTime.Now.ToString("dd"))+1)).Contains(int.Parse(oefening["Datum"].ToString().Substring(0,2))))
                    {
                        weekOef.Add(oefening);
                    }
                    if (int.Parse(DateTime.Now.ToString("MM")) == int.Parse(oefening["Datum"].ToString().Substring(3, 2)))
                    {
                        maandOef.Add(oefening);
                    }
                }
                LblOefWeek.Text = weekOef.Count().ToString();
                LblOefMaand.Text = maandOef.Count().ToString();
            });
            MakeEntriesKcal();
            MakeEntriesOef();
            

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
                    NameChangeEntry.Placeholder = Application.Current.Properties["Naam"].ToString();
                    NameChangeEntry.IsVisible = true;
                    NameChangeEntry.IsEnabled = true;
                    Username.IsVisible = false;
                    Potlood.IsVisible = false;
                })
            });

            NameChangeEntry.Unfocused += async (sender, e) => {
                NameChangeEntry.IsVisible = false;
                NameChangeEntry.IsEnabled = false;
                Username.Text = NameChangeEntry.Text;
                Username.IsVisible = true;
                Potlood.IsVisible = true;
            };


            Task.Run(async () =>
            {
                var data = await DBManager.GetUserData(Application.Current.Properties["Naam"].ToString(), "Naam");
                weightInput.Text = data["Gewicht"].ToString();
                ageInput.Text = data["Leeftijd"].ToString();
                heightInput.Text = data["Lengte"].ToString();
                waterInput.Text = data["WaterDoel"].ToString();
            });
        }

        private void MakeEntriesKcal()
        {
            List<string> listKleuren = new List<string> {
                "#EE9F44","#EE9944","#EE9344","#EE8E44","#EE8844","#EE8244","#EE7D44","#EE8244"
            };
            List<string> listLabels = new List<string> {
                "Vr","Za","Zo","Ma","Di","Wo","Do","Vr"
            };
            List<string> listValues = new List<string> {
                "42","45","40","120","81","83","60","5"
            };

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
            List<JObject> oef = new List<JObject>();
            List<string> data = new List<string>();
            for (int i = 0; i < weekOef.Count(); i++)
            {
                if (i == 0)
                {
                    data.Add(weekOef[i]["Datum"].ToString().Substring(0, 10).Replace(" ", ""));
                    oef.Add(weekOef[i]);
                }
                else if (!data.Contains(weekOef[i]["Datum"].ToString().Substring(0, 10).Replace(" ", "")))
                {
                    data.Add(weekOef[i]["Datum"].ToString().Substring(0, 10).Replace(" ", ""));
                    oef.Add(weekOef[i]);
                }
            }
            List<string> listKleuren = new List<string> {
                "#FF4A4A","#F74848","#F74848","#F74848","#E64343","#E64343","#E64343","#E64343"
            };
            List<string> listLabels = new List<string>();
            foreach (string dag in data)
            {
                listLabels.Add(DateTime.ParseExact(dag, "dddd", System.Globalization.CultureInfo.InvariantCulture).ToString().Substring(0, 2));
            }
            List<string> listValues = new List<string> {
                "42","45","40","120","81","83","60","5"
            };

            List<Entry> entriesOef = new List<Entry> { };
            for (int i = 0; i < 8; i++)
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


        private async void Button_Clicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsRunning = true;

            JObject user = new JObject();
            
            user["Lengte"] = heightInput.Text.ToString();
            user["Gewicht"] = weightInput.Text.ToString();
            user["Leeftijd"] = ageInput.Text.ToString();
            user["WaterDoel"] = waterInput.Text.ToString();
            weightInput.Text = user["Gewicht"].ToString();
            ageInput.Text = user["Leeftijd"].ToString();
            heightInput.Text = user["Lengte"].ToString();
            waterInput.Text = user["WaterDoel"].ToString();
            await DBManager.PutUserData(Application.Current.Properties["Naam"].ToString(), "Naam", user);
                LoadingIndicator.IsRunning = false;
            var vUpdatedPage = new AccountPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            await Navigation.PopAsync();



        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

    }
}