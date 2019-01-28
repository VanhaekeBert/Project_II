using FormsControls.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rg.Plugins.Popup.Extensions;
using StreetWorkoutV2.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : AnimationPage
    {
        public LoginPage()
        {
            InitializeComponent();
            //---------------------------------------------------------------------------------------//
            //----------------------------------Diverse Assignments----------------------------------//
            //---------------------------------------------------------------------------------------//
            imgBackground.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.BackgroundLogin.png");
            eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.EyeOff.png");
            imgBtnBack.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.backbutton.png");
            entryPassword.IsPassword = true;


            //---------------------------------------------------------------------------------------//
            //---------------------------------Gesture Recognisers-----------------------------------//
            //---------------------------------------------------------------------------------------//
            Password_reset.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new ForgotPasswordPage());
                })
            });

            btnBack.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {

                    await Navigation.PushAsync(new RegisterPage());
                })
            });

            eyeimage.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    if (entryPassword.IsPassword == true)
                    {
                        entryPassword.IsPassword = false;
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.Eye.png");
                    }
                    else
                    {
                        entryPassword.IsPassword = true;
                        eyeimage.Source = FileImageSource.FromResource("StreetWorkoutV2.Asset.EyeOff.png");
                    }
                })
            });
        }
        //---------------------------------------------------------------------------------------//
        //----------------------------Uitschakelen van de backbutton-----------------------------//
        //---------------------------------------------------------------------------------------//
        protected override bool OnBackButtonPressed()
        {
            return true;
        }


        //---------------------------------------------------------------------------------------//
        //----------------------------Uitschakelen van de backbutton-----------------------------//
        //---------------------------------------------------------------------------------------//

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //---Kijken of er internetverbinding is---//
            if (Connection.CheckConnection())
            {
                LoadingIndicator.IsRunning = false;
                lblError.IsVisible = true;

                //---Kijken of de inputvelden niet leeg zijn---//
                if (entryPassword.Text != null && entryUserName.Text != null)
                {
                    LoadingIndicator.IsRunning = true;
                    bool Login = await DBManager.Login(entryUserName.Text.Replace(" ", ""), DBManager.Encrypt(entryPassword.Text));

                    //---Kijken of de login gegevens correct zijn---//
                    if (Login)
                    {
                        //---Ophalen van alle userdata om op de dashboard dan te tonen---//
                        JObject user = await DBManager.GetUserData(entryUserName.Text.Replace(" ", ""), "Name");
                        JArray exercises = await DBManager.GetExerciseData(entryUserName.Text.Replace(" ", ""));
                        JArray water = await DBManager.GetWaterData(entryUserName.Text.Replace(" ", ""));
                        var latestWater = await DBManager.GetLatestWaterData(entryUserName.Text.Replace(" ", ""));
                        if (latestWater != null)
                        {
                            DateTime date = (DateTime)latestWater["date"];
                            if (date.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy"))
                            {
                                Preferences.Set("WaterGoal", int.Parse(latestWater["waterGoal"].ToString()));
                                Preferences.Set("WaterDrunk", int.Parse(latestWater["waterDrunk"].ToString()));
                            }
                            else
                            {
                                await DBManager.PostWaterData(entryUserName.Text.Replace(" ", ""), int.Parse(latestWater["waterGoal"].ToString()), 0);
                                water = await DBManager.GetWaterData(entryUserName.Text.Replace(" ", ""));
                                Preferences.Set("WaterGoal", int.Parse(latestWater["waterGoal"].ToString()));
                                Preferences.Set("WaterDrunk", 0);
                            }
                        }
                        else
                        {
                            DBManager.PostWaterData(entryUserName.Text.Replace(" ", ""), 0, 0);
                            Preferences.Set("WaterGoal", 0);
                            Preferences.Set("WaterDrunk", 0);
                        }
                        var waterTojson = JsonConvert.SerializeObject(water);
                        var exerciseTojson = JsonConvert.SerializeObject(exercises);
                        Preferences.Set("Name", user["name"].ToString());
                        Preferences.Set("ApiName", user["apiName"].ToString());
                        Preferences.Set("Email", user["email"].ToString());
                        Preferences.Set("Age", user["age"].ToString());
                        Preferences.Set("Length", user["length"].ToString());
                        Preferences.Set("Weight", user["weight"].ToString());
                        Preferences.Set("Exercises", exerciseTojson.ToString());
                        Preferences.Set("Water", waterTojson.ToString());
                        await Navigation.PushModalAsync(new NavigationPage(new MainPage()));
                    }
                    else
                    {
                        lblError.Text = "Onjuiste ingave.";
                        lblError.IsVisible = true;
                        LoadingIndicator.IsRunning = false;
                    }
                }
                else
                {
                    lblError.Text = "Vul alle gegevens in.";
                    lblError.IsVisible = true;
                    LoadingIndicator.IsRunning = false;
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