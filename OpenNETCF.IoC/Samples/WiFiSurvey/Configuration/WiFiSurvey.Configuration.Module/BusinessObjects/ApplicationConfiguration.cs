using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Configuration.BusinessObjects
{
    class ApplicationConfiguration : IApplicationConfiguration
    {
        public int AdapterPollInterval { get; set; }
    }
}
