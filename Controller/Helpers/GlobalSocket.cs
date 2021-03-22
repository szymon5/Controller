using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Controller.Helpers
{
    public class GlobalSocket
    {
        public static Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        public static IPAddress DroneIP;
        public static int DronePort;

        public static void SendCommand(string cmd)
        {
            if (GlobalSocket.client.Connected)
            {
                byte[] buff = Encoding.ASCII.GetBytes(cmd);

                GlobalSocket.client.Send(buff);
            }
        }

        public static string ReceiveData()
        {
            byte[] recBuffer = new byte[64000];

            int cnt = GlobalSocket.client.Receive(recBuffer);
            string data = Encoding.ASCII.GetString(recBuffer, 0, cnt);

            Array.Clear(recBuffer, 0, recBuffer.Length);

            return data;
        }

        public static string ReceivePicture()
        {
            byte[] recBuffer = new byte[64000];
            int cnt = 0;
            string check, data = "";

            while (true)
            {
                cnt = client.Receive(recBuffer);
                check = Encoding.ASCII.GetString(recBuffer, 0, cnt);

                Array.Clear(recBuffer, 0, cnt);

                data += check;

                if (data.Contains("stop"))
                {
                    data = data.Replace("stop", "");
                    break;
                }
            }

            return data;
        }
    }
}
