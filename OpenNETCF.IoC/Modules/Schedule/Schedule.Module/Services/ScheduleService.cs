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

namespace OpenNETCF.Schedule
{
    internal class ScheduleService
    {
        private List<RecurringEvent> m_recurring;
        private List<SingleEvent> m_single;

        public ScheduleService()
        {
            m_recurring = new List<RecurringEvent>();
            m_single = new List<SingleEvent>();
        }

        public ScheduleEventInfo GetNextEvent(DateTime startPoint)
        {
            // look for recurring
            var recur = GetNextRecurring(startPoint);

            // todo: look for single events

            // todo: find the earliest
            return recur;
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

        public void AddEvent(SingleEvent @event)
        {
            throw new NotImplementedException();
        }

        public void AddEvent(RecurringEvent @event)
        {
            // todo: ensure it's not a duplicate
            // todo: sort?
            m_recurring.Add(@event);
        }

        /// <summary>
        /// Deletes a scheduled event based on the provided event's Identifier
        /// </summary>
        /// <param name="event"></param>
        public void DeleteEvent(ScheduleEvent @event)
        {
            DeleteEvent(@event.Identifier);
        }

        /// <summary>
        /// Deletes a scheduled event based on the provided event Identifier
        /// </summary>
        /// <param name="event"></param>
        public void DeleteEvent(Guid eventIdentifier)
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
        public ScheduleEventInfo GetEvents(DateTime day)
        {
            throw new NotImplementedException();
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
