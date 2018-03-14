using System;
using System.Data.Common;
using System.IO;

namespace MouseHeatmap.Collector
{
    public class DatabaseConfiguration
    {

        public MouseHeatmapDbContext InitializeDbContext()
        {
            var sourceFolder = FindSourceFolder();

            var databasePath = Path.Combine(sourceFolder.FullName, "MouseHeatmapDb.sqlite");

            var conn = DbProviderFactories.GetFactory("System.Data.SQLite").CreateConnection();
            conn.ConnectionString = new SQLiteConnectionString(databasePath).ToString();

            var dbContext = new MouseHeatmapDbContext(conn);

            return dbContext;

        }

        private DirectoryInfo FindSourceFolder()
        {
            var currentDirectory = new DirectoryInfo(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            while (currentDirectory.Name != "Source")
            {
                currentDirectory = currentDirectory.Parent;
            }

            return currentDirectory;
        }
    }
}