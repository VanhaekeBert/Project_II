using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
{
    public class Connection
    {
        public static bool CheckConnection()
        {
            bool connectie = CrossConnectivity.Current.IsConnected;
            return connectie;
        }
    }
}
