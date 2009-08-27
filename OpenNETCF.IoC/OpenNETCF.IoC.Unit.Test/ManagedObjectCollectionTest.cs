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
    public class ManagedObjectCollectionTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void AddNewNoIDTestPositive()
        {
            WorkItem root = new WorkItem();
            ManagedObjectCollection<object> items = new ManagedObjectCollection<object>(root);

            items.AddNew<MockTypeA>();

            Assert.AreEqual(1, items.Count, "Incorrect count after add");
        }

        [TestMethod()]
        public void AddNewWithIDTestPositive()
        {
            WorkItem root = new WorkItem();
            ManagedObjectCollection<object> items = new ManagedObjectCollection<object>(root);

            string id = "test ID 1";
            
            items.AddNew<MockTypeB>(id);

            Assert.AreEqual(1, items.Count, "Incorrect count after add");

            MockTypeB a1 = items.Get<MockTypeB>(id);
            Assert.IsNotNull(a1, "Get didn't return the instance");

            MockTypeB a2 = items.Get<MockTypeB>("non-existent");
            Assert.IsNull(a2, "Get didn't return null");
        }

        [TestMethod()]
        public void AddNoIDTestPositive()
        {
            WorkItem root = new WorkItem();
            ManagedObjectCollection<object> items = new ManagedObjectCollection<object>(root);

            MockTypeA a = new MockTypeA();

            items.Add(a);

            Assert.AreEqual(1, items.Count, "Incorrect count after add");
        }

        [TestMethod()]
        public void AddWithIDTestPositive()
        {
            WorkItem root = new WorkItem();
            ManagedObjectCollection<object> items = new ManagedObjectCollection<object>(root);

            items.AddNew<MockTypeA>("test ID 1");

            Assert.AreEqual(1, items.Count, "Incorrect count after add");
        }

        [TestMethod()]
        public void GenericFindByTypeTestPositive()
        {
            WorkItem root = new WorkItem();
            ManagedObjectCollection<object> items = new ManagedObjectCollection<object>(root);

            items.AddNew<MockTypeA>("test ID 1");
            items.AddNew<MockTypeB>("test ID 2");
            items.AddNew<MockTypeB>("test ID 3");
            Assert.AreEqual(3, items.Count, "Incorrect count after add");

            Assert.AreEqual(1, items.FindByType<MockTypeA>().Count);
            Assert.AreEqual(2, items.FindByType<MockTypeB>().Count);
        }
        
        [TestMethod()]
        public void CreateNewAttributeCtorTestPositive()
        {
            WorkItem root = new WorkItem();
            root.Items.AddNew<TypeWithCreateNewAttribCtor>("id");

            TypeWithCreateNewAttribCtor i = root.Items["id"] as TypeWithCreateNewAttribCtor;
            Assert.IsNotNull(i, "creation failed");
            Assert.IsNotNull(i.B, "injection failed");
        }

        [TestMethod()]
        public void CreateNewAttributeMethodTestPositive()
        {
            WorkItem root = new WorkItem();
            root.Items.AddNew<TypeWithCreateNewAttribMethod>("id");

            TypeWithCreateNewAttribMethod i = root.Items["id"] as TypeWithCreateNewAttribMethod;
            Assert.IsNotNull(i, "creation failed");
            Assert.IsNotNull(i.B, "injection failed");
        }

        [TestMethod()]
        public void RemoveTestPositive()
        {
            WorkItem root = new WorkItem();

            root.Items.AddNew<MockTypeA>("test ID 1");
            root.Items.AddNew<MockTypeB>("test ID 2");
            root.Items.AddNew<MockTypeB>("test ID 3");
            Assert.AreEqual(3, root.Items.Count, "Incorrect count after add");

            object o = root.Items["test ID 2"];
            root.Items.Remove(o);
            Assert.AreEqual(1, root.Items.FindByType<MockTypeB>().Count);

            object foo = new object();
            root.Items.Remove(foo);
        }

        [TestMethod()]
        public void AddedEventTestPositive()
        {
            WorkItem root = new WorkItem();
            ManagedObjectCollection<object> items = new ManagedObjectCollection<object>(root);

            AutoResetEvent are = new AutoResetEvent(false);
            items.Added += new EventHandler<DataEventArgs<KeyValuePair<string,object>>>(
                delegate
                {
                    are.Set();
                });

            items.AddNew<MockTypeA>();
            Assert.IsTrue(are.WaitOne(100, false));
        }

        [TestMethod()]
        public void RemovedEventTestPositive()
        {
            WorkItem root = new WorkItem();
            ManagedObjectCollection<object> items = new ManagedObjectCollection<object>(root);

            string id = "test";
            items.AddNew<MockTypeA>(id);

            AutoResetEvent are = new AutoResetEvent(false);
            items.Removed += new EventHandler<DataEventArgs<KeyValuePair<string,object>>>(
                delegate
                {
                    are.Set();
                });
            items.Remove(items[id]);
            Assert.IsTrue(are.WaitOne(100, false));
        }
    }
}
