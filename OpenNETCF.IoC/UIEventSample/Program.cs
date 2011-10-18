using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;

namespace UIEventSample
{
    class Program : SmartClientApplication<MainForm>
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            new Program().Start();
        }
    }
}
