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
    public static class Extensions
    {
        public static ScheduleDay GetScheduleDay(this DateTime dt)
        {
            return dt.DayOfWeek.GetScheduleDay();
        }

        public static ScheduleDay GetScheduleDay(this DayOfWeek dow)
        {
            switch (dow)
            {
                case DayOfWeek.Sunday:
                    return ScheduleDay.Sunday;
                case DayOfWeek.Monday:
                    return ScheduleDay.Monday;
                case DayOfWeek.Tuesday:
                    return ScheduleDay.Tuesday;
                case DayOfWeek.Wednesday:
                    return ScheduleDay.Wednesday;
                case DayOfWeek.Thursday:
                    return ScheduleDay.Thursday;
                case DayOfWeek.Friday:
                    return ScheduleDay.Friday;
                case DayOfWeek.Saturday:
                    return ScheduleDay.Saturday;
            }

            throw new ArgumentException();
        }

        public static ScheduleDay NextDay(this ScheduleDay sd)
        {
            switch (sd)
            {
                case ScheduleDay.Saturday:
                    return ScheduleDay.Sunday;
                default:
                    return (ScheduleDay)((int)sd << 1);
            }
        }

        public static ScheduleDay PreviousDay(this ScheduleDay sd)
        {
            switch (sd)
            {
                case ScheduleDay.Sunday:
                    return ScheduleDay.Saturday;
                default:
                    return (ScheduleDay)((int)sd >> 1);
            }
        }
    }
}
