using System;
namespace Controller.Helpers
{
    public class ImageReceivedEventArgs : EventArgs
    {
        public readonly string ImageName;
        public readonly byte[] ImageData;
        public readonly string message;


        public ImageReceivedEventArgs(string ImageName, byte[] ImageData,string message)
        {
            this.ImageName = ImageName;
            this.ImageData = ImageData;
            this.message = message;
        }
    }
}
