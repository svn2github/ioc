using System;

namespace OpenNETCF.Schedule
{
    public interface IScheduleService
    {
        event EventHandler<GenericEventArgs<ScheduleEventInfo>> ScheduleEventStart;

        bool Enabled { get; set; }

        void Initialize();
        void AddEvent(SingleEvent @event);
        void AddEvent(RecurringEvent @event);
        void DeleteEvent(Guid eventIdentifier);
        void DeleteEvent(ScheduleEvent @event);
        ScheduleEvent[] GetEvents(string description);
        T[] GetAllEvents<T>() where T : ScheduleEvent;

        ScheduleEventInfo GetNextEvent();
        ScheduleEventInfo[] GetDaysEvents(DateTime day);
        ScheduleEventInfo GetNextEvent(DateTime startPoint);
        ScheduleEventInfo GetNextRecurring(DateTime startPoint);
    }
}
