﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using StreetWorkoutV2;
using StreetWorkoutV2.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace StreetWorkoutV2.Droid
{
    public class CustomFrameRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public CustomFrameRenderer(Context context)
          : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var drawable = new GradientDrawable();
                UpdateBackgroundColor(drawable);
                UpdateCornerRadius(drawable);
                UpdateOutlineColor(drawable);
                UpdateShadow();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

          
        }

       

        private void UpdateCornerRadius(GradientDrawable drawable)
        {
            drawable.SetCornerRadius(Element.CornerRadius);
        }

        private void UpdateOutlineColor(GradientDrawable drawable)
        {
            drawable.SetStroke(1, Element.BorderColor.ToAndroid());
        }

        private void UpdateBackgroundColor(GradientDrawable drawable)
        {
            drawable.SetColor(Element.BackgroundColor.ToAndroid());
        }

        private void UpdateShadow()
        {
     
            if (Element.HasShadow)
            {
                Elevation = 10;
            }
            else
            {
                Elevation = 0;
            }
        }
    }
}