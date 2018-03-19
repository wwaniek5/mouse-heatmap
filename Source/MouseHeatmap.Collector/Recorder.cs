using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseHeatmap.Collector
{
    public class Recorder
    {
        private MouseHeatmapDbContextFactory _dbContextFactory;

        public Recorder(MouseHeatmapDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task RecordAsync(MouseEventArgs lastEvent, MouseEventArgs newEvent, long timeOfLastEvent, long timeOfNewEvent)
        {
            await Task.Run(() => Record(lastEvent, newEvent, timeOfLastEvent, timeOfNewEvent));
        }

        private void Record(MouseEventArgs lastEvent, MouseEventArgs newEvent, long timeOfLastEvent, long timeOfNewEvent)
        {
            using (var dbContext = _dbContextFactory.Create())
            {
                RecordOnlyEndPosition(dbContext, newEvent);
                RecordAllIntermediatePositionsAndSpeed(dbContext, lastEvent, newEvent, timeOfLastEvent, timeOfNewEvent);
                dbContext.SaveChanges();
            }

        }

        internal void Setup()
        {
            _dbContextFactory.FindDatabase();
        }

        private void RecordAllIntermediatePositionsAndSpeed(MouseHeatmapDbContext dbContext, MouseEventArgs lastEvent, MouseEventArgs newEvent, long timeOfLastEvent, long timeOfNewEvent)
        {
            var speed = CalculateSpeed(lastEvent, newEvent, timeOfLastEvent, timeOfNewEvent);
            
            var coursorPath = CoursorPath.FromEndBlocks(lastEvent.ToScreenBlock(), newEvent.ToScreenBlock());
            foreach (Point screenBlock in coursorPath)
            {
                var screenUnit = GetScreenUnit(dbContext,screenBlock);
                screenUnit.MousePassedCount++;
                screenUnit.SpeedCount = screenUnit.SpeedCount + speed;
            }
        }

        private long CalculateSpeed(MouseEventArgs lastEvent, MouseEventArgs newEvent, long timeOfLastEvent, long timeOfNewEvent)
        {
            var distance = PointUtils.CalculateDistance(lastEvent.Location, newEvent.Location);
  
            var time = timeOfNewEvent - timeOfLastEvent;

            var speed = (long)(distance * Math.Pow(10, 7) / time);
            return speed;
        }

        private void RecordOnlyEndPosition(MouseHeatmapDbContext dbContext, MouseEventArgs mouseEvent)
        {
            var screenUnit = GetScreenUnit(dbContext, mouseEvent.ToScreenBlock());
            screenUnit.MouseFinishedCount++;

        }

        private ScreenUnit GetScreenUnit(MouseHeatmapDbContext dbContext, Point screenBlock)
        {
            var screenUnit = dbContext.ScreenUnits.FirstOrDefault(unit => (unit.X == screenBlock.X && unit.Y == screenBlock.Y));

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

                dbContext.ScreenUnits.Add(screenUnit);
            }

            return screenUnit;
        }
    }
}