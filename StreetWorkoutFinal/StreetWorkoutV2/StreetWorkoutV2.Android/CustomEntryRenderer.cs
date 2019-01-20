using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using StreetWorkoutV2;
using StreetWorkoutV2.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]

namespace StreetWorkoutV2.Droid
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context)
           : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                GradientDrawable gd = new GradientDrawable();
                gd.SetColor(global::Android.Graphics.Color.White);
                gd.SetCornerRadius(100);
                gd.SetStroke(2, global::Android.Graphics.Color.LightGray);
                Control.Elevation = 6.0f;
                Control.TranslationY = 3.0f;
                this.Control.SetBackground(gd);
                Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.Argb(99, 0, 0, 0)));
                Control.SetPadding(60, 50, 0, 0);

            }
        }
    }
}