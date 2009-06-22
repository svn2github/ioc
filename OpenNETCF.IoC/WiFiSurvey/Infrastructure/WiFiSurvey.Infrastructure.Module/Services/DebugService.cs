using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Constants;
using OpenNETCF.IoC;
using System.Diagnostics;

namespace WiFiSurvey.Infrastructure.Services
{
    public static class DebugService
    {
        public static void WriteLine(string message)
        {
            if(DebugLine != null)
            {
                DebugLine("DebugService", new GenericEventArgs<string>(message));
            }
            Trace.WriteLine(message);
        }

        [EventPublication(EventNames.DebugLine)]
        public static event EventHandler<GenericEventArgs<string>> DebugLine;
    }
}
