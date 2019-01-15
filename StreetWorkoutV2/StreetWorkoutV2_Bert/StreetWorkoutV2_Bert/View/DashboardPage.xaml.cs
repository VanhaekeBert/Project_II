using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.BackgroundDashboard_2x.png");
            ImgCal.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Vuur.png");
            ImgWater.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Beker.png");
            ImgStartWorkout.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.StartWorkout.png");
            ImgStartWorkout_Cover.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.StartWorkout_Cover.png");
            imgQr.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.qrcode.png");
            imgSpier.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.spier.png");
            imgToestel.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.toestel.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            lblwelkom.Text = "Welkom " + Application.Current.Properties["Naam"].ToString();

        }

        private async Task QrTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QrPage());
        }
        private async Task SpierTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QrPage());
        }
        private async Task ToestelTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Picker_Toestel_Page());
        }



        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PopupView2());
        }
    }
}