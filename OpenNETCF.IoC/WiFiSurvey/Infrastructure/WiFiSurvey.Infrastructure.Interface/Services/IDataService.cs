using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Infrastructure.Services
{
    public interface IDataService
    {
        void ShutDown();
        void InsertNetworkData(INetworkData data);
        void InsertDesktopData(IDesktopData data);
    }
}
