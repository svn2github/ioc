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

namespace WiFiSurvey.Shell
{
    public partial class DebugView : SmartPart
    {
        public DebugView()
        {
            InitializeComponent();
        }

        [EventSubscription(EventNames.DebugLine, ThreadOption.UserInterface)]
        public void NewDebugLine(object sender, GenericEventArgs<string> args)
        {
            Trace.WriteLine(args);
        }
    }
}
