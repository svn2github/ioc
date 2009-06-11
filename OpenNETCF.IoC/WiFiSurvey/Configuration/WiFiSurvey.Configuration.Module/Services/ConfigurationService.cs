using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Services;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Configuration.Services
{
    public class ConfigurationService : IConfigurationService
    {
        public IApplicationConfiguration ApplicationConfig
        {
            get { throw new NotImplementedException(); }
        }
    }
}
