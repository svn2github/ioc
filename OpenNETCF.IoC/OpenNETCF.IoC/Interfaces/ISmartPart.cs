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

namespace OpenNETCF.IoC.UI
{
    public interface ISmartPart : IDisposable
    {
        event EventHandler<GenericEventArgs<bool>> VisibleChanged;

        bool Visible { get;  set; }
        bool Focused { get; }
        void BringToFront();
        bool Focus();
        DockStyle Dock { set; }
        string Name { get; set; }
        Control Parent { get; }
        bool Enabled { get; set; }
        void OnActivated();
        void OnDeactivated();
        void Show();
        void Hide();
        IWorkspace Workspace { get; }
    }
}
