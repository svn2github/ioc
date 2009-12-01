using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using WiFiSurvey.Infrastructure;
using System.Data;

namespace WiFiSurvey.DAL.BusinessObjects
{
    public interface ITable
    {
        /// <summary>
        /// Returns a parameterized command that will allow table inserts
        /// </summary>
        /// <param name="databaseVersion"></param>
        /// <returns>SQL</returns>
        IDbCommand GetInsertCommand();

        /// <summary>
        /// Gets the SQL to create the associated table for the latest (current) version of the database
        /// </summary>
        /// <returns>SQL</returns>
        string GetCreateTableSql();

        /// <summary>
        /// The table name
        /// </summary>
        string Name { get; }

        ColumnInfo KeyField { get; }

    }
}
