using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Services;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Threading;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.BusinessObjects;
using OpenNETCF.Net.Sockets;
using OpenNETCF.Net.NetworkInformation;

namespace WiFiSurvey.DAL.Services
{
    public class DataService : IDataService
    {
        public DataService()
        {

        }

        public event EventHandler<DataServiceArgs<string>> OnNewDataEvent;
        public event EventHandler<EventArgs> OnClearEvents;

        public void NewEvent(string Event)
        {
            if (OnNewDataEvent == null) return;
            OnNewDataEvent(this.ToString() , new DataServiceArgs<string>(Event));
            EventCount++;
        }

        public AccessPointCollection AvailableAccessPoints { get; set; }
        public int NetworkDownTime { get; set; }

        public void ClearEvents()
        {
            EventCount = 0;
            if (OnClearEvents != null)
            {
                OnClearEvents(this, new EventArgs());
            }
        }
        public int EventCount { get; set; }
        //publish an Event to a DataBase
    }
}
