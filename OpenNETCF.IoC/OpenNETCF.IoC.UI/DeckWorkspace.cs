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
using System.Windows.Forms;
using System.Drawing;

namespace OpenNETCF.IoC.UI
{
    public class DeckWorkspace : Workspace
    {
        //protected override void OnShow(ISmartPart smartPart, ISmartPartInfo smartPartInfo)
        //{
        //    ISmartPart current = ActiveSmartPart;
        //    if ((current != null) && (smartPart != current))
        //    {
        //        current.Visible = false;
        //    }
        //    smartPart.Dock = DockStyle.Fill;
        //    base.OnShow(smartPart, smartPartInfo);
        //}

        //public override ISmartPart ActiveSmartPart
        //{
        //    get
        //    {
        //        return SmartParts.FirstOrDefault(s => s.Visible);
        //    }
        //}
    }
}
