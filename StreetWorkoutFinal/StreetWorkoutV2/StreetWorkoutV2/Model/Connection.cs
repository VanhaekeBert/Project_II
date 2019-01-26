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
            bool connection = CrossConnectivity.Current.IsConnected;
            return connection;
        }
    }
}
