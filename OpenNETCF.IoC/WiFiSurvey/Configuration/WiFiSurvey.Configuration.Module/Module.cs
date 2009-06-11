using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Services;
using WiFiSurvey.Configuration.Services;

namespace WiFiSurvey.Configuration
{
    public class Module : ModuleInit
    {
        public override void Load()
        {
        }

        public override void AddServices()
        {
            RootWorkItem.Services.AddNew<ConfigurationService, IConfigurationService>();
        }
    }
}
