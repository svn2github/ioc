using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace WiFiSurvey.Infrastructure
{
    public struct ColumnInfo
    {
        public string Name;
        public SqlDbType DataType;
        public int Size;

        public string ToColumnDeclaration()
        {
            switch (DataType)
            {
                case SqlDbType.NVarChar:
                    return string.Format("{0} nvarchar({1})", Name, Size);
                case SqlDbType.NChar:
                    return string.Format("{0} nchar({1})", Name, Size);
                case SqlDbType.Int:
                    return string.Format("{0} int", Name);
                case SqlDbType.DateTime:
                    return string.Format("{0} datetime", Name);
                case SqlDbType.Image:
                    return string.Format("{0} image", Name);
                case SqlDbType.Bit:
                    return string.Format("{0} bit", Name);
                case SqlDbType.SmallMoney:
                    return string.Format("{0} smallmoney", Name);
                case SqlDbType.Money:
                    return string.Format("{0} money", Name);
                default:
                    throw new NotSupportedException(string.Format("Type '{0}' not supported", DataType));
            }
        }
    }
}
