using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;
using Entry = Microcharts.Entry;

namespace Mobile_Chart
{
    public partial class MainPage : ContentPage
    {
        List<Entry> entries = new List<Entry>
        {
            new Entry(42)
            {
                Color = SKColor.Parse("#EE9F44"),
                Label = "Vr",
                ValueLabel = "42"
            },
            new Entry(35)
            {
                Color = SKColor.Parse("#EE9944"),
                Label = "Za",
                ValueLabel = "35"
            },
            new Entry(40)
            {
                Color = SKColor.Parse("#EE9344"),
                Label = "Zo",
                ValueLabel = "40"
            },
            new Entry(120)
            {
                Color = SKColor.Parse("#EE8E44"),
                Label = "Ma",
                ValueLabel = "120"
            },
            new Entry(81)
            {
                Color = SKColor.Parse("#EE8844"),
                Label = "Di",
                ValueLabel = "81"
            },
            new Entry(83)
            {
                Color = SKColor.Parse("#EE8244"),
                Label = "Wo",
                ValueLabel = "83"
            },
            new Entry(60)
            {
                Color = SKColor.Parse("#EE7D44"),
                Label = "Do",
                ValueLabel = "60"
            },
            new Entry(5)
            {
                Color = SKColor.Parse("#EE7744"),
                Label = "Vr",
                ValueLabel = "5",
            },
        };
        public MainPage()
        {
            InitializeComponent();
            Chart1.Chart = new LineChart()
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                PointSize = 22,
                LabelTextSize = 22,
                ValueLabelOrientation = Microcharts.Orientation.Horizontal, 
                LabelOrientation = Microcharts.Orientation.Horizontal
            };
        }

    }
}
