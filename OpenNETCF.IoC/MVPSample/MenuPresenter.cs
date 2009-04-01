using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;

namespace MVPSample
{
    public class MenuPresenter : Presenter<Menu>
    {
        [InjectionConstructor]
        public MenuPresenter(Menu view, IWorkspace workspace)
            : base(view, workspace)
        {
        }

        public void LoadConfiguration()
        {
            // Do something non-UI related
        }

        public void ShowEntryView()
        {
            ShowView(typeof(DataEntry), ViewNames.DataEntryView);
        }

        public void ShowSettingsView()
        {
            ShowView(typeof(Settings), ViewNames.SettingsView);
        }
    }
}
