using Gma.System.MouseKeyHook;
using Serilog;
using System;
using System.Windows.Forms;


namespace MouseHeatmap.Collector
{
    class Program
    {
        private static MouseHeatmapDbContext _dbContext;

        static void Main(string[] args)
        {
            ConfigureLogger();


//            using (var context = new FootballDbContext("footballDb"))
//            {
//                context.Stadions.Add(
//     new Stadion
//     {
//         Name = "Stade de Suisse",
//         City = "Bern",
//         Street = "Papiermühlestrasse 71"
//     }
//);

//                context.SaveChanges();
//            }

            var configuration = new DatabaseConfiguration();
            _dbContext = configuration.InitializeDbContext();


            var screenUnit = new ScreenUnit
            {

            };
            _dbContext.ScreenUnits.Add(screenUnit);
            _dbContext.SaveChanges();
            collectdata();

            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run();


        }

        private static void OnExit(object sender, EventArgs e)
        {
            _dbContext.SaveChanges();
            _dbContext.Dispose();
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
