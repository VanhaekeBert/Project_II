﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using Xamarin.Forms.Xaml;

namespace StreetWorkoutV2_Bert.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Oefening_Complete_Page : ContentPage
	{
		public Oefening_Complete_Page ()
		{
			InitializeComponent ();
            BckgrImage.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Oefening_Complete_Background.png");
            backbutton.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Backbutton.png");
            Heart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart.png");
            ImgCal.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Vuur.png");
            ImgHeart.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Compleet.png");
            RatingHeart1.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");
            RatingHeart2.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");
            RatingHeart3.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");
            RatingHeart4.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");
            RatingHeart5.Source = FileImageSource.FromResource("StreetWorkoutV2_Bert.Asset.Heart_Colored.png");


            this.BackgroundColor = Color.FromHex("2B3049");
            MakeEntriesHartslag();
        }

        private void MakeEntriesHartslag()
        {
            List<string> listKleuren = new List<string> {
                "#FF4A4A","#F74848","#F74848","#F74848","#E64343","#E64343","#E64343","#E64343"
            };
            List<string> listLabels = new List<string> {
                "13:01","13:02","13:02","13:03","13:04","13:05","13:06","13:07"
            };
            List<string> listValues = new List<string> {
                "42","45","40","120","81","83","60","5"
            };

            List<Entry> entriesOef = new List<Entry> { };
            for (int i = 0; i < 8; i++)
            {
                float value = float.Parse(listValues[i]);

                entriesOef.Add(new Entry(value)
                {
                    Color = SKColor.Parse(listKleuren[i]),
                    Label = listLabels[i],
                    ValueLabel = listValues[i]
                });
            }
            chartHartslag.Chart = new LineChart()
            {
                Entries = entriesOef,
                BackgroundColor = SKColors.Transparent,
                PointSize = 22,
                LabelTextSize = 22,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal,
                LabelOrientation = Microcharts.Orientation.Horizontal,
                LabelColor = SKColor.Parse("#FFFFFF"),
            };
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}