using Gma.System.MouseKeyHook;
using Serilog;
using System;
using Topshelf;

namespace MouseHeatmap.Collector
{
    class Program
    {

        static void Main(string[] args)
        {
            
            HostFactory.Run(x =>
            {
                ConfigureLogger();

                x.UseSerilog();

                x.UseAssemblyInfoForServiceInfo();

                x.Service(settings => new Service(), s =>
                {
                    s.BeforeStartingService(_ => Console.WriteLine("BeforeStart"));
                    s.BeforeStoppingService(_ => Console.WriteLine("BeforeStop"));
                });

                x.OnException((exception) =>
                {
                    Console.WriteLine("Exception thrown - " + exception.Message);
                });
            });
        }

        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.RollingFile("C:/Logs/MouseHeatmapLogs/MouseHeatmapCollector.log")
                .CreateLogger();
        }
    }
}
