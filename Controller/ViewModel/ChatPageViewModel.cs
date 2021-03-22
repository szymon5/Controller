using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Input;
using Controller.Helpers;
using Xamarin.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Controller.ViewModel
{
    public class ChatPageViewModel
    {
        private byte[] imageBytes;

        public bool isPictureExpected = false;

        public EventHandler<ImageReceivedEventArgs> ImageReceivedEventArgs;

        public ICommand DisconnectCommand { get; private set; }
        public ICommand SendMessageCommand { get; private set; }

        public IPageService pageService;

        public ChatPageViewModel(IPageService pageService)
        {
            this.pageService = pageService;
            DisconnectCommand = new Command(Disconnect);
            SendMessageCommand = new Command<string>(async (msg) => await SendMessage(msg));
        }

        private void Disconnect()
        {
            if (GlobalSocket.client != null)
            {
                if (GlobalSocket.client.Connected)
                {
                    byte[] buff = Encoding.ASCII.GetBytes(Commands.Disconnect.DISCONNECT);
                    GlobalSocket.client.Send(buff);

                    GlobalSocket.client.Shutdown(SocketShutdown.Both);
                    GlobalSocket.client.Disconnect(true);


                    if (!GlobalSocket.client.Connected)
                    {
                        pageService.DisplayAlert("Disconnected", null, "ok");
                        pageService.PopAsync();
                    }
                }
            }
        }

        public async Task SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                await pageService.DisplayAlert("Error", "You cannot send empty message.", "Cancel");
                isPictureExpected = false;
            }
            else
            {
                byte[] buff = Encoding.ASCII.GetBytes(message);
                GlobalSocket.client.Send(buff);

                isPictureExpected = false;
                
                if (message == Commands.Camera.SEND_LAST_PICTURE)
                {
                    byte[] recBuffer = new byte[64000];
                    int cnt = GlobalSocket.client.Receive(recBuffer);
                    string data = Encoding.ASCII.GetString(recBuffer, 0, cnt);
                    
                    Parse(data);

                    isPictureExpected = true;
                }
            }
        }

        public void SetImage(Image image)
        {
            image.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }

        public void Parse(string recData)
        {
            string data = recData.Trim(new char[] { '[', ']', ' ' });
            string trimData = "";
            int dotCnt = 1;
            for (int i = 0; i < data.Length; i++)
            {
                if (recData[i] == ',') dotCnt++;
            }

            string[] strBytes = new string[dotCnt];
            int index = 0;

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] != ' ') trimData += data[i];
            }

            for (int i = 0; i < trimData.Length; i++)
            {
                if (trimData[i] == ',') index++;
                else strBytes[index] += trimData[i];
            }

            imageBytes = new byte[dotCnt];

            for (int i = 0; i < strBytes.Length; i++)
            {
                imageBytes[i] = Convert.ToByte(strBytes[i]);
            }
        }

        private void OnRaiseImageReceivedEvent(ImageReceivedEventArgs imageReceivedEventArgs)
        {
            ImageReceivedEventArgs?.Invoke(this, imageReceivedEventArgs);
        }
    }
}
