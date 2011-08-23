using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.Services;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;
using System.Threading;
using OpenNETCF.IoC;
using WiFiSurvey.Infrastructure.BusinessObjects;
using OpenNETCF.Net.NetworkInformation;
using WiFiSurvey.DAL.SQLCE;
using WiFiSurvey.DAL.Unit_of_Work;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Infrastructure;
using OpenNETCF;

namespace WiFiSurvey.DAL.Services
{
    public class HistoricEventService : IHistoricEventService
    {
        private SQLCEDB Database { get; set; }
        private NetworkInfoUnitOfWork NetworkInfoUnitOfWork { get; set; }
        private StatisticsUnitOfWork StatisticsUnitOfWork { get; set; }

        public List<IStatisticsData> HistoryEvents = new List<IStatisticsData>();

        [ServiceDependency]
        IDebugService DebugService { get; set; }

        public HistoricEventService()
        {
            Database = new SQLCEDB();

            // create the DB in the background so it doesn't slow app bring-up
            ThreadPool.QueueUserWorkItem(delegate { Database.EnsureDatabaseExists(); });
        }

        public void ShutDown()
        {
            Database.ShutDown();
        }

        public void InsertNetworkData(INetworkData data)
        {
            // lazy load
            if(NetworkInfoUnitOfWork == null) NetworkInfoUnitOfWork = new NetworkInfoUnitOfWork(Database);

            Debug.WriteLine("Inserting network info into database");
            int et = Environment.TickCount;
            NetworkInfoUnitOfWork.Insert(data);
            NetworkInfoUnitOfWork.Commit();
            et = Environment.TickCount - et;
            DebugService.WriteLine(string.Format("Network data insert took {0}ms", et));
        }

        public void InsertStatisticsData(IStatisticsData data)
        {
            HistoryEvents.Add(data);
            // lazy load
            if (StatisticsUnitOfWork == null) StatisticsUnitOfWork = new StatisticsUnitOfWork(Database);

            Debug.WriteLine("Inserting statistics info into database");
            int et = Environment.TickCount;
            StatisticsUnitOfWork.Insert(data);
            StatisticsUnitOfWork.Commit();
            et = Environment.TickCount - et;
            DebugService.WriteLine(string.Format("Stats data insert took {0}ms", et));
        }

        [EventSubscription(EventNames.NetworkDataChange, ThreadOption.UserInterface)]
        public void OnNetworkDataChange(object sender, GenericEventArgs<INetworkData> args)
        {
            InsertNetworkData(args.Value);
        }

        [EventSubscription(EventNames.NewStatisticsEvent, ThreadOption.UserInterface)]
        public void OnNewStatistic(object sender, GenericEventArgs<IStatisticsData> args)
        {
            InsertStatisticsData(args.Value);
        }

        [EventSubscription(EventNames.ClearHistory, ThreadOption.UserInterface)]
        public void OnClearEvents(object sender, EventArgs e)
        {
            HistoryEvents.Clear();
        }
    }
}
