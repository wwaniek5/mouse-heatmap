using System;
using System.Drawing;

namespace MouseHeatmap.Collector
{
    public static class PointUtils
    {
        public static double CalculateDistance(Point p1, Point p2) =>
            Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
    }
}
