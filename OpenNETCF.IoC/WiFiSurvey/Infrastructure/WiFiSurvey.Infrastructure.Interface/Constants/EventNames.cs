using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace WiFiSurvey.Infrastructure.Constants
{
    public static class EventNames
    {
        public const string NewAPConnection = "event:NewAP";
        public const string NetworkDataChange = "event:NetworkDataChange";
        public const string DesktopConnectionChange = "event:DesktopConnectionChange";
        public const string NewStatisticsEvent = "event:NewStatistic";
        public const string ClearHistory = "event:ClearHistory";
        public const string DebugLine = "event:DebugLine";
        public const string RestartBroadcasting = "event:RestartBroadcast";
    }
}
