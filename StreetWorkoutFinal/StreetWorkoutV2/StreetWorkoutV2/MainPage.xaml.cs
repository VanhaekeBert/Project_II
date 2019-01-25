using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StreetWorkoutV2.Model;
using StreetWorkoutV2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace StreetWorkoutV2
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            if (Preferences.Get("From", false))
            {
                if (Connection.CheckConnection())
                {
                    DataLoader();
                    Preferences.Set("Connection", true);
                }
                else
                {
                    Preferences.Set("Connection", false);
                }
                Preferences.Set("From", false);
            }
            var navAcccount = new AnimationNavigationPage(new AccountPage());
            navAcccount.Icon = "account.png";
            navAcccount.Title = "Account";

            var navDashboard = new AnimationNavigationPage(new DashboardPage());
            navDashboard.Icon = "dashboard.png";
            navDashboard.Title = "Dashboard";

            Children.Add(new SettingsPage());
            Children.Add(navDashboard);
            Children.Add(navAcccount);
            CurrentPage = Children[1];



        }

        public async Task DataLoader()
        {
            JObject user = await DBManager.GetUserData(Preferences.Get("Naam", ""), "Naam");
            JArray exercises = await DBManager.GetOefeningenData(Preferences.Get("Naam", ""));
            JArray water = await DBManager.GetWater(Preferences.Get("Naam", ""));
            var latestWater = await DBManager.GetLatestWater(Preferences.Get("Naam", ""));
            if (latestWater != null)
            {
                DateTime date = (DateTime)latestWater["Datum"];
                if (date.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy"))
                {
                    Preferences.Set("WaterDoel", int.Parse(latestWater["WaterDoel"].ToString()));
                    Preferences.Set("WaterGedronken", int.Parse(latestWater["WaterGedronken"].ToString()));
                }
                else
                {
                    DBManager.PostWater(Preferences.Get("Naam", ""), int.Parse(latestWater["WaterDoel"].ToString()), 0);
                    Preferences.Set("WaterDoel", int.Parse(latestWater["WaterDoel"].ToString()));
                    Preferences.Set("WaterGedronken", 0);
                }
            }
            else
            {
                DBManager.PostWater(Preferences.Get("Naam", ""), 0, 0);
                Preferences.Set("WaterDoel", 0);
                Preferences.Set("WaterGedronken", 0);
            }
            var waterTojson = JsonConvert.SerializeObject(water);
            var jsonToSaveValue = JsonConvert.SerializeObject(exercises);
            Preferences.Set("Naam", user["Naam"].ToString());
            Preferences.Set("ApiNaam", user["ApiNaam"].ToString());
            Preferences.Set("Email", user["Email"].ToString());
            Preferences.Set("Leeftijd", user["Leeftijd"].ToString());
            Preferences.Set("Lengte", user["Lengte"].ToString());
            Preferences.Set("Gewicht", user["Gewicht"].ToString());
            Preferences.Set("API", user["API"].ToString());
            Preferences.Set("Oefeningen", jsonToSaveValue.ToString());
            Preferences.Set("Water", waterTojson.ToString());
        }

    }
}
