using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
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
    public partial class PopupView2 : PopupPage
    {
        PickerClass pickerchoice = new PickerClass();
        public PopupView2()
        {
            InitializeComponent();
        }

        private async void makkelijk_Clicked(object sender, EventArgs e)
        {
           // await Navigation.PushAsync(new ExercisePage("1/3"));
            await PopupNavigation.Instance.PopAsync();
        }

        private async void gemiddeld_Clicked(object sender, EventArgs e)
        {
            // await Navigation.PushAsync(new ExerciseListPage(pickerchoice, "gemiddeld"));
            await PopupNavigation.Instance.PopAsync();
        }

        private async void moeilijk_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new ExerciseListPage(pickerchoice, "moeilijk"));
            await PopupNavigation.Instance.PopAsync();
        }
    }
}