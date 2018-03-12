using Gma.System.MouseKeyHook;
using MouseHeatmap.SQLite;
using Serilog;
using System;
using System.Windows.Forms;


namespace MouseHeatmap.Collector
{
    class Program
    {
        private static object _dbContext;

        static void Main(string[] args)
        {
            ConfigureLogger();

            var configuration = new DatabaseConfiguration();
            _dbContext = configuration.InitializeDbContext();


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
