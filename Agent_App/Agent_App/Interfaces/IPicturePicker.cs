using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Agent_App.Interfaces
{
    public interface IPicturePicker
    {
        Task<Stream> GetImageStreamAsync();
    }
}
