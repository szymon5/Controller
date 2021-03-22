using Controller.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.Helpers
{
    public class Settings
    {
        public struct CameraSettings
        {
            public static bool isCameraEnabled = false;
            public static bool hasRecordingStarted = false;
            public static bool hasStreamingStarted = false;
        }

        public struct FlightSettings
        {
            public static bool droneControlMode = true;//true -> akcelerometr....false -> przyciski
        }

        public static void SetSettings()
        {
            string get_settings = GlobalSocket.ReceiveData();

            var config = JsonConvert.DeserializeObject<List<Config>>(get_settings).ToArray();

            Settings.CameraSettings.isCameraEnabled = config[0].isCameraEnabled;
            Settings.CameraSettings.hasRecordingStarted = config[0].hasRecordingStarted;
            Settings.CameraSettings.hasStreamingStarted = config[0].hasStreamingStarted;

            Settings.FlightSettings.droneControlMode = config[0].droneControlMode;
        }
    }
}
