using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure;
using System.Data;

namespace WiFiSurvey.DAL.Tables
{
    internal class NetworkStats : Table
    {
        public override string Name { get { return "NetworkStats"; } }

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
                new ColumnInfo { Name = "TimeStamp", DataType = SqlDbType.DateTime },
                new ColumnInfo { Name = "StatType", DataType = SqlDbType.NVarChar, Size = 50 },
                new ColumnInfo { Name = "TimeData", DataType = SqlDbType.Int, Size = 50 },
                new ColumnInfo { Name = "Note", DataType = SqlDbType.NVarChar },
            };
        }
    }
}
