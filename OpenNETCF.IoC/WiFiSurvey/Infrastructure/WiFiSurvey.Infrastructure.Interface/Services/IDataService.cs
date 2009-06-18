using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Infrastructure.Services
{
    public interface IDataService
    {
        event EventHandler<DataServiceArgs<DataEvent>> OnNewDataEvent;
        event EventHandler<EventArgs> OnClearEvents;

        void NewEvent(string Location, string Event);
        void ClearEvents();
        int EventCount { get; set; }
    }
}
