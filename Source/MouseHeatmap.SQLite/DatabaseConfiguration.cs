using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MouseHeatmap.SQLite
{
    public class DatabaseConfiguration
    {


        public MouseHeatmapDbContext InitializeDbContext()
        {

            var sourceFolder = FindSourceFolder();

            var databasePath = Path.Combine(sourceFolder.FullName, "MouseHeatmapDb.sqlite");

            var dbContext = new MouseHeatmapDbContext(new SQLiteConnectionString(databasePath));
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