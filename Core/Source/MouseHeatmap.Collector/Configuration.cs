using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace MouseHeatmap.Collector
{
    internal class DatabaseConfiguration
    {
        public DatabaseConfiguration()
        {
        }

        internal MouseHeatmapDbContext InitializeDbContext()
        {
            var sourceFolder = FindSourceFolder();

            var databasePath = Path.Combine(sourceFolder.FullName, "MouseHeatmapDb.sqlite");

            var dbContext =new MouseHeatmapDbContext(new SQLiteConnectionString(databasePath));
            dbContext.Database.Migrate();
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