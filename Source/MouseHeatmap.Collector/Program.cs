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
            
            HostFactory.Run(hostConfigurator =>
            {
                ConfigureLogger();

                hostConfigurator.UseSerilog();
                hostConfigurator.UseAssemblyInfoForServiceInfo();

                hostConfigurator.Service(settings => new Service());

                hostConfigurator.OnException((exception) =>
                {
                    Log.Error(exception,"Service errored with exception: ");
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
