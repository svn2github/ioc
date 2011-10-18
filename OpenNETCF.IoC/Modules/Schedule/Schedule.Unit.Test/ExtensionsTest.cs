using OpenNETCF.Schedule;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Schedule.Unit.Test
{
    [TestClass()]
    public class ExtensionsTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void GetScheduleDayTest()
        {
            var inputs = Enum.GetValues(typeof(DayOfWeek));

            foreach (var dow in inputs)
            {
                var sd = ((DayOfWeek)dow).GetScheduleDay();

                switch((DayOfWeek)dow)
                {
                    case DayOfWeek.Sunday:
                        Assert.AreEqual(ScheduleDay.Sunday, sd);
                        break;
                    case DayOfWeek.Monday:
                        Assert.AreEqual(ScheduleDay.Monday, sd);
                        break;
                    case DayOfWeek.Tuesday:
                        Assert.AreEqual(ScheduleDay.Tuesday, sd);
                        break;
                    case DayOfWeek.Wednesday:
                        Assert.AreEqual(ScheduleDay.Wednesday, sd);
                        break;
                    case DayOfWeek.Thursday:
                        Assert.AreEqual(ScheduleDay.Thursday, sd);
                        break;
                    case DayOfWeek.Friday:
                        Assert.AreEqual(ScheduleDay.Friday, sd);
                        break;
                    case DayOfWeek.Saturday:
                        Assert.AreEqual(ScheduleDay.Saturday, sd);
                        break;
                }
            }
        }

        [TestMethod()]
        public void NextDayTest()
        {
            var inputs = Enum.GetValues(typeof(ScheduleDay));

            foreach (var sd in inputs)
            {
                var next = ((ScheduleDay)sd).NextDay();

                switch ((ScheduleDay)sd)
                {
                    case ScheduleDay.Sunday:
                        Assert.AreEqual(ScheduleDay.Monday, next);
                        break;
                    case ScheduleDay.Monday:
                        Assert.AreEqual(ScheduleDay.Tuesday, next);
                        break;
                    case ScheduleDay.Tuesday:
                        Assert.AreEqual(ScheduleDay.Wednesday, next);
                        break;
                    case ScheduleDay.Wednesday:
                        Assert.AreEqual(ScheduleDay.Thursday, next);
                        break;
                    case ScheduleDay.Thursday:
                        Assert.AreEqual(ScheduleDay.Friday, next);
                        break;
                    case ScheduleDay.Friday:
                        Assert.AreEqual(ScheduleDay.Saturday, next);
                        break;
                    case ScheduleDay.Saturday:
                        Assert.AreEqual(ScheduleDay.Sunday, next);
                        break;
                }
            }
        }

        [TestMethod()]
        public void PreviousDayTest()
        {
            var inputs = Enum.GetValues(typeof(ScheduleDay));

            foreach (var sd in inputs)
            {
                var next = ((ScheduleDay)sd).PreviousDay();

                switch ((ScheduleDay)sd)
                {
                    case ScheduleDay.Sunday:
                        Assert.AreEqual(ScheduleDay.Saturday, next);
                        break;
                    case ScheduleDay.Monday:
                        Assert.AreEqual(ScheduleDay.Sunday, next);
                        break;
                    case ScheduleDay.Tuesday:
                        Assert.AreEqual(ScheduleDay.Monday, next);
                        break;
                    case ScheduleDay.Wednesday:
                        Assert.AreEqual(ScheduleDay.Tuesday, next);
                        break;
                    case ScheduleDay.Thursday:
                        Assert.AreEqual(ScheduleDay.Wednesday, next);
                        break;
                    case ScheduleDay.Friday:
                        Assert.AreEqual(ScheduleDay.Thursday, next);
                        break;
                    case ScheduleDay.Saturday:
                        Assert.AreEqual(ScheduleDay.Friday, next);
                        break;
                }
            }
        }
    }
}
