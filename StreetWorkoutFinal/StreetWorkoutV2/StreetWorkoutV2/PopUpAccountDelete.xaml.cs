using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using StreetWorkoutV2.Model;
using StreetWorkoutV2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopUpAccountDelete : PopupPage
    {
        public PopUpAccountDelete()
        {
            InitializeComponent();
        }

        private async void Cancel(object sender, EventArgs e)
        {
            await Navigation.PopAllPopupAsync();

        }

        private async void Delete(object sender, EventArgs e)
        {
            await DBManager.DeleteProfilePicture(Preferences.Get("Name", ""));
            await DBManager.DeleteUserData(Preferences.Get("Name", ""));
            await DBManager.DeleteExerciseData(Preferences.Get("Name", ""));
            await DBManager.DeleteWaterData(Preferences.Get("Name", ""));
            Preferences.Set("Name", null);
            Preferences.Set("Email", null);
            Preferences.Set("Age", null);
            Preferences.Set("Length", null);
            Preferences.Set("Weight", null);
            Preferences.Set("API", null);
            Preferences.Set("WaterGoal", null);
            Preferences.Set("WaterDrunk", null);
            Preferences.Set("Exercises", null);
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage()));
            await Navigation.PopPopupAsync();

        }
    }
}