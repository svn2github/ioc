using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Infrastructure.Services
{
    public interface IDataService
    {
        event EventHandler<DataServiceArgs<string>> OnNewDataEvent;
        event EventHandler<EventArgs> OnClearEvents;

        void NewEvent(string Event);
        void ClearEvents();
        int EventCount { get; set; }
    }
}
