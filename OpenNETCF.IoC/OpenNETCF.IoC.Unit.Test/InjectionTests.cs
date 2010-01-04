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
using System.Threading;
using System;

namespace OpenNETCF.IoC.Unit.Test
{
    [TestClass()]
    public class InjectionTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void TestInjectionMethodPositive()
        {
            WorkItem root = new WorkItem();
            string bid = "BID";
            string did = "DID";
            root.Items.AddNew<MockTypeB>(bid);

            root.Items.AddNew<MockTypeD>(did);
            MockTypeD d = root.Items[did] as MockTypeD;
            MockTypeB b = root.Items[bid] as MockTypeB;

            Assert.IsNotNull(d, "Creation of D failed");
            Assert.IsNotNull(d.B, "Injected item is null");
            Assert.AreEqual(b, d.B, "Injected item is incorrect");
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void InjectionMethodNullInjectionTypeTest()
        {
            WorkItem root = new WorkItem();
            string did = "DID";
            root.Items.AddNew<MockTypeD>(did);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void ValueTypeInjectionMethodTest()
        {
            WorkItem root = new WorkItem();
            root.Items.AddNew<InvalidInjectionMethodObject>();
        }

        [TestMethod()]
        public void TestInjectionCtorPositive()
        {
            WorkItem root = new WorkItem();
            string aid = "AID";
            string cid = "CID";
            root.Items.AddNew<MockTypeA>(aid);

            root.Items.AddNew<MockTypeC>(cid);
            MockTypeC c = root.Items[cid] as MockTypeC;
            MockTypeA a = root.Items[aid] as MockTypeA;

            Assert.IsNotNull(c, "Creation of C failed");
            Assert.IsNotNull(c.A, "Injected item is null");
            Assert.AreEqual(a, c.A, "Injected item is incorrect");
        }

        [TestMethod()]
        public void ServiceDependencySetterInjectionWithExistingServiceTest()
        {
            WorkItem root = new WorkItem();
            root.Services.AddNew<MockTypeA>();
            root.Items.AddNew<ServiceConsumerA>("ida");

            ServiceConsumerA a = root.Items["ida"] as ServiceConsumerA;
            Assert.IsTrue(root.Items.Contains("ida"), "WorkItem not in collection");
            Assert.AreEqual(a.AService, root.Services.Get<MockTypeA>(), "Injected item is incorrect");
        }

        [TestMethod()]
        public void ServiceDependencySetterInjectionWithExistingAliasedServiceTest()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddNew<MockTypeA, IMockType>();
                root.Items.AddNew<ServiceConsumerAI>("idb");

                ServiceConsumerAI a = root.Items["idb"] as ServiceConsumerAI;
                Assert.IsTrue(root.Items.Contains("idb"), "WorkItem not in collection");
                Assert.IsFalse(root.Services.Contains<MockTypeA>(), "Alias failure");
                Assert.AreEqual(a.AService, root.Services.Get<IMockType>(), "Injected item is incorrect");
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ServiceMissingException))]
        public void ServiceDependencySetterInjectionWithoutExistingServiceTest()
        {
            WorkItem root = new WorkItem();
            root.Items.AddNew<ServiceConsumerA>("idc");
        }

        [TestMethod()]
        public void ServiceDependencySetterInjectionAutoCreateServiceTest()
        {
            WorkItem root = new WorkItem();

            root.Items.AddNew<ServiceConsumerB>("idd");
            ServiceConsumerB b = root.Items["idd"] as ServiceConsumerB;
            Assert.IsNotNull(b, "WorkItem creation failed");
            Assert.IsTrue(root.Services.Contains<MockTypeB>(), "Injected service does not exist");
            Assert.AreEqual(b.BService, root.Services.Get<MockTypeB>(), "Injected item is incorrect");
        }

        [TestMethod()]
        public void ServiceDependencyCtorInjectionWithExistingServiceTest()
        {
            WorkItem root = new WorkItem();
            root.Services.AddNew<MockTypeA>();
            root.Items.AddNew<ServiceConsumerC>("id");

            ServiceConsumerC svc = root.Items["id"] as ServiceConsumerC;
            Assert.IsTrue(root.Items.Contains("id"), "WorkItem not in collection");
            Assert.AreEqual(svc.AService, root.Services.Get<MockTypeA>(), "Injected item is incorrect");
        }

        [TestMethod()]
        public void ServiceDependencyCtorInjectionWithExistingAliasedServiceTest()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddNew<MockTypeA, IMockType>();
                root.Items.AddNew<ServiceConsumerCI>("id");

                ServiceConsumerCI svc = root.Items["id"] as ServiceConsumerCI;
                Assert.IsTrue(root.Items.Contains("id"), "WorkItem not in collection");
                Assert.IsFalse(root.Services.Contains<MockTypeA>(), "Alias failure");
                Assert.AreEqual(svc.AService, root.Services.Get<IMockType>(), "Injected item is incorrect");
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ServiceMissingException))]
        public void ServiceDependencyCtorInjectionWithoutExistingServiceTest()
        {
            WorkItem root = new WorkItem();
            root.Items.AddNew<ServiceConsumerC>("id");
        }

        [TestMethod()]
        public void ServiceDependencyCtorInjectionAutoCreateServiceTest()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Items.AddNew<ServiceConsumerD>("id");
                ServiceConsumerD svc = root.Items["id"] as ServiceConsumerD;
                Assert.IsNotNull(svc, "WorkItem creation failed");
                Assert.IsTrue(root.Services.Contains<MockTypeB>(), "Injected service does not exist");
                Assert.AreEqual(svc.BService, root.Services.Get<MockTypeB>(), "Injected item is incorrect");
            }
        }
    }
}
