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
                config.SetDescription( "Service collecting data about movements of your mouse.");

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
                        //metoda servicerunner.Reinstall wywołuje servicestop, który zatrzymuje stary serwis ale przy 
                        //okazji dostajemy event o zatrzymaniu serwisu i trafiamy tutej, ale ta instancja jeszcze nie powstała
                        if (IsThisInstanceOfTheServiceBeingStopped(service))
                        {
                            Log.Information("Stopping {@serviceName}", serviceName);
                            service.Stop();
                        }
                    });

                    bool IsThisInstanceOfTheServiceBeingStopped(IMicroService service)
                    {
                        return service != null;
                    }

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
                .WriteTo.RollingFile("C:/Logs/MouseHeatmapLogs/MouseHeatmapCollector.log")
                .CreateLogger();
        }
    }
}
