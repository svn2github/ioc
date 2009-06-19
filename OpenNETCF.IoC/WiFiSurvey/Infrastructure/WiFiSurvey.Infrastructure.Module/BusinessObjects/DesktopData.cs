using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Constants;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public class DesktopData : IDesktopData
    {
        public DesktopStatus Status { get; set; }
        public string IPAddress { get; set; }
    }
}
