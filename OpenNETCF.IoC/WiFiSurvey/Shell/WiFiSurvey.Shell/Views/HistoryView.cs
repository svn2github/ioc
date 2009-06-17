using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;

namespace WiFiSurvey.Shell.Views
{
    public partial class HistoryView : SmartPart
    {
        private ListViewItem item;

        public HistoryView()
        {
            InitializeComponent();
            this.Name = "History";

            ResizeView();

            NewHistoryEvent("EvenName", "Desktop Access");
        }

        public void ResizeView()
        {
            int m_HistWidth = historyListView.Width;
            m_HistWidth = (m_HistWidth / historyListView.Columns.Count);

            int i = 1;
            foreach(ColumnHeader header in historyListView.Columns)
            {
                header.Width = m_HistWidth - i++;
            }
        }

        public void NewHistoryEvent(string Name, string Event)
        {
            item = new ListViewItem();
            item.Text = Name;
            item.SubItems.Add(Event);

            historyListView.Items.Add(item);

            //TODO: add the history to a Database
        }
    }
}
