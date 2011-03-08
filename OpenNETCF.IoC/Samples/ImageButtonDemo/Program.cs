using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;

namespace ImageButtonDemo
{
    class Program : SmartClientApplication<ContainerForm>
    {
        [MTAThread]
        static void Main()
        {
            Program p = new Program();

            // since Program derives from SmartClientApplication<T>, this will create and show a ContainerForm
            p.Start();
        }
    }
}