using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Shell.Views
{
    public partial class ToolsView : SmartPart
    {
        public ToolsView()
        {
            InitializeComponent();

            this.Name = "Tools";

            btnDisableDesktop.Enabled = true;
            btnEnableDesktop.Enabled = false;
            // TODO: allow set up of PC app IP, collection interval, etc.
        }

        private void btnEnableDesktop_Click(object sender, EventArgs e)
        {
            WU.DesktopAppDisabled = true;
            if (WU.DesktopAppDisabled)
            {
                btnEnableDesktop.Enabled = true;
            }
            else
            {
                btnEnableDesktop.Enabled = false;
            }
        }

        private void btnDisableDesktop_Click(object sender, EventArgs e)
        {
            WU.DesktopAppDisabled = false;
            if (WU.DesktopAppDisabled)
            {
                btnDisableDesktop.Enabled = false;
            }
            else
            {
                btnDisableDesktop.Enabled = true;
            }
        }
    }
}
