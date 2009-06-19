using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Infrastructure.Services
{
    public interface IStatisticsService
    {
        TimeSpan NetworkDownTime { get; set; }
        void FoundNetwork(string IPAddress);
        void LostNetwork(string IPAddress);

        TimeSpan AcessPointDownTime { get; set; }
        void FoundAccessPoint(APInfo info);
        void LostAccessPoint();
    }
}
