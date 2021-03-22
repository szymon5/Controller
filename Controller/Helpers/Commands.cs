using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.Helpers
{
    public class Commands
    {
        public struct Flight
        {
            public const string GET_UP = "GET UP";
            public const string LAND = "LAND";
            public const string FLY_RIGHT = "FLY RIGHT";
            public const string FLY_LEFT = "FLY LEFT";
            public const string FORWARD = "FORWARD";
            public const string BACKWARD = "BACKWARD";
            public const string TURN_RIGHT = "ROUND RIGHT";
            public const string TURN_LEFT = "ROUND LEFT";
            public const string NONE = "NONE";
            public const string ACCELEROMETER = "ACCELEROMETER";
            public const string BUTTONS = "BUTTONS";
        }
        
        public struct Camera
        {
            public const string ENABLE_CAMERA = "ENABLE CAMERA";
            public const string DISABLE_CAMERA = "DISABLE CAMERA";
            public const string START_RECORDING = "START RECORDING";
            public const string STOP_RECORDING = "STOP RECORDING";
            public const string TAKE_A_PHOTO = "TAKE A PHOTO";
            public const string SEND_LAST_PICTURE = "SEND LAST PICTURE";
            public const string START_STREAMING = "START STREAMING";
            public const string STOP_STREAMING = "STOP STREAMING";
        }

        public struct Disconnect
        {
            public const string DISCONNECT = "DISCONNECT";
        }
    }
}
