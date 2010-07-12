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
using OpenNETCF.IoC.UI;

namespace OpenNETCF.IoC.Unit.Test
{
    public interface IMockType
    {
        string Name { get; set; }
    }

    public class MockTypeA : IMockType
    {
        private static int m_instanceCount = 0;

        public MockTypeA()
        {
            m_instanceCount++;

            Name = "Type A - instance " + m_instanceCount;
        }

        public string Name { get; set; }
    }

    public class MockTypeB : IMockType, IDisposable
    {
        private static int m_instanceCount = 0;

        public event EventHandler Disposed;

        public MockTypeB()
        {
            m_instanceCount++;

            Name = "Type B - instance " + m_instanceCount;
        }

        public string Name { get; set; }

        public void Dispose()
        {
            if (Disposed != null)
            {
                Disposed(this, null);
            }
        }
    }

    public class MockTypeC : IMockType
    {
        private static int m_instanceCount = 0;

        [InjectionConstructor]
        public MockTypeC(MockTypeA a)
        {
            m_instanceCount++;
            A = a;
            Name = "Type C - instance " + m_instanceCount;
        }

        public MockTypeA A { get; set; }
        public string Name { get; set; }
    }

    public class MockTypeD : IMockType
    {
        private static int m_instanceCount = 0;

        public MockTypeD()
        {
            m_instanceCount++;

            Name = "Type D - instance " + m_instanceCount;
        }

        [InjectionMethod]
        void InjectB(MockTypeB b)
        {
            B = b;
        }

        public MockTypeB B { get; set; }
        public string Name { get; set; }
    }

    public class TypeWithCreateNewAttribCtor : IMockType
    {
        private static int m_instanceCount = 0;

        [InjectionConstructor]
        public TypeWithCreateNewAttribCtor([CreateNew]MockTypeB b)
        {
            m_instanceCount++;
            B = b;

            Name = "Type TypeWithCreateNewAttribCtor - instance " + m_instanceCount;
        }

        public MockTypeB B { get; set; }
        public string Name { get; set; }
    }

    public class ServiceConsumerA : IMockType
    {
        private static int m_instanceCount = 0;

        public ServiceConsumerA()
        {
            Name = "Type ServiceConsumerA - instance " + m_instanceCount;
        }

        [ServiceDependency]
        public MockTypeA AService { set; get; }

        public string Name { get; set; }
    }

    public class ServiceConsumerB : IMockType
    {
        private static int m_instanceCount = 0;

        public ServiceConsumerB()
        {
            Name = "Type ServiceConsumerB - instance " + m_instanceCount;
        }

        [ServiceDependency(EnsureExists=true)]
        public MockTypeB BService { set; get; }

        public string Name { get; set; }
    }

    public class ServiceConsumerAI : IMockType
    {
        private static int m_instanceCount = 0;

        public ServiceConsumerAI()
        {
            Name = "Type ServiceConsumerAI - instance " + ++m_instanceCount;
        }

        [ServiceDependency(RegistrationType=typeof(IMockType))]
        public MockTypeA AService { set; get; }

        public string Name { get; set; }
    }

    public class OnDemandServiceConsumer : IMockType
    {
        private static int m_instanceCount = 0;

        public OnDemandServiceConsumer()
        {
            Name = "Type OnDemandServiceConsumer - instance " + m_instanceCount;
        }

        [ServiceDependency(EnsureExists=false)]
        public MockTypeB BService { set; get; }

        public string Name { get; set; }
    }

    public class ServiceConsumerC : IMockType
    {
        private static int m_instanceCount = 0;

        [InjectionConstructor]
        public ServiceConsumerC([ServiceDependency]MockTypeA a)
        {
            AService = a;
            Name = "Type ServiceConsumerC - instance " + ++m_instanceCount;
        }
        
        public MockTypeA AService { set; get; }

        public string Name { get; set; }
    }

    public class ServiceConsumerCI : IMockType
    {
        private static int m_instanceCount = 0;

        [InjectionConstructor]
        public ServiceConsumerCI([ServiceDependency(RegistrationType=typeof(IMockType))]MockTypeA a)
        {
            AService = a;
            Name = "Type ServiceConsumerCI - instance " + ++m_instanceCount;
        }

        public MockTypeA AService { set; get; }

        public string Name { get; set; }
    }

    public class ServiceConsumerD : IMockType
    {
        private static int m_instanceCount = 0;

        [InjectionConstructor]
        public ServiceConsumerD([ServiceDependency(EnsureExists = true)]MockTypeB b)
        {
            BService = b;
            Name = "Type ServiceConsumerD - instance " + ++m_instanceCount;
        }
        
        public MockTypeB BService { set; get; }

        public string Name { get; set; }
    }

    public class TypeWithCreateNewAttribMethod : IMockType
    {
        private static int m_instanceCount = 0;

        public TypeWithCreateNewAttribMethod()
        {
            m_instanceCount++;

            Name = "Type TypeWithCreateNewAttribCtor - instance " + m_instanceCount;
        }

        [InjectionMethod]
        void InjectB([CreateNew]MockTypeB b)
        {
            B = b;
        }

        public MockTypeB B { get; set; }
        public string Name { get; set; }
    }

    public class InvalidInjectionMethodObject
    {
        public InvalidInjectionMethodObject()
        {
        }

        [InjectionMethod]
        public void BadInjector(int i)
        {
        }
    }

    public class TypeNoDefaultCtor
    {
        public TypeNoDefaultCtor(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }

    public class MockEventComposite1
    {
        public void RaiseEventB()
        {
            if (OnEventB == null) return;

            OnEventB(this, null);
        }

        [EventPublication("IoC Event B")]
        public event EventHandler OnEventB;

        [EventSubscription(EventNames.EventA, ThreadOption.Caller)]
        public void MyAHandler(object senter, EventArgs a)
        {
            AEventReceived = true;
        }

        public bool AEventReceived { get; set; }
    }

    public class MockEventSource : WorkItem
    {
        [EventPublication(EventNames.EventA)]
        public event EventHandler OnEventA;

        [EventPublication(EventNames.EventA)]
        public event EventHandler OnEventA2;

        [EventPublication("IoC Event B")]
        public event EventHandler OnEventB;

        [EventPublication("IoC Event C")]
        public event EventHandler OnEventC;

        public MockEventSource()
        {
        }

        public void RaiseEventA()
        {
            if (OnEventA == null) return;

            OnEventA(this, null);
        }
        
        public void RaiseEventB()
        {
            if (OnEventB == null) return;

            OnEventB(this, null);
        }

        public void RaiseEventC()
        {
            if (OnEventC == null) return;

            OnEventC(this, null);
        }
    }

    public class MockEventSink : WorkItem
    {
        public int ACount { get; set; }
        public int BCount { get; set; }

        public MockEventSink()
        {
            ACount = 0;
            BCount = 0;
            AEventReceived = false;
            BEventReceived = false;
        }

        [EventSubscription(EventNames.EventA, ThreadOption.Caller)]
        public void MyAHandler(object senter, EventArgs a)
        {
            ACount++;
            AEventReceived = true;
        }

        [EventSubscription("IoC Event B", ThreadOption.Caller)]
        public void MyBHandler(object senter, EventArgs a)
        {
            BCount++;
            BEventReceived = true;
        }

        public bool AEventReceived { get; set; }
        public bool BEventReceived { get; set; }
    }

    public class MockEventPublishingSmartPart : SmartPart
    {
        [EventPublication(EventNames.EventA)]
        public event EventHandler EventA;

        public void RaiseEventA()
        {
            if (EventA == null) return;

            EventA(this, null);
        }

    }
}
