using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Constants;
using OpenNETCF.IoC;
using System.Diagnostics;
using OpenNETCF;

namespace WiFiSurvey.Infrastructure.Services
{
    public class DebugService : IDebugService
    {
        public event EventHandler<GenericEventArgs<string>> DebugLine;

        public void WriteLine(string message)
        {
            Debug.WriteLine(message);

            if (DebugLine != null)
            {
                try
                {
                    DebugLine("DebugService", new GenericEventArgs<string>(message));
                }
                catch
                {
                    // if we get an exception in a handler, there's little we can do about it.
                }
            }
        }
    }
}
