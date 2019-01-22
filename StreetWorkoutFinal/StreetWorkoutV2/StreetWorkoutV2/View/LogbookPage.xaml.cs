using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogbookPage : AnimationPage
    {
        List<OefeningDB> weekOef = new List<OefeningDB>();
        List<string> data = new List<string>();
        List<List<Logboek>> lijstLogboek = new List<List<Logboek>>();
        public LogbookPage()
        {
            InitializeComponent();
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");

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
            foreach (OefeningDB oefening in weekOef)
            {
                if (lijstLogboek.Count == 0)
                {
                    data.Add(oefening.Datum.DayOfWeek + " " + oefening.Datum.ToString("MM-dd"));
                    List<Logboek> lijst = new List<Logboek>();
                    Logboek logboek = new Logboek();
                    logboek.Date = oefening.Datum.DayOfWeek + " " + oefening.Datum.ToString("MM-dd");
                    logboek.Naam = oefening.Workout;
                    logboek.Moeilijkheidsgraad = oefening.Moeilijkheidsgraad;
                    logboek.Total_hearts_given = int.Parse(oefening.Gevoel);
                    List<int> herhalingen = new List<int>();
                    var herhalingenTemp = oefening.Herhalingen.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                    foreach (var item in herhalingenTemp)
                    {
                        herhalingen.Add(int.Parse(item.Substring(0, item.Length - 1)));
                    }
                    logboek.ExerciseRepetitions = herhalingen;
                    List<Color> kleuren = new List<Color>();
                    foreach (var item in herhalingenTemp)
                    {
                        if (item.Substring(item.ToString().Length - 1, 1) == "G")
                        {
                            kleuren.Add(Color.LightGreen);
                        }
                        else
                        {
                            kleuren.Add(Color.Red);
                        }
                    }
                    logboek.Color = kleuren;
                    lijst.Add(logboek);
                    lijstLogboek.Add(lijst);
                }
                else
                {
                    //foreach (List<Logboek> logboeks in lijstLogboek)
                    for (int i = 0; i < lijstLogboek.Count; i++)
                    {
                        if (oefening.Datum.DayOfWeek + " " + oefening.Datum.ToString("MM-dd") == lijstLogboek[i][0].Date)
                        {
                            Logboek logboek = new Logboek();
                            logboek.Date = oefening.Datum.DayOfWeek + " " + oefening.Datum.ToString("MM-dd");
                            logboek.Naam = oefening.Workout;
                            logboek.Moeilijkheidsgraad = oefening.Moeilijkheidsgraad;
                            logboek.Total_hearts_given = int.Parse(oefening.Gevoel);
                            List<int> herhalingen = new List<int>();
                            var herhalingenTemp = oefening.Herhalingen.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                            foreach (var item in herhalingenTemp)
                            {
                                herhalingen.Add(int.Parse(item.Substring(0, item.Length - 1)));
                            }
                            logboek.ExerciseRepetitions = herhalingen;
                            List<Color> kleuren = new List<Color>();
                            foreach (var item in herhalingenTemp)
                            {
                                if (item.Substring(item.ToString().Length - 1, 1) == "G")
                                {
                                    kleuren.Add(Color.LightGreen);
                                }
                                else
                                {
                                    kleuren.Add(Color.Red);
                                }
                            }
                            logboek.Color = kleuren;
                            lijstLogboek[i].Add(logboek);
                        }
                        else
                        {
                            data.Add(oefening.Datum.DayOfWeek + " " + oefening.Datum.ToString("MM-dd"));
                            List<Logboek> lijst = new List<Logboek>();
                            Logboek logboek = new Logboek();
                            logboek.Date = oefening.Datum.DayOfWeek + " " + oefening.Datum.ToString("MM-dd");
                            logboek.Naam = oefening.Workout;
                            logboek.Moeilijkheidsgraad = oefening.Moeilijkheidsgraad;
                            logboek.Total_hearts_given = int.Parse(oefening.Gevoel);
                            List<int> herhalingen = new List<int>();
                            var herhalingenTemp = oefening.Herhalingen.Replace("[", "").Replace("]", "").Replace(" ", "").Split(',');
                            foreach (var item in herhalingenTemp)
                            {
                                herhalingen.Add(int.Parse(item.Substring(0, item.Length - 1)));
                            }
                            logboek.ExerciseRepetitions = herhalingen;
                            List<Color> kleuren = new List<Color>();
                            foreach (var item in herhalingenTemp)
                            {
                                if (item.Substring(item.ToString().Length - 1, 1) == "G")
                                {
                                    kleuren.Add(Color.LightGreen);
                                }
                                else
                                {
                                    kleuren.Add(Color.Red);
                                }
                            }
                            logboek.Color = kleuren;
                            lijst.Add(logboek);
                            lijstLogboek.Add(lijst);
                        }
                    }
                }
            }
            
            for (int i = 1; i <= data.Count; i++)
            {
                if (i == 1)
                {
                    lvwDag1.IsVisible = true;
                    lblDag1.Text = data[data.Count-1];
                    lvwDag1.ItemsSource = lijstLogboek[lijstLogboek.Count - 1];
                }
                else if(i == 2)
                {
                    lvwDag2.IsVisible = true;
                    lblDag1.Text = data[data.Count - 2];
                    lvwDag1.ItemsSource = lijstLogboek[lijstLogboek.Count - 2];
                }
                else if (i == 3)
                {
                    lvwDag3.IsVisible = true;
                    lblDag1.Text = data[data.Count - 3];
                    lvwDag1.ItemsSource = lijstLogboek[lijstLogboek.Count - 3];
                }
                else if (i == 4)
                {
                    lvwDag4.IsVisible = true;
                    lblDag1.Text = data[data.Count - 4];
                    lvwDag1.ItemsSource = lijstLogboek[lijstLogboek.Count - 4];
                }
                else if (i == 5)
                {
                    lvwDag5.IsVisible = true;
                    lblDag1.Text = data[data.Count - 5];
                    lvwDag1.ItemsSource = lijstLogboek[lijstLogboek.Count - 5];
                }
                else if (i == 6)
                {
                    lvwDag6.IsVisible = true;
                    lblDag1.Text = data[data.Count - 6];
                    lvwDag1.ItemsSource = lijstLogboek[lijstLogboek.Count - 6];
                }
                else
                {
                    lvwDag7.IsVisible = true;
                    lblDag1.Text = data[data.Count - 7];
                    lvwDag1.ItemsSource = lijstLogboek[lijstLogboek.Count - 7];
                }
            }
        }
    }
}