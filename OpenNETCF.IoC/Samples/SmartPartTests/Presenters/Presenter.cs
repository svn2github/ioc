using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SmartPartTests.SmartParts;
using OpenNETCF.IoC;
using OpenNETCF.IoC.UI;

namespace SmartPartTests.Presenters
{
    class Presenter
    {
        public Presenter()
        {
            // pre-create a SmartPart
            RootWorkItem.SmartParts.AddNew<Part1>("MySmartPart");
        }

        public int DoAction(int step)
        {
            switch (step)
            {
                case 1:
                    CreateAndShowWorkItem();
                    break;
                case 2:
                    ShowPreCreatedWorkItem();
                    return 1;
                default:
                    return 1;
            }

            return step + 1;
        }

        private void CreateAndShowWorkItem()
        {
            // manually create a new SmartPart
            var newPart = RootWorkItem.Items.AddNew<Part1>();
            //var newPart = new Part1();

            // Show it
            RootWorkItem.Workspaces[WorkspaceNames.Main].Show(newPart);
        }

        private void ShowPreCreatedWorkItem()
        {
            // get smartpart from container
            var existingPart = RootWorkItem.SmartParts["MySmartPart"];

            // Show it
            RootWorkItem.Workspaces[WorkspaceNames.Main].Show(existingPart);
        }

        [EventSubscription(EventNames.CloseSmartPart, ThreadOption.UserInterface)]
        public void OnCloseRequested(object sender, EventArgs args)
        {
            RootWorkItem.Workspaces[WorkspaceNames.Main].Close(sender as SmartPart);
        }
    }
}
