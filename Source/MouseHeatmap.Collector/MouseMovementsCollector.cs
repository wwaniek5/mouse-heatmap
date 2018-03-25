using Gma.System.MouseKeyHook;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseHeatmap.Collector
{
    public class MouseMovementsCollector
    {
        private ITimeProvider _timeProvider;

        private MouseEventArgs _lastEvent;
        private long _timeOfLastEvent;

        private List<ScreenUnit> _screenUnitsForSaving = new List<ScreenUnit>();

        public Task DatabaseUpdateTask=Task.CompletedTask;

        private DataRecorder _dataRecorder;
        private NewScreenUnitsCalculator _newScreenUnitsCalculator;
        private IKeyboardMouseEventsFactory _keyboardMouseEventsFactory;


        public MouseMovementsCollector(ITimeProvider timeProvider,DataRecorder dataRecorder, IKeyboardMouseEventsFactory keyboardMouseEventsFactory)
        {
            _timeProvider = timeProvider;
            _dataRecorder = dataRecorder;
            _newScreenUnitsCalculator = new NewScreenUnitsCalculator();
            _keyboardMouseEventsFactory = keyboardMouseEventsFactory;
        }

        public void Start()
        {
            _dataRecorder.Setup();
            InitializeFirstMouseEvent();

            _keyboardMouseEventsFactory.Create().MouseMove += OnMouseMoved;
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

            _screenUnitsForSaving.AddRange(
                _newScreenUnitsCalculator.CalculateForNewEvent(_lastEvent, mouseEvent, _timeOfLastEvent, now));

            AttemptSavingToDatabase();

            _timeOfLastEvent = now;
            _lastEvent = mouseEvent;
        }

        private void AttemptSavingToDatabase()
        {
            if (DatabaseUpdateTask.IsCompleted)
            {
                Log.Debug("Trying to save: " + _screenUnitsForSaving.Count);

                DatabaseUpdateTask =_dataRecorder.SaveAsync(new List<ScreenUnit>(_screenUnitsForSaving));
                _screenUnitsForSaving.Clear();
            }
        }

    }
}