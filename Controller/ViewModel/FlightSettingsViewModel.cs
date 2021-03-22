using Controller.Helpers;
using Controller.Model;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Controller.ViewModel
{
    public class FlightSettingsViewModel
    {
        public IPageService pageService { get; set; }

        public FlightSettingsViewModel(IPageService pageService)
        {
            this.pageService = pageService;
        }

        public void ControllByButtons(Button buttons,Button accelerometer)
        {
            Settings.FlightSettings.droneControlMode = false;
            buttons.BackgroundColor = Color.YellowGreen;
            accelerometer.BackgroundColor = Color.LightGray;
            Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.BUTTONS));
        }

        public void ControllByAccelerometer(Button buttons, Button accelerometer)
        {
            Settings.FlightSettings.droneControlMode = true;
            buttons.BackgroundColor = Color.LightGray;
            accelerometer.BackgroundColor = Color.YellowGreen;
            Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.ACCELEROMETER));
        }
        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            AccelerometerValues.AccX = (float)Math.Round(e.Reading.Acceleration.X, 2);
            AccelerometerValues.AccY = (float)Math.Round(e.Reading.Acceleration.Y, 2);
            AccelerometerValues.AccZ = (float)Math.Round(e.Reading.Acceleration.Z, 2);
        }
        public void SetLayout(Button buttons,Button accelerometer)
        {
            if(Settings.FlightSettings.droneControlMode)
            {
                buttons.BackgroundColor = Color.LightGray;
                accelerometer.BackgroundColor = Color.YellowGreen;
            }
            else
            {
                buttons.BackgroundColor = Color.YellowGreen;
                accelerometer.BackgroundColor = Color.LightGray;
            }
        }
    }
}
