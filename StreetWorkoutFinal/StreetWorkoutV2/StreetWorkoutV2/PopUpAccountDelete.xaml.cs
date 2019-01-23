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
		public PopUpAccountDelete ()
		{
			InitializeComponent ();
        }

        private async void Annuleren(object sender, EventArgs e)
        {
            await Navigation.PopAllPopupAsync();

        }

        private async void Verwijderen(object sender, EventArgs e)
        {
            await DBManager.DeleteProfilePicture(Preferences.Get("Naam", ""));
            await DBManager.DeleteUserData(Preferences.Get("Naam", ""));
            await DBManager.DeleteOefeningenData(Preferences.Get("Naam", ""));
            await DBManager.DeleteWater(Preferences.Get("Naam", ""));
            Preferences.Set("Naam", null);
            Preferences.Set("Email", null);
            Preferences.Set("Leeftijd", null);
            Preferences.Set("Lengte", null);
            Preferences.Set("Gewicht", null);
            Preferences.Set("API", null);
            Preferences.Set("WaterDoel", null);
            Preferences.Set("WaterGedronken", null);
            Preferences.Set("Oefeningen", null);
       
           Navigation.PushModalAsync(new LoginPage());
            await Navigation.PopPopupAsync();

        }
    }
}