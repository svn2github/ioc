using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure;
using System.Data;
using System.Data.SqlServerCe;
using WiFiSurvey.DAL.SQLCE;

namespace WiFiSurvey.DAL.Tables
{
    internal class NearbyAPs : Table
    {
        private int nextID = -1;

        public override string Name { get { return "NearbyAPs"; } }

        // his is a foreign key back to the AssociatedAP table
        public override ColumnInfo KeyField
        {
            get
            {
                return new ColumnInfo { Name = "DataID", DataType = SqlDbType.Int };
            }
        }

        internal protected override ColumnInfo[] GetColumnInfo()
        {
            return new ColumnInfo[]
            {
                KeyField,
                new ColumnInfo { Name = "Name", DataType = SqlDbType.NVarChar, Size = 50 },
                new ColumnInfo { Name = "MAC", DataType = SqlDbType.NVarChar, Size = 50 },
                new ColumnInfo { Name = "Signal", DataType = SqlDbType.Int },
            };
        }
    }
}
