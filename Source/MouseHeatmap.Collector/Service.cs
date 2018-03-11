using System;
using System.Windows.Forms;
using Gma.System.MouseKeyHook;
using Serilog;
using Topshelf;

namespace MouseHeatmap.Collector
{
    internal class Service: ServiceControl
    {
        private IKeyboardMouseEvents m_GlobalHook;

        public bool Start(HostControl hostControl)
        {
            Log.Information("starting");


            Do(Application.Exit);
            Application.Run(new ApplicationContext());

            return true;
        }

        public static void Do(Action quit)
        {
            Console.WriteLine("Press Q to quit.");
            Hook.GlobalEvents().KeyPress += (sender, e) =>
            {
                Console.Write(e.KeyChar);
                if (e.KeyChar == 'q') quit();
            };


            Hook.GlobalEvents().MouseMove += (sender, e) =>
            {
                Console.Write(e.X + " , " + e.Y);
            };
        }

        private void MouseMoveHook(object sender, MouseEventArgs e)
        {
            Log.Debug(e.X+" , "+e.Y);
        }

        public bool Stop(HostControl hostControl)
        {
            Log.Information("stopping");
            return true;
        }
    }
}