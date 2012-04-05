using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenNETCF.IoC;
using System.Diagnostics;

namespace M4APluginModule
{
    public class Module : ModuleInit
    {
        public override void Load()
        {
            Debug.WriteLine("Module.Load");
        }
    }
}
