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
using OpenNETCF.Net.Sockets;
using OpenNETCF.Net.NetworkInformation;
using WiFiSurvey.DAL.SQLCE;
using WiFiSurvey.DAL.Unit_of_Work;
using WiFiSurvey.Infrastructure.Constants;
using WiFiSurvey.Infrastructure;

namespace WiFiSurvey.DAL.Services
{
    public class DataService : IDataService
    {
        SQLCEDB Database;

        public DataService()
        {
            Database = new SQLCEDB();

            // create the DB in the background so it doesn't slow app bring-up
            ThreadPool.QueueUserWorkItem(delegate { Database.EnsureDatabaseExists(); });
        }

        public void ShutDown()
        {
            Database.ShutDown();
        }

        private NetworkInfoUnitOfWork NetworkInfoUnitOfWork { get; set; }

        public void InsertNetworkData(INetworkData data)
        {
            // lazy load
            if(NetworkInfoUnitOfWork == null) NetworkInfoUnitOfWork = new NetworkInfoUnitOfWork(Database);

            Debug.WriteLine("Inserting network info into database");
            int et = Environment.TickCount;
            NetworkInfoUnitOfWork.Insert(data);
            NetworkInfoUnitOfWork.Commit();
            et = Environment.TickCount - et;
            Debug.WriteLine(string.Format("Insert took {0}ms", et));
        }

        public void InsertDesktopData(IDesktopData data)
        {

        }

        [EventSubscription(EventNames.NetworkDataChange, ThreadOption.UserInterface)]
        public void OnNetworkDataChange(object sender, GenericEventArgs<INetworkData> args)
        {
            InsertNetworkData(args.Value);
        }
    }
}
