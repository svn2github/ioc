using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;
using TabGestureSample.Services;

namespace TabGestureSample
{
    internal class Program : SmartClientApplication<MainForm>
    {
        [STAThread]
        static void Main()
        {
            new Program().Start();
        }

        public override void AddServices()
        {
            // register global services
            RootWorkItem.Services.AddOnDemand<MyService>();
        }

        public override void OnApplicationRun(Form form)
        {
            base.OnApplicationRun(form);
        }
    }
}
