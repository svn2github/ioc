using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC;
using App.Shell.Presenters;
using OpenNETCF.IoC.UI;
using Infrastructure.Interface.Services;

namespace App.Shell.Views
{
    public partial class HomeView : UserControl, ISmartPart
    {
        HomePresenter Presenter { get; set; }

        [InjectionConstructor]
        public HomeView([ServiceDependency]HomePresenter presenter)
        {
            Presenter = presenter;

            InitializeComponent();
        }
    }
}
