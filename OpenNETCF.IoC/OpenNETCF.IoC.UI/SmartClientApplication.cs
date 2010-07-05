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
using System.Reflection;

namespace OpenNETCF.IoC.UI
{
    public abstract class SmartClientApplication<TShell>
        where TShell : Form
    {
        /// <summary>
        /// This method loads the Profile Catalog Modules by calling GetModuleInfoStore which, unless overridden, uses a DefaultModuleInfoStore instance.
        /// It then creates an instance of TShell and calls Application.Run with that instance.
        /// </summary>
        public void Start()
        {
            // load up the profile catalog here
            IModuleInfoStore store = GetModuleInfoStore();

            Start(store);
        }

        public void Start(string profileCatalog)
        {
            // load up the profile catalog here
            IModuleInfoStore store = new DefaultModuleInfoStore(profileCatalog);

            Start(store);
        }

        private void Start(IModuleInfoStore store)
        {
            ModuleInfoStoreService storeService = RootWorkItem.Services.AddNew<ModuleInfoStoreService>();

            // add a generic "control" to the Items list.
            RootWorkItem.Items.AddNew<Control>("IOCEventInvoker");

            if (store != null)
            {
                storeService.ModuleLoaded += new EventHandler<GenericEventArgs<string>>(storeService_ModuleLoaded);
                storeService.LoadModulesFromStore(store);
            }

            // create the shell form after all modules are loaded
            TShell shellForm = RootWorkItem.Items.AddNew<TShell>();

            AfterShellCreated();

            OnApplicationRun(shellForm);
        }

        void storeService_ModuleLoaded(object sender, GenericEventArgs<string> e)
        {
            OnModuleLoadComplete(e.Value);
        }

        public virtual IModuleInfoStore GetModuleInfoStore()
        {
            return new DefaultModuleInfoStore();
        }

        protected virtual void AfterShellCreated()
        {
        }

        /// <summary>
        /// When overridden by a derived class, an application can choose to use an alternate for Application.Run.
        /// </summary>
        /// <remarks>
        /// The Compact Framework does not support IMessageFilter, so if you want to add one you must use something like OpenNETCF's Application2
        /// class.  By overriding this method, you can create and add filters and then call Application2.Run.  Note that you <b>must</b> call some form of Run
        /// method (Application.Run or otherwise) if you override this.  It's also ill advised to call the base implementation if you use your own Run
        /// implementation.
        /// </remarks>
        /// <param name="form">Form instance to be passed tot he Run method.</param>
        public virtual void OnApplicationRun(Form form)
        {
            Application.Run(form);
        }

        public virtual void OnModuleLoadComplete(string moduleName)
        {
        }
    }
}
