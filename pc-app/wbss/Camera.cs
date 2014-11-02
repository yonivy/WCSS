using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WBSS
{
    public class Camera
    {
        public Camera(
            string name,
            string url,
            uint port,
            string stream, 
            bool motionDetection,
            double threshold,
            bool alerts,
            DateTime recordTime,
            string id = "") 
        {
            this.Id = id;
            this.Name = name;
            this.URL = url;
            this.Port = port;
            this.Stream = stream;
            this.MotionDetection = motionDetection;
            this.Threshold = threshold;
            this.Alerts = alerts;
            this.RecordTime = recordTime;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public uint Port { get; set; }
        public string Stream { get; set; }
        public bool MotionDetection { get; set; }
        public double Threshold { get; set; }
        public bool Alerts { get; set; }
        public DateTime RecordTime { get; set; }
    }
}
