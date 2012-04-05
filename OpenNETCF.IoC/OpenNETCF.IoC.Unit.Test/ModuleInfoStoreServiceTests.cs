// LICENSE
// -------
// This software was originally authored by Christopher Tacke of OpenNETCF Consulting, LLC
// On March 10, 2009 is was placed in the public domain, meaning that all copyright has been disclaimed.
//
// You may use this code for any purpose, commercial or non-commercial, free or proprietary with no legal 
// obligation to acknowledge the use, copying or modification of the source.
//
// OpenNETCF will maintain an "official" version of this software at www.opennetcf.com and public 
// submissions of changes, fixes or updates are welcomed but not required
//

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace OpenNETCF.IoC.Unit.Test
{
    [TestClass()]
    public class ModuleInfoStoreServiceTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        public void Foo()
        {
            var t1 = Environment.TickCount;
            var c = RootWorkItem.Modules.Count();
            var t2 = Environment.TickCount;

            var et = t2 - t1;
            Trace.WriteLine(et);
        }
    }
}
