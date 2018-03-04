using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogger();

            Log.Information("Starting mouse heatmap collector");

            var configuration = new Configuration();
            configuration.RecreateDatabaseIfNecessary();

            

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
