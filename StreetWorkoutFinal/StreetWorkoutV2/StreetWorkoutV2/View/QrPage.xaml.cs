using FormsControls.Base;
using Newtonsoft.Json;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QrPage : AnimationPage
    {
        PickerClass _SelectedItem = new PickerClass();
        List<Oefening> _Oefeningslijst = new List<Oefening>();
        public QrPage()
        {
            InitializeComponent();
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            lblTitle.Text = "QR SCANNER";
            GrabJson();
        }

        public void GrabJson()
        {
            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2.Asset.oefeningenV2.json");
            StreamReader oSR = new StreamReader(stream);
            string json = oSR.ReadToEnd();
            _Oefeningslijst = JsonConvert.DeserializeObject<List<Oefening>>(json);
        }

        public void GetDevice(string ScannedDevice)
        {

            List<Oefening> PassList = new List<Oefening>();
            foreach (Oefening oefening in _Oefeningslijst)
            {
                if (oefening.Toestel == ScannedDevice)
                {
                    PassList.Add(oefening);
                }
            }
             Navigation.PushAsync(new ExerciseListPage(PassList));
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                var options = new MobileBarcodeScanningOptions
                {
                    AutoRotate = false,
                    UseFrontCameraIfAvailable = false,
                    TryHarder = true
                };

                var overlay = new ZXingDefaultOverlay
                {
                    TopText = "Scan de QR code op een toestel",
                    BottomText = "Zorg dat de QR code zich in het kader bevindt"
                };

                var QRScanner = new ZXingScannerPage(options, overlay);

                await Navigation.PushModalAsync(QRScanner);

                QRScanner.OnScanResult += (result) =>
                {
                    // Stop scanning
                    QRScanner.IsScanning = false;

                    // Pop the page and show the result
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Navigation.PopModalAsync(true);                      
                        GetDevice(result.Text);                     
                       
                    });
                };

            }
            catch (Exception ex)
            {
            }

        }
    }
}