using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using OpenNETCF.IoC;
using System.Reflection;
using System.Net;

namespace M4AModuleLoadSample
{
    public class NICService : Service
    {
        public override void OnCreate()
        {
            var name = Dns.GetHostName();

            base.OnCreate();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }

    [Activity(Label = "M4AModuleLoadSample", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            var intent = new Intent(this, typeof(NICService));
            StartService(intent);


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

