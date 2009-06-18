using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlServerCe;
using WiFiSurvey.Infrastructure;
using System.Data;
using WiFiSurvey.DAL.BusinessObjects;

namespace WiFiSurvey.DAL.Tables
{
    public abstract class Table : ITable
    {
        internal protected abstract ColumnInfo[] GetColumnInfo();

        public abstract string Name { get; }
        public abstract ColumnInfo KeyField { get; }

        public virtual string GetCreateTableSql()
        {
            // default implementation - override for different versions
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("CREATE TABLE {0} (", Name));

            ColumnInfo[] infoset = GetColumnInfo();
            int i;
            for (i = 0; i < (infoset.Length - 1); i++)
            {
                sb.Append(string.Format("{0},", infoset[i].ToColumnDeclaration()));
            }

            sb.Append(string.Format("{0}", infoset[i].ToColumnDeclaration()));
            sb.Append(")");

            return sb.ToString();
        }

        public IDbCommand GetInsertCommand()
        {
            SqlCeCommand cmd = new SqlCeCommand();

            StringBuilder fieldList = new StringBuilder();
            StringBuilder valueList = new StringBuilder();

            fieldList.Append(string.Format("INSERT INTO {0} ( ", Name));
            valueList.Append("VALUES ( ");

            ColumnInfo[] infoset = GetColumnInfo();
            int i;
            string paramName;
            for (i = 0; i < (infoset.Length - 1); i++)
            {
                fieldList.Append(string.Format("{0}, ", infoset[i].Name));

                paramName = string.Format("@{0}", infoset[i].Name);
                valueList.Append(paramName + ", ");
                if (infoset[i].Size > 0)
                {
                    cmd.Parameters.Add(paramName, infoset[i].DataType, infoset[i].Size);
                }
                else
                {
                    cmd.Parameters.Add(paramName, infoset[i].DataType);
                }
            }

            fieldList.Append(string.Format("{0}", infoset[i].Name));
            paramName = string.Format("@{0}", infoset[i].Name);
            valueList.Append(paramName);
            if (infoset[i].Size > 0)
            {
                cmd.Parameters.Add(paramName, infoset[i].DataType, infoset[i].Size);
            }
            else
            {
                cmd.Parameters.Add(paramName, infoset[i].DataType);
            }

            fieldList.Append(") ");
            valueList.Append(")");
            fieldList.Append(valueList.ToString());

            cmd.CommandText = fieldList.ToString();

            return cmd;
        }
    }
}
