using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using OpenNETCF.IoC;
using System.Reflection;

namespace M4AModuleLoadSample
{
    [Activity(Label = "M4AModuleLoadSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            //RootWorkItem.SetModuleInfoStore(new ResourceModuleInfoStore(Assembly.GetExecutingAssembly()));
            RootWorkItem.SetModuleInfoStore(new ResourceModuleInfoStore());

            RootWorkItem.Modules.LoadModules();
            var c = RootWorkItem.Modules.Count;
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);

            button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
        }
    }
}

