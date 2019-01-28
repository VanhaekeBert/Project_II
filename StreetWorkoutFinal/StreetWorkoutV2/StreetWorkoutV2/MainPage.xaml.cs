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
            JObject user = await DBManager.GetUserData(Preferences.Get("Name", ""), "Name");
            JArray exercises = await DBManager.GetExerciseData(Preferences.Get("Name", ""));
            JArray water = await DBManager.GetWaterData(Preferences.Get("Name", ""));
            var latestWater = await DBManager.GetLatestWaterData(Preferences.Get("Name", ""));
            if (latestWater != null)
            {
                DateTime date = (DateTime)latestWater["date"];
                if (date.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy"))
                {
                    Preferences.Set("WaterGoal", int.Parse(latestWater["waterGoal"].ToString()));
                    Preferences.Set("WaterDrunk", int.Parse(latestWater["waterDrunk"].ToString()));
                }
                else
                {
                    await DBManager.PostWaterData(Preferences.Get("Name", ""), int.Parse(latestWater["waterGoal"].ToString()), 0);
                    water = await DBManager.GetWaterData(Preferences.Get("Name", ""));
                    Preferences.Set("WaterGoal", int.Parse(latestWater["waterGoal"].ToString()));
                    Preferences.Set("WaterDrunk", 0);
                }
            }
            else
            {
                DBManager.PostWaterData(Preferences.Get("Name", ""), 0, 0);
                Preferences.Set("WaterGoal", 0);
                Preferences.Set("WaterDrunk", 0);
            }
            var waterTojson = JsonConvert.SerializeObject(water);
            var jsonToSaveValue = JsonConvert.SerializeObject(exercises);
            Preferences.Set("Name", user["name"].ToString());
            Preferences.Set("ApiName", user["apiName"].ToString());
            Preferences.Set("Email", user["email"].ToString());
            Preferences.Set("Age", user["age"].ToString());
            Preferences.Set("Length", user["length"].ToString());
            Preferences.Set("Weight", user["weight"].ToString());
            Preferences.Set("Exercises", jsonToSaveValue.ToString());
            Preferences.Set("Water", waterTojson.ToString());
        }
    }
}
