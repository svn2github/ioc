using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Constants;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public interface IDesktopData
    {
        DesktopStatus Status { get; set; }
        string IPAddress { get; set; }
    }
}
