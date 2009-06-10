using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;

namespace App.Shell
{
    public class Program : SmartClientApplication<ContainerForm>
    {
        [MTAThread]
        static void Main()
        {
            // this will create an instance of MainForm and load it into the RootWorkItem.Items collection
            new Program().Start();
        }

    }
}