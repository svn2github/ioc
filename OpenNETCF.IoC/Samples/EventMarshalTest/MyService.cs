using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;
using System.Threading;
using OpenNETCF;

namespace EventMarshalTest
{
    public class IntArgs : EventArgs
    {
        public int Value { get; set; }

        public IntArgs(int value)
        {
            Value = value;
        }
    }

    public class MyService
    {
        public const string EventNameA = "event:ServiceEventA";
        public const string EventNameB = "event:ServiceEventB";
        private int m_number = 0;

        [EventPublication(EventNameA)]
        public event EventHandler<GenericEventArgs<int>> ServiceEventA;

        [EventPublication(EventNameB)]
        public event EventHandler ServiceEventB;

        public void RaiseServiceEvents()
        {
            m_number++;

            if (ServiceEventA != null)
                ServiceEventA(this, new GenericEventArgs<int>(m_number));

            if (ServiceEventB != null)
                ServiceEventB(this, new IntArgs(m_number++));
        }

        public void BeginRaiseServiceEvent()
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                RaiseServiceEvents();
            });
        }
    }
}
