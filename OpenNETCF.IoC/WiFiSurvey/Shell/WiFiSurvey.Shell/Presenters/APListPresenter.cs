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

namespace WiFiSurvey.Shell.Presenters
{
    public class APListPresenter
    {
        public AccessPointCollection AccessPoints { get; set; }

        private INetworkService NetworkService { get; set; }
        private Thread AccessPointThread;
        private Boolean Done { get; set; }

        WirelessZeroConfigNetworkInterface Adapter{get;set;}

        public APListPresenter()
        {
            NetworkService = RootWorkItem.Services.Get<INetworkService>();
            Adapter = NetworkService.Adapter;

            AccessPointThread = new Thread(GetAccessPoints);
            AccessPointThread.IsBackground = true;
            AccessPointThread.Start();
        }

        ~APListPresenter()
        {
            AccessPointThread.Abort();
            Done = true;
        }

        public void GetAccessPoints()
        {
            if (Adapter != null)
            {
                while (!Done)
                {
                    AccessPoints = Adapter.NearbyAccessPoints;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
