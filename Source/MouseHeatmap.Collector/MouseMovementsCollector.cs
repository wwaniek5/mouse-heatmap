using Gma.System.MouseKeyHook;
using Serilog;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MouseHeatmap.Collector
{
    internal class MouseMovementsCollector
    {
        private ITimeProvider _timeProvider;

        private MouseEventArgs _lastEvent;
        private long _timeOfLastEvent;

        private List<ScreenUnit> _screenUnitsForSaving = new List<ScreenUnit>();
        private bool _isDatabaseFree = true;

        private DataRecorder _dataRecorder;
        private NewScreenUnitsCalculator _newScreenUnitsCalculator;

        public MouseMovementsCollector(ITimeProvider timeProvider,DataRecorder dataRecorder)
        {
            _timeProvider = timeProvider;
            _dataRecorder = dataRecorder;
            _newScreenUnitsCalculator = new NewScreenUnitsCalculator();
        }

        internal void Start()
        {
            _dataRecorder.Setup();
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

            _screenUnitsForSaving.AddRange(
                _newScreenUnitsCalculator.CalculateForNewEvent(_lastEvent, mouseEvent, _timeOfLastEvent, now));

            AttemptSavingToDatabase();

            _timeOfLastEvent = now;
            _lastEvent = mouseEvent;
        }

        private void AttemptSavingToDatabase()
        {
            if (_isDatabaseFree)
            {
                _isDatabaseFree = false;
                _dataRecorder.SaveAsync(_screenUnitsForSaving, OnEndOfSaving);
                _screenUnitsForSaving.Clear();
            }
        }

        private void OnEndOfSaving() => _isDatabaseFree = true;
    }
}