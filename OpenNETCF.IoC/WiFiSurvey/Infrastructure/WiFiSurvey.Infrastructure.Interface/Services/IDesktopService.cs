using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.Net.NetworkInformation;

namespace WiFiSurvey.Infrastructure.Services
{
    public interface IDesktopService
    {
        void StartListening();
    }
}
