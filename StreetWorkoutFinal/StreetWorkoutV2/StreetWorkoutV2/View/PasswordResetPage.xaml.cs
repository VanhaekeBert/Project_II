﻿using FormsControls.Base;
using Newtonsoft.Json.Linq;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PasswordResetPage : AnimationPage
    {
        public PasswordResetPage()
        {
            InitializeComponent();
            //---------------------------------------------------------------------------------------//
            //----------------------------------Several Assignments----------------------------------//
            //---------------------------------------------------------------------------------------//
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundLogin.png");
            imgEyeOld.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.EyeOff.png");
            imgEyeNew.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.EyeOff.png");
            imgEyeNewRepeat.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.EyeOff.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            entryPasswordNew.IsPassword = true;
            entryPasswordNewRepeat.IsPassword = true;
            entryPasswordOld.IsPassword = true;


            //---------------------------------------------------------------------------------------//
            //---------------------------------Gesture Recognisers-----------------------------------//
            //---------------------------------------------------------------------------------------//
            imgEyeNew.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (entryPasswordNew.IsPassword == true)
                    {
                        entryPasswordNew.IsPassword = false;
                        imgEyeNew.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Eye.png");
                    }
                    else
                    {
                        entryPasswordNew.IsPassword = true;
                        imgEyeNew.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.EyeOff.png");
                    }
                })
            });

            imgEyeNewRepeat.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (entryPasswordNewRepeat.IsPassword == true)
                    {
                        entryPasswordNewRepeat.IsPassword = false;
                        imgEyeNewRepeat.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Eye.png");
                    }
                    else
                    {
                        entryPasswordNewRepeat.IsPassword = true;
                        imgEyeNewRepeat.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.EyeOff.png");
                    }
                })
            });

            imgEyeOld.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (entryPasswordOld.IsPassword == true)
                    {
                        entryPasswordOld.IsPassword = false;
                        imgEyeOld.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Eye.png");
                    }
                    else
                    {
                        entryPasswordOld.IsPassword = true;
                        imgEyeOld.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.EyeOff.png");
                    }
                })
            });

            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PopAsync();
                })
            });
        }

        //---------------------------------------------------------------------------------------//
        //---------------------------------Gesture Recognisers-----------------------------------//
        //---------------------------------------------------------------------------------------//
        private async void BtnConfirm_Clicked(object sender, EventArgs e)
        {
            //---Kijken of er internetverbinding is---//
            if (Connection.CheckConnection())
            {
                //---Kijken of de inputvelden niet leeg zijn---//
                if (entryPasswordOld.Text != null && entryPasswordNew.Text != null)
                {
                    //---Kijken of beide wachtwoorden gelijk zijn---//

                    if (entryPasswordNew.Text == entryPasswordNewRepeat.Text)
                    {
                        //---Kijken of het wachtwoord langer is dan 8 tekens---//

                        if (entryPasswordNew.Text.Length >= 8)
                        {
                            bool CheckOldWW = await DBManager.Login(Preferences.Get("Name", ""), DBManager.Encrypt(entryPasswordOld.Text));
                            //---Kijken of oude wachtwoord correct is---//

                            if (CheckOldWW)
                            {
                                //---Veranderen en opslaan van nieuwe wachtwoord---//
                                JObject data = await DBManager.GetUserData(Preferences.Get("Name", ""), "Name");
                                JObject dataTemp = new JObject();
                                dataTemp["Password"] = DBManager.Encrypt(entryPasswordNew.Text);
                                await DBManager.PutUserData(data["email"].ToString(), "Email", dataTemp);
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                lblError.Text = "Uw oude wachtwoord is niet correct";
                                lblError.IsVisible = true;
                            }
                        }
                        else
                        {
                            lblError.Text = "Uw nieuw wachtwoord moet minstens 8 tekens bevatten";
                            lblError.IsVisible = true;
                        }

                    }
                    else
                    {
                        lblError.Text = "Uw wachtwoorden komen niet overeen";
                        lblError.IsVisible = true;
                    }
                }
                else
                {
                    lblError.Text = "vult de shit aan";
                    lblError.IsVisible = true;
                }
            }
            else
            {
                lblError.Text = "Oeps, zorg voor een internetverbinding.";
                lblError.IsVisible = true;
            }
        }
    }
}