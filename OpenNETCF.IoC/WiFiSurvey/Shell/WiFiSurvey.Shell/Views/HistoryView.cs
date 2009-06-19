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
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Shell.Presenters;
using WiFiSurvey.Infrastructure;

namespace WiFiSurvey.Shell.Views
{
    public partial class HistoryView : SmartPart
    {
        private ListViewItem item;
        public HistoryPresenter Presenter { get; set; }

        private Dictionary<string, string> Events = new Dictionary<string, string>();
        private Boolean m_ColumnsResized = false;

        public HistoryView()
        {
            Presenter = RootWorkItem.Items.AddNew<HistoryPresenter>(PresenterNames.History);

            InitializeComponent();
            this.Name = "History";

            Presenter.OnNewHistoryEvent += new EventHandler<GenericEventArgs<IStatisticsData>>(Presenter_OnNewHistoryEvent);
        }

        void Presenter_OnNewHistoryEvent(object sender, GenericEventArgs<IStatisticsData> e)
        {
            NewHistoryEvent(DateTime.Now, e.Value.Description);
        }

        void DataService_OnClearEvents(object sender, EventArgs e)
        {
            historyListView.Invoke(new ClearListView(historyListView.Items.Clear));
        }

        public void ResizeView()
        {
            historyListView.Columns[0].Width = -1;
            historyListView.Columns[1].Width = -2;
            m_ColumnsResized = true;
        }
        delegate void NewHistoryDelegate(DateTime time, string Event);
        delegate void ClearListView();
        delegate ListViewItem AddListItem(ListViewItem item);

        public void NewHistoryEvent(DateTime time, string Event)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new NewHistoryDelegate(NewHistoryEvent), new object[]{time, Event});
                return;
            }
            
            item = new ListViewItem();
            item.Text = time.ToShortTimeString();
            item.SubItems.Add(Event);

            historyListView.Invoke(new AddListItem(historyListView.Items.Add), new object[] { item });

            if (!m_ColumnsResized)
            {
                ResizeView();
            }
        }
    }
}
