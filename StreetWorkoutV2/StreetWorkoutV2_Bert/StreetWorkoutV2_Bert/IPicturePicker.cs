using System;
using System.IO;
using System.Threading.Tasks;

namespace StreetWorkoutV2_Bert
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
