using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.Helpers
{
    public class JPGPicture
    {
        public static List<string> ImagesString = new List<string>();

        public List<byte[]> ByteArrayListOfImages = new List<byte[]>();
        public void Parse(string recData)
        {
            string data = recData;
            int dotCnt = 1;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '.') dotCnt++;
            }

            string[] strBytes = new string[dotCnt];
            int index = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == '.') index++;
                else strBytes[index] += data[i];
            }

            byte[] imageBytes = new byte[dotCnt];

            for (int i = 0; i < strBytes.Length; i++)
            {
                imageBytes[i] = Convert.ToByte(strBytes[i]);
            }

            ByteArrayListOfImages.Add(imageBytes);
        }
    }
}
