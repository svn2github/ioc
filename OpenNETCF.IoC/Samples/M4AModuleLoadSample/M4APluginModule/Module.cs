using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenNETCF.IoC;
using System.Diagnostics;
using System.Net;
using System.Threading;

namespace M4APluginModule
{
    public class Module : ModuleInit
    {
        internal NICInfo Info { get; set; }

        public override void Load()
        {
            Debug.WriteLine("Module.Load");

            ThreadPool.QueueUserWorkItem(delegate
            {
                Info = Activator.CreateInstance(typeof(NICInfo)) as NICInfo;
            });
        }
    }

    internal class NICInfo
    {
        public string HostName { get; set; }

        public NICInfo()
        {
            var name = Dns.GetHostName();
            HostName = name;
        }

    }

}
