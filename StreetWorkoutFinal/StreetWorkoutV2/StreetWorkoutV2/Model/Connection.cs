﻿using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace StreetWorkoutV2.Model
{
    //---------------------------------------------------------------------------------------//
    //---------------------------Controleren op internetconnectie----------------------------//
    //---------------------------------------------------------------------------------------//

    public class Connection
    {
        //---aanmaken van classe---//
        public static bool CheckConnection()
        {
            bool connection = CrossConnectivity.Current.IsConnected;
            return connection;
        }
    }
}
