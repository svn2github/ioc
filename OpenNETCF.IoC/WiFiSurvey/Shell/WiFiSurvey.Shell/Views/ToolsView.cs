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
    public partial class ToolsView : UserControl, ISmartPart
    {
        public ToolsView()
        {
            InitializeComponent();

            this.Name = "Tools";

            // TODO: allow set up of PC app IP, collection interval, etc.
        }
    }
}
