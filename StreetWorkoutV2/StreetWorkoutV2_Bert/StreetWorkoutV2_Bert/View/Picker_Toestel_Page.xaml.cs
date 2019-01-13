using StreetWorkoutV2_Bert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Toestel toestel1 = new Toestel();
            toestel1.Name = "Parallel Bars";
            toestel1.Aantal_Oefeningen = 7;
            toestel1.Image = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Parallel_Bars.png");
            toestel1.Go_To_button = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Go_To_Button.png");
            List<Toestel> Toestellijst = new List<Toestel>();
            Toestellijst.Add(toestel1);
            Toestellen.ItemsSource = Toestellijst;
            this.BackgroundColor = Color.FromHex("2B3049");


        }
    }
}