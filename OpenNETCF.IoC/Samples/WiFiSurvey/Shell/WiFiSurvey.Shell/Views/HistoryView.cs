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
using OpenNETCF;

namespace WiFiSurvey.Shell.Views
{
    public partial class HistoryView : SmartPart
    {
        private delegate void NewHistoryDelegate(DateTime time, string Event);
        private delegate void ClearListView();
        private delegate ListViewItem AddListItem(ListViewItem item);

        private Boolean m_ColumnsResized = false;

        public HistoryPresenter Presenter { get; set; }

        public HistoryView()
        {
            Presenter = RootWorkItem.Items.AddNew<HistoryPresenter>(PresenterNames.History);

            InitializeComponent();
            this.Name = "History";

            Presenter.OnNewHistoryEvent += Presenter_OnNewHistoryEvent;
            Presenter.ClearEvents += Presenter_ClearEvents;
        }

        void Presenter_ClearEvents(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                historyListView.Invoke(new ClearListView(historyListView.Items.Clear));
            }
            else
            {
                historyListView.Items.Clear();
            }
        }

        void Presenter_OnNewHistoryEvent(object sender, GenericEventArgs<IStatisticsData> e)
        {
            NewHistoryEvent(DateTime.Now, e.Value.Description);
        }

        public void ResizeView()
        {
            historyListView.Columns[0].Width = -1;
            historyListView.Columns[1].Width = -2;
            m_ColumnsResized = true;
        }

        public void NewHistoryEvent(DateTime time, string Event)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new NewHistoryDelegate(NewHistoryEvent), new object[]{time, Event});
                return;
            }
            
            ListViewItem item = new ListViewItem();
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
