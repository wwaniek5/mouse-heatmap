using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Serilog;
using System;

namespace MouseHeatmap.Collector
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogger();

            Log.Information("Starting mouse heatmap collector");
            
            var configuration = new Configuration();
            //  configuration.RecreateDatabaseIfNecessary();


            using (var db = new MouseHeatmapDbContext())
            {
                db.Database.Migrate();

                var screenUnit = new ScreenUnit
                {
                    ScreenUnitId = 1
                };
                db.ScreenUnits.Add(screenUnit);
                db.SaveChanges();
            }
                Console.ReadKey();
        }

        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.RollingFile("MouseHeatmapCollector.log")
                .CreateLogger();
        }
    }
}
