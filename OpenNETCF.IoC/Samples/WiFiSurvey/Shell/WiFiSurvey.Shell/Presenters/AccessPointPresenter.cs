using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Services;
using OpenNETCF.Net.NetworkInformation;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Infrastructure.BusinessObjects;
using System.Threading;
using WiFiSurvey.Infrastructure;

namespace WiFiSurvey.Shell.Presenters
{
    public class AccessPointPresenter
    {
        public AccessPointCollection AccessPoints { get; set; }

        private IDesktopService NetworkService { get; set; }
        private Thread AccessPointThread;
        private Boolean Done { get; set; }

        public event EventHandler<GenericEventArgs<INetworkData>> NetworkDataChanged;

        [ServiceDependency]
        IAPMonitorService APMonitorService { get; set; }

        [EventSubscription(EventNames.NetworkDataChange, ThreadOption.UserInterface)]
        public void OnNetworkDataChange(object sender, GenericEventArgs<INetworkData> args)
        {
            if (NetworkDataChanged != null)
            {
                NetworkDataChanged(this, args);
            }
        }
    }
}
