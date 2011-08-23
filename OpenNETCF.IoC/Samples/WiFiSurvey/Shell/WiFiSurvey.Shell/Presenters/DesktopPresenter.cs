using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure;
using WiFiSurvey.Infrastructure.BusinessObjects;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;
using OpenNETCF;

namespace WiFiSurvey.Shell.Presenters
{
    public class DesktopPresenter
    {
        public event EventHandler<GenericEventArgs<IDesktopData>> DesktopConnectionChange;

        [EventSubscription(EventNames.DesktopConnectionChange, ThreadOption.UserInterface)]
        public void NewDesktopStatus(object sender, GenericEventArgs<IDesktopData> args)
        {
            if (DesktopConnectionChange == null) return;
            DesktopConnectionChange(sender, args);
        }

        [EventPublication(EventNames.RestartBroadcasting)]
        public event EventHandler<EventArgs> RestartBroadcasting;

        public void RestartBroadcast()
        {
            RestartBroadcasting(this, new EventArgs());
        }
    }
}
