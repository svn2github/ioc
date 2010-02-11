using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;

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

        [EventSubscription(MyService.EventName, ThreadOption.UserInterface)]
        public void ServiceEventHandler(object sender, GenericEventArgs<int> e)
        {
            result.Text = e.Value.ToString();
        }

        private void ui_Click(object sender, EventArgs e)
        {
            Service.RaiseServiceEvent();
        }

        private void background_Click(object sender, EventArgs e)
        {
            Service.BeginRaiseServiceEvent();
        }
    }
}