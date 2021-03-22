using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.Helpers
{
    public interface IPicture
    {
        void SavePictureToDisk(string filename, byte[] imageData);
    }
}
