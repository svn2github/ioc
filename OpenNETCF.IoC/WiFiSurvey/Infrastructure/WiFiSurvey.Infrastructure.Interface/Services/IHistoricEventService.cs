using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Infrastructure.Services
{
    public interface IHistoricEventService
    {
        void ShutDown();
        void InsertNetworkData(INetworkData data);
        void InsertStatisticsData(IStatisticsData data);
    }
}
