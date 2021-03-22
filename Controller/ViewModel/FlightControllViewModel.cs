
using Controller.Helpers;
using Controller.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Controller.ViewModel
{
    public class FlightControllViewModel
    {
        #region AccelerometerValues
        private float AccX;
        private float AccY;
        private float AccZ;
        private string currentCommand;
        private string previousCommand;
        #endregion

        #region Accelerometer
        public ICommand EnableAccelerometerCommand { get; private set; }
        public ICommand DisableAccelerometerCommand { get; private set; }
        #endregion

        #region Toolbar
        public ICommand GoToFlightSettingsCommand { get; private set; }
        public ICommand GoToCameraSettingsCommand { get; private set; }
        public ICommand ShowPicturesCommand { get; private set; }
        public ICommand GetPictureCommand { get; private set; }
        public ICommand DisconnectCommand { get; private set; }
        #endregion


        #region Buttons
        public ICommand FlyLeftCommand { get; private set; }
        public ICommand GetUpCommand { get; private set; }
        public ICommand ForwardCommand { get; private set; }
        public ICommand BackwardCommand { get; private set; }
        public ICommand LandCommand { get; private set; }
        public ICommand FlyRightCommand { get; private set; }
        public ICommand TakeAPictureCommand { get; private set; }
        public ICommand RoundLeftCommand { get; private set; }
        public ICommand RoundRightCommand { get; private set; }
        #endregion

        public IPageService pageService { get; private set; }

        public FlightControllViewModel(IPageService pageService)
        {
            this.pageService = pageService;
            EnableAccelerometerCommand = new Command(EnableAccelerometer);
            DisableAccelerometerCommand = new Command(DisableAccelerometer);
            GoToFlightSettingsCommand = new Command(async () => await GoToFlightSettings());
            GoToCameraSettingsCommand = new Command(GoToCameraSettings);
            ShowPicturesCommand = new Command(ShowPictures);
            GetPictureCommand = new Command(SendLastPicture);
            DisconnectCommand = new Command(Disconnect);
            FlyLeftCommand = new Command(FlyLeft);
            GetUpCommand = new Command(GetUp);
            ForwardCommand = new Command(Forward);
            BackwardCommand = new Command(Backward);
            LandCommand = new Command(Land);
            FlyRightCommand = new Command(FlyRight);
            TakeAPictureCommand = new Command(TakeAPicture);
            RoundLeftCommand = new Command(RoundLeft);
            RoundRightCommand = new Command(RoundRight);
        }

        private async Task GoToFlightSettings()
        {
            await pageService.PushAsync(new FlightSettings());
        }

        private async void Disconnect()
        {
            await Task.Run(() => GlobalSocket.SendCommand(Commands.Disconnect.DISCONNECT));

            GlobalSocket.client.Shutdown(SocketShutdown.Both);
            GlobalSocket.client.Disconnect(true);


            if (!GlobalSocket.client.Connected)
            {
                DisableAccelerometer();
                await pageService.DisplayAlert("Disconnected", null, "ok");
                await pageService.PopAsync();
            }
        }
        private void EnableAccelerometer()
        {
            if (Accelerometer.IsMonitoring) return;

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            Accelerometer.Start(SensorSpeed.UI);
        }
        private void DisableAccelerometer()
        {
            if (!Accelerometer.IsMonitoring) return;

            Accelerometer.ReadingChanged -= Accelerometer_ReadingChanged;
            Accelerometer.Stop();
        }
        private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            AccX = (float)Math.Round(e.Reading.Acceleration.X, 2);
            AccY = (float)Math.Round(e.Reading.Acceleration.Y, 2);
            AccZ = (float)Math.Round(e.Reading.Acceleration.Z, 2);

            if (AccY > 0.40) currentCommand = Commands.Flight.FLY_RIGHT;
            else if (AccY < -0.40) currentCommand = Commands.Flight.FLY_LEFT;
            else if ((AccY <= 0.40) && (AccY >= -0.40)) currentCommand = Commands.Flight.NONE;

            if (AccX > 0.90) currentCommand = Commands.Flight.BACKWARD;
            else if (AccX < 0.10) currentCommand = Commands.Flight.FORWARD;
            else if ((AccX <= 0.20) && (AccX >= 0.70)) currentCommand = Commands.Flight.NONE;

            if(previousCommand != currentCommand)
            {
                GlobalSocket.SendCommand(currentCommand);
                Debug.WriteLine(currentCommand);
            }

            previousCommand = currentCommand;

            for (int i = 0; i < 200000; i++) ;
        }
        private async void GoToCameraSettings()
        {
            await pageService.PushAsync(new CameraSettings());
        }

        private async void ShowPictures()
        {
            await pageService.PushAsync(new Pictures());
        }

        private async void SendLastPicture() =>  await Task.Run(() =>
        {
            GlobalSocket.SendCommand(Commands.Camera.SEND_LAST_PICTURE);
            JPGPicture.ImagesString.Add(GlobalSocket.ReceivePicture());
        });

        private async void FlyLeft() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.FLY_LEFT));

        private async void GetUp() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.GET_UP));

        private async void Forward() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.FORWARD));

        private async void Backward() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.BACKWARD));
        
        private async void Land() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.LAND));

        private async void FlyRight() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.FLY_RIGHT));

        private async void TakeAPicture() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Camera.TAKE_A_PHOTO));

        private async void RoundLeft() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.TURN_LEFT));

        private async void RoundRight() => await Task.Run(() => GlobalSocket.SendCommand(Commands.Flight.TURN_RIGHT));

        public void SetLayoutSettings(AbsoluteLayout buttons,AbsoluteLayout accelerometer)
        {
            if (Settings.FlightSettings.droneControlMode)
            {
                accelerometer.IsEnabled = true;
                accelerometer.IsVisible = true;

                buttons.IsEnabled = false;
                buttons.IsVisible = false;
            }
            else
            {
                accelerometer.IsEnabled = false;
                accelerometer.IsVisible = false;

                buttons.IsEnabled = true;
                buttons.IsVisible = true;
            }

            AbsoluteLayout.SetLayoutBounds(accelerometer, new Rectangle(0.1, 1.255, 700, 100));
        }
    }
}
