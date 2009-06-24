using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.Infrastructure.Services
{
    public interface IDebugService
    {
        event EventHandler<GenericEventArgs<string>> DebugLine;
        
        void WriteLine(string message);
    }
}
