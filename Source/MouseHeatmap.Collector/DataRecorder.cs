using Serilog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector
{
    public class DataRecorder
    {
        private MouseHeatmapDbContextFactory _dbContextFactory;

        public DataRecorder(MouseHeatmapDbContextFactory mouseHeatmapDbContextFactory)
        {
            this._dbContextFactory = mouseHeatmapDbContextFactory;
        }

        private void Save(List<ScreenUnit> screenUnitsForSaving)
        {
            var groupedScreenUnits = GroupSameScreenUnits(screenUnitsForSaving);

            using (var dbContext = _dbContextFactory.Create())
            {
                
                foreach (var newScreenUnit in groupedScreenUnits)
                {
                    var screenUnitInDatabase = GetScreenUnit(dbContext, new Point(newScreenUnit.X, newScreenUnit.Y));

                    screenUnitInDatabase.MouseFinishedCount = screenUnitInDatabase.MouseFinishedCount + newScreenUnit.MouseFinishedCount;
                    screenUnitInDatabase.MousePassedCount = screenUnitInDatabase.MousePassedCount + newScreenUnit.MousePassedCount;
                    screenUnitInDatabase.SpeedCount = screenUnitInDatabase.SpeedCount + newScreenUnit.SpeedCount;
                }

                dbContext.SaveChanges();

                Log.Debug("Saved entries: " + groupedScreenUnits.Count());
                foreach(var su in groupedScreenUnits)
                {
                    Log.Debug("Saved: " + su.X+" , "+su.Y);
                }
            }
            
        }

        internal void Setup()
        {
            _dbContextFactory.FindDatabase();
        }

        public  IEnumerable<ScreenUnit> GroupSameScreenUnits(List<ScreenUnit> screenUnits)
        {
            return screenUnits
                     .GroupBy(su => new { su.X, su.Y })
                     .Select(grouping => new ScreenUnit
                     {
                         X = grouping.Key.X,
                         Y = grouping.Key.Y,
                         MousePassedCount = grouping.Sum(su => su.MousePassedCount),
                         MouseFinishedCount = grouping.Sum(su => su.MouseFinishedCount),
                         SpeedCount = grouping.Sum(su => su.SpeedCount),
                     });
        }

        public async Task SaveAsync(List<ScreenUnit> screenUnitsForSaving)
        {
           await Task.Run(() => Save(screenUnitsForSaving));
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