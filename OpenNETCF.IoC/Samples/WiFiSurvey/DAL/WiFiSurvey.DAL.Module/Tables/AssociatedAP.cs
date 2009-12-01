using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure;
using System.Data;

namespace WiFiSurvey.DAL.Tables
{
    internal class AssociatedAP : Table
    {
        public override string Name { get { return "AssociatedAP"; } }

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
                new ColumnInfo { Name = "Name", DataType = SqlDbType.NVarChar, Size = 50 },
                new ColumnInfo { Name = "MAC", DataType = SqlDbType.NVarChar, Size = 50 },
                new ColumnInfo { Name = "Signal", DataType = SqlDbType.Int },
            };
        }
    }
}
