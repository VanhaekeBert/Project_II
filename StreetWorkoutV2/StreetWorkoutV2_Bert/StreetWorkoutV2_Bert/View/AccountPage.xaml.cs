using Newtonsoft.Json.Linq;
using StreetWorkoutV2_Bert.Model;
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

namespace StreetWorkoutV2_Bert.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AccountPage : ContentPage
    {

        public AccountPage()
        {
            InitializeComponent();
            // BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.BackgroundAccount_2x.png");
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Profile_BackCover.png");
            imgProfile.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Profile_Picture.png");
            this.BackgroundColor = Color.FromHex("2B3049");
            MakeEntriesKcal();
            MakeEntriesOef();

            Task.Run(async () =>
            {
                var data = await DBManager.GetUserData(Application.Current.Properties["Naam"].ToString(), "Naam");
                weightInput.Placeholder = data["Gewicht"].ToString();
                ageInput.Placeholder = data["Leeftijd"].ToString();
                heightInput.Placeholder = data["Lengte"].ToString();
                waterInput.Placeholder = data["WaterDoel"].ToString();
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
            for (int i = 0; i < 8; i++)
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
            List<string> listLabels = new List<string> {
                "Vr","Za","Zo","Ma","Di","Wo","Do","Vr"
            };
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
            
                JObject user = new JObject();
                user["Lengte"] = heightInput.Text.ToString();
                user["Gewicht"] = weightInput.Text.ToString();
                user["Leeftijd"] = ageInput.Text.ToString();
                user["WaterDoel"] = waterInput.Text.ToString();
                weightInput.Placeholder = user["Gewicht"].ToString();
                ageInput.Placeholder = user["Leeftijd"].ToString();
                heightInput.Placeholder = user["Lengte"].ToString();
                waterInput.Placeholder = user["WaterDoel"].ToString();
                await DBManager.PutUserData(Application.Current.Properties["Naam"].ToString(), "Naam", user);
           
        }


    }
}