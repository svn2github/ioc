using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Practices.ServiceLocation;

namespace OpenNETCF.IoC.Unit.Test
{
    [TestClass()]
    public class CSLTest
    {
        private TypeA a1;
        private TypeA a2;
        private TypeB b1;

        public WorkItem Initialize()
        {
            WorkItem w = new WorkItem();

            a1 = w.Items.AddNew<TypeA>("A1");
            a2 = w.Items.AddNew<TypeA>("A2");
            b1 = w.Items.AddNew<TypeB>("B1");

            return w;
        }

        [TestMethod()]
        [Description("Ensures GetInstance returns a default object when no key is provided")]
        public void GetInstanceNoKeyTest()
        {
            using (WorkItem root = Initialize())
            {
                OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(root);
                var instance = locator.GetInstance<TypeA>();
                Assert.IsNotNull(instance, "GetInstance returned null");
                Assert.AreEqual<TypeA>(a1, instance, "GetInstance returned an unexpected instance");
            }
        }

        [TestMethod()]
        [Description("Ensures GetInstance returns an object when a key is provided")]
        public void GetInstanceWithKeyTest()
        {
            using (WorkItem root = Initialize())
            {
                OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(root);
                var instance = locator.GetInstance<TypeA>("A2");
                Assert.IsNotNull(instance, "GetInstance returned null");
                Assert.AreEqual<TypeA>(a2, instance, "GetInstance returned an unexpected instance");
            }
        }

        [TestMethod()]
        [Description("Ensures GetInstance throws when a key is provided with no matching object")]
        [ExpectedException(typeof(ActivationException))]
        public void GetInstanceWithKeyNoObjectTest()
        {
            using (WorkItem root = Initialize())
            {
                OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(root);
                var instance = locator.GetInstance<TypeA>("A3");
            }
        }

        [TestMethod()]
        [Description("Ensures GetInstance can find a child of a base type")]
        public void GetInstanceByBaseTypeTest()
        {
            using (WorkItem root = Initialize())
            {
                OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(root);
                var instance = locator.GetInstance<BaseType>("A1");
                Assert.IsNotNull(instance, "GetInstance returned null");
                Assert.AreEqual<BaseType>(a1, instance, "GetInstance returned an unexpected instance");
            }
        }

        [TestMethod()]
        [Description("Ensures GetInstance by ID gets a valid instance when it's not the first in the object list")]
        public void GetInstanceByByIDTest2()
        {
            using (WorkItem root = Initialize())
            {
                OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(root);
                var instance = locator.GetInstance<TypeB>("B1");
                Assert.IsNotNull(instance, "GetInstance returned null");
                Assert.AreEqual<TypeB>(b1, instance, "GetInstance returned an unexpected instance");
            }
        }

        [TestMethod()]
        [Description("Ensures GetInstance throws when the type provided doesn't exist in the list")]
        [ExpectedException(typeof(ActivationException))]
        public void GetObjectWhenTypeIsNotInListTest()
        {
            using (WorkItem root = Initialize())
            {
                OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(root);
                var instance = locator.GetInstance<TypeC>();
            }
        }

        [TestMethod()]
        [Description("Ensures GetInstance can get a list of instances by type")]
        public void GetAllInstancesBySpecificTypeTest()
        {
            using (WorkItem root = Initialize())
            {
                OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(root);
                var aList = locator.GetAllInstances<TypeA>();
                Assert.IsNotNull(aList, "GetAllInstances returned null");
                Assert.AreEqual<int>(2, aList.Count(), "GetAllInstances returned an unexpected sized list");

                foreach (var i in aList)
                {
                    Assert.IsTrue(i is TypeA, "GetAllInstances item is of unexpected type");
                }
            }
        }

        [TestMethod()]
        [Description("Ensures GetInstance can get a list of instances that derive from a single base type")]
        public void GetAllInstancesByBaseTypeTest()
        {
            using (WorkItem root = Initialize())
            {
                OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(root);
                var aList = locator.GetAllInstances<BaseType>();
                Assert.IsNotNull(aList, "GetAllInstances returned null");
                Assert.AreEqual<int>(3, aList.Count(), "GetAllInstances returned an unexpected sized list");

                foreach (var i in aList)
                {
                    Assert.IsTrue(i is BaseType, "GetAllInstances item is of unexpected type");
                }
            }
        }
    }

    abstract class BaseType
    {
        private int m_instanceNumber = 0;

        protected abstract int InstanceCount { get; set; }

        protected BaseType()
        {
            m_instanceNumber = ++InstanceCount;
        }

        public string Name
        {
            get { return string.Format("{0} #{1}", this.GetType().Name, m_instanceNumber); }
        }

        public override string ToString()
        {
            return Name;
        }
    }

    class TypeA : BaseType
    {
        private static int m_instanceCount = 0;

        protected override int InstanceCount
        {
            get { return m_instanceCount; }
            set { m_instanceCount = value; }
        }
    }

    class TypeB : BaseType
    {
        private static int m_instanceCount = 0;

        protected override int InstanceCount
        {
            get { return m_instanceCount; }
            set { m_instanceCount = value; }
        }
    }

    class TypeC : BaseType
    {
        private static int m_instanceCount = 0;

        protected override int InstanceCount
        {
            get { return m_instanceCount; }
            set { m_instanceCount = value; }
        }
    }
}
