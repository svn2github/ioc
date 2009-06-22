using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Infrastructure;
using System.Diagnostics;
using WiFiSurvey.Infrastructure.Services;

namespace WiFiSurvey.Shell
{
    public partial class DebugView : SmartPart
    {
        public DebugView()
        {
            InitializeComponent();
            DebugService.DebugLine += new EventHandler<GenericEventArgs<string>>(DebugService_DebugLine);
        }

        void DebugService_DebugLine(object sender, GenericEventArgs<string> e)
        {
            Trace.WriteLine(e.Value);
        }
    }
}
