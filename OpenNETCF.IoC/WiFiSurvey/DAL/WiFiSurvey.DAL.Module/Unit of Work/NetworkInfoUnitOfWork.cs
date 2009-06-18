using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure.BusinessObjects;
using WiFiSurvey.DAL.SQLCE;
using System.Data.SqlServerCe;
using System.Diagnostics;

namespace WiFiSurvey.DAL.Unit_of_Work
{
    internal class NetworkInfoUnitOfWork : IUnitOfWork<INetworkData>
    {
        private SQLCEDB Database { get; set; }
        private List<INetworkData> m_inserts = new List<INetworkData>();
        private List<INetworkData> m_updates = new List<INetworkData>();
        private List<INetworkData> m_deletes = new List<INetworkData>();
        private object m_syncRoot = new object();

        public NetworkInfoUnitOfWork(SQLCEDB database)
        {
            Database = database;
        }

        public void Insert(INetworkData item)
        {
            lock (m_syncRoot)
            {
                m_inserts.Add(item);
            }
        }

        public void Update(INetworkData item)
        {
            lock (m_syncRoot)
            {
                m_updates.Add(item);
            }
        }

        public void Delete(INetworkData item)
        {
            lock (m_syncRoot)
            {
                m_deletes.Add(item);
            }
        }

        public void Commit()
        {
            // TODO: consider putting this into a ThreadPool

            lock (m_syncRoot)
            {
                SqlCeTransaction transaction;

                SqlCeConnection connection = Database.GetDatabaseConnection();

                foreach (INetworkData data in m_inserts)
                {
                    transaction = connection.BeginTransaction() as SqlCeTransaction;

                    try
                    {
                        int id = Database.InsertAssociatedAPRow(data.AssociatedAP, connection);

                        foreach (var nearby in data.NearbyAPs)
                        {
                            nearby.ID = id;
                            Database.InsertNearbyAPRow(nearby, connection);
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Debug.WriteLine(ex.Message);
                        if (Debugger.IsAttached) Debugger.Break();
                    }
                }

                // TODO: updates and deletes
            }
        }
        

        public void Rollback()
        {
            lock (m_syncRoot)
            {
                m_inserts.Clear();
                m_deletes.Clear();
                m_updates.Clear();
            }
        }
    }
}
