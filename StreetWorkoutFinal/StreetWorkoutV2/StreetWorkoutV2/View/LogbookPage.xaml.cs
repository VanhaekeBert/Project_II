using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogbookPage : AnimationPage
    {
        List<OefeningDB> weekExerciseList = new List<OefeningDB>();
        List<string> data = new List<string>();
        List<List<Logboek>> logbookList = new List<List<Logboek>>();
        public LogbookPage()
        {
            InitializeComponent();
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            CultureInfo dutch = new CultureInfo("nl-BE");

            if (Preferences.Get("Oefeningen", "") != "[]")
            {
                var exercisesRaw = Preferences.Get("Oefeningen", "").ToString().Replace("[", "").Replace("]", "").Split('}');
                List<OefeningDB> exerciseList = new List<OefeningDB>();
                for (int i = 0; i < exercisesRaw.Count(); i++)
                {
                    if (i == 0)
                    {
                        exerciseList.Add(JsonConvert.DeserializeObject<OefeningDB>(exercisesRaw[i].ToString() + "}"));
                    }
                    else if (i != (exercisesRaw.Count() - 1))
                    {
                        exerciseList.Add(JsonConvert.DeserializeObject<OefeningDB>(exercisesRaw[i].ToString().Remove(0, 1) + "}"));
                    }
                }
                foreach (OefeningDB exercise in exerciseList)
                {
                    if (Enumerable.Range((int.Parse(DateTime.Now.ToString("dd")) - 6), (int.Parse(DateTime.Now.ToString("dd")) + 1)).Contains(exercise.Datum.Day))
                    {
                        weekExerciseList.Add(exercise);
                    }
                }
            }
            foreach (OefeningDB exercise in weekExerciseList)
            {
                if (logbookList.Count == 0)
                {
                    string dayName = exercise.Datum.ToString("dddd", dutch).First().ToString().ToUpper() + exercise.Datum.ToString("dddd", dutch).Substring(1);
                    data.Add(dayName + " " + exercise.Datum.ToString("MM-dd"));
                    List<Logboek> lijst = new List<Logboek>();
                    Logboek logbook = new Logboek();
                    logbook.Naam = exercise.Workout;

                    logbook.Date = dayName + " " + exercise.Datum.ToString("MM-dd");
                    logbook.Moeilijkheidsgraad = exercise.Moeilijkheidsgraad;
                    logbook.Total_hearts_given = int.Parse(exercise.Gevoel);
                    List<int> repetitions = new List<int>();
                    var repetitionsTemp = exercise.Herhalingen.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                    foreach (var item in repetitionsTemp)
                    {
                        repetitions.Add(int.Parse(item.Substring(0, item.Length - 1)));
                    }
                    logbook.ExerciseRepetitions = repetitions;
                    List<Color> colorList = new List<Color>();
                    foreach (var item in repetitionsTemp)
                    {
                        if (item.Substring(item.ToString().Length - 1, 1) == "G")
                        {
                            colorList.Add(Color.LightGreen);
                        }
                        else
                        {
                            colorList.Add(Color.Red);
                        }
                    }
                    logbook.Color = colorList;
                    lijst.Add(logbook);
                    logbookList.Add(lijst);
                }
                else
                {
                    for (int i = 0; i <= logbookList.Count; i++)
                    {
                        if (i != logbookList.Count)
                        {
                            string dayName = exercise.Datum.ToString("dddd", dutch).First().ToString().ToUpper() + exercise.Datum.ToString("dddd", dutch).Substring(1);

                            if (dayName + " " + exercise.Datum.ToString("MM-dd") == logbookList[i][0].Date)
                            {
                                Logboek logbook = new Logboek();
                                logbook.Naam = exercise.Workout;
                                logbook.Date = dayName + " " + exercise.Datum.ToString("MM-dd");

                                logbook.Moeilijkheidsgraad = exercise.Moeilijkheidsgraad;
                                logbook.Total_hearts_given = int.Parse(exercise.Gevoel);
                                List<int> repetitions = new List<int>();
                                var repetitionsTemp = exercise.Herhalingen.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                                foreach (var item in repetitionsTemp)
                                {
                                    repetitions.Add(int.Parse(item.Substring(0, item.Length - 1)));
                                }
                                logbook.ExerciseRepetitions = repetitions;
                                List<Color> colorList = new List<Color>();
                                foreach (var item in repetitionsTemp)
                                {
                                    if (item.Substring(item.ToString().Length - 1, 1) == "G")
                                    {
                                        colorList.Add(Color.LightGreen);
                                    }
                                    else
                                    {
                                        colorList.Add(Color.Red);
                                    }
                                }
                                logbook.Color = colorList;
                                logbookList[i].Add(logbook);
                                break;
                            }
                        }
                        else
                        {
                            string dayName = exercise.Datum.ToString("dddd", dutch).First().ToString().ToUpper() + exercise.Datum.ToString("dddd", dutch).Substring(1);

                            data.Add(dayName + " " + exercise.Datum.ToString("MM-dd"));
                            List<Logboek> list = new List<Logboek>();
                            Logboek logbook = new Logboek();
                            logbook.Naam = exercise.Workout;
                            logbook.Date = dayName + " " + exercise.Datum.ToString("MM-dd");
                            logbook.Moeilijkheidsgraad = exercise.Moeilijkheidsgraad;
                            logbook.Total_hearts_given = int.Parse(exercise.Gevoel);
                            List<int> repetitions = new List<int>();
                            var repetitionsTemp = exercise.Herhalingen.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                            foreach (var item in repetitionsTemp)
                            {
                                repetitions.Add(int.Parse(item.Substring(0, item.Length - 1)));
                            }
                            logbook.ExerciseRepetitions = repetitions;
                            List<Color> colorList = new List<Color>();
                            foreach (var item in repetitionsTemp)
                            {
                                if (item.Substring(item.ToString().Length - 1, 1) == "G")
                                {
                                    colorList.Add(Color.LightGreen);
                                }
                                else
                                {
                                    colorList.Add(Color.Red);
                                }
                            }
                            logbook.Color = colorList;
                            list.Add(logbook);
                            logbookList.Add(list);
                            break;
                        }
                    }
                }
            }

            for (int i = 1; i <= data.Count; i++)
            {
                if (i == 1)
                {
                    List<Logboek> lvwList = new List<Logboek>();
                    for (int j = logbookList[logbookList.Count - i].Count - 1; j >= 0; j--)
                    {
                        lvwList.Add(logbookList[logbookList.Count - i][j]);
                    }
                    lvwDay1.IsVisible = true;
                    lblDay1.Text = data[data.Count - i];
                    lvwDay1.ItemsSource = lvwList;
                    lvwDay1.HeightRequest = (115 * logbookList[logbookList.Count - i].Count) + 125;
                }
                else if (i == 2)
                {
                    List<Logboek> lvwList = new List<Logboek>();
                    for (int j = logbookList[logbookList.Count - i].Count - 1; j >= 0; j--)
                    {
                        lvwList.Add(logbookList[logbookList.Count - i][j]);
                    }
                    lvwDay2.IsVisible = true;
                    lblDay2.Text = data[data.Count - i];
                    lvwDay2.ItemsSource = lvwList;
                    lvwDay2.HeightRequest = (115 * logbookList[logbookList.Count - i].Count) + 125;
                }
                else if (i == 3)
                {
                    List<Logboek> lvwList = new List<Logboek>();
                    for (int j = logbookList[logbookList.Count - i].Count - 1; j >= 0; j--)
                    {
                        lvwList.Add(logbookList[logbookList.Count - i][j]);
                    }
                    lvwDay3.IsVisible = true;
                    lblDay3.Text = data[data.Count - i];
                    lvwDay3.ItemsSource = lvwList;
                    lvwDay3.HeightRequest = (115 * logbookList[logbookList.Count - i].Count) + 125;
                }
                else if (i == 4)
                {
                    List<Logboek> lvwList = new List<Logboek>();
                    for (int j = logbookList[logbookList.Count - i].Count - 1; j >= 0; j--)
                    {
                        lvwList.Add(logbookList[logbookList.Count - i][j]);
                    }
                    lvwDay4.IsVisible = true;
                    lblDay4.Text = data[data.Count - i];
                    lvwDay4.ItemsSource = lvwList;
                    lvwDay4.HeightRequest = (115 * logbookList[logbookList.Count - i].Count) + 125;
                }
                else if (i == 5)
                {
                    List<Logboek> lvwList = new List<Logboek>();
                    for (int j = logbookList[logbookList.Count - i].Count - 1; j >= 0; j--)
                    {
                        lvwList.Add(logbookList[logbookList.Count - i][j]);
                    }
                    lvwDay5.IsVisible = true;
                    lblDay5.Text = data[data.Count - i];
                    lvwDay5.ItemsSource = lvwList;
                    lvwDay5.HeightRequest = (115 * logbookList[logbookList.Count - i].Count) + 125;
                }
                else if (i == 6)
                {
                    List<Logboek> lvwList = new List<Logboek>();
                    for (int j = logbookList[logbookList.Count - i].Count - 1; j >= 0; j--)
                    {
                        lvwList.Add(logbookList[logbookList.Count - i][j]);
                    }
                    lvwDay6.IsVisible = true;
                    lblDay6.Text = data[data.Count - i];
                    lvwDay6.ItemsSource = lvwList;
                    lvwDay6.HeightRequest = (115 * logbookList[logbookList.Count - i].Count) + 125;
                }
                else
                {
                    List<Logboek> lvwList = new List<Logboek>();
                    for (int j = logbookList[logbookList.Count - i].Count - 1; j >= 0; j--)
                    {
                        lvwList.Add(logbookList[logbookList.Count - i][j]);
                    }
                    lvwDay7.IsVisible = true;
                    lblDay7.Text = data[data.Count - i];
                    lvwDay7.ItemsSource = lvwList;
                    lvwDay7.HeightRequest = (115 * logbookList[logbookList.Count - i].Count) + 125;
                }
            }
            btnBack.GestureRecognizers.Add(
           new TapGestureRecognizer()
           {
               Command = new Command(async () =>
               {
                   await btnBack.FadeTo(0.3, 150);
                   await Navigation.PopAsync();
                   await btnBack.FadeTo(1, 150);
               })
           });
        }

    }
}