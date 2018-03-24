using MouseHeatmap.Collector;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Generator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var dbContextFactory = new MouseHeatmapDbContextFactory();
            dbContextFactory.FindDatabase();

            using (var dbContext = dbContextFactory.Create())
            {
               var maxX= dbContext.ScreenUnits.Max(screenUnit => screenUnit.X);
                var maxY = dbContext.ScreenUnits.Max(screenUnit => screenUnit.Y);

                var bitmap = new Bitmap(maxX+2, maxY+2);


                var cleanedScreenUnits = Clean(dbContext.ScreenUnits.ToList());


                var maxMousePassedCount = cleanedScreenUnits.Max(screenUnit => screenUnit.MousePassedCount);
                var minMousePassedCount = cleanedScreenUnits.Min(screenUnit => screenUnit.MousePassedCount);

                foreach (var screenUnit in cleanedScreenUnits)
                {
                    bitmap.SetPixel(screenUnit.X+1, screenUnit.Y+1, TranslateValueToColor(screenUnit.MousePassedCount, minMousePassedCount, maxMousePassedCount));
                };

                bitmap.Save("MousePassed.bmp");

                
               
            }
            
        }

        private static Color TranslateValueToColor(long count, long min, long max)
        {
            double relativeValue = (double)(count - min) / (max - min);

            return Color.FromArgb(
                255,
                (int)(255 * relativeValue),
                (int)(255 * (1 - relativeValue)));

        }

        public static IEnumerable<ScreenUnit> Clean(List<ScreenUnit> screenUnits)
        {
           return screenUnits
                    .Where(su => su.X >= 0 && su.Y >= 0)
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
    }
}
