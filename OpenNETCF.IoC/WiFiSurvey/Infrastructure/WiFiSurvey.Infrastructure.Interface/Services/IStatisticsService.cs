using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.Infrastructure.Services
{
    public interface IStatisticsService
    {
        TimeSpan NetworkDownTime { get; set; }
        void FoundNetwork();
        void LostNetwork();

        TimeSpan AcessPointDownTime { get; set; }
        void FoundAccessPoint();
        void LostAccessPoint();
    }
}
