using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Generator
{
    public static class BitmapExtensions
    {
        public static Bitmap ScaleUp(this Bitmap bitmap, int scalingFactor)
        {
            return new Bitmap(bitmap, new Size(bitmap.Width * scalingFactor, bitmap.Height * scalingFactor));
        }

        public static void SaveToSubdirectory(this Bitmap bitmap,string subdirectory, string fileName)
        {
            var executionDirectory = new DirectoryInfo(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath).Parent;
            var resultDirectory = executionDirectory.CreateSubdirectory(subdirectory);
            var path = Path.Combine(resultDirectory.FullName, fileName);
            bitmap.Save(path);
        }
    }
}
