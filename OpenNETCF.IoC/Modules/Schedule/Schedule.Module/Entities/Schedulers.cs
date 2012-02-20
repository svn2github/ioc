// LICENSE
// -------
// This software was originally authored by Christopher Tacke of OpenNETCF Consulting, LLC
// On March 10, 2009 is was placed in the public domain, meaning that all copyright has been disclaimed.
//
// You may use this code for any purpose, commercial or non-commercial, free or proprietary with no legal 
// obligation to acknowledge the use, copying or modification of the source.
//
// OpenNETCF will maintain an "official" version of this software at www.opennetcf.com and public 
// submissions of changes, fixes or updates are welcomed but not required
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;

namespace OpenNETCF.Schedule
{
    internal interface IScheduler
    {
        event EventHandler<GenericEventArgs<ScheduleEventInfo>> StartTimeArrived;

        void RegisterNextEvent(ScheduleEventInfo nextEvent, TimeSpan offset);
        void Stop();
        void Start();
    }

    internal class BasicScheduler : IScheduler
    {
        public event EventHandler<GenericEventArgs<ScheduleEventInfo>> StartTimeArrived;

        private ScheduleEventInfo m_event;
        private DateTime m_scheduleTime;
        private AutoResetEvent m_shutdown = new AutoResetEvent(false);

        public int GranularitySeconds { get; set; }
        public bool Running { get; private set; }

        public BasicScheduler()
        {
            GranularitySeconds = 2;
        }

        ~BasicScheduler()
        {
            Stop();
        }

        public void Stop()
        {
            m_shutdown.Set();
        }

        public void Start()
        {
            if (Running) return;

            new Thread(SchedulerThreadProc)
            {
                IsBackground = true
            }
            .Start();
        }

        private void SchedulerThreadProc()
        {
            m_shutdown.Reset();
            Running = true;

            try
            {
                while (true)
                {
                    var secondsUntilEvent = Math.Abs((DateTime.Now - m_scheduleTime).TotalSeconds);
                    Debug.WriteLine(string.Format("BasicScheduler: {0} seconds until next event", secondsUntilEvent));

                    if (secondsUntilEvent < GranularitySeconds)
                    {
                        var handler = StartTimeArrived;
                        if (handler != null)
                        {
                            // raise the event
                            handler(this, new GenericEventArgs<ScheduleEventInfo>(m_event));

                            // now clear the event as the time has passed
                            m_event = null;
                        }
                    }

                    if (m_shutdown.WaitOne(GranularitySeconds * 1000))
                    {
                        return;
                    }
                }
            }
            finally
            {
                Running = false;
            }
        }

        public void RegisterNextEvent(ScheduleEventInfo nextEvent, TimeSpan offset)
        {
            if (nextEvent == null) return;

            var scheduleTime = nextEvent.Start - offset;
            Debug.WriteLine(string.Format("BasicScheduler.RegisterNextEvent({0})", scheduleTime.ToString("s")));
            m_scheduleTime = scheduleTime;
            m_event = nextEvent;
        }
    }
}
