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
        List<ExerciseDB> weekExerciseList = new List<ExerciseDB>();
        List<string> data = new List<string>();
        List<List<Logbook>> logbookList = new List<List<Logbook>>();
        public LogbookPage()
        {

            //---------------------------------------------------------------------------------------//
            //---------------------------------Diverse Assignments----------------------------------//
            //---------------------------------------------------------------------------------------//
            InitializeComponent();
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            CultureInfo dutch = new CultureInfo("nl-BE");

            //---------------------------------------------------------------------------------------//
            //-----------------------------Alle exercises ophalen------------------------------------//
            //---------------------------------------------------------------------------------------//

            if (Preferences.Get("Exercises", "") != "[]")
            {
                var exercisesRaw = Preferences.Get("Exercises", "").ToString().Replace("[", "").Replace("]", "").Split('}');
                List<ExerciseDB> exerciseList = new List<ExerciseDB>();
                for (int i = 0; i < exercisesRaw.Count(); i++)
                {
                    if (i == 0)
                    {
                        exerciseList.Add(JsonConvert.DeserializeObject<ExerciseDB>(exercisesRaw[i].ToString() + "}"));
                    }
                    else if (i != (exercisesRaw.Count() - 1))
                    {
                        exerciseList.Add(JsonConvert.DeserializeObject<ExerciseDB>(exercisesRaw[i].ToString().Remove(0, 1) + "}"));
                    }
                }
                foreach (ExerciseDB exercise in exerciseList)
                {
                    if (Enumerable.Range((int.Parse(DateTime.Now.ToString("dd")) - 6), (int.Parse(DateTime.Now.ToString("dd")) + 1)).Contains(exercise.Date.Day))
                    {
                        weekExerciseList.Add(exercise);
                    }
                }
            }

            //---Overlopen van exercises om deze toe te voegen aan een list per dag, en dan alle daglists in de logboek list---//
            foreach (ExerciseDB exercise in weekExerciseList)
            {
                //---eerste oefening is automatisch de eerste oefening die wordt toegevoegd, dus een eerste dag lijst wordt aangemaakt met deze oefening---//
                if (logbookList.Count == 0)
                {
                    string dayName = exercise.Date.ToString("dddd", dutch).First().ToString().ToUpper() + exercise.Date.ToString("dddd", dutch).Substring(1);
                    data.Add(dayName + " " + exercise.Date.ToString("MM-dd"));
                    List<Logbook> list = new List<Logbook>();
                    Logbook logbook = new Logbook();
                    logbook.Name = exercise.Workout;

                    logbook.Date = dayName + " " + exercise.Date.ToString("MM-dd");
                    logbook.Difficulty = exercise.Difficulty;
                    logbook.Total_hearts_given = int.Parse(exercise.Feeling);
                    List<int> repetitions = new List<int>();
                    List<string> repetitionscolor = new List<string>();
                    var repetitionsTemp = exercise.Repetitions.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                    var repetitionscolorTemp = exercise.RepetitionsColor.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                    foreach (var item in repetitionsTemp)
                    {
                        repetitions.Add(int.Parse(item));
                    }
                    logbook.ExerciseRepetitions = repetitions;
                    List<Color> colorList = new List<Color>();
                    foreach (var item in repetitionscolorTemp)
                    {
                        if (item == "G")
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
                }
                //---indien de oefening niet de eerste oefening is, beland deze hier---//
                else
                {
                    //---hier worden alle dag lijsten in de logboek lijst overlopen---//
                    for (int i = 0; i <= logbookList.Count; i++)
                    {
                        //---als we de laatste daglijst voorbij zijn en de oefening is nog niet toegevoegd aan een daglijst, dan wordt in de else van deze if een nieuwe dag lijst teogevoegd met deze oefening---//
                        if (i != logbookList.Count)
                        {
                            string dayName = exercise.Date.ToString("dddd", dutch).First().ToString().ToUpper() + exercise.Date.ToString("dddd", dutch).Substring(1);

                            //---als de data overeenkomen, wordt de oefening aan deze lijst toegevoegd---//
                            if (dayName + " " + exercise.Date.ToString("MM-dd") == logbookList[i][0].Date)
                            {
                                Logbook logbook = new Logbook();
                                logbook.Name = exercise.Workout;
                                logbook.Date = dayName + " " + exercise.Date.ToString("MM-dd");

                                logbook.Difficulty = exercise.Difficulty;
                                logbook.Total_hearts_given = int.Parse(exercise.Feeling);
                                List<int> repetitions = new List<int>();
                                List<string> repetitionscolor = new List<string>();
                                var repetitionsTemp = exercise.Repetitions.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                                var repetitionscolorTemp = exercise.RepetitionsColor.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                                foreach (var item in repetitionsTemp)
                                {
                                    repetitions.Add(int.Parse(item));
                                }
                                logbook.ExerciseRepetitions = repetitions;
                                List<Color> colorList = new List<Color>();
                                foreach (var item in repetitionscolorTemp)
                                {
                                    if (item == "G")
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
                            string dayName = exercise.Date.ToString("dddd", dutch).First().ToString().ToUpper() + exercise.Date.ToString("dddd", dutch).Substring(1);

                            data.Add(dayName + " " + exercise.Date.ToString("MM-dd"));
                            List<Logbook> list = new List<Logbook>();
                            Logbook logbook = new Logbook();
                            logbook.Name = exercise.Workout;
                            logbook.Date = dayName + " " + exercise.Date.ToString("MM-dd");
                            logbook.Difficulty = exercise.Difficulty;
                            logbook.Total_hearts_given = int.Parse(exercise.Feeling);
                            List<int> repetitions = new List<int>();
                            List<string> repetitionscolor = new List<string>();
                            var repetitionsTemp = exercise.Repetitions.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                            var repetitionscolorTemp = exercise.RepetitionsColor.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                            foreach (var item in repetitionsTemp)
                            {
                                repetitions.Add(int.Parse(item));
                            }
                            logbook.ExerciseRepetitions = repetitions;
                            List<Color> colorList = new List<Color>();
                            foreach (var item in repetitionscolorTemp)
                            {
                                if (item == "G")
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
            //---------------------------------------------------------------------------------------//
            //-----------------------------Listviews opvullen aan de hand----------------------------//
            //--------------------------van een lijst die per dag opgevult is------------------------//
            //---------------------------------------------------------------------------------------//
            for (int i = 1; i <= data.Count; i++)
            {
                if (i == 1)
                {
                    List<Logbook> lvwList = new List<Logbook>();
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
                    List<Logbook> lvwList = new List<Logbook>();
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
                    List<Logbook> lvwList = new List<Logbook>();
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
                    List<Logbook> lvwList = new List<Logbook>();
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
                    List<Logbook> lvwList = new List<Logbook>();
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
                    List<Logbook> lvwList = new List<Logbook>();
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
                    List<Logbook> lvwList = new List<Logbook>();
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


            //---------------------------------------------------------------------------------------//
            //---------------------------------Backbutton tapgesture---------------------------------//
            //---------------------------------------------------------------------------------------//
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