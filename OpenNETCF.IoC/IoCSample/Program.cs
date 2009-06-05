using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenNETCF.IoC;
using System.Reflection;
using System.Runtime.InteropServices;
using OpenNETCF.IoC.UI;

namespace IoCSample
{
    static class Program
    {
        [MTAThread]
        static void Main()
        {
            RootWorkItem.Items.AddNew<MenuForm>("menu");
            Application.Run(RootWorkItem.Items["menu"] as Form);
        }
    }
}