using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenNETCF.IoC;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenNETCF.IoC.UI;

namespace MVPSample
{
    public class Program : SmartClientApplication<MainForm> 
    {
        [MTAThread]
        static void Main()
        {
            // this will create an instance of MainForm and load it into the RootWorkItem.Items collection
            new Program().Start();
        }

    }

}