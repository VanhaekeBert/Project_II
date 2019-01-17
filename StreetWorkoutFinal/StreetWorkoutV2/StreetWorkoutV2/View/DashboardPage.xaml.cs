using FormsControls.Base;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : AnimationPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundDashboard_2x.png");
            ImgCal.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Vuur.png");
            ImgWater.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Beker.png");
            ImgStartWorkout.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.StartWorkout.png");
            ImgStartWorkout_Cover.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.StartWorkout_Cover.png");
            imgQr.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.qrcode.png");
            imgSpier.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.spier.png");
            imgToestel.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.toestel.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Heart.png");
            lblwelkom.Text = "Welkom " + Application.Current.Properties["Naam"].ToString();
            Task.Run(async () =>
            {
                JObject data = await DBManager.GetUserData(Application.Current.Properties["Naam"].ToString(), "Naam");
                lblWaterTotaal.Text = " / " + data["WaterDoel"].ToString() + " ";
            });

            imgQr.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                    await Navigation.PushAsync(new QrPage());
                })
            });
            imgSpier.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                await Navigation.PushAsync(new Picker_Toestel_Page("Spiergroep"));  
                })
            });
            imgToestel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () => {
                await Navigation.PushAsync(new Picker_Toestel_Page("Toestel"));  
                })
            });
        }

         



        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    PopupNavigation.Instance.PushAsync(new PopupView2());
        //}
    }
}