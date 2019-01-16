﻿using FormsControls.Base;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using StreetWorkoutV2_Bert.Model;
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

namespace StreetWorkoutV2_Bert.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExercisePage : AnimationPage
	{
        PickerClass _SelectedItem;
        List<Oefening> _FinalList;
        string _json;
        List<Oefening> Oefeningslijst = new List<Oefening>();

        public ExercisePage (PickerClass picker, string moeilijkheidsgraad)
		{
			InitializeComponent ();
            BackButtonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            _SelectedItem = picker;
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            Titlelabel.Text = picker.Name;
            
            string json = InlezenJson();
            
            List<Oefening> Semifinallijst = CreateSemiFinalLijst(json, moeilijkheidsgraad);
            List<Oefening> Finallijst = CreateFinalLijst(picker,Semifinallijst);
            Oefeningen.ItemsSource = Finallijst;


            _json = json;
            _FinalList = Finallijst;
            BackButton.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new  Command(async() => {
                   await Navigation.PopAsync();
                })
            });


            Oefeningen.ItemTapped += async (o, e) =>
            {
                var myList = (ListView)o;
                var myAction = (myList.SelectedItem as Oefening);
                await Navigation.PushAsync(new OefeningPage(myAction, "1/3"));
                //await popupView.PushAsync(new ExercisePage());
                myList.SelectedItem = null;
            };

            ChangeDifficulty.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Popup.IsEnabled = true;
                    Popup.IsVisible = true;
                })
            });
        }

        private List<Oefening> CreateFinalLijst(PickerClass picker, List<Oefening> semifinallijst)
        {
            List<Oefening> Finallijst = new List<Oefening>();
            if (picker.Type == "Spiergroep")
            {
                foreach (Oefening oefening in semifinallijst)
                {
                    if (oefening.Spiergroep == picker.Name)
                    {
                        Finallijst.Add(oefening);
                    }
                }
                return Finallijst;
            }
            else if (picker.Type == "Toestel")
            {
                foreach (Oefening oefening in semifinallijst)
                {
                    if (oefening.Toestel == picker.Name)
                    {
                        Finallijst.Add(oefening);
                    }
                }
                return Finallijst;
            }
            else
            {
                return Finallijst = semifinallijst;
            }
        }

        private List<Oefening> CreateSemiFinalLijst(string json, string moeilijkheidsgraad)
        {

            Oefeningslijst = JsonConvert.DeserializeObject<List<Oefening>>(json);
            List<Oefening> Semifinallijst = new List<Oefening>();
            switch (moeilijkheidsgraad)
            {
                case "gemakkelijk":
                    Moeilijkheidsgraadlabel.Text = "Gemakkelijk";
                    foreach (Oefening oefening in Oefeningslijst)
                    {
                        if (oefening.Moeilijkheidsgraad.Contains("Easy"))
                        {
                            Semifinallijst.Add(oefening);
                        }
                    }
                    return Semifinallijst;
                case "gemiddeld":
                    Moeilijkheidsgraadlabel.Text = "Gemiddeld";
                    foreach (Oefening oefening in Oefeningslijst)
                    {

                        if (oefening.Moeilijkheidsgraad.Contains("Medium"))
                        {
                            Semifinallijst.Add(oefening);
                        }
                    }
                    return Semifinallijst;
                case "moeilijk":
                    Moeilijkheidsgraadlabel.Text = "Moeilijk";
                    foreach (Oefening oefening in Oefeningslijst)
                    {
                        if (oefening.Moeilijkheidsgraad.Contains("Hard"))
                        {
                            Semifinallijst.Add(oefening);
                        }
                    }
                    return Semifinallijst;
                    
                default:
                    Semifinallijst = Oefeningslijst;
                    return Semifinallijst;

            }
        }

        private string InlezenJson()
        {
            //Inlezen JSON


            //bestandnaam? , Pad?
            // opgelet bovenaan -> using System.Reflection; toevoegen
            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2_Bert.Asset.oefeningenV2.json");

            //bytes uit het bestand gaan inlezen en verwerken
            StreamReader oSR = new StreamReader(stream);

            string json = oSR.ReadToEnd();
            return json;
        }

        private void Makkelijk_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
            List<Oefening> list1 = CreateSemiFinalLijst(_json, "gemakkelijk");
            List<Oefening> list2 = CreateFinalLijst(_SelectedItem, list1);
            Oefeningen.ItemsSource = list2;
            //global final list updaten voor textChanged
            _FinalList = list2;
            OefeningNaamEntry.Text = "";
            Popup.IsVisible = false;
        }

        private void Gemiddeld_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
            List<Oefening> list1 = CreateSemiFinalLijst(_json, "gemiddeld");
            List<Oefening> list2 = CreateFinalLijst(_SelectedItem, list1);
            Oefeningen.ItemsSource = list2;
            //global final list updaten voor textChanged
            _FinalList = list2;
            OefeningNaamEntry.Text = "";
            Popup.IsVisible = false;
        }

        private void Moeilijk_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
            List<Oefening> list1 = CreateSemiFinalLijst(_json, "moeilijk");
            List<Oefening> list2 = CreateFinalLijst(_SelectedItem, list1);
            Oefeningen.ItemsSource = list2;
            //global final list updaten voor textChanged
            _FinalList = list2;
            OefeningNaamEntry.Text = "";
            Popup.IsVisible = false;

        }

        private void OefeningNaamEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Oefening> myOefeningList = new List<Oefening>();
            if (OefeningNaamEntry.Text != null)
            {
                foreach (Oefening oefening in _FinalList)
                {
                    if (oefening.Oefeningnaam.ToLower().Contains(OefeningNaamEntry.Text.ToLower()))
                    {
                        myOefeningList.Add(oefening);
                    }
                }
                Oefeningen.ItemsSource = myOefeningList;
            }
            else
            {
                Oefeningen.ItemsSource = _FinalList;
            }
        }
        //private async void ontap(Xamarin.Forms.View arg1, object arg2)
        //{

        //}
    }
}