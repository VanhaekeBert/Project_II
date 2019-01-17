using System;
using System.IO;
using System.Threading.Tasks;

namespace StreetWorkoutV2
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
