using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using StreetWorkoutV2_Bert.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace StreetWorkoutV2_Bert.iOS
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {

                Control.BorderStyle = UITextBorderStyle.None;
                Control.Layer.CornerRadius = 100;
                Control.TextColor = UIColor.Black;
                Control.BackgroundColor = UIColor.White;
                Control.LeftView = new UIView(new CGRect(50,40,0,0));
                Control.LeftViewMode = UITextFieldViewMode.Always;

            }
        }
    }
}