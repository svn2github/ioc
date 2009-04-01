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
            Assert.AreEqual(1, ObjectFactory.GetEventSinksFromTypeByName(type, "IoC Event A").Length,
                "Unexpected number of methods returned");

            Assert.AreEqual(1, ObjectFactory.GetEventSinksFromTypeByName(type, "IoC Event B").Length,
                "Unexpected number of methods returned");

            Assert.AreEqual(0, ObjectFactory.GetEventSinksFromTypeByName(type, "Unknown").Length,
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
        public void InterClassEventTest1()
        {
            MockEventSource src = RootWorkItem.Items.AddNew<MockEventSource>();
            MockEventSink sinkA = RootWorkItem.Items.AddNew<MockEventSink>();
            MockEventComposite1 composite1 = RootWorkItem.Items.AddNew<MockEventComposite1>();

            src.RaiseEventA();
            Assert.IsTrue(sinkA.AEventReceived, "Event A not received by sinkA");
            Assert.IsTrue(composite1.AEventReceived, "Event A not received by composite1");

            composite1.RaiseEventB();
            Assert.IsTrue(sinkA.BEventReceived);

            RootWorkItem.Items.Remove(src);
            RootWorkItem.Items.Remove(sinkA);
            RootWorkItem.Items.Remove(composite1);
        }

        [TestMethod()]
        public void InterClassEventTest2()
        {
            // test same stuff, but different creation order
            MockEventSink sink = RootWorkItem.Items.AddNew<MockEventSink>();
            MockEventComposite1 composite1 = RootWorkItem.Items.AddNew<MockEventComposite1>();
            MockEventSource src = RootWorkItem.Items.AddNew<MockEventSource>();

            src.RaiseEventA();
            Assert.IsTrue(sink.AEventReceived);
            Assert.IsTrue(composite1.AEventReceived);

            composite1.RaiseEventB();
            Assert.IsTrue(sink.BEventReceived);

            RootWorkItem.Items.Remove(src);
            RootWorkItem.Items.Remove(sink);
            RootWorkItem.Items.Remove(composite1);
        }
    }
}
