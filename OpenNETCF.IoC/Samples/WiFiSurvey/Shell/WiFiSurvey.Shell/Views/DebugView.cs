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
using OpenNETCF;

namespace WiFiSurvey.Shell
{
    public partial class DebugView : SmartPart
    {
        IDebugService DebugService { get; set; }

        [InjectionConstructor]
        public DebugView([ServiceDependency]IDebugService debugService)
        {
            InitializeComponent();
            this.Name = "Debug";

            debugList.Columns[0].Width = -2;
            clear.Click += delegate { debugList.Items.Clear(); };

            DebugService = debugService;
            DebugService.DebugLine += DebugService_DebugLine;
        }

        public delegate void DebugLine(object sender, GenericEventArgs<string> e);

        void DebugService_DebugLine(object sender, GenericEventArgs<string> e)
        {
            try
            {
                if (this.InvokeRequired)
                {
                    Invoke(new DebugLine(DebugService_DebugLine), new object[] { sender, e });
                    return;
                }

                if (!enable.Checked) return;

                debugList.Items.Insert(0, (new ListViewItem(e.Value)));
            }
            catch (ObjectDisposedException)
            {
                // this can happen when shutting down the app (getting an event after the Form has been disposed)
            }
        }
    }
}
