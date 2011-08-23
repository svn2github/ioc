using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;
using OpenNETCF;

namespace EventMarshalTest
{
    public partial class Form1 : Form
    {
        private MyService Service { get; set; }

        public Form1()
        {
            Service = RootWorkItem.Services.AddNew<MyService>();

            InitializeComponent();
        }

        [EventSubscription(MyService.EventNameA, ThreadOption.UserInterface)]
        public void ServiceEventHandlerA(object sender, GenericEventArgs<int> e)
        {
            result.Text = e.Value.ToString();
        }

        [EventSubscription(MyService.EventNameB, ThreadOption.UserInterface)]
        public void ServiceEventHandlerB(object sender, EventArgs e)
        {
            // note that InvokeRequired will be "false" here
//            var args = (e as IntArgs);
//            result.Text = args.Value.ToString();
        }

        private void ui_Click(object sender, EventArgs e)
        {
            Service.RaiseServiceEvents();
        }

        private void background_Click(object sender, EventArgs e)
        {
            Service.BeginRaiseServiceEvent();
        }
    }
}