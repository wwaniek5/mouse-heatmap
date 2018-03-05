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
            
            var configuration = new DatabaseConfiguration();
          var dbContext= configuration.InitializeDbContext();


     

                var screenUnit = new ScreenUnit
                {
                    ScreenUnitId = 1
                };
            dbContext.ScreenUnits.Add(screenUnit);


            dbContext.SaveChanges();
            dbContext.Dispose();

            Log.Information("Quiting mouse heatmap collector");

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
