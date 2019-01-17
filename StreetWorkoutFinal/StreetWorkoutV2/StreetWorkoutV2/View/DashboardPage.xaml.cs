using FormsControls.Base;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Services;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : AnimationPage
    {

        string _result;
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
                    var scan = new ZXingScannerPage();
                    await Navigation.PushAsync(scan);
                    Debug.WriteLine("AwaitScanResult");

                    scan.OnScanResult += (result) =>
                    {
                        Debug.WriteLine("OnScanResult");
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Navigation.PopToRootAsync();
                            Debug.WriteLine("Start If statment");
                            //incline press
                            if (result.Text == "http://kaywa.me/s58ti")
                            {
                                Popup.IsEnabled = true;
                                Popup.IsVisible = true;
                                _result = "Incline Press";
                            }
                            //pushupbars
                            else if (result.Text == "http://kaywa.me/wdxX3")
                            {
                                Popup.IsEnabled = true;
                                Popup.IsVisible = true;
                                _result = "Push Up Bars";
                            }
                            //overheadladder
                            else if (result.Text == "http://kaywa.me/Y2UJp")
                            {
                                Popup.IsEnabled = true;
                                Popup.IsVisible = true;
                                _result = "Overhead Ladder";
                            }
                            //parrallel bars
                            else if (result.Text == "http://kaywa.me/sle3O")
                            {
                                Popup.IsEnabled = true;
                                Popup.IsVisible = true;
                                _result = "Parallel Bars";
                            }
                            //decline bench
                            else if (result.Text == "http://kaywa.me/3nCNB")
                            {
                                Popup.IsEnabled = true;
                                Popup.IsVisible = true;
                                _result = "Decline Bench";
                            }
                            else
                            {
                                PopupNoQr.IsEnabled = true;
                                PopupNoQr.IsVisible = true;
                            }

                            Debug.WriteLine("End If statment");
                        });

                    };
                    Debug.WriteLine("VoorbijScanResult");
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


        private async void Makkelijk_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
            PickerClass _QrItem = new PickerClass() { Name = _result, Type = "Toestel" };
            await Navigation.PushAsync(new ExercisePage(_QrItem, "gemakkelijk"));
            Popup.IsVisible = false;
        }

        private async void Gemiddeld_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
            PickerClass _QrItem = new PickerClass() { Name = _result, Type = "Toestel" };
            await Navigation.PushAsync(new ExercisePage(_QrItem, "gemiddeld"));
            Popup.IsVisible = false;
        }

        private async void Moeilijk_Clicked(object sender, EventArgs e)
        {
            // Popup.FadeTo(0, 250);
            Popup.IsEnabled = false;
            PickerClass _QrItem = new PickerClass() { Name = _result, Type = "Toestel" };
            await Navigation.PushAsync(new ExercisePage(_QrItem, "moeilijk"));
            Popup.IsVisible = false;
        }


        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            PopupNoQr.IsEnabled = false;
            PopupNoQr.IsVisible = false;
            Navigation.PopToRootAsync();
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    PopupNavigation.Instance.PushAsync(new PopupView2());
        //}
    }
}