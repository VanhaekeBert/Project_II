using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using Xamarin.Forms.Xaml;
using FormsControls.Base;
using StreetWorkoutV2.Model;
using Newtonsoft.Json.Linq;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseCompletePage : AnimationPage
    {
        public ExerciseCompletePage()
        {
            InitializeComponent();
            Application.Current.Properties["EndWorkout"] = DateTime.Now;
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Oefening_Complete_Background.png");
            //backbuttonImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Backbutton.png");
            //ImgCal.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Vuur.png");
            //ImgHeart.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Heart_Compleet.png");
            List<int> repetitions = new List<int>();
            repetitions.Add(13);
            repetitions.Add(15);
            repetitions.Add(15);
            Logboek logboek = new Logboek() { Name = "Triceps Extensions", Moeilijkheidsgraad = "Makkelijk", Total_hearts_given = 3, ExerciseRepetitions = repetitions };
            NameToestel.BindingContext = logboek;
            //Moeilijkheidsgraad.BindingContext = logboek;
            Repetition1.BindingContext = logboek;
            Repetition2.BindingContext = logboek;
            Repetition3.BindingContext = logboek;

            var picUncolored = FileImageSource.FromResource("StreetWorkoutV2.Asset.Heart_Uncolored.png");

            imgRatingHeart1.Source = picUncolored;
            imgRatingHeart2.Source = picUncolored;
            imgRatingHeart3.Source = picUncolored;
            imgRatingHeart4.Source = picUncolored;
            imgRatingHeart5.Source = picUncolored;
            var picColored = FileImageSource.FromResource("StreetWorkoutV2.Asset.Heart_Colored.png");
            imgRatingHeartFull1.Source = picColored;
            imgRatingHeartFull2.Source = picColored;
            imgRatingHeartFull3.Source = picColored;
            imgRatingHeartFull4.Source = picColored;
            imgRatingHeartFull5.Source = picColored;

            Rate3Stars();

            this.BackgroundColor = Color.FromHex("2B3049");
            MakeEntriesHartslag();

            //houd  rating bij tussen 1-5 (wordt nog niets mee gedaan )
            int rating;

            imgRatingHeart1.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate1Star();
                    rating = 1;
                })
            });
            imgRatingHeart2.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate2Stars();
                    rating = 2;
                })
            });
            imgRatingHeart3.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate3Stars();
                    rating = 3;
                })
            });
            imgRatingHeart4.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate4Stars();
                    rating = 4;
                })
            });
            imgRatingHeart5.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate5Stars();
                    rating = 5;
                })
            });

            imgRatingHeartFull1.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate1Star();
                    rating = 1;
                })
            });
            imgRatingHeartFull2.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate2Stars();
                    rating = 2;
                })
            });
            imgRatingHeartFull3.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate3Stars();
                    rating = 3;

                })
            });
            imgRatingHeartFull4.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate4Stars();
                    rating = 4;
                })
            });
            imgRatingHeartFull5.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    Rate5Stars();
                    rating = 5;
                })
            });
        }

        private void Rate1Star()
        {
            imgRatingHeartFull1.IsVisible = true; imgRatingHeartFull1.IsEnabled = true;
            imgRatingHeartFull2.IsVisible = false; imgRatingHeartFull2.IsEnabled = false;
            imgRatingHeartFull3.IsVisible = false; imgRatingHeartFull3.IsEnabled = false;
            imgRatingHeartFull4.IsVisible = false; imgRatingHeartFull4.IsEnabled = false;
            imgRatingHeartFull5.IsVisible = false; imgRatingHeartFull5.IsEnabled = false;
            imgRatingHeart1.IsVisible = false; imgRatingHeart1.IsEnabled = false;
            imgRatingHeart2.IsVisible = true; imgRatingHeart2.IsEnabled = true;
            imgRatingHeart3.IsVisible = true; imgRatingHeart3.IsEnabled = true;
            imgRatingHeart4.IsVisible = true; imgRatingHeart4.IsEnabled = true;
            imgRatingHeart5.IsVisible = true; imgRatingHeart4.IsEnabled = true;
        }

        private void Rate2Stars()
        {
            imgRatingHeartFull1.IsVisible = true; imgRatingHeartFull1.IsEnabled = true;
            imgRatingHeartFull2.IsVisible = true; imgRatingHeartFull2.IsEnabled = true;
            imgRatingHeartFull3.IsVisible = false; imgRatingHeartFull3.IsEnabled = false;
            imgRatingHeartFull4.IsVisible = false; imgRatingHeartFull4.IsEnabled = false;
            imgRatingHeartFull5.IsVisible = false; imgRatingHeartFull5.IsEnabled = false;
            imgRatingHeart1.IsVisible = false; imgRatingHeart1.IsEnabled = false;
            imgRatingHeart2.IsVisible = false; imgRatingHeart2.IsEnabled = false;
            imgRatingHeart3.IsVisible = true; imgRatingHeart3.IsEnabled = true;
            imgRatingHeart4.IsVisible = true; imgRatingHeart4.IsEnabled = true;
            imgRatingHeart5.IsVisible = true; imgRatingHeart4.IsEnabled = true;

        }

        private void Rate3Stars()
        {
            imgRatingHeartFull1.IsVisible = true; imgRatingHeartFull1.IsEnabled = true;
            imgRatingHeartFull2.IsVisible = true; imgRatingHeartFull2.IsEnabled = true;
            imgRatingHeartFull3.IsVisible = true; imgRatingHeartFull3.IsEnabled = true;
            imgRatingHeartFull4.IsVisible = false; imgRatingHeartFull4.IsEnabled = false;
            imgRatingHeartFull5.IsVisible = false; imgRatingHeartFull5.IsEnabled = false;
            imgRatingHeart1.IsVisible = false; imgRatingHeart1.IsEnabled = false;
            imgRatingHeart2.IsVisible = false; imgRatingHeart2.IsEnabled = false;
            imgRatingHeart3.IsVisible = false; imgRatingHeart3.IsEnabled = false;
            imgRatingHeart4.IsVisible = true; imgRatingHeart4.IsEnabled = true;
            imgRatingHeart5.IsVisible = true; imgRatingHeart4.IsEnabled = true;
        }
        private void Rate4Stars()
        {
            imgRatingHeartFull1.IsVisible = true; imgRatingHeartFull1.IsEnabled = true;
            imgRatingHeartFull2.IsVisible = true; imgRatingHeartFull2.IsEnabled = true;
            imgRatingHeartFull3.IsVisible = true; imgRatingHeartFull3.IsEnabled = true;
            imgRatingHeartFull4.IsVisible = true; imgRatingHeartFull4.IsEnabled = true;
            imgRatingHeartFull5.IsVisible = false; imgRatingHeartFull5.IsEnabled = false;
            imgRatingHeart1.IsVisible = false; imgRatingHeart1.IsEnabled = false;
            imgRatingHeart2.IsVisible = false; imgRatingHeart2.IsEnabled = false;
            imgRatingHeart3.IsVisible = false; imgRatingHeart3.IsEnabled = false;
            imgRatingHeart4.IsVisible = false; imgRatingHeart4.IsEnabled = false;
            imgRatingHeart5.IsVisible = true; imgRatingHeart4.IsEnabled = true;
        }
        private void Rate5Stars()
        {
            imgRatingHeart1.IsVisible = false; imgRatingHeart1.IsEnabled = false;
            imgRatingHeart2.IsVisible = false; imgRatingHeart2.IsEnabled = false;
            imgRatingHeart3.IsVisible = false; imgRatingHeart3.IsEnabled = false;
            imgRatingHeart4.IsVisible = false; imgRatingHeart4.IsEnabled = false;
            imgRatingHeart5.IsVisible = false; imgRatingHeart4.IsEnabled = false;
            imgRatingHeartFull1.IsVisible = true; imgRatingHeartFull1.IsEnabled = true;
            imgRatingHeartFull2.IsVisible = true; imgRatingHeartFull2.IsEnabled = true;
            imgRatingHeartFull3.IsVisible = true; imgRatingHeartFull3.IsEnabled = true;
            imgRatingHeartFull4.IsVisible = true; imgRatingHeartFull4.IsEnabled = true;
            imgRatingHeartFull5.IsVisible = true; imgRatingHeartFull5.IsEnabled = true;
        }


        private void MakeEntriesHartslag()
        {
            List<string> listKleuren = new List<string> {
                "#FF4A4A","#F74848","#F74848","#F74848","#E64343","#E64343","#E64343","#E64343"
            };
            List<string> listLabels = new List<string> {
                "13:01","13:02","13:02","13:03","13:04","13:05","13:06","13:07"
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
            //chartHartslag.Chart = new LineChart()
            //{
            //    Entries = entriesOef,
            //    BackgroundColor = SKColors.Transparent,
            //    PointSize = 22,
            //    LabelTextSize = 22,
            //    ValueLabelOrientation = Microcharts.Orientation.Horizontal,
            //    LabelOrientation = Microcharts.Orientation.Horizontal,
            //    LabelColor = SKColor.Parse("#FFFFFF"),
            //};
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            JObject oefening = new JObject();
            oefening["Naam"] = Application.Current.Properties["Naam"].ToString();
            oefening["Datum"] = DateTime.Now.ToString();
            oefening["Duur"] = Application.Current.Properties["WorkTime"].ToString();
            oefening["Workout"] = Application.Current.Properties["Workout"].ToString();
            oefening["Moeilijkheidsgraad"] = Application.Current.Properties["Difficulty"].ToString();
            if (imgRatingHeartFull5.IsVisible)
            {
                oefening["Gevoel"] = "5";
            }
            else if (imgRatingHeartFull4.IsVisible)
            {
                oefening["Gevoel"] = "4";
            }
            else if (imgRatingHeartFull3.IsVisible)
            {
                oefening["Gevoel"] = "3";
            }
            else if (imgRatingHeartFull2.IsVisible)
            {
                oefening["Gevoel"] = "2";
            }
            else
            {
                oefening["Gevoel"] = "1";
            }
            await DBManager.PostOefening(oefening);
            await Navigation.PopToRootAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            }
            JObject oefening = new JObject();
            oefening["Naam"] = Application.Current.Properties["Naam"].ToString();
            oefening["Datum"] = DateTime.Now.ToString();
            oefening["Duur"] = Application.Current.Properties["WorkTime"].ToString();
            oefening["Workout"] = Application.Current.Properties["Workout"].ToString();
            oefening["Moeilijkheidsgraad"] = Application.Current.Properties["Difficulty"].ToString();
            if (imgRatingHeartFull5.IsVisible)
            {
                oefening["Gevoel"] = "5";
            }
            else if (imgRatingHeartFull4.IsVisible)
            {
                oefening["Gevoel"] = "4";
            }
            else if (imgRatingHeartFull3.IsVisible)
            {
                oefening["Gevoel"] = "3";
            }
            else if (imgRatingHeartFull2.IsVisible)
            {
                oefening["Gevoel"] = "2";
            }
            else
            {
                oefening["Gevoel"] = "1";
            }
            await DBManager.PostOefening(oefening);
            await Navigation.PopAsync();
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}