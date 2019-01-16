using FormsControls.Base;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
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
        List<Oefening> Finallijst = new List<Oefening>();
        List<Oefening> Oefeningslijst = new List<Oefening>();

        public ExercisePage (PickerClass picker, string moeilijkheidsgraad)
		{
			InitializeComponent ();
            BackButtonImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            _SelectedItem = picker;
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            Titlelabel.Text = picker.Name;
            //Inlezen JSON
            

            //bestandnaam? , Pad?
            // opgelet bovenaan -> using System.Reflection; toevoegen
            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2_Bert.Asset.oefeningenV2.json");

            //bytes uit het bestand gaan inlezen en verwerken
            StreamReader oSR = new StreamReader(stream);

            string json = oSR.ReadToEnd();
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
                    break;
                case "gemiddeld":
                    Moeilijkheidsgraadlabel.Text = "Gemiddeld";
                    foreach (Oefening oefening in Oefeningslijst)
                    {

                        if (oefening.Moeilijkheidsgraad.Contains("Medium"))
                        {
                            Semifinallijst.Add(oefening);
                        }
                    }
                    break;
                case "moeilijk":
                    Moeilijkheidsgraadlabel.Text = "Moeilijk";
                    foreach (Oefening oefening in Oefeningslijst)
                    {
                        if (oefening.Moeilijkheidsgraad.Contains("Hard"))
                        {
                            Semifinallijst.Add(oefening);
                        }
                    }
                    break;
                default:
                    Semifinallijst = Oefeningslijst;
                        break;
            }

            
            if (picker.Type == "Spiergroep")
            {
                foreach (Oefening oefening in Semifinallijst)
                {
                    if (oefening.Spiergroep == picker.Name)
                    {
                        Finallijst.Add(oefening);
                    }
                }
            }
            else if (picker.Type == "Toestel")
            {
                foreach (Oefening oefening in Semifinallijst)
                {
                    if (oefening.Toestel == picker.Name)
                    {
                        Finallijst.Add(oefening);
                    }
                }
            }


            //Listview opvullen
            Oefeningen.ItemsSource = Finallijst;

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

            SelecteerDificultyAgain.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    Popup.IsEnabled = true;
                    Popup.IsVisible = true;
                    Popup.FadeTo(1, 250);
                })
            });
        }


        private async void Makkelijk_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
            await Navigation.PushAsync(new ExercisePage(_SelectedItem, "makkelijk"), true);
            Popup.IsVisible = false;
        }

        private async void Gemiddeld_Clicked(object sender, EventArgs e)
        {
            Popup.IsEnabled = false;
            // Popup.FadeTo(0, 250);

            await Navigation.PushAsync(new ExercisePage(_SelectedItem, "gemiddeld"), true);
            Popup.IsVisible = false;


        }

        private async void Moeilijk_Clicked(object sender, EventArgs e)
        {
            // Popup.FadeTo(0, 250);

            Popup.IsEnabled = false;
            await Navigation.PushAsync(new ExercisePage(_SelectedItem, "moeilijk"));
            Popup.IsVisible = false;

        }

        private void OefeningNaamEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<Oefening> myOefeningList = new List<Oefening>();
            if (OefeningNaamEntry.Text != null)
            {
                foreach (Oefening oefening in Finallijst)
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
                Oefeningen.ItemsSource = Finallijst;
            }
        }
        //private async void ontap(Xamarin.Forms.View arg1, object arg2)
        //{

        //}
    }
}