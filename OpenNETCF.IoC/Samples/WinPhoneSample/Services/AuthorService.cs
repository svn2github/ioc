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
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WinPhoneSample.Services
{
    public class AuthorService : IAuthorService
    {
        private ObservableCollection<Author> m_authors;

        public ObservableCollection<Author> GetAuthors()
        {
            if (m_authors == null)
            {
                m_authors = new ObservableCollection<Author>();

                foreach(var a in GetSampleAuthors())
                {
                    m_authors.Add(a);
                }
            }

            return m_authors;
        }

        private IEnumerable<Author> GetSampleAuthors()
        {
            return new Author[]
            {
                new Author
                {
                     FirstName = "Alexadre",
                     LastName = "Dumas"
                },
                new Author
                {
                     FirstName = "David",
                     LastName = "McCullough"
                },
                new Author
                {
                     FirstName = "John",
                     LastName = "Steinbeck"
                },
            };
        }
    }
}
