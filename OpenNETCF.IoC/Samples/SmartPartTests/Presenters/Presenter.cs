using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using SmartPartTests.SmartParts;
using OpenNETCF.IoC;
using OpenNETCF.IoC.UI;
using System.Diagnostics;

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
            // manually create a new SmartPart if it needs creating, otherwise get the existing
            var newPart = RootWorkItem.Items.GetOrCreate<Part1>("SecondSmartPart");
            newPart.Name = "SecondSmartPart";
            
            // Show it
            RootWorkItem.Workspaces[WorkspaceNames.Main].Show(newPart);
        }

        private void ShowPreCreatedWorkItem()
        {
            // get smartpart from container
            var existingPart = RootWorkItem.SmartParts["MySmartPart"];
            existingPart.Name = "MySmartPart";

            // if we called "Close" on the SmartPart, it will no longer be in the collection
            if (existingPart == null)
            {
                Debug.WriteLine("Recreating 'MySmartPart'...");
                existingPart = RootWorkItem.SmartParts.AddNew<Part1>("MySmartPart");
            }

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
