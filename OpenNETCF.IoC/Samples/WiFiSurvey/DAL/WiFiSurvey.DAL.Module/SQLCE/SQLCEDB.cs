using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using WiFiSurvey.Infrastructure.BusinessObjects;
using System.IO;
using WiFiSurvey.DAL.BusinessObjects;
using System.Reflection;

namespace WiFiSurvey.DAL.SQLCE
{
    internal class SQLCEDB
    {
        // /TODO: pull this from the configuration service
        private const string DB_PATH = "\\WiFiSurvey.sdf";
        private SqlCeConnection m_connection;

        private string ConnectionString { get; set; }
        private SqlCeCommand NearbyAPInsertCommand { get; set; }
        private SqlCeCommand AssociatedAPInsertCommand { get; set; }
        private SqlCeCommand StatisticsInsertCommand { get; set; }
        private Dictionary<string, ITable> Tables { get; set; }

        public SQLCEDB()
        {
            ConnectionString = string.Format("Data Source={0};", DB_PATH);

            Tables = CreateTableObjects();
        }

        internal SqlCeConnection GetDatabaseConnection()
        {
            if (m_connection == null)
            {
                m_connection = new SqlCeConnection(ConnectionString);
                m_connection.Open();
            }

            return m_connection;
        }

        internal void EnsureDatabaseExists()
        {
            if (!File.Exists(DB_PATH))
            {
                using (SqlCeEngine engine = new SqlCeEngine(ConnectionString))
                {
                    engine.CreateDatabase();
                }
            }

            VerifyTables();
        }

        public void InsertNearbyAPRow(APInfo data, IDbConnection connection)
        {
            if (data == null) return;

            if (NearbyAPInsertCommand == null)
            {
                NearbyAPInsertCommand = Tables["NearbyAPs"].GetInsertCommand() as SqlCeCommand;
            }

            NearbyAPInsertCommand.Connection = connection as SqlCeConnection;

            // set all of the parameters
            NearbyAPInsertCommand.Parameters["@DataID"].Value = data.ID;
            NearbyAPInsertCommand.Parameters["@Name"].Value = data.Name;
            NearbyAPInsertCommand.Parameters["@MAC"].Value = data.MAC;
            NearbyAPInsertCommand.Parameters["@Signal"].Value = data.SignalStrength;

            if (connection.State != ConnectionState.Open) return;

            NearbyAPInsertCommand.ExecuteNonQuery();
        }

        public void InsertStatisticsRow(IStatisticsData data, IDbConnection connection)
        {
            if (data == null) return;

            if (NearbyAPInsertCommand == null)
            {
                StatisticsInsertCommand = Tables["NetworkStats"].GetInsertCommand() as SqlCeCommand;
            }

            if (connection.State != ConnectionState.Open) return;

            object o = ExecuteScalar("SELECT MAX(DataID) FROM NetworkStats", connection as SqlCeConnection);
            int id = o.Equals(DBNull.Value) ? 0 : (int)o;
            id++;

            StatisticsInsertCommand.Connection = connection as SqlCeConnection;

            // set all of the parameters
            StatisticsInsertCommand.Parameters["@DataID"].Value = id;
            StatisticsInsertCommand.Parameters["@TimeStamp"].Value = DateTime.Now;
            StatisticsInsertCommand.Parameters["@StatType"].Value = data.Event.ToString();
            StatisticsInsertCommand.Parameters["@TimeData"].Value = data.EventTime;
            StatisticsInsertCommand.Parameters["@Note"].Value = data.Description;

            if (connection.State != ConnectionState.Open) return;

            StatisticsInsertCommand.ExecuteNonQuery();
        }

        public int InsertAssociatedAPRow(APInfo data, IDbConnection connection)
        {
            if (data == null) return 0;

            if (AssociatedAPInsertCommand == null)
            {
                AssociatedAPInsertCommand = Tables["AssociatedAP"].GetInsertCommand() as SqlCeCommand;
            }

            if (connection.State != ConnectionState.Open) return 0;

            object o = ExecuteScalar("SELECT MAX(DataID) FROM AssociatedAP", connection as SqlCeConnection);
            int id = o.Equals(DBNull.Value) ? 0 : (int)o;
            id++;

            AssociatedAPInsertCommand.Connection = connection as SqlCeConnection;

            // set all of the parameters
            AssociatedAPInsertCommand.Parameters["@DataID"].Value = id;
            AssociatedAPInsertCommand.Parameters["@TimeStamp"].Value = DateTime.Now;
            AssociatedAPInsertCommand.Parameters["@Name"].Value = data.Name;
            AssociatedAPInsertCommand.Parameters["@MAC"].Value = data.MAC;
            AssociatedAPInsertCommand.Parameters["@Signal"].Value = data.SignalStrength;

            if (connection.State != ConnectionState.Open) return 0;

            AssociatedAPInsertCommand.ExecuteNonQuery();

            return id;
        }

        internal void ShutDown()
        {
            if (m_connection == null) return;
            m_connection.Close();
            m_connection.Dispose();
            m_connection = null;
        }

        private Dictionary<string, ITable> CreateTableObjects()
        {
            Dictionary<string, ITable> list = new Dictionary<string, ITable>();

            var tableTypes = from t in Assembly.GetExecutingAssembly().GetTypes()
                             where t.Implements<ITable>() && !t.IsAbstract
                             select t;

            Type[] empty = new Type[0];
            foreach (var type in tableTypes)
            {
                //ConstructorInfo ci = type.GetConstructor(empty); 
                //ITable table = ci.Invoke(null) as ITable;
                ITable table = Activator.CreateInstance(type) as ITable;
                list.Add(table.Name, table);
            }

            return list;
        }

        private void VerifyTables()
        {
            using (SqlCeConnection connection = new SqlCeConnection(ConnectionString))
            {
                connection.Open();
                foreach (var table in Tables)
                {
                    if (!TableExists(table.Value.Name, connection))
                    {
                        ExecuteNonQuery(table.Value.GetCreateTableSql(), connection);
                    }
                    else
                    {
                        // TODO: check version and run any upgrade scripts necessary
                        //       (not implemented in this version)
                    }
                }
                connection.Close();
            }
        }

        private object ExecuteScalar(string sql, SqlCeConnection connection)
        {
            if ((sql == null) || (sql == string.Empty))
            {
                throw new ArgumentException("invalid tablename");
            }

            if (connection.State != ConnectionState.Open) return null;

            using (SqlCeCommand cmd = new SqlCeCommand(sql, connection))
            {
                return cmd.ExecuteScalar();
            }
        }

        public bool TableExists(string tablename, SqlCeConnection connection)
        {
            if ((tablename == null) || (tablename == string.Empty))
            {
                throw new ArgumentException("invalid tablename");
            }

            string sql = string.Format("SELECT DISTINCT(TABLE_NAME) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{0}'", tablename);
            object o = ExecuteScalar(sql, connection);

            return (string.Compare((string)o, tablename, true) == 0);
        }

        private int ExecuteNonQuery(string sql, SqlCeConnection connection)
        {
            if (connection.State != ConnectionState.Open) return 0;

            using (SqlCeCommand command = new SqlCeCommand(sql, connection))
            {
                return command.ExecuteNonQuery();
            }
        }
    }
}
