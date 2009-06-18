using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using OpenNETCF.IoC.UI;
using WiFiSurvey.Infrastructure.Services;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.Shell.Views
{
    public partial class HistoryView : SmartPart
    {
        private ListViewItem item = null;
        private IDataService DataService { get; set; }
        private Boolean m_columnWidthsSet = false;
        private Dictionary<string, string> Events = new Dictionary<string, string>();

        public HistoryView()
        {
            InitializeComponent();
            this.Name = "History";

            //ResizeView();

            DataService = RootWorkItem.Services.Get<IDataService>();

            DataService.OnNewDataEvent += new EventHandler<DataServiceArgs<DataEvent>>(DataService_NewDataEvent);
            DataService.OnClearEvents += new EventHandler<EventArgs>(DataService_OnClearEvents);
        }

        void DataService_OnClearEvents(object sender, EventArgs e)
        {
            historyListView.Invoke(new ClearListView(historyListView.Items.Clear));
        }

        void DataService_NewDataEvent(object sender, DataServiceArgs<DataEvent> e)
        {
            NewHistoryEvent(e.Value.Location, e.Value.Description);
        }

        public void UpdateColumnsWidth()
        {
            historyListView.Columns[0].Width = -2;
            historyListView.Columns[1].Width = -2;
            m_columnWidthsSet = true;

            //int m_HistWidth = historyListView.Width;
            //m_HistWidth = (m_HistWidth / historyListView.Columns.Count);

            //int i = 1;
            //foreach(ColumnHeader header in historyListView.Columns)
            //{
            //    header.Width = m_HistWidth - i++;
            //}
        }
        delegate void NewHistoryDelegate(string Name, string Event);
        delegate void ClearListView();
        delegate ListViewItem AddListItem(ListViewItem item);

        public void NewHistoryEvent(string Name, string Event)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new NewHistoryDelegate(NewHistoryEvent), new object[]{Name, Event});
            }

            item = new ListViewItem();
            item.Text = Name;
            item.SubItems.Add(Event);

            if (DataService.EventCount == historyListView.Items.Count + 1)
            {
                historyListView.Invoke(new AddListItem(historyListView.Items.Add), new object[] { item });
            }
            if (!m_columnWidthsSet)
            {
                UpdateColumnsWidth();
            }
        }
    }
}
