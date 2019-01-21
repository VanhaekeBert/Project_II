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
using Xamarin.Essentials;
using System.Diagnostics;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseCompletePage : AnimationPage
    {
        public ExerciseCompletePage()
        {
            InitializeComponent();
            if (Preferences.Get("API", "") == "FitBit")
            {
                FrmChart.IsVisible = true;
                GrdAPIData.IsVisible = true;
                MakeEntriesHartslag();
            }
            Preferences.Set("EndWorkout", DateTime.Now);
            NameToestel.Text = Preferences.Get("Workout", "");
            Repetition1.Text = Preferences.Get("Repetition1", "");
            if (Preferences.Get("Doel", 0) > int.Parse(Preferences.Get("Repetition1", "")))
            {
                Repetition1.TextColor = Color.Red;
            }
            Repetition2.Text = Preferences.Get("Repetition2", "");
            if (Preferences.Get("Doel", 0) > int.Parse(Preferences.Get("Repetition2", "")))
            {
                Repetition2.TextColor = Color.Red;
            }
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Oefening_Complete_Background.png");
            //backbuttonImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Backbutton.png");
            //ImgCal.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Vuur.png");
            //ImgHeart.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Heart_Compleet.png");

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
        private void PopupRepetitionsConfirm_Clicked(object sender, EventArgs e)
        {
            popExerciseReview.IsVisible = false;
            popExerciseReview.IsEnabled = false;
            Preferences.Set($"Repetition{Preferences.Get("Counter", 0)}", inputRepetitions.Text);
            Repetition3.Text = inputRepetitions.Text;
            if (Preferences.Get("Doel", 0) > int.Parse(inputRepetitions.Text))
            {
                Repetition3.TextColor = Color.Red;
            }
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
                chartHartslag.Chart = new LineChart()
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

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            JObject oefening = new JObject();
            string herhalingen = "[";
            oefening["Naam"] = Preferences.Get("Naam", "");
            oefening["Datum"] = DateTime.Now.ToString();
            oefening["Duur"] = Preferences.Get("WorkTime", 0);
            Preferences.Set("WorkTime", 0);
            oefening["Workout"] = Preferences.Get("Workout", "");
            oefening["Moeilijkheidsgraad"] = Preferences.Get("Difficulty", "");
            for (int i = 0; i < 3; i++)
            {
                    if (i == 2)
                    {
                        herhalingen += "\"" + Preferences.Get($"Repetition{i}", "") + "\"" + "]";
                }
                    else
                    {
                        herhalingen += "\"" + Preferences.Get($"Repetition{i}", "") + "\"" + ", ";
                }
            }
            oefening["Herhalingen"] = herhalingen;
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
            if (Preferences.Get("API", "") == "FitBit")
            {
                oefening["Kcal"] = 5;
                oefening["MaxHeart"] = 5;
                oefening["AverageHeart"] = 5;
            }
            else
            {
                oefening["Kcal"] = 0;
                oefening["MaxHeart"] = 0;
                oefening["AverageHeart"] = 0;
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
            string herhalingen = "[";
            oefening["Naam"] = Preferences.Get("Naam", "");
            oefening["Datum"] = DateTime.Now.ToString();
            oefening["Duur"] = Preferences.Get("WorkTime", 0);
            Preferences.Set("WorkTime", 0);
            oefening["Workout"] = Preferences.Get("Workout", ""); ;
            oefening["Moeilijkheidsgraad"] = Preferences.Get("Difficulty", 0);
            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    herhalingen += Preferences.Get($"Repetition{i}", "") + "]";
                    Debug.WriteLine(herhalingen);
                }
                else
                {
                    herhalingen += Preferences.Get($"Repetition{i}", "") + ", ";
                }
            }
            oefening["Herhalingen"] = herhalingen;
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
            if (Preferences.Get("API", "") == "FitBit")
            {
                oefening["Kcal"] = "5";
                oefening["MaxHeart"] = "5";
                oefening["AverageHeart"] = "5";
            }
            else
            {
                oefening["Kcal"] = 0;
                oefening["MaxHeart"] = 0;
                oefening["AverageHeart"] = 0;
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