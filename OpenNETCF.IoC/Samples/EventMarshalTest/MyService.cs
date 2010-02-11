using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using System.Threading;

namespace EventMarshalTest
{
    public class MyService
    {
        public const string EventName = "event:ServiceEvent";
        private int m_number = 0;

        [EventPublication(EventName)]
        public event EventHandler<GenericEventArgs<int>> ServiceEvent;

        public void RaiseServiceEvent()
        {
            if (ServiceEvent == null) return;

            ServiceEvent(this, new GenericEventArgs<int>(m_number++));
        }

        public void BeginRaiseServiceEvent()
        {
            if (ServiceEvent == null) return;

            ThreadPool.QueueUserWorkItem(delegate
            {
                ServiceEvent(this, new GenericEventArgs<int>(m_number++));
            });
        }
    }
}
