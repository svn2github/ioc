using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;

namespace WiFiSurvey.Shell
{
    public class Program : SmartClientApplication<ContainerForm>
    {
        [MTAThread]
        static void Main()
        {
            new Program().Start();
        }
    }
}