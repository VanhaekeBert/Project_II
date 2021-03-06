﻿using FormsControls.Base;
using Newtonsoft.Json;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
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

            btnBack.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () =>
                {

                    await btnBack.FadeTo(0.3, 150);
                    await btnBack.FadeTo(1, 150);
                    await Navigation.PopAsync();
                })
            });

        }


        //---------------------------------------------------------------------------------------//
        //-------------------------------Ophalen van oefeningendata------------------------------//
        //---------------------------------------------------------------------------------------//

        public void GrabJson()
        {
            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2.Asset.oefeningenV2.json");
            StreamReader oSR = new StreamReader(stream);
            string json = oSR.ReadToEnd();
            _Oefeningslijst = JsonConvert.DeserializeObject<List<Oefening>>(json);
            CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Camera);
        }

        //---------------------------------------------------------------------------------------//
        //-------------------Ophalen van Toestel waarvan naam ingescanned is---------------------//
        //---------------------------------------------------------------------------------------//

        public void GetDevice(string ScannedDevice)
        {

            List<Oefening> PassList = new List<Oefening>();
            foreach (Oefening oefening in _Oefeningslijst)
            {
                if (oefening.Device == ScannedDevice)
                {
                    PassList.Add(oefening);
                }
            }
            Navigation.PushAsync(new ExerciseListPage(PassList));
        }


        //---------------------------------------------------------------------------------------//
        //---------------------------------Openen van QR Scanner---------------------------------//
        //---------------------------------------------------------------------------------------//
        private async void BtnScanner_Clicked(object sender, EventArgs e)
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
                bool isAlerted = false;
                PermissionStatus status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Eerste keer?", "Omdat het de eerste keer is dat je de qr-scanner wilt gebruiken zul je de app moeten herstarten", "Ok");

                }
                else
                {
                    QRScanner.OnScanResult += (result) =>
                    {
                        QRScanner.IsScanning = false;

                        //---Doorgeven van gescanned toestel---//
                        Device.BeginInvokeOnMainThread(() =>
                        {

                            List<string> devices = new List<string>();
                            foreach (var oefening in _Oefeningslijst)
                            {
                                devices.Add(oefening.Device);
                            }

                            if (devices.Contains(result.Text))
                            {
                                Navigation.PopModalAsync(true);
                                GetDevice(result.Text);
                            }
                            else if (isAlerted == false)
                            {
                                DisplayAlert("Geen toestel gevonden", "De gescande QR-code was ongeldig.", "Ok");
                                isAlerted = true;
                            }

                        });
                    };
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}