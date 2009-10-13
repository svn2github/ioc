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
using System.Threading;

namespace OpenNETCF.IoC.Unit.Test
{
    [TestClass()]
    public class DisposableWrapperTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        [Description("Ensures that a basic DisposableWrappedObject raises an event when disposed and that the underlying object gets disposed")]
        public void BasicDisposeTest()
        {
            TestDisposableObject instance = new TestDisposableObject();
            AutoResetEvent are = new AutoResetEvent(false);

            DisposableWrappedObject<TestDisposableObject> @object = new DisposableWrappedObject<TestDisposableObject>(instance);
            @object.Disposing += delegate(object sender, GenericEventArgs<IDisposable> e)
            {
                are.Set();
            };

            @object.Dispose();
            Assert.IsTrue(are.WaitOne(1000, false), "Disposing did not fire");

            Assert.IsTrue(instance.Disposed, "Underlying Object is not Disposed");
        }

        [TestMethod()]
        [Description("Ensures that calling AddNewDisposable with no ID creates a DisposableWrappedObject<IDisposable> instance")]
        public void ItemsAddNewDisposableNoIDTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                object @object = wi.Items.AddNewDisposable(typeof(TestDisposableObject));
                Assert.IsNotNull(@object, "AddNewDisposable failed to create an object");
                Assert.IsTrue(@object is DisposableWrappedObject<IDisposable>, "AddNewDisposable created an object of unexpected type");
            }
        }

        [TestMethod()]
        [Description("Ensures that calling AddNewDisposable with an ID creates a DisposableWrappedObject<IDisposable> instance")]
        public void ItemsAddNewDisposableWithIDTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                object @object = wi.Items.AddNewDisposable(typeof(TestDisposableObject), "MyID");
                Assert.IsNotNull(@object, "AddNewDisposable failed to create an object");
                Assert.IsTrue(@object is DisposableWrappedObject<IDisposable>, "AddNewDisposable created an object of unexpected type");

                object retrieved = wi.Items.Get("MyID");
                Assert.IsNotNull(retrieved, "Retrieved object was null");
                Assert.AreEqual(@object, retrieved, "Retrieved object was unexpected instance");
            }
        }

        [TestMethod()]
        [Description("Ensures that an object created with AddNewDisposable gets removed from the Items collection when Disposed")]
        public void ItemsAddNewDisposableDisposeFromListTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                DisposableWrappedObject<IDisposable> @object = wi.Items.AddNewDisposable(typeof(TestDisposableObject), "MyID") as DisposableWrappedObject<IDisposable>;
                Assert.AreEqual<int>(1, wi.Items.Count(), "Unexpected count after add");

                AutoResetEvent are = new AutoResetEvent(false);

                @object.Disposing += delegate
                {
                    are.Set();
                };

                @object.Dispose();

                are.WaitOne(1000, false);

                Assert.AreEqual<int>(0, wi.Items.Count(), "Unexpected count after Dispose");
            }
        }

        [TestMethod()]
        [Description("Ensures that calling AddNewDisposable<> with no ID creates a DisposableWrappedObject<IDisposable> instance")]
        public void ItemsGenericAddNewDisposableNoIDTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                var @object = wi.Items.AddNewDisposable<TestDisposableObject>();
                Assert.IsNotNull(@object, "AddNewDisposable failed to create an object");
                Assert.IsTrue(@object is DisposableWrappedObject<IDisposable>, "AddNewDisposable created an object of unexpected type");
            }
        }

        [TestMethod()]
        [Description("Ensures that calling AddNewDisposable<> with an ID creates a DisposableWrappedObject<IDisposable> instance")]
        public void ItemsGenericAddNewDisposableWithIDTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                object @object = wi.Items.AddNewDisposable<TestDisposableObject>("MyID");
                Assert.IsNotNull(@object, "AddNewDisposable failed to create an object");
                Assert.IsTrue(@object is DisposableWrappedObject<IDisposable>, "AddNewDisposable created an object of unexpected type");

                object retrieved = wi.Items.Get("MyID");
                Assert.IsNotNull(retrieved, "Retrieved object was null");
                Assert.AreEqual(@object, retrieved, "Retrieved object was unexpected instance");
            }
        }

        [TestMethod()]
        [Description("Ensures that an object created with AddNewDisposable<> gets removed from the Items collection when Disposed")]
        public void ItemsGenericAddNewDisposableDisposeFromListTest()
        {
            using (WorkItem wi = new WorkItem())
            {
                DisposableWrappedObject<IDisposable> @object = wi.Items.AddNewDisposable<TestDisposableObject>("MyID");
                Assert.AreEqual<int>(1, wi.Items.Count(), "Unexpected count after add");

                AutoResetEvent are = new AutoResetEvent(false);

                @object.Disposing += delegate
                {
                    are.Set();
                };

                @object.Dispose();

                are.WaitOne(1000, false);

                Assert.AreEqual<int>(0, wi.Items.Count(), "Unexpected count after Dispose");
            }
        }
    }

    internal class TestDisposableObject : IDisposable
    {
        public bool Disposed { get; private set; }

        public void Dispose()
        {
            Disposed = true;
        }
    }

}
