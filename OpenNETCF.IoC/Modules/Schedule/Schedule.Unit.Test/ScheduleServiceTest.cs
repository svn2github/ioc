using OpenNETCF.Schedule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Schedule.Unit.Test
{
    [TestClass()]
    public class ScheduleServiceTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void SingleRecurringEventWeekdaysTest()
        {
            var svc = new ScheduleService();

            // create a single recurring event for weekdays
            var evt = new RecurringEvent()
            {
                Description = "Test Event",
                Days = ScheduleDay.Weekdays,
                StartTime = new TimeSpan(12, 0, 0),
                Duration = 60
            };

            svc.AddEvent(evt);

            // 8/29/11 is a Monday

            // test next event is same day, after now
            var next = svc.GetNextRecurring(DateTime.Parse("8/30/11 11:59:00"));
            Assert.IsTrue(next.Start.Month == 8);
            Assert.IsTrue(next.Start.Day == 30);
            
            // test next event is next day
            next = svc.GetNextRecurring(DateTime.Parse("8/30/11 12:01:00"));
            Assert.IsTrue(next.Start.Month == 8);
            Assert.IsTrue(next.Start.Day == 31);

            // test next event is next day
            next = svc.GetNextRecurring(DateTime.Parse("8/30/11 23:59:00"));
            Assert.IsTrue(next.Start.Month == 8);
            Assert.IsTrue(next.Start.Day == 31);

            next = svc.GetNextRecurring(DateTime.Parse("8/31/11 02:24:00"));
            Assert.IsTrue(next.Start.Month == 8);
            Assert.IsTrue(next.Start.Day == 31);

            next = svc.GetNextRecurring(DateTime.Parse("8/31/11 13:11:00"));
            Assert.IsTrue(next.Start.Month == 9);
            Assert.IsTrue(next.Start.Day == 1);

            // test Friday->Monday wrap
            next = svc.GetNextRecurring(DateTime.Parse("9/2/11 13:11:00"));
            Assert.IsTrue(next.Start.Month == 9);
            Assert.IsTrue(next.Start.Day == 5);

            // test Saturday->Monday wrap
            next = svc.GetNextRecurring(DateTime.Parse("9/3/11 11:00:00"));
            Assert.IsTrue(next.Start.Month == 9);
            Assert.IsTrue(next.Start.Day == 5);
        }

        [TestMethod()]
        public void SingleRecurringEventSingleDayTest()
        {
            var svc = new ScheduleService();

            // create a single recurring event for weekdays
            var evt = new RecurringEvent()
            {
                Description = "Test Event",
                Days = ScheduleDay.Wednesday,
                StartTime = new TimeSpan(13, 0, 0),
                Duration = 60
            };

            svc.AddEvent(evt);

            // 10/17/11 is a Monday

            //Monday-> Wednesday
            var next = svc.GetNextRecurring(DateTime.Parse("10/17/11 11:59:00"));
            Assert.IsTrue(next.Start.Month == 10);
            Assert.IsTrue(next.Start.Day == 19);

            //same day, before event
            next = svc.GetNextRecurring(DateTime.Parse("10/19/11 12:30:00"));
            Assert.IsTrue(next.Start.Month == 10);
            Assert.IsTrue(next.Start.Day == 19);

            //same day, After event
            next = svc.GetNextRecurring(DateTime.Parse("10/19/11 13:30:00"));
            Assert.IsTrue(next.Start.Month == 10);
            Assert.IsTrue(next.Start.Day == 26);

            //week wrap
            next = svc.GetNextRecurring(DateTime.Parse("10/20/11 08:00:00"));
            Assert.IsTrue(next.Start.Month == 10);
            Assert.IsTrue(next.Start.Day == 26);
        }

        [TestMethod()]
        public void GetEventsByDescriptionTest()
        {
            var svc = new ScheduleService();

            svc.AddEvent(
                new RecurringEvent()
                {
                    Description = "Description0",
                    Days = ScheduleDay.Wednesday,
                    StartTime = new TimeSpan(13, 0, 0),
                    Duration = 60
                }
            );

            Assert.AreEqual(0, svc.GetEvents("Description1").Length);

            svc.AddEvent(
                new RecurringEvent()
                {
                    Description = "Description1",
                    Days = ScheduleDay.Wednesday,
                    StartTime = new TimeSpan(13, 0, 0),
                    Duration = 60
                }
            );

            Assert.AreEqual(1, svc.GetEvents("Description1").Length);

            svc.AddEvent(
                new RecurringEvent()
                {
                    Description = "Description2",
                    Days = ScheduleDay.Wednesday,
                    StartTime = new TimeSpan(13, 0, 0),
                    Duration = 60
                }
            );

            Assert.AreEqual(1, svc.GetEvents("Description1").Length);

            svc.AddEvent(
                new RecurringEvent()
                {
                    Description = "Description1",
                    Days = ScheduleDay.Wednesday,
                    StartTime = new TimeSpan(13, 0, 0),
                    Duration = 60
                }
            );

            Assert.AreEqual(2, svc.GetEvents("Description1").Length);
        }

        [TestMethod()]
        public void DeleteEventTest()
        {
            var svc = new ScheduleService();

            svc.AddEvent(
                new RecurringEvent()
                {
                    Description = "Foo",
                    Days = ScheduleDay.Monday,
                    StartTime = new TimeSpan(13, 0, 0),
                    Duration = 60
                }
            );
            svc.AddEvent(
                new RecurringEvent()
                {
                    Description = "Bar",
                    Days = ScheduleDay.Tuesday,
                    StartTime = new TimeSpan(13, 0, 0),
                    Duration = 60
                }
            );
            svc.AddEvent(
                new RecurringEvent()
                {
                    Description = "Baz",
                    Days = ScheduleDay.Wednesday,
                    StartTime = new TimeSpan(13, 0, 0),
                    Duration = 60
                }
            );

            var evt = svc.GetEvents("Bar").FirstOrDefault();
            Assert.IsNotNull(evt);
            svc.DeleteEvent(evt);
            evt = svc.GetEvents("Bar").FirstOrDefault();
            Assert.IsNull(evt);
        }
    }
}
