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

namespace OpenNETCF.IoC.Unit.Test
{
    [TestClass()]
    public class WorkItemTests
    {
        public TestContext TestContext { get; set; }

        [TestMethod()]
        [Description("Ensures that when a child is added via AddNew with an ID, the Parent property gets set")]
        public void WorkItemAddNewWithIDParentTest()
        {
            WorkItem parent = new WorkItem();
            WorkItem child = parent.Items.AddNew<WorkItem>("child");
            Assert.AreEqual<WorkItem>(parent, child.Parent, "Parent-child relationship invalid");
        }

        [TestMethod()]
        [Description("Ensures that when a child is added via AddNew without an ID, the Parent property gets set")]
        public void WorkItemAddNewWithoutIDParentTest()
        {
            WorkItem parent = new WorkItem();
            WorkItem child = parent.Items.AddNew<WorkItem>();
            Assert.AreEqual<WorkItem>(parent, child.Parent, "Parent-child relationship invalid");
        }

        [TestMethod()]
        [Description("Ensures that when a child is added via Add with an ID, the Parent property gets set")]
        public void WorkItemAddWithIDParentTest()
        {
            WorkItem parent = new WorkItem();
            WorkItem child = new WorkItem();
            Assert.IsNull(child.Parent, "Parent was expected to be null after creation");
            parent.Items.Add(child, "child");
            Assert.AreEqual<WorkItem>(parent, child.Parent, "Parent-child relationship invalid");
        }

        [TestMethod()]
        [Description("Ensures that when a child is added via Add without an ID, the Parent property gets set")]
        public void WorkItemAddWithoutIDParentTest()
        {
            WorkItem parent = new WorkItem();
            WorkItem child = new WorkItem();
            Assert.IsNull(child.Parent, "Parent was expected to be null after creation");
            parent.Items.Add(child);
            Assert.AreEqual<WorkItem>(parent, child.Parent, "Parent-child relationship invalid");
        }
    }
}
