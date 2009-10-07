using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC;

namespace CommonServiceLocator.Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            object instance;

            RootWorkItem.Items.AddNew<TypeA>("A1");
            RootWorkItem.Items.AddNew<TypeA>("A2");
            RootWorkItem.Items.AddNew<TypeB>("B1");

            OpenNETCFServiceLocator locator = new OpenNETCFServiceLocator(RootWorkItem.Instance);
            instance = locator.GetInstance<TypeA>();
            instance = locator.GetInstance<TypeA>("A2");
            instance = locator.GetInstance<TypeA>("A3");
            instance = locator.GetInstance<BaseType>("A1");
            instance = locator.GetInstance<TypeB>("B1");
            instance = locator.GetInstance<TypeC>();
            var aList = locator.GetAllInstances<TypeA>();
            var baseList = locator.GetAllInstances<BaseType>();
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
