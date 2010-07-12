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
using System.Collections.Generic;
using System;

namespace OpenNETCF.IoC.Unit.Test
{       
    [TestClass()]
    public class ServiceCollectionTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void AddOnDemandTestPositive()
        {
            WorkItem root = new WorkItem();
            ServiceCollection services = new ServiceCollection(root);

            var serviceCount = services.GetInstanciatedServiceCount();

            services.AddOnDemand<MockTypeA>();

            // service count should not change
            Assert.AreEqual(serviceCount, services.GetInstanciatedServiceCount());

            MockTypeA o = services.Get<MockTypeA>();

            Assert.AreEqual(serviceCount + 1, services.GetInstanciatedServiceCount());
            Assert.IsNotNull(o);
        }

        [TestMethod()]
        public void AddNewTestPositive()
        {
            WorkItem root = new WorkItem();
            ServiceCollection services = new ServiceCollection(root);

            services.AddNew<MockTypeA>();

            Assert.AreEqual(1, services.GetInstanciatedServiceCount());

            MockTypeA o = services.Get<MockTypeA>();

            Assert.IsNotNull(o);
        }

        [TestMethod()]
        public void GetUnregisteredServiceNoExceptionTestPositive1()
        {
            WorkItem root = new WorkItem();
            ServiceCollection services = new ServiceCollection(root);

            MockTypeA o = services.Get<MockTypeA>();

            Assert.IsNull(o);
        }

        [TestMethod()]
        public void GetUnregisteredServiceNoExceptionTestPositive2()
        {
            WorkItem root = new WorkItem();
            ServiceCollection services = new ServiceCollection(root);

            MockTypeA o = services.Get<MockTypeA>(false);

            Assert.IsNull(o);
        }

