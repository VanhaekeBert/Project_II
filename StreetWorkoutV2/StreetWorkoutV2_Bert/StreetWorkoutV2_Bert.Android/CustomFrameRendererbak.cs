using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using StreetWorkoutV2_Bert;
using StreetWorkoutV2_Bert.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRendererbak))]
namespace StreetWorkoutV2_Bert.Droid
{
    public class CustomFrameRendererbak : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public CustomFrameRendererbak(Context context)
          : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement == null)
                return;

            UpdateElevation();
        }


        private void UpdateElevation()
        {
          

            // we need to reset the StateListAnimator to override the setting of Elevation on touch down and release.
            Control.StateListAnimator = new Android.Animation.StateListAnimator();

            // set the elevation manually
            ViewCompat.SetElevation(this, 20);
            ViewCompat.SetElevation(Control, 20);

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "Elevation")
            {
                UpdateElevation();
            }
        }

    }
}