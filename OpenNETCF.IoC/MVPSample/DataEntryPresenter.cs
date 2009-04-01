using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using OpenNETCF.IoC.UI;
using OpenNETCF.IoC;

namespace MVPSample
{
    public class DataEntryPresenter : Presenter<DataEntry>
    {
        [InjectionConstructor]
        public DataEntryPresenter(DataEntry view, IWorkspace workspace)
            : base(view, workspace)
        {
        }

        public void ShowMenuView()
        {
            ShowView(typeof(Menu), ViewNames.MenuView);
        }
    }
}
