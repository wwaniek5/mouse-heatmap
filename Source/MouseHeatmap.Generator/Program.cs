using MouseHeatmap.Collector;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MouseHeatmap.Generator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dbContextFactory = new MouseHeatmapDbContextFactory(
                        databaseLocation: "Source",
                        databaseName: "MouseHeatmapDb.sqlite");
            dbContextFactory.FindDatabase();

            using (var dbContext = dbContextFactory.Create())
            {
                var cleanedScreenUnits = RemoveNegativeValues(dbContext.ScreenUnits);

                var heatmap = new Heatmap(cleanedScreenUnits);

                heatmap
                    .Draw(MousePassedSelector)
                    .ScaleUp(2)
                    .SaveToSubdirectory("results", "MousePassedHeatmap.jpg");

                heatmap
                     .Draw(MouseFinishedSelector)
                     .ScaleUp(2)
                     .SaveToSubdirectory("results", "MouseFinishedHeatmap.jpg");

                heatmap
                     .Draw(SpeedSelector)
                     .ScaleUp(2)
                     .SaveToSubdirectory("results", "SpeedHeatmap.jpg");
            }
        }

        private static long MousePassedSelector(ScreenUnit screenUnit) =>
            (long)Math.Pow(screenUnit.MousePassedCount, 0.5);

        private static long MouseFinishedSelector(ScreenUnit screenUnit) =>
            (long)Math.Pow(screenUnit.MouseFinishedCount, 0.5);

        private static long SpeedSelector(ScreenUnit screenUnit)
        {
            if (screenUnit.MouseFinishedCount == 0)
                return 0;
            return (long)Math.Pow(screenUnit.SpeedCount / screenUnit.MouseFinishedCount, 0.5);
        }     

        public static IEnumerable<ScreenUnit> RemoveNegativeValues(IEnumerable<ScreenUnit> screenUnits) => screenUnits
                     .Where(su => su.X >= 0 && su.Y >= 0);
    }
}
