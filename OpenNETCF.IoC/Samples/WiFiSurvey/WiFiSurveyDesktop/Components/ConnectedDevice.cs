using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace WiFiSurveyDesktop.Components
{
    public class ConnectedDevice
    {
        public IPAddress IPAdress { get; set; }
        public string Data { get; set; }
        public DateTime LastPing { get; set; }
    }
}
