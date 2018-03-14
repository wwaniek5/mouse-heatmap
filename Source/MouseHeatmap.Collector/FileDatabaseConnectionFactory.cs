using System;
using System.Data.Common;

namespace MouseHeatmap.Collector
{
    internal class FileDatabaseConnectionFactory
    {

        internal DbConnection CreateForDatabase(string databasePath)
        {
            var connection = DbProviderFactories.GetFactory("System.Data.SQLite").CreateConnection();
            connection.ConnectionString = new SQLiteConnectionString(databasePath).ToString();

            return connection;

        }
    }
}