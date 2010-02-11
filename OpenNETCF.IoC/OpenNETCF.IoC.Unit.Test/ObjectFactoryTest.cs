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

using OpenNETCF.IoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace OpenNETCF.IoC.Unit.Test
{
    [TestClass()]
    public class ObjectFactoryTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void GetEventSourcesFromTypeByNameTest()
        {
            Type type = typeof(MockEventSource);
            Assert.AreEqual(2, ObjectFactory.GetEventSourcesFromTypeByName(type, "IoC Event A").Length,
                "Unexpected number of events returned");

            Assert.AreEqual(1, ObjectFactory.GetEventSourcesFromTypeByName(type, "IoC Event B").Length,
                "Unexpected number of events returned");

            Assert.AreEqual(0, ObjectFactory.GetEventSourcesFromTypeByName(type, "Unknown").Length,
                "Unexpected number of events returned");
        }

        [TestMethod()]
        public void GetEventSinksFromTypeByNameTest()
        {
            Type type = typeof(MockEventSink);
            Assert.AreEqual(1, ObjectFactory.GetEventSinksFromTypeByName(type, "IoC Event A", ThreadOption.Caller).Length,
                "Unexpected number of methods returned");

            Assert.AreEqual(1, ObjectFactory.GetEventSinksFromTypeByName(type, "IoC Event B", ThreadOption.Caller).Length,
                "Unexpected number of methods returned");

            Assert.AreEqual(0, ObjectFactory.GetEventSinksFromTypeByName(type, "Unknown", ThreadOption.Caller).Length,
                "Unexpected number of methods returned");
        }

        [TestMethod()]
        public void GetEventSourceNamesTest()
        {
            Type type = typeof(MockEventSource);
            Assert.AreEqual(3, ObjectFactory.GetEventSourceNames(type).Length,
                "Unexpected number of names returned");

            Assert.AreEqual(0, ObjectFactory.GetEventSourceNames(typeof(MockEventSink)).Length,
                "Unexpected number of names returned");
        }

        [TestMethod()]
        public void GetEventSinkNamesTest()
        {
            Assert.AreEqual(0, ObjectFactory.GetEventSinkSubscriptions(typeof(MockEventSource)).Length,
                "Unexpected number of subscriptions returned");

            Assert.AreEqual(2, ObjectFactory.GetEventSinkSubscriptions(typeof(MockEventSink)).Length,
                "Unexpected number of subscriptions returned");
        }

        [TestMethod()]
        [Description("Checks to ensure events happen for items pre-created and added versus created with AddNew")]
        public void InterClassEventTest3()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource src = new MockEventSource();
                wi.Items.Add(src);
                MockEventSink sinkA = new MockEventSink();
                wi.Items.Add(sinkA);
                MockEventComposite1 composite1 = new MockEventComposite1();
                wi.Items.Add(composite1);

                src.RaiseEventA();
                Assert.IsTrue(sinkA.AEventReceived, "Event A not received by sinkA");
                Assert.IsTrue(composite1.AEventReceived, "Event A not received by composite1");

                composite1.RaiseEventB();
                Assert.IsTrue(sinkA.BEventReceived);
            }
        }

        [TestMethod()]
        public void EventSourceNameCacheTest()
        {
            int start = Environment.TickCount;
            string[] preCache = ObjectFactory.GetEventSourceNames(typeof(MockEventSource));
            int et1 = Environment.TickCount - start;

            start = Environment.TickCount;
            string[] postCache = ObjectFactory.GetEventSourceNames(typeof(MockEventSource));
            int et2 = Environment.TickCount - start;

            Assert.AreEqual<int>(preCache.Length, postCache.Length, "Lengths not equal");
            Assert.AreEqual<int>(0, preCache.Except(postCache).Count(), "preCache contains values not in postCache");
            Assert.AreEqual<int>(0, postCache.Except(preCache).Count(), "postCache contains values not in preCache");
            Assert.IsTrue(et2 < et1, "Cached retrieval was not faster");

            // uncomment to see actual times
            // Assert.Fail(string.Format("{0} {1}", et1, et2));
        }

        [TestMethod()]
        public void EventSourceTypeCacheTest()
        {
            int start = Environment.TickCount;
            ObjectFactory.PublicationDescriptor[] preCache = ObjectFactory.GetEventSources(typeof(MockEventSource));
            int et1 = Environment.TickCount - start;

            start = Environment.TickCount;
            ObjectFactory.PublicationDescriptor[] postCache = ObjectFactory.GetEventSources(typeof(MockEventSource));
            int et2 = Environment.TickCount - start;

            Assert.AreEqual<int>(preCache.Length, postCache.Length, "Lengths not equal");
            Assert.AreEqual<int>(0, preCache.Except(postCache).Count(), "preCache contains values not in postCache");
            Assert.AreEqual<int>(0, postCache.Except(preCache).Count(), "postCache contains values not in preCache");
            Assert.IsTrue(et2 < et1, "Cached retrieval was not faster");

            // uncomment to see actual times
            // Assert.Fail(string.Format("{0} {1}", et1, et2));
        }

        [TestMethod()]
        public void EventSinkTypeCacheTest()
        {
            int start = Environment.TickCount;
            ObjectFactory.SubscriptionDescriptor[] preCache = ObjectFactory.GetEventSinks(typeof(MockEventSink));
            int et1 = Environment.TickCount - start;

            start = Environment.TickCount;
            ObjectFactory.SubscriptionDescriptor[] postCache = ObjectFactory.GetEventSinks(typeof(MockEventSink));
            int et2 = Environment.TickCount - start;

            Assert.AreEqual<int>(preCache.Length, postCache.Length, "Lengths not equal");
            Assert.AreEqual<int>(0, preCache.Except(postCache).Count(), "preCache contains values not in postCache");
            Assert.AreEqual<int>(0, postCache.Except(preCache).Count(), "postCache contains values not in preCache");
            Assert.IsTrue(et2 < et1, "Cached retrieval was not faster");

            // uncomment to see actual times
            // Assert.Fail(string.Format("{0} {1}", et1, et2));
        }

        [TestMethod()]
        public void ObjectCreationUsingCacheTestA()
        {
            using(WorkItem wi = new WorkItem())
            {
                int start = Environment.TickCount;
                MockEventComposite1 preCache = wi.Items.AddNew<MockEventComposite1>();
                int et1 = Environment.TickCount - start;
                Assert.IsNotNull(preCache, "preCache is null");

                start = Environment.TickCount;
                MockEventComposite1 postCache = wi.Items.AddNew<MockEventComposite1>();
                int et2 = Environment.TickCount - start;
                Assert.IsNotNull(postCache, "postCache is null");

                Assert.IsTrue(et2 < et1, string.Format("Cached retrieval was not faster - uncached: {0}, cached: {1}", et1, et2));

                // uncomment to see actual times
                // Assert.Fail(string.Format("{0} {1}", et1, et2));
            }
        }
    }
}
