using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StreetWorkoutV2_Bert;
using StreetWorkoutV2_Bert.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Java.Util.ResourceBundle;

[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameRenderer))]
namespace StreetWorkoutV2_Bert.Droid
{
    public class CustomFrameRenderer:FrameRenderer
    {
        public CustomFrameRenderer(Context context) : base(context)
        {
        }

        protected override bool DrawChild(Canvas canvas, Android.Views.View child, long drawingTime)
        {

            try
            {

                var radius = Element.CornerRadius;
                var borderThickness = 1f;
                float strokeWidth = 0f;
                
                if (borderThickness > 0)
                {
                    var logicalDensity = Forms.Context.Resources.DisplayMetrics.Density;
                    strokeWidth = (float)Math.Ceiling(borderThickness * logicalDensity + .5f);
                }

                radius -= strokeWidth / 2f;

                var path = new Path();

                var rect = new RectF(0, 0, Width, Height);
                float rx = Forms.Context.ToPixels(Element.CornerRadius);
                float ry = Forms.Context.ToPixels(Element.CornerRadius);
                path.AddRoundRect(rect, rx, ry, Path.Direction.Ccw);

                canvas.Save();
                canvas.ClipPath(path);

                //can add code for filling canvas - frame background here, gradient whatever

                //clip children
                var result = base.DrawChild(canvas, child, drawingTime);

                rect.Dispose();
                path.Dispose();
                canvas.Restore();

                //can add code to stroke frame border here, look as image circle plugin renderer for more info

                path.Dispose();
                return result;
            }
            catch 
            {
                
            }
            return base.DrawChild(canvas, child, drawingTime);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (Element == null)
            {
                return;
            }

            UpdateBackground();
            UpdateElevation();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (string.Equals(e.PropertyName, "BackgroundColor"))
            {
                UpdateBackground();
            }
            else if (string.Equals(e.PropertyName, "HasShadow"))
            {
                UpdateElevation();
            }
        }

        private void UpdateBackground()
        {
            int[] colors = { Element.BackgroundColor.ToAndroid(), Element.BackgroundColor.ToAndroid() };
            var gradientDrawable = new GradientDrawable(GradientDrawable.Orientation.LeftRight, colors);

            this.SetBackground(gradientDrawable);
        }

        private void UpdateElevation()
        {
            if (Build.VERSION.SdkInt >= (BuildVersionCodes)21)
                this.Elevation = Element.HasShadow ? 6 : 0;
        }
    }
}