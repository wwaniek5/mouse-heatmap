
using Serilog;
using System;
using System.Threading;
using System.Windows.Forms;


namespace MouseHeatmap.Collector
{
    class Program
    {
       private  static Mutex mutex = new Mutex(false, "MouseHeatmap.Collector");

        private static MouseHeatmapDbContext _dbContext;
        private static MouseMovementsCollector _collector;

        static void Main(string[] args)
        {
            EnsureAnotherInstanceIsNotRunning();         
     

            ConfigureLogger();


            var configuration = new DatabaseConfiguration();
            _dbContext = configuration.InitializeDbContext();

            _collector = new MouseMovementsCollector(_dbContext);
            _collector.Start();
 

            Application.ApplicationExit += new EventHandler(OnExit);
            Application.Run();


        }
        private static void EnsureAnotherInstanceIsNotRunning()
        {           

            if(!mutex.WaitOne(TimeSpan.FromSeconds(5), false))
            {
                throw new Exception("Another instance of MouseHeatmap.Collector is already runnig");
            }
   
        }

        private static void OnExit(object sender, EventArgs e)
        {
            _dbContext.SaveChanges();
            _dbContext.Dispose();
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
