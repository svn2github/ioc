using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using OpenNETCF.IoC;
using WinPhoneSample.ViewModels;

namespace WinPhoneSample.Views
{
    public partial class AuthorsView : PhoneApplicationPage
    {
        public AuthorsView()
        {
            InitializeComponent();
            ViewModel = RootWorkItem.Items.Get<AuthorViewModel>("AVM");

            this.Loaded += new RoutedEventHandler(AuthorsView_Loaded);
        }

        void AuthorsView_Loaded(object sender, RoutedEventArgs e)
        {
            authorList.DataContext = ViewModel.Authors;
        }

        public AuthorViewModel ViewModel { get; set; }
    }
}