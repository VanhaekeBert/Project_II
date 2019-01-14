using Newtonsoft.Json;
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
    public partial class Picker_Toestel_Page : ContentPage
    {
        public Picker_Toestel_Page()
        {
            InitializeComponent();
            BackgroundImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Picker_Background.png");
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");

            //Inlezen JSON
            List<Oefening> Oefeningslijst = new List<Oefening>();

            //bestandnaam? , Pad?
            // opgelet bovenaan -> using System.Reflection; toevoegen
            var assembly = typeof(Oefening).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("StreetWorkoutV2_Bert.Asset.oefeningenV2.json");

            //bytes uit het bestand gaan inlezen en verwerken
            StreamReader oSR = new StreamReader(stream);

            string json = oSR.ReadToEnd();
            Oefeningslijst = JsonConvert.DeserializeObject<List<Oefening>>(json);

            List<string> Filteredlist = new List<string>();

            foreach(Oefening duts in Oefeningslijst)
            {
                Toestel toestel = new Toestel() { Name = duts.Toestel };
                if (Filteredlist.Contains(toestel.Name))
                {
                        
                }
                else
                {
                    Filteredlist.Add(toestel.Name);
                }
            }


            List<Toestel> toestels = new List<Toestel>();

            foreach(string toestel in Filteredlist)
            {
                Toestel toestelname = new Toestel() { Name = toestel };
                toestels.Add(toestelname);
            }

            //Listview opvullen
            Toestellen.ItemsSource =  toestels;

            this.BackgroundColor = Color.FromHex("2B3049");

            BackDashboard.GestureRecognizers.Add(
            new TapGestureRecognizer()
            {
                Command = new Command(async () => { await Navigation.PopAsync(); })
            });
        }

        private async Task Toestellen_ItemTapped(object sender, ItemTappedEventArgs e)
        {    
            await Navigation.PushAsync(new ExercisePage()); //pass content if you want to pass the clicked item object to another page
        }
    }
}