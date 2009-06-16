using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;

namespace WiFiSurvey.Shell.Views
{
    public partial class StatusFooterView : SmartPart
    {
        public StatusFooterView()
        {
            InitializeComponent();

            // TODO: display current PC connection status, updates with ET for last AP and PC connection
        }
    }
}
