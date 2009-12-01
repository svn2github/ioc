using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.Infrastructure.BusinessObjects
{
    public class StatisticsData : IStatisticsData
    {
        public StatsEvent Event { get; set; }
        public int EventTime { get; set; }
        public string Description { get; set; }
    }
}
