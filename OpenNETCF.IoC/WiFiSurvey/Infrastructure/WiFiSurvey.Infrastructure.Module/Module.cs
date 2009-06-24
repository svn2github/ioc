using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Services;

namespace WiFiSurvey.Infrastructure
{
    public class Module : ModuleInit
    {
        public override void Load()
        {
        }

        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<DebugService, IDebugService>();
            RootWorkItem.Services.AddNew<APMonitorService, IAPMonitorService>();
            RootWorkItem.Services.AddNew<DesktopService, IDesktopService>();
            RootWorkItem.Services.AddNew<StatisticsService, IStatisticsService>();
        }
    }
}
