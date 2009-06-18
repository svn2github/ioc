using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Services;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Configuration.BusinessObjects;

namespace WiFiSurvey.Configuration.Services
{
    public class ConfigurationService : IConfigurationService
    {
        ApplicationConfiguration m_config;

        public ConfigurationService()
        {
            m_config = new ApplicationConfiguration();
            m_config.AdapterPollInterval = 1000;
        }

        public IApplicationConfiguration ApplicationConfig
        {
            get { return m_config; }
        }
    }
}