        [TestMethod()]
        [ExpectedException(typeof(ServiceMissingException))]
        public void GetUnregisteredServiceWithExceptionTestPositive()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                MockTypeA o = services.Get<MockTypeA>(true);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(RegistrationTypeInUseException))]
        public void AddOnDemandDuplicateRegistrationTypeTest1()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddOnDemand<MockTypeA, IMockType>();
                root.Services.AddOnDemand<MockTypeB, IMockType>();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(RegistrationTypeInUseException))]
        public void AddOnDemandDuplicateRegistrationTypeTest2()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddOnDemand<MockTypeA>();
                root.Services.AddOnDemand<MockTypeA>();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(RegistrationTypeInUseException))]
        public void AddNewDuplicateRegistrationTypeTest1()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddNew<MockTypeA, IMockType>();
                root.Services.AddNew<MockTypeB, IMockType>();
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(RegistrationTypeInUseException))]
        public void AddNewDuplicateRegistrationTypeTest2()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddNew<MockTypeA>();
                root.Services.AddNew<MockTypeA>();
            }
        }

        [TestMethod()]
        public void AddOnDemand2TypesPositive()
        {
            using (WorkItem root = new WorkItem())
            {

                ServiceCollection services = new ServiceCollection(root);

                Assert.IsNotNull(services, "ServiceCollection not created");

                services.AddOnDemand<MockTypeA>();
                Assert.AreEqual(0, services.GetInstanciatedServiceCount(), "First type add failed");

                services.AddOnDemand<MockTypeB>();
                Assert.AreEqual(0, services.GetInstanciatedServiceCount(), "Second type add failed");

                MockTypeA a = services.Get<MockTypeA>();
                Assert.IsNotNull(a, "did not retrieve MockTypeA");
                Assert.AreEqual(1, services.GetInstanciatedServiceCount());
                Assert.IsNotNull(a, "First type created failed");

                MockTypeB b = services.Get<MockTypeB>();
                Assert.IsNotNull(b, "did not retrieve MockTypeA");
                Assert.AreEqual(2, services.GetInstanciatedServiceCount());
                Assert.IsNotNull(b, "Second type created failed");
            }
        }

        [TestMethod()]
        public void AddNew2TypesPositive()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                services.AddNew<MockTypeA>();
                Assert.AreEqual(1, services.GetInstanciatedServiceCount(), "First type add failed");

                services.AddNew<MockTypeB>();
                Assert.AreEqual(2, services.GetInstanciatedServiceCount(), "Second type add failed");
            }
        }

        [TestMethod()]
        public void GetClassTwiceReturnsSameObjectTestPositive1()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                services.AddNew<MockTypeA>();
                services.AddNew<MockTypeB>();

                MockTypeA a1 = services.Get<MockTypeA>();
                MockTypeA a2 = services.Get<MockTypeA>();

                Assert.AreEqual(a1, a2, "instances not equal");
            }
        }

        [TestMethod()]
        public void GetClassTwiceReturnsSameObjectTestPositive2()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                services.AddOnDemand<MockTypeA>();
                services.AddOnDemand<MockTypeB>();

                MockTypeA a1 = services.Get<MockTypeA>();
                MockTypeA a2 = services.Get<MockTypeA>();

                Assert.AreEqual(a1, a2, "instances not equal");
            }
        }

        [TestMethod()]
        public void AddNewContainsTestPositive()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                services.AddNew<MockTypeA>();
                services.AddNew<MockTypeB>();

                MockTypeA a1 = services.Get<MockTypeA>();
                MockTypeA a2 = services.Get<MockTypeA>();

                Assert.AreEqual(a1, a2, "instances not equal");
            }
        }

        [TestMethod()]
        public void GenericRemoveTestPositive()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                services.AddNew<MockTypeA>();
                services.AddNew<MockTypeB>();
                Assert.AreEqual(2, services.GetInstanciatedServiceCount(), "incorrect number of initial services");

                services.Remove<MockTypeA>();
                Assert.AreEqual(1, services.GetInstanciatedServiceCount(), "Remove failure");
            }
        }

        [TestMethod()]
        public void InstanceRemoveTestPositive()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                services.AddNew<MockTypeA>();
                services.AddNew<MockTypeB>();
                Assert.AreEqual(2, services.GetInstanciatedServiceCount(), "incorrect number of initial services");

                MockTypeB b = services.Get<MockTypeB>();

                services.Remove(b);
                Assert.AreEqual(1, services.GetInstanciatedServiceCount(), "Remove failure");
            }
        }

        [TestMethod()]
        public void RemoveIDisposableTestPositive()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                services.AddNew<MockTypeA>();
                services.AddNew<MockTypeB>();
                Assert.AreEqual(2, services.GetInstanciatedServiceCount(), "incorrect number of initial services");

                MockTypeB b = services.Get<MockTypeB>();
                AutoResetEvent are = new AutoResetEvent(false);
                b.Disposed += new System.EventHandler(
                    delegate
                    {
                        are.Set();
                    });
                services.Remove(b);
                Assert.IsTrue(are.WaitOne(100, false));
            }
        }

        [TestMethod()]
        public void AddedEventTestPositive()
        {
            using (WorkItem root = new WorkItem())
            {
                ServiceCollection services = new ServiceCollection(root);

                AutoResetEvent are = new AutoResetEvent(false);
                services.Added += new System.EventHandler<DataEventArgs<object>>(
                    delegate
                    {
                        are.Set();
                    });

                services.AddNew<MockTypeA>();
                Assert.IsTrue(are.WaitOne(100, false));
            }
        }

        [TestMethod()]
        public void RemovedEventTestPositive()
        {
            WorkItem root = new WorkItem();
            ServiceCollection services = new ServiceCollection(root);

            services.AddNew<MockTypeA>();

            AutoResetEvent are = new AutoResetEvent(false);
            services.Removed += new System.EventHandler<DataEventArgs<object>>(
                delegate
                {
                    are.Set();
                });
            services.Remove<MockTypeA>();
            Assert.IsTrue(are.WaitOne(100, false));
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewClassDerivesFromRegisteredTypeTest()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddNew(typeof(MockTypeB), typeof(MockTypeA));
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddNewClassDerivesFromRegisteredInterfaceNegativeTest()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddNew(typeof(MockTypeB), typeof(IEnumerable<object>));
            }
        }

        [TestMethod()]
        public void AddNewClassDerivesFromRegisteredInterfacePositiveTest()
        {
            using (WorkItem root = new WorkItem())
            {
                root.Services.AddNew(typeof(MockTypeB), typeof(IMockType));
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddClassDerivesFromRegisteredTypeTest()
        {
            using (WorkItem root = new WorkItem())
            {
                MockTypeB b = new MockTypeB();
                root.Services.Add(typeof(MockTypeC), b);
            }
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AddClassDerivesFromRegisteredInterfaceNegativeTest()
        {
            using (WorkItem root = new WorkItem())
            {
                MockTypeB b = new MockTypeB();
                root.Services.Add(typeof(IEnumerable<object>), b);
            }
        }

        [TestMethod()]
        public void AddClassDerivesFromRegisteredInterfacePositiveTest()
        {
            using (WorkItem root = new WorkItem())
            {
                MockTypeB b = new MockTypeB();
                root.Services.Add(typeof(IMockType), b);
            }
        }

    }
}
