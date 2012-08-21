using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using TabGestureSample.Presenters;
using OpenNETCF.IoC;

namespace TabGestureSample.Views
{
    public partial class MidView : SmartPart
    {
        private MyPresenter Presenter { get; set; }

        public MidView()
        {
            // this ctor is for the designer
            InitializeComponent();
        }

        [InjectionConstructor]
        public MidView([ServiceDependency]MyPresenter presenter)
        {
            InitializeComponent();

            // this is the injection ctor
            Presenter = presenter;
        }
    }
}
