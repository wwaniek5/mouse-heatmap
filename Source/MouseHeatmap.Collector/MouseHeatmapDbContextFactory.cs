using System;
using System.IO;

namespace MouseHeatmap.Collector
{
    public class MouseHeatmapDbContextFactory
    {
        public string _databasePath;

        public void FindDatabase()
        {
            var sourceFolder = FindSourceFolder();

            _databasePath = Path.Combine(sourceFolder.FullName, "MouseHeatmapDb.sqlite");
        }

        public MouseHeatmapDbContext Create()
        {
            return new MouseHeatmapDbContext(
                new FileDatabaseConnectionFactory()
                .CreateForDatabase(_databasePath));
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
