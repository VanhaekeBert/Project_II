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
        List<Oefening> weekOef = new List<Oefening>();
        List<Oefening> maandOef = new List<Oefening>();

        public AccountPage()
        {
            InitializeComponent();

            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundAccount.png");
            imgPencil.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.pencil.png");
            imgSelector.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.ImageSelect.png");
            lblUsername.Text = Application.Current.Properties["Naam"].ToString();
            NameChangeEntry.Text = Application.Current.Properties["Naam"].ToString();
            List<Oefening> oefeningen = (List<Oefening>)Application.Current.Properties["Oefeningen"];

            foreach (Oefening oefening in oefeningen)
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
            MakeEntriesKcal();
            MakeEntriesOef();
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
                    NameChangeEntry.Placeholder = Application.Current.Properties["Naam"].ToString();
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

            weightInput.Text = Application.Current.Properties["Gewicht"].ToString();
            ageInput.Text = Application.Current.Properties["Leeftijd"].ToString();
            heightInput.Text = Application.Current.Properties["Lengte"].ToString();
            waterInput.Text = Application.Current.Properties["WaterDoel"].ToString();
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
            List<string> data = new List<string>();
            for (int i = 0; i < weekOef.Count(); i++)
            {
                if (i == 0)
                {
                    data.Add(weekOef[i].Datum.ToString().Substring(0, 10).Replace(" ", ""));
                }
                else if (!data.Contains(weekOef[i].Datum.ToString().Substring(0, 10).Replace(" ", "")))
                {
                    data.Add(weekOef[i].Datum.ToString().Substring(0, 10).Replace(" ", ""));
                }
            }
            List<string> listKleuren = new List<string> {
                "#FF4A4A","#F74848","#F74848","#F74848","#E64343","#E64343","#E64343","#E64343"
            };
            List<string> listLabels = new List<string>();
            for (int i = 6; i >= 0; i--)
            {
                listLabels.Add(DateTime.Now.AddDays(-i).DayOfWeek.ToString().Substring(0, 4));
            }
            List<string> listValues = new List<string>();
            foreach (string date in listLabels)
            {
                int i = 0;
                foreach (Oefening oefening in weekOef)
                {
                    if (date == DateTime.Parse(oefening.Datum.ToString()).DayOfWeek.ToString().Substring(0, 4))
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

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            LoadingIndicator.IsRunning = true;
            JObject user = new JObject();
            user["Lengte"] = heightInput.Text.ToString();
            user["Gewicht"] = weightInput.Text.ToString();
            user["Leeftijd"] = ageInput.Text.ToString();
            user["WaterDoel"] = waterInput.Text.ToString();
            user["Naam"] = NameChangeEntry.Text.ToString();
            Application.Current.Properties["Lengte"] = user["Lengte"];
            Application.Current.Properties["Gewicht"] = user["Gewicht"];
            Application.Current.Properties["Leeftijd"] = user["Leeftijd"];
            Application.Current.Properties["WaterDoel"] = user["WaterDoel"];
            await Application.Current.SavePropertiesAsync();
            DBManager.PutUserData(Application.Current.Properties["Naam"].ToString(), "Naam", user);
            Application.Current.Properties["Naam"] = user["Naam"];
            await Application.Current.SavePropertiesAsync();
            LoadingIndicator.IsRunning = false;
            var vUpdatedPage = new AccountPage();
            Navigation.InsertPageBefore(vUpdatedPage, this);
            await Navigation.PopAsync();
        }
    }
}