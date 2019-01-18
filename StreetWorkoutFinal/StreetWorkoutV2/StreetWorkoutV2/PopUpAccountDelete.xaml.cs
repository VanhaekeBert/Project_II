using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using StreetWorkoutV2.Model;
using StreetWorkoutV2.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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



            //FraAD.GestureRecognizers.Add(new TapGestureRecognizer
            //{
            //    Command = new Command(async () => {

            //        await DBManager.DeleteUserData(Application.Current.Properties["Naam"].ToString());
            //        Application.Current.Properties["Naam"] = null;
            //        await Application.Current.SavePropertiesAsync();
            //        await Navigation.PushAsync(new LoginPage());
            //    })
            //});
        }

        private async void Annuleren(object sender, EventArgs e)
        {
            await Navigation.PopAllPopupAsync();

        }

        private async void Verwijderen(object sender, EventArgs e)
        {
            await DBManager.DeleteUserData(Application.Current.Properties["Naam"].ToString());
            await DBManager.DeleteOefeningenData(Application.Current.Properties["Naam"].ToString());
            Application.Current.Properties["Naam"] = null;
            await Application.Current.SavePropertiesAsync();
            await Navigation.PushAsync(new LoginPage());


        }
    }
}