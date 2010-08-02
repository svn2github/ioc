using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using WinPhoneSample.Entities;
using OpenNETCF.IoC;
using WinPhoneSample.Services;
using System.Collections.ObjectModel;

namespace WinPhoneSample.ViewModels
{
    public class AuthorViewModel
    {
        public AuthorViewModel()
        {
        }

        public ObservableCollection<Author> Authors
        {
            get
            {
                var svc = RootWorkItem.Services.Get<IAuthorService>();
                return svc.GetAuthors();
            }
        }
    }
}
