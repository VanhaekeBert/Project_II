﻿using FormsControls.Base;
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

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ForgotPasswordPage : AnimationPage
    {
        public ForgotPasswordPage()
        {
            InitializeComponent();


            //---------------------------------------------------------------------------------------//
            //---------------------------------Diverse Assignments-----------------------------------//
            //---------------------------------------------------------------------------------------//

            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundForgotPassword.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");


            //---------------------------------------------------------------------------------------//
            //---------------------------------Gesture Recognizers----------------------------------//
            //---------------------------------------------------------------------------------------//

            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await btnBack.FadeTo(0.3, 150);
                    await btnBack.FadeTo(1, 150);
                    await Navigation.PushAsync(new LoginPage());
                })
            });
        }


        private async void Button_Clicked(object sender, EventArgs e)
        {
            //---Kijken of er internetverbinding is---//
            if (Connection.CheckConnection())
            {
                //---Kijken of inputveld leeg is---//
                if (entryEmail.Text != null)
                {
                    //---Kijken of er @ teken aanwezig is---//
                    if (entryEmail.Text.ToLower().Contains("@"))
                    {
                        //---Kijken of emailadres aanwezig is in DB---//
                        string email = entryEmail.Text.Replace(" ", "");
                        bool EmailCheck = await DBManager.CheckUserData(email, "Email");
                        if (EmailCheck == true)
                        {
                            JObject data = await DBManager.GetUserData(email, "Email");
                            string ww = DBManager.Encrypt(await DBManager.MailService(email, data["name"].ToString()));
                            if (ww != null)
                            {
                                JObject userData = new JObject();
                                userData["Password"] = ww;
                                await PopupNavigation.Instance.PushAsync(new PopUp_ForgotPassword());
                                await DBManager.PutUserData(email, "Email", userData);
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                lblError.Text = "Probleem bij verzenden. Probeer later opnieuw.";
                                lblError.IsVisible = true;
                            }
                        }
                        else
                        {
                            lblError.Text = "Account is nog niet geregistreerd.";
                            lblError.IsVisible = true;
                        }
                    }
                    else
                        lblError.Text = "Uw email is onjuist.";
                    lblError.IsVisible = true;
                }
                else
                {
                    lblError.Text = "Geliewe uw email in te voeren";
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
