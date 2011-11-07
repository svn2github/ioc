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
using OpenNETCF.IoC;
using System.Diagnostics;
using System.Threading;

namespace OpenNETCF.Schedule
{
    internal class ScheduleService : IScheduleService
    {
        [EventPublication(EventNames.ScheduleServiceReady)]
        public event EventHandler ScheduleServiceReady;

        public event EventHandler<GenericEventArgs<ScheduleEventInfo>> ScheduleEventStart;

        private List<RecurringEvent> m_recurring;
        private List<SingleEvent> m_single;

        private IScheduler m_scheduler;
        private ScheduleEventInfo m_nextEvent = null;
        private bool m_enabled = false;

        public ScheduleService()
        {
            m_recurring = new List<RecurringEvent>();
            m_single = new List<SingleEvent>();

            m_scheduler = new BasicScheduler();

            m_scheduler.StartTimeArrived += new EventHandler<GenericEventArgs<ScheduleEventInfo>>(OnStartTimeArrived);
        }

        public bool Enabled 
        {
            get { return m_enabled; }
            set
            {
                if (value == Enabled) return;
                m_enabled = value;

                if (value)
                {
                    m_scheduler.Start();
                    UpdateScheduler();
                }
                else
                {
                    m_scheduler.Stop();
                }
            }
        }

        public virtual void Initialize()
        {
            // this can't happen in the ctor becasue the event handler doesn't get wired up until *after* the instance is created
            var handler = ScheduleServiceReady;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        protected virtual DateTime CurrentTime
        {
            get { return DateTime.Now; }
        }

        private void OnStartTimeArrived(object sender, GenericEventArgs<ScheduleEventInfo> e)
        {
            Debug.WriteLine("ScheduleService.OnStartTimeArrived");

            var handler = ScheduleEventStart;

            if (handler != null)
            {
                handler(this, e);
            }

            m_nextEvent = null;

            // wait to ensure that we don't reschedule for the same event
            Thread.Sleep(2000);

            UpdateScheduler();
        }

        public ScheduleEventInfo GetNextEvent()
        {
            return GetNextEvent(CurrentTime);
        }

        public ScheduleEventInfo GetNextEvent(DateTime startPoint)
        {
            // look for recurring
            var recur = GetNextRecurring(startPoint);

            // todo: look for single events

            // todo: find the earliest
            return recur;
        }

        public T[] GetAllEvents<T>() 
            where T : ScheduleEvent
        {
            if (typeof(T).Equals(typeof(RecurringEvent)))
            {
                return m_recurring.ToArray() as T[];
            }

            if (typeof(T).Equals(typeof(SingleEvent)))
            {
                return m_single.ToArray() as T[];
            }

            throw new NotSupportedException();
        }
        
        public ScheduleEventInfo GetNextRecurring(DateTime startPoint)
        {
            var checkDay = startPoint.DayOfWeek.GetScheduleDay();

            // is there one on the given day?
            var next = (from e in m_recurring
                       where e.Start.TimeOfDay >= startPoint.TimeOfDay
                       && (e.Days & checkDay) == checkDay
                       select e).FirstOrDefault();

            if (next != null)
            {
                // there is an event in the same day
                return new ScheduleEventInfo()
                {
                    Start = new DateTime(
                        startPoint.Year,
                        startPoint.Month,
                        startPoint.Day,
                        next.StartTime.Hours,
                        next.StartTime.Minutes,
                        next.StartTime.Seconds),
                    Duration = next.Duration,
                    Description = next.Description
                };
            }

            startPoint = new DateTime(startPoint.Year, startPoint.Month, startPoint.Day, 0, 0, 0).AddDays(1);
            checkDay = startPoint.DayOfWeek.GetScheduleDay();
            var addDays = 0;
            do
            {
                var @event = m_recurring.FirstOrDefault(c => (c.Days & checkDay) == checkDay);
                if (@event != null)
                {
                    return new ScheduleEventInfo()
                    {
                        Start = new DateTime(
                            startPoint.Year,
                            startPoint.Month,
                            startPoint.Day,
                            @event.StartTime.Hours,
                            @event.StartTime.Minutes,
                            @event.StartTime.Seconds).AddDays(addDays),
                        Duration = @event.Duration,
                        Description = @event.Description
                    };
                }
                addDays++;
                checkDay = checkDay.NextDay();
                if (checkDay == startPoint.DayOfWeek.GetScheduleDay()) break;
            } while (true);

            return null;
        }


        private SingleEvent GetNextSingle(DateTime startPoint)
        {
            throw new NotImplementedException();
        }

        public virtual void AddEvent(SingleEvent @event)
        {
            throw new NotImplementedException();
        }

        public virtual void AddEvent(RecurringEvent @event)
        {
            Debug.WriteLine("ScheduleService.AddEvent(recurring)");

            // ensure it's not a duplicate
            if (!m_recurring.Contains(@event))
            {
                // todo: sort?
                m_recurring.Add(@event);
            }

            UpdateScheduler();
        }

        private void UpdateScheduler()
        {
            var apparentNow = CurrentTime;
            var platformNow = DateTime.Now;
            var offset = apparentNow - platformNow;

            var next = GetNextEvent(apparentNow);
            if (next == null) return;

            // see if we need to schedule this (the scheduler will give us an event when it occurs)
            if ((m_nextEvent == null)
                || (m_nextEvent.Start < apparentNow)
                || (next.Start < m_nextEvent.Start))
            {
                m_nextEvent = next;
                m_scheduler.RegisterNextEvent(m_nextEvent, offset);
            }
        }

        /// <summary>
        /// Deletes a scheduled event based on the provided event's Identifier
        /// </summary>
        /// <param name="event"></param>
        public virtual void DeleteEvent(ScheduleEvent @event)
        {
            DeleteEvent(@event.Identifier);
        }

        /// <summary>
        /// Deletes a scheduled event based on the provided event Identifier
        /// </summary>
        /// <param name="event"></param>
        public virtual void DeleteEvent(Guid eventIdentifier)
        {
            m_recurring.RemoveAll(e => e.Identifier.Equals(eventIdentifier));
            m_single.RemoveAll(e => e.Identifier.Equals(eventIdentifier));
        }

        /// <summary>
        /// Gets all events (single and recurring) that happen on the date specified. 
        /// </summary>
        /// <remarks>The time of the input DateTime is ignored, so events that occur before the specified time, but on that day, will still be retrieved </remarks>
        /// <param name="day"></param>
        /// <returns></returns>
        public ScheduleEventInfo[] GetDaysEvents(DateTime day)
        {
            var events = new List<ScheduleEventInfo>();
            
            ScheduleEventInfo next = null;
            var start = day.StartOfDay();
            do
            {
                next = GetNextEvent(start);
                if ((next == null) || (!next.Start.FallsOnSameDayAs(day))) break;
                events.Add(next);
                start = next.Start;
            } while (true);

            return events.ToArray();
        }

        /// <summary>
        /// Returns all events that match the given description
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public ScheduleEvent[] GetEvents(string description)
        {
            var aggregate = new List<ScheduleEvent>();

            aggregate.AddRange(from r in m_recurring
                            where r.Description == description
                            select r as ScheduleEvent);

            aggregate.AddRange(from s in m_single
                         where s.Description == description
                         select s as ScheduleEvent);

            return aggregate.ToArray();

        }
    }
}
