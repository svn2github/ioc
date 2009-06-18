using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;
using OpenNETCF.Net.NetworkInformation;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Infrastructure.Services;

namespace WiFiSurvey.Shell.Presenters
{
    public class ContainerPresenter
    {
        private INetworkService NetworkService {get;set;}

        public ContainerPresenter()
        {
            NetworkService = RootWorkItem.Services.Get<INetworkService>();
        }


    }
}
