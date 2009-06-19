using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;
using OpenNETCF.Net.NetworkInformation;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Infrastructure.Services;
using WiFiSurvey.Infrastructure;

namespace WiFiSurvey.Shell.Presenters
{
    public class ContainerPresenter
    {
        [ServiceDependency]
        INetworkService NetworkService {get;set;}

        [ServiceDependency]
        IDataService DataService { get; set; }

        public event EventHandler<GenericEventArgs<IDesktopData>> DesktopConnectionChanged;

        public event EventHandler<GenericEventArgs<INetworkData>> APConnectionChanged;

        public ContainerPresenter()
        {
        }

        [EventSubscription(EventNames.DesktopConnectionChange, ThreadOption.UserInterface)]
        public void OnDesktopConnectionChanged(object sender, GenericEventArgs<IDesktopData> args)
        {
            if(DesktopConnectionChanged != null)
            {
                DesktopConnectionChanged(this, args);
            }
            DataService.InsertDesktopData(args.Value);
        }
        [EventSubscription(EventNames.NetworkDataChange, ThreadOption.UserInterface)]
        public void OnApConnectionChanged(object sender, GenericEventArgs<INetworkData> args)
        {
            if (APConnectionChanged != null)
            {
                APConnectionChanged(this, args);
            }
            DataService.InsertNetworkData(args.Value);
        }

    }
}
