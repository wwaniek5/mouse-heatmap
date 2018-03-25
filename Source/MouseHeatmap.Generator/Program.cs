using MouseHeatmap.Collector;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Generate(su => su.MousePassedCount, "MousePassedHeatmap.bmp", dbContext);
                Generate(su => su.MouseFinishedCount, "MouseFinishedHeatmap.bmp", dbContext);
                Generate(CalculateAvarageSpeed, "SpeedHeatmap.bmp", dbContext);
            }
        }

        private static long CalculateAvarageSpeed(ScreenUnit screenUnit)
        {
            if (screenUnit.MouseFinishedCount == 0)
                return 0;
            return screenUnit.SpeedCount / screenUnit.MouseFinishedCount;
        }

        private static void Generate(Func<ScreenUnit, long> selector, string fileName,  MouseHeatmapDbContext dbContext)
        {
            var screenUnits = RemoveNegativeValues(dbContext.ScreenUnits);

            var maxX = screenUnits.Max(screenUnit => screenUnit.X);
            var maxY = screenUnits.Max(screenUnit => screenUnit.Y);

            var bitmap = CreateBitmap(maxX, maxX);

            var maxValue = screenUnits.Max(selector);
            var minValue = screenUnits.Min(selector);

            foreach (var screenUnit in screenUnits)
            {
                bitmap.SetPixel(
                    screenUnit.X + 1,
                    screenUnit.Y + 1,
                    TranslateValueToColor(selector(screenUnit), minValue, maxValue));
            };

            SaveImage(fileName, bitmap);
        }

        private static void SaveImage(string fileName, Bitmap bitmap)
        {
            var executionDirectory = new DirectoryInfo(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath).Parent;
            var resultDirectory = executionDirectory.CreateSubdirectory("results");
            var path = Path.Combine(resultDirectory.FullName, fileName);
            bitmap.Save(path);
        }

        private static Bitmap CreateBitmap(int maxX, int maxY)
        {
           var bitmap= new Bitmap(maxX + 2, maxY + 2);
            return bitmap;
        }

        private static Color TranslateValueToColor(long count, long min, long max)
        {
            double relativeValue = (double)(count - min) / (max - min);

            return Color.FromArgb(
                255,
                (int)(255 * (1 - relativeValue)),
                (int)(255 * relativeValue));

        }

        public static IEnumerable<ScreenUnit> RemoveNegativeValues(IEnumerable<ScreenUnit> screenUnits) => screenUnits
                     .Where(su => su.X >= 0 && su.Y >= 0);
    }
}
