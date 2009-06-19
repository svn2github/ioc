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
using WiFiSurvey.Infrastructure.Constants;

namespace WiFiSurvey.Shell.Views
{
    public partial class StatusFooterView : SmartPart
    {
        public StatusFooterView()
        {
            InitializeComponent();
        }

        public void UpdateConnection(IDesktopData data)
        {
            switch (data.Status)
            {
                case DesktopStatus.Connected:    label1.Text = "Desktop Connected";           break;
                case DesktopStatus.Disabled:     label1.Text = "Desktop Connection Disabled"; break;
                case DesktopStatus.Disconnected: label1.Text = "Desktop Not Connected";       break;
            }
        }
    }
}
