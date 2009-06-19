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
    public partial class CurrentAPHeaderView : SmartPart
    {
        public CurrentAPHeaderView()
        {
            InitializeComponent();
        }

        delegate void SetCurrentAPDelegate(string name, string MAC, string strength);

        public void SetCurrentAP(string Name, string MAC, string Strength)
        {
            if(this.InvokeRequired)
            {
                this.Invoke(new SetCurrentAPDelegate(SetCurrentAP), new object[]{Name, MAC, Strength});
            }

            lblSSIDName.Text = Name;
            lblSignalStrength.Text = "Signal: "+ Strength;
            lblMacAdress.Text = MAC;
        }
    }
}
