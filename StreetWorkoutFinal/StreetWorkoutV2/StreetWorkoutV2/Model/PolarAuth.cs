using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
{
    public class PolarAuth
    {
        public static string Grant_type { get; set; } = "authorization_code";
        public static string Code { get; set; }
        public static string Redirect_uri { get; set; } = new Uri("com.nmct.SICWorkout:/oauth2redirect").ToString();
    }
}
