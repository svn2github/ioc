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
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>The date portion of Start is ignored for recurring events</remarks>
    public class RecurringEvent : ScheduleEvent, IEquatable<RecurringEvent>
    {
        public RecurringEvent()
        {
        }
        
        public RecurringEvent(Guid id)
            : base(id)
        {
        }

        public ScheduleDay Days { get; set; }
        public TimeSpan StartTime 
        {
            get { return Start.TimeOfDay; }
            set { Start = new DateTime(2011, 1, 1, value.Hours, value.Minutes, value.Seconds); }
        }

        public bool Equals(RecurringEvent other)
        {
            // if the IDs are the same, they are equal
            if (this.Identifier == other.Identifier) return true;

            // if the Start times are > 1 second apart, they are different
            if (Math.Abs((this.Start - other.Start).TotalSeconds) > 1)
            {
                return false;
            }

            // if the Days are different, they are different
            if (this.Days != other.Days) return false;

            return true;
        }
    }
}
