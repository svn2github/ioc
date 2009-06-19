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
    internal class StatisticsUnitOfWork : IUnitOfWork<IStatisticsData>
    {
        private SQLCEDB Database { get; set; }
        private List<IStatisticsData> m_inserts = new List<IStatisticsData>();
        private List<IStatisticsData> m_updates = new List<IStatisticsData>();
        private List<IStatisticsData> m_deletes = new List<IStatisticsData>();
        private object m_syncRoot = new object();

        public StatisticsUnitOfWork(SQLCEDB database)
        {
            Database = database;
        }

        public void Insert(IStatisticsData item)
        {
            lock (m_syncRoot)
            {
                m_inserts.Add(item);
            }
        }

        public void Update(IStatisticsData item)
        {
            lock (m_syncRoot)
            {
                m_updates.Add(item);
            }
        }

        public void Delete(IStatisticsData item)
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

                foreach (IStatisticsData data in m_inserts)
                {
                    transaction = connection.BeginTransaction() as SqlCeTransaction;

                    try
                    {
                        Database.InsertStatisticsRow(data, connection);
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
