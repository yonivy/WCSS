using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WBSS
{
    public class Md
    {
        public bool On { get; set; }
        public bool AlertsOn { get; set; }

        public Md(bool on, bool alertsOn)
        {
            this.On = on;
            this.AlertsOn = alertsOn;
        }
    }
}
