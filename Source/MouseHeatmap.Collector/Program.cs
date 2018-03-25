
using Serilog;
using Serilog.Events;
using System;
using System.Threading;
using System.Windows.Forms;


namespace MouseHeatmap.Collector
{
    class Program
    {
       private  static Mutex mutex = new Mutex(false, "MouseHeatmap.Collector");
        
        private static MouseMovementsCollector _collector;

        static void Main(string[] args)
        {
            EnsureAnotherInstanceIsNotRunning();         
     

            ConfigureLogger();

            try
            {
                Log.Information("starting");

                _collector = new MouseMovementsCollector( new TimeProvider(), new DataRecorder(new MouseHeatmapDbContextFactory()));
                _collector.Start();


                Application.ApplicationExit += new EventHandler(OnExit);
                Application.Run();
            }
            catch(Exception e)
            {
                Log.Error(e,"MouseHeatmap.Collector errored: ");
                Log.CloseAndFlush();

                throw e;
            }

        }
        private static void EnsureAnotherInstanceIsNotRunning()
        {           

            if(!mutex.WaitOne(TimeSpan.FromSeconds(5), false))
            {
                throw new Exception("Another instance of MouseHeatmap.Collector is already running");
            }
   
        }

        private static void OnExit(object sender, EventArgs e)
        {
            
        }


        private static void ConfigureLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .WriteTo.RollingFile(
                "C:/Logs/MouseHeatmapLogs/MouseHeatmapCollector.log", 
                restrictedToMinimumLevel: LogEventLevel.Information)         
                .CreateLogger();
        }
    }
}
