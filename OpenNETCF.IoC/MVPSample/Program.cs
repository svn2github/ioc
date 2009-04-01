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
    static class Program
    {
        [MTAThread]
        static void Main()
        {
//            MainForm main = new MainForm();
//            Application.Run(main);

            Form form = RootWorkItem.Items.AddNew<MainForm>("container");
            Application.Run(form);
        }

    }

}