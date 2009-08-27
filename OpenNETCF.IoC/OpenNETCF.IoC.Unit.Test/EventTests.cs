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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OpenNETCF.IoC.Unit.Test
{
    [TestClass()]
    public class EventTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        [Description("Ensures that events get dispatched when the publisher is created before the subscriber")]
        public void PublisherBeforeSubscriberTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource source = wi.Items.AddNew<MockEventSource>("PublisherBeforeSubscriberTestSource");
                MockEventSink sink = wi.Items.AddNew<MockEventSink>("PublisherBeforeSubscriberTestSink");

                sink.AEventReceived = false;
                source.RaiseEventA();

                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        [Description("Ensures that events get dispatched when the subscriber is created before the publisher")]
        public void SubscriberBeforePublisherTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = wi.Items.AddNew<MockEventSink>("PublisherBeforeSubscriberTestSink");
                MockEventSource source = wi.Items.AddNew<MockEventSource>("PublisherBeforeSubscriberTestSource");

                sink.AEventReceived = false;
                source.RaiseEventA();

                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void InterClassEventTest1()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource src = wi.Items.AddNew<MockEventSource>();
                MockEventSink sinkA = wi.Items.AddNew<MockEventSink>();
                MockEventComposite1 composite1 = wi.Items.AddNew<MockEventComposite1>();

                src.RaiseEventA();
                Assert.IsTrue(sinkA.AEventReceived, "Event A not received by sinkA");
                Assert.IsTrue(composite1.AEventReceived, "Event A not received by composite1");

                composite1.RaiseEventB();
                Assert.IsTrue(sinkA.BEventReceived, "B Not Received by sinkA");
            }
        }

        [TestMethod()]
        public void InterClassEventTest2()
        {
            using (WorkItem wi = new WorkItem())
            {
                // test same stuff, but different creation order
                MockEventSink sink = wi.Items.AddNew<MockEventSink>();
                MockEventComposite1 composite1 = wi.Items.AddNew<MockEventComposite1>();
                MockEventSource src = wi.Items.AddNew<MockEventSource>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "A Not Received by sink");
                Assert.IsTrue(composite1.AEventReceived, "A Not Received by composite");

                composite1.RaiseEventB();
                Assert.IsTrue(sink.BEventReceived, "B Not Received by sink");
            }
        }

        [TestMethod()]
        public void ServiceToItemSrcFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource src = wi.Services.AddNew<MockEventSource>();
                MockEventSink sink = wi.Items.AddNew<MockEventSink>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void ServiceToItemSinkFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = wi.Items.AddNew<MockEventSink>();
                MockEventSource src = wi.Services.AddNew<MockEventSource>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void ItemToServiceSrcFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource src = wi.Items.AddNew<MockEventSource>();
                MockEventSink sink = wi.Services.AddNew<MockEventSink>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void ItemToServiceSinkFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = wi.Services.AddNew<MockEventSink>();
                MockEventSource src = wi.Items.AddNew<MockEventSource>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }
        
        [TestMethod()]
        public void WorkItemToServiceSrcFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource src = wi.WorkItems.AddNew<MockEventSource>();
                MockEventSink sink = wi.Services.AddNew<MockEventSink>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void WorkItemToServiceSinkFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = wi.Services.AddNew<MockEventSink>();
                MockEventSource src = wi.WorkItems.AddNew<MockEventSource>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void ServiceToWorkItemSrcFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource src = wi.Services.AddNew<MockEventSource>();
                MockEventSink sink = wi.WorkItems.AddNew<MockEventSink>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void ServiceToWorkItemSinkFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = wi.WorkItems.AddNew<MockEventSink>();
                MockEventSource src = wi.Services.AddNew<MockEventSource>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void WorkItemToItemSrcFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource src = wi.WorkItems.AddNew<MockEventSource>();
                MockEventSink sink = wi.Items.AddNew<MockEventSink>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void WorkItemToItemSinkFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = wi.Items.AddNew<MockEventSink>();
                MockEventSource src = wi.WorkItems.AddNew<MockEventSource>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void ItemToWorkItemSrcFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSource src = wi.Items.AddNew<MockEventSource>();
                MockEventSink sink = wi.WorkItems.AddNew<MockEventSink>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void ItemToWorkItemSinkFirstEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = wi.WorkItems.AddNew<MockEventSink>();
                MockEventSource src = wi.Items.AddNew<MockEventSource>();

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        public void SiblingEventTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = new MockEventSink();
                MockEventSource src = new MockEventSource();

                wi.Items.Add(src);
                wi.Items.Add(sink);

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        [Description("Ensures that a parent can send an event to a child when the child was added to the parent *after* the parent was added to a root workitem")]
        public void ParentToChildEventTestA()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = new MockEventSink();
                MockEventSource src = new MockEventSource();

                wi.WorkItems.Add(src);
                src.WorkItems.Add(sink);

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }

        [TestMethod()]
        [Description("Ensures that a parent can send an event to a child when the child was added to the parent *after* the parent was added to a root workitem")]
        public void ParentToChildEventTestB()
        {
            using (WorkItem wi = new WorkItem())
            {
                MockEventSink sink = new MockEventSink();
                MockEventSource src = new MockEventSource();

                src.WorkItems.Add(sink);
                wi.WorkItems.Add(src);

                src.RaiseEventA();
                Assert.IsTrue(sink.AEventReceived, "Event A not received by sink");
            }
        }
    }
}
