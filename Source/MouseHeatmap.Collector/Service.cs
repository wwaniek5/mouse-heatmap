using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using POC;
using Serilog;
using Topshelf;

namespace MouseHeatmap.Collector
{
    internal class Service: ServiceControl
    {
        private Controller controller;

        public bool Start(HostControl hostControl)
        {
            Log.Information("Starting service");


            Do(Application.Exit);
             Application.Run();

             controller = new Controller();
            
            controller.SetupKeyboardHooks();

            return true;
        }

        public static void Do(Action quit)
        {



            Hook.GlobalEvents().MouseMove += (sender, e) =>
            {
                Log.Information(e.X + " , " + e.Y);
            };
        }

        private void MouseMoveHook(object sender, MouseEventArgs e)
        {
            Log.Debug(e.X+" , "+e.Y);
        }

        public bool Stop(HostControl hostControl)
        {
            controller.Dispose();
            Log.Information("stopping");
            return true;
        }
    }
}