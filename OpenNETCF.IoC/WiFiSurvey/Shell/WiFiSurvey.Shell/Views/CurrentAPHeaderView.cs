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

        public void SetCurrentAP(string Name, string MAC, string Strength)
        {
            lblSSIDName.Text = Name;
            lblSignalStrength.Text = "Signal: "+ Strength;
            lblMacAdress.Text = MAC;
        }
    }
}
