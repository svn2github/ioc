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
using WiFiSurvey.DAL.SQLCE;

namespace WiFiSurvey.DAL.Services
{
    public class DataService : IDataService
    {
        SQLCEConnection Database;

        public DataService()
        {
            Database = new SQLCEConnection();
        }

        public event EventHandler<DataServiceArgs<DataEvent>> OnNewDataEvent;
        public event EventHandler<EventArgs> OnClearEvents;

        public void NewEvent(string Location, string Event)
        {
            if (OnNewDataEvent == null) return;
            DataEvent dataEvent = new DataEvent(){Location = Location, Description = Event};
            OnNewDataEvent(this.ToString() , new DataServiceArgs<DataEvent>(dataEvent) );
            //Database.InsertValueIntoTable(dataEvent);
            EventCount++;
        }

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
