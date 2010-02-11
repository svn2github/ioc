using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;

namespace EventMarshalTest
{
    public class Program : SmartClientApplication<Form1>
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            new Program().Start();
        }
    }
}