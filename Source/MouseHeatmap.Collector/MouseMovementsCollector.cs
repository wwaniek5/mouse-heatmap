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
        private MouseHeatmapDbContext _dbContext;
        private int _counter;
        private MouseEventArgs _lastEvent;
        private long _timeOfLastEvent;

        public MouseMovementsCollector(MouseHeatmapDbContext dbContext,ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
                        _dbContext = dbContext;
        }

        internal void Start()
        {
            InitializeFields();

            Hook.GlobalEvents().MouseMove += OnMouseMoved;

        }

        private void InitializeFields()
        {
            _counter = 0;
            _lastEvent = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);          
            _timeOfLastEvent = _timeProvider.Now();
        }

        private void OnMouseMoved(object sender,MouseEventArgs mouseEvent)
        {
            Log.Debug(mouseEvent.X + " , " + mouseEvent.Y);
            
            RecordOnlyEndPosition(mouseEvent);
            RecordAllIntermediatePositionsAndSpeed(mouseEvent);

            _lastEvent = mouseEvent;
            _counter++;
            if (_counter > 50)
            {
                _dbContext.SaveChanges();
                _counter = 0;
            }
            Console.WriteLine("end");
                                
         }





        private long CalculateSpeed(MouseEventArgs lastEvent, MouseEventArgs newEvent)
        {
            var distance = PointUtils.CalculateDistance(lastEvent.Location, newEvent.Location);

            var now = _timeProvider.Now();
            var time = now - _timeOfLastEvent;

            _timeOfLastEvent = now;

            var speed =(long) (distance* Math.Pow(10, 7) / time);

            return speed;
        }

        private void RecordAllIntermediatePositionsAndSpeed( MouseEventArgs newMouseEvent)
        {
           var speed= CalculateSpeed(_lastEvent, newMouseEvent);

            var coursorPath = CoursorPath.FromEndBlocks(_lastEvent.ToScreenBlock(), newMouseEvent.ToScreenBlock());
            foreach(Point screenBlock in coursorPath)
            {
                var screenUnit = GetScreenUnit(screenBlock);
                screenUnit.MousePassedCount++;
                screenUnit.SpeedCount = screenUnit.SpeedCount + speed;
            }            
        }

        private void RecordOnlyEndPosition(MouseEventArgs mouseEvent)
        {
            var screenUnit= GetScreenUnit(mouseEvent.ToScreenBlock());
            screenUnit.MouseFinishedCount++;

        }

        private ScreenUnit GetScreenUnit(Point screenBlock)
        {
            var screenUnit = _dbContext.ScreenUnits.FirstOrDefault(unit => (unit.X == screenBlock.X && unit.Y == screenBlock.Y));

            if (screenUnit == null)
            {
                screenUnit = new ScreenUnit
                {
                    X = screenBlock.X,
                    Y = screenBlock.Y,
                    MousePassedCount = 0,
                    MouseFinishedCount = 0,
                    SpeedCount = 0
                };

                _dbContext.ScreenUnits.Add(screenUnit);
            }

            return screenUnit;
        }

   
    }
}