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
using System.Windows.Forms;
using System.Reflection;

namespace OpenNETCF.IoC
{
    internal class BasicInvoker
    {
        private Control m_invoker;
        private Delegate m_targetDelegate;
        private MethodInfo m_methodInfo = null;

        public BasicInvoker(Control invoker)
        {
            m_invoker = invoker;
        }

        public BasicInvoker(Control invoker, Delegate targetDelegate)
        {
            m_invoker = invoker;
            m_targetDelegate = targetDelegate;
        }

        public void Handler(object source, EventArgs args)
        {
            m_invoker.Invoke(m_targetDelegate, new object[] { source, args });
        }

        public MethodInfo HandlerMethod
        {
            get
            {
                if (m_methodInfo == null)
                {
                    m_methodInfo = this.GetType().GetMethod("Handler", BindingFlags.Public | BindingFlags.Instance);
                }

                return m_methodInfo;
            }
        }
    }
}
