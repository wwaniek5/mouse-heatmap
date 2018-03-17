using Gma.System.MouseKeyHook;
using Serilog;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MouseHeatmap.Collector
{
    internal class MouseMovementsCollector
    {
        private int _numberOfPixelsInScreenUnit;
        private MouseHeatmapDbContext _dbContext;

        private Point _lastEndPosition = new Point();

      

        public MouseMovementsCollector(MouseHeatmapDbContext dbContext,int numberOfPixelsInScreenUnit)
        {
            _numberOfPixelsInScreenUnit = numberOfPixelsInScreenUnit;
            _dbContext = dbContext;
        }

        internal void Start()
        {
            Hook.GlobalEvents().MouseMove += OnMouseMoved;

        }

        private void OnMouseMoved(object sender,MouseEventArgs mouseEvent)
        {
           
           var newEndPoint= GetPointWhereMouseFinishedMovement(mouseEvent);

            Log.Debug(newEndPoint.X + " , " + newEndPoint.Y);

            RecordOnlyEndPosition(newEndPoint);
            RecordAllIntermediatePositions(newEndPoint);

            _dbContext.SaveChanges();

            _lastEndPosition = newEndPoint;



        }

        private void RecordAllIntermediatePositions(Point newEndPoint)
        {
            var coursorPath = CoursorPath.FromEndPoints(_lastEndPosition, newEndPoint);
            
        }

        private  Point GetPointWhereMouseFinishedMovement(MouseEventArgs mouseEvent)
        {
            return new Point(mouseEvent.X / _numberOfPixelsInScreenUnit, mouseEvent.Y / _numberOfPixelsInScreenUnit);
        }

        private void RecordOnlyEndPosition(Point endPoint)
        {
            RecordPoint(endPoint);

        }

        private void RecordPoint(Point screenPoint)
        {
            var screenUnit = _dbContext.ScreenUnits.FirstOrDefault(unit => (unit.X == screenPoint.X && unit.Y == screenPoint.Y));

            if (screenUnit == null)
            {
                _dbContext.ScreenUnits.Add(new ScreenUnit
                {
                    X = screenPoint.X,
                    Y = screenPoint.Y,
                    MouseEnteredCount = 1,
                });
            }
            else
            {
                screenUnit.MouseEnteredCount++;
            }
        }
    }
}