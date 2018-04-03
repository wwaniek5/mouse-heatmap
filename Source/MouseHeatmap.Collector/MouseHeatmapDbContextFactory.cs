using System;
using System.Data.Common;
using System.IO;

namespace MouseHeatmap.Collector
{
    public class MouseHeatmapDbContextFactory
    {
        private string _databasePath;
        private string _databaseLocation;
        private string _databaseName;

        public MouseHeatmapDbContextFactory(string databaseLocation,string databaseName)
        {
            _databaseLocation = databaseLocation;
            _databaseName = databaseName;
        }

        public void FindDatabase()
        {
            var databaseLocationFolder = new DirectoryFinder().Find(_databaseLocation);
            _databasePath = Path.Combine(databaseLocationFolder.FullName, _databaseName);
        }

        public MouseHeatmapDbContext Create()
        {
            return new MouseHeatmapDbContext(
                new FileDatabaseConnectionFactory()
                .CreateForDatabase(_databasePath));
        }
    }
}
