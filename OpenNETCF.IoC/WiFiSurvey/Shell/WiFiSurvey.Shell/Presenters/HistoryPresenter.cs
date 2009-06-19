using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Services;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.Infrastructure.Constants;

namespace WiFiSurvey.Shell.Presenters
{
    public class HistoryPresenter
    {
        [ServiceDependency]
        IDataService DataService { get; set; }

        public event EventHandler<DataServiceArgs<DataEvent>> OnNewHistoryEvent;

        public HistoryPresenter()
        {
        }

        [EventSubscription(EventNames.DesktopConnectionChange, ThreadOption.UserInterface)]
        void DataService_NewDataEvent(object sender, DataServiceArgs<DataEvent> e)
        {
            if(OnNewHistoryEvent == null)return;
            OnNewHistoryEvent(this, e);
        }
    }
}
