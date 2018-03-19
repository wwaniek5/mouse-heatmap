using Gma.System.MouseKeyHook;
using Serilog;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MouseHeatmap.Collector
{
    internal class MouseMovementsCollector
    {
        private ITimeProvider _timeProvider;
        private Recorder _recorder;
        private MouseEventArgs _lastEvent;
        private long _timeOfLastEvent;

        public MouseMovementsCollector(ITimeProvider timeProvider,Recorder recorder)
        {
            _timeProvider = timeProvider;
            _recorder = recorder;
        }

        internal void Start()
        {
            _recorder.Setup();
            InitializeFirstMouseEvent();

            Hook.GlobalEvents().MouseMove += OnMouseMoved;
        }

        private void InitializeFirstMouseEvent()
        {
            _lastEvent = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);          
            _timeOfLastEvent = _timeProvider.Now();
        }

        private void OnMouseMoved(object sender,MouseEventArgs mouseEvent)
        {
            Log.Debug(mouseEvent.X + " , " + mouseEvent.Y);

            var now = _timeProvider.Now();

            _recorder.RecordAsync(_lastEvent,mouseEvent, _timeOfLastEvent, now);

            _timeOfLastEvent = now;
            _lastEvent = mouseEvent;                               
         }
    }
}