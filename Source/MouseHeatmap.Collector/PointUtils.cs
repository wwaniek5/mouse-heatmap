using System;
using System.Drawing;

namespace MouseHeatmap.Collector
{
    public static class PointUtils
    {
        public static int NumberOfPixelsInScreenBlock=10;

        public static Point GetScreenBlockFromPixel(Point pixel)
        {
            return new Point(pixel.X / NumberOfPixelsInScreenBlock, pixel.Y / NumberOfPixelsInScreenBlock);
        }

        public static double CalculateDistanceSquared(Point p1, Point p2) => Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2);

        public static double CalculateDistance(Point p1, Point p2) => Math.Sqrt(CalculateDistanceSquared(p1,p2));
    }
}
