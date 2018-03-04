using System;
using System.Data.SQLite;
using System.IO;

namespace MouseHeatmap.Collector
{
    internal class Configuration
    {
        public Configuration()
        {
        }

        internal void RecreateDatabaseIfNecessary()
        {
            var sourceFolder = FindSourceFolder();

            var databasePath = Path.Combine(sourceFolder.FullName, "MouseHeatmapDb.sqlite");

            if (!File.Exists(databasePath))
            {
                SQLiteConnection.CreateFile(databasePath);
            }
        }

        private DirectoryInfo FindSourceFolder()
        {
            var currentDirectory =new DirectoryInfo(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            while (currentDirectory.Name != "Source")
            {
                currentDirectory = currentDirectory.Parent;
            }
          
            return currentDirectory;
        }
    }
}