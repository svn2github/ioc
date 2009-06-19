using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.DAL.Services;
using WiFiSurvey.Infrastructure.Services;

namespace WiFiSurvey.DAL
{
    public class Module : ModuleInit
    {
        public override void Load()
        {

        }

        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<HistoricEventService, IHistoricEventService>();
        }
    }
}
