using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;
using WiFiSurvey.Infrastructure.BusinessObjects;

namespace WiFiSurvey.DAL.SQLCE
{
    public class SQLCEConnection
    {
        private SqlCeConnection m_connection;

        private string m_connectString = "Data Source=\\WIFISURVEY.sdf";

        private string m_apCurrentTable = "CurrentAP";
        private string m_apListTable = "NearbyAPList";
        private string m_desktopConnectTable = "DesktopConnection";
        // Retrieve the connection string from the settings file.
        // Your connection string name will end in "ConnectionString"
        // So it could be coolConnectionString or something like that.

        public SQLCEConnection()
        {

        }
        private string GetTable(DataEvent Event)
        {
            switch (Event.Location)
            {
                case "Desktop Conn.": return m_desktopConnectTable;
                case "AP List": return m_apListTable;
                default: return m_apCurrentTable;
            }
        }

        public void InsertValueIntoTable(DataEvent Event)
        {
            // Open the connection using the connection string.
            using (m_connection = new SqlCeConnection(m_connectString))
            {
                // Insert into the SqlCe table. ExecuteNonQuery is best for inserts.
                string table = GetTable(Event);
                if (m_connection.State == ConnectionState.Closed)
                {
                    m_connection.Open();
                }
                using (SqlCeCommand com = new SqlCeCommand("INSERT INTO " + table + " VALUES(@num)", m_connection))
                {
                    com.Parameters.AddWithValue("@num", Event.Description);
                    com.ExecuteNonQuery();
                }
                m_connection.Close();
            }
        }
    }
}
