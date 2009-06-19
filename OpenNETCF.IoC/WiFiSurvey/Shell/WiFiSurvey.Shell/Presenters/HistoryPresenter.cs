using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Services;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Infrastructure;

namespace WiFiSurvey.Shell.Presenters
{
    public class HistoryPresenter
    {
        public int EventCount { get; set; }

        public event EventHandler<GenericEventArgs<IStatisticsData>> OnNewHistoryEvent;

        [EventPublication(EventNames.ClearHistory)]
        public event EventHandler<EventArgs> ClearEvents;

        public HistoryPresenter()
        {

        }

        [EventSubscription(EventNames.NewStatisticsEvent, ThreadOption.UserInterface)]
        public void DataService_NewDataEvent(object sender, GenericEventArgs<IStatisticsData> e)
        {
            if(OnNewHistoryEvent == null)return;
            OnNewHistoryEvent(this, e);
            EventCount++;            
        }

        public void ClearHistory()
        {
            EventCount = 0;
            ClearEvents(this, new EventArgs());
        }
    }
}
