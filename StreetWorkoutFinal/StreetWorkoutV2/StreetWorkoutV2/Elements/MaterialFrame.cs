using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StreetWorkoutV2
{
    public class MaterialFrame: Frame
    {
        public static BindableProperty ElevationProperty = BindableProperty.Create(nameof(Elevation), typeof(float), typeof(MaterialFrame), 4.0f);

        public float Elevation
        {
            get
            {
                return (float)GetValue(ElevationProperty);
            }
            set
            {
                SetValue(ElevationProperty, value);
            }
        }
    }
}
