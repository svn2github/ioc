using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Services;
using OpenNETCF.Net.NetworkInformation;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Shell.Presenters
{
    public class APListPresenter
    {
        private INetworkService NetworkService { get; set; }

        WirelessZeroConfigNetworkInterface Adapter{get;set;}

        public APListPresenter()
        {
            NetworkService = RootWorkItem.Services.Get<INetworkService>();
            Adapter = NetworkService.Adapter;
        }

        public AccessPointCollection GetAccessPoints()
        {
            if(NetworkService == null)return null;

            if (Adapter != null && Adapter.NearbyAccessPoints.Count > 0)
            {
                return Adapter.NearbyAccessPoints;
            }
            return null;
        }

        public void SetCurrentAP(AccessPoint accessPoint)
        {
            if (Adapter != null && accessPoint != null)
            {
                //OnNewAccessPointConnection(this, new AccessPointEventArgs(accessPoint));
            }
        }

    }
}
