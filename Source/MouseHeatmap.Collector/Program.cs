using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using PeterKottas.DotNetCore.WindowsService;
using PeterKottas.DotNetCore.WindowsService.Interfaces;
using Serilog;
using System;

namespace MouseHeatmap.Collector
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigureLogger();
            
            ServiceRunner<Service>.Run(config =>
            {
                var serviceName = "MouseHeatmapColector";
                config.SetName(serviceName);
                config.Service(serviceConfig =>
                {
                    serviceConfig.ServiceFactory((extraArguments, controller) =>
                    {
                        return new Service();
                    });

                    serviceConfig.OnStart((service, extraParams) =>
                    {
                        Log.Information("Starting {@serviceName}", serviceName);
                        service.Start();
                    });

                    serviceConfig.OnStop(service =>
                    {
                        Log.Information("Stopping {@serviceName}", serviceName);
                        service.Stop();
                    });

                    serviceConfig.OnError(e =>
                    {
                        Log.Error(e, "Service errored with exception: ");
                    });
                });
            });
                       
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
