using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StreetWorkoutV2.Model
{
    //---------------------------------------------------------------------------------------//
    //------------------------------ProfilePicture properties--------------------------------//
    //---------------------------------------------------------------------------------------//

    public class ProfilePicture
    {
        public string Name { get; set; }
        public byte[] stream { get; set; }
    }
}
