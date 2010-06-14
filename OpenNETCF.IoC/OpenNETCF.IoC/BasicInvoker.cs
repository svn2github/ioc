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
