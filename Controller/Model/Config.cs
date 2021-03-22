using System;
using System.Collections.Generic;
using System.Text;

namespace Controller.Model
{
    public class Config
    {
        public bool isCameraEnabled { get; set; }
        public bool hasRecordingStarted { get; set; }
        public bool hasStreamingStarted { get; set; }
        public bool droneControlMode { get; set; }//true -> akcelerometr....false -> przyciski
    }
}
