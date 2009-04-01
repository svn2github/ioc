using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;

namespace MVPSample
{
    public class SettingsPresenter : Presenter<Settings>
    {
        [InjectionConstructor]
        public SettingsPresenter(Settings view, IWorkspace workspace)
            : base(view, workspace)
        {
        }

        public void ShowMenuView()
        {
            ShowView(typeof(Menu), ViewNames.MenuView);
        }
    }
}
