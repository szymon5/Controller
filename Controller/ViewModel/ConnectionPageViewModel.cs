using System;
using System.Net;
using System.Windows.Input;
using Controller.Helpers;
using Xamarin.Forms;

namespace Controller.ViewModel
{
    public class ConnectionPageViewModel
    {
        public ICommand ConnectCommand { get; private set; }

        public IPageService pageService;

        public ConnectionPageViewModel(IPageService pageService)
        {
            this.pageService = pageService;
            ConnectCommand = new Command<string>(Connect);
        }

        public void Connect(string ip_port)
        {
            ParseIPAndPort(ip_port);

            try
            {
                GlobalSocket.client.Connect(GlobalSocket.DroneIP, GlobalSocket.DronePort);

                if (GlobalSocket.client.Connected) pageService.PushAsync(new FlightControll());
                else pageService.DisplayAlert("Error", "Something went wrong. Try again.", "Cancel");
            }
            catch (Exception ex)
            {
                pageService.DisplayAlert("Error", ex.Message, "Cancel");
            }
        }

        private void ParseIPAndPort(string ip_port)
        {
            string ip = "";
            string port = "";
            bool star = true;

            for (int i = 0; i < ip_port.Length; i++)
            {
                if (ip_port[i] == '*')
                {
                    star = false;
                    i++;
                }

                if (star) ip += ip_port[i];
                else port += ip_port[i];
            }

            try
            {
                if (!IPAddress.TryParse(ip, out GlobalSocket.DroneIP))
                {
                    pageService.DisplayAlert("Error", "Invalid IP Address.", "Cancel");
                    return;
                }

                if (!int.TryParse(port.Trim(), out GlobalSocket.DronePort))
                {
                    pageService.DisplayAlert("Error", "Invalid Port number.", "Cancel");
                    return;
                }
            }
            catch (Exception ex)
            {
                pageService.DisplayAlert("Error", ex.Message, "Cancel");
            }
        }
    }
}
