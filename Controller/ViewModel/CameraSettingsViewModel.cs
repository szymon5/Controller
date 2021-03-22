using Controller.Helpers;
using Controller.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Controller.ViewModel
{
    public class CameraSettingsViewModel
    {
        private string camE, rec, stream;
        public ICommand StartRecordingCommand { get;private set; }
        public ICommand StartStreamingCommand { get; private set; }

        public IPageService pageService { get; set; }

        public CameraSettingsViewModel(IPageService pageService)
        {
            this.pageService = pageService;
            StartRecordingCommand = new Command<Button>((btn) => StartRecording(btn));
            StartStreamingCommand = new Command<Button>((btn) => StartStreaming(btn));
        }

        public void EnableCamera(Button cameraEnabled, Button startRecording, Button startStreaming)
        {
            if (Settings.CameraSettings.isCameraEnabled)
            {
                Settings.CameraSettings.isCameraEnabled = false;
                Settings.CameraSettings.hasRecordingStarted = false;
                Settings.CameraSettings.hasStreamingStarted = false;
                cameraEnabled.BackgroundColor = Color.LightGray;
                startRecording.BackgroundColor = Color.LightGray;
                startStreaming.BackgroundColor = Color.LightGray;

                Task.Run(() => GlobalSocket.SendCommand(Commands.Camera.DISABLE_CAMERA));

                pageService.DisplayAlert("Camera disabled.", null, "Ok");
            }
            else
            {
                Settings.CameraSettings.isCameraEnabled = true;
                cameraEnabled.BackgroundColor = Color.YellowGreen;

                Task.Run(() => GlobalSocket.SendCommand(Commands.Camera.ENABLE_CAMERA));

                pageService.DisplayAlert("Camera enabled.", null, "Ok");
            }

        }

        private void StartRecording(Button button)
        {
            if (Settings.CameraSettings.isCameraEnabled)
            {
                if (Settings.CameraSettings.hasRecordingStarted)
                {
                    Settings.CameraSettings.hasRecordingStarted = false;
                    button.BackgroundColor = Color.LightGray;

                    Task.Run(() => GlobalSocket.SendCommand(Commands.Camera.STOP_RECORDING));

                    pageService.DisplayAlert("Recording has been stopped.", null, "Ok");
                }
                else
                {
                    Settings.CameraSettings.hasRecordingStarted = true;
                    button.BackgroundColor = Color.YellowGreen;

                    Task.Run(() => GlobalSocket.SendCommand(Commands.Camera.START_RECORDING));

                    pageService.DisplayAlert("Recording has started!", null, "Ok");
                }
            }
            else pageService.DisplayAlert("Camera is disabled.", null, "Cancel");
        }

        private void StartStreaming(Button button)
        {
            if(Settings.CameraSettings.isCameraEnabled && Settings.CameraSettings.hasRecordingStarted)
            {
                if (Settings.CameraSettings.hasStreamingStarted)
                {
                    Settings.CameraSettings.hasStreamingStarted = false;
                    button.BackgroundColor = Color.LightGray;

                    Task.Run(() => GlobalSocket.SendCommand(Commands.Camera.STOP_STREAMING));

                    pageService.DisplayAlert("Recording has been stopped!", null, "Ok");
                }
                else
                {
                    Settings.CameraSettings.hasStreamingStarted = true;
                    button.BackgroundColor = Color.YellowGreen;

                    Task.Run(() => GlobalSocket.SendCommand(Commands.Camera.START_STREAMING));

                    pageService.DisplayAlert("Streaming has started!", null, "Ok");
                }
            }
            else pageService.DisplayAlert("Camera or recording is disabled.", null, "Cancel");
        }
        public void SetLayoutSettings(Button cameraEnabled, Button startRecording, Button startStreaming)
        {
            if (Settings.CameraSettings.isCameraEnabled)
            {
                cameraEnabled.BackgroundColor = Color.YellowGreen;
                startRecording.BackgroundColor = Color.LightGray;
                startStreaming.BackgroundColor = Color.LightGray;

                if (Settings.CameraSettings.hasRecordingStarted)
                {
                    startRecording.BackgroundColor = Color.YellowGreen;

                    if(Settings.CameraSettings.hasStreamingStarted)
                    {
                        startStreaming.BackgroundColor = Color.YellowGreen;
                    }
                }
            }
            else
            {
                cameraEnabled.BackgroundColor = Color.LightGray;
                startRecording.BackgroundColor = Color.LightGray;
                startStreaming.BackgroundColor = Color.LightGray;
            }
        }
    }
}
