using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace OpenNETCF.IoC.UI
{
    public interface IWorkspace
    {
        event EventHandler<DataEventArgs<ISmartPart>> SmartPartActivated;
        event EventHandler<DataEventArgs<ISmartPart>> SmartPartClosing;

        void Show(ISmartPart smartPart);
        void Hide(ISmartPart smartPart);
        void Close(ISmartPart smartPart);
        void Activate(ISmartPart smartPart);

        ISmartPart ActiveSmartPart { get; }
        SmartPartCollection SmartParts { get; }
    }

    public class DeckWorkspace : Workspace
    {
        protected override void OnShow(ISmartPart smartPart)
        {
            smartPart.Dock = DockStyle.Fill;
            base.OnShow(smartPart);
        }
    }

}
