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

            btnDisableDesktop.Enabled = false;
            btnEnableDesktop.Enabled = true;
            // TODO: allow set up of PC app IP, collection interval, etc.
        }

        private void btnEnableDesktop_Click(object sender, EventArgs e)
        {
            WU.DesktopAppDisabled = false;
            if (WU.DesktopAppDisabled)
            {
                btnEnableDesktop.Enabled = true;
                btnDisableDesktop.Enabled = false;
            }
            else
            {
                btnEnableDesktop.Enabled = false;
                btnDisableDesktop.Enabled = true;
            }
        }

        private void btnDisableDesktop_Click(object sender, EventArgs e)
        {
            WU.DesktopAppDisabled = true;
            if (WU.DesktopAppDisabled)
            {
                btnDisableDesktop.Enabled = false;
                btnEnableDesktop.Enabled = true;
            }
            else
            {
                btnDisableDesktop.Enabled = true;
                btnEnableDesktop.Enabled = false;
            }
        }
    }
}
