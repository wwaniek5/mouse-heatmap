using Gma.System.MouseKeyHook;
using Serilog;
using System;
using System.Windows.Forms;
using Topshelf;

namespace MouseHeatmap.Collector
{
    class Program
    {

        static void Main(string[] args)
        {
            ConfigureLogger();

            collectdata();

            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run();


        }

        private static void OnExit(object sender, EventArgs e)
        {
            //todo
        }

        public static void collectdata()
        {



            Hook.GlobalEvents().MouseMove += (sender, e) =>
            {
                Log.Information(e.X + " , " + e.Y);
            };
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
