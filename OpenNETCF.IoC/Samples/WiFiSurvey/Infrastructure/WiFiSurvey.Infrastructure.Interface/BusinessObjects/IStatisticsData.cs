using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public interface IStatisticsData
    {
        StatsEvent Event { get; set; }
        int EventTime { get; set; }
        string Description { get; set; }
    }
}
