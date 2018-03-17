using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace MouseHeatmap.Collector
{
    internal class CoursorPath : IEnumerable<Point>
    {
        private HashSet<Point> _screenBlocks = new HashSet<Point>();


        private CoursorPath(HashSet<Point> points)
        {
            this._screenBlocks = points;
        }

        internal static CoursorPath FromEndPoints(Point p1, Point p2)
        {
            var distance = CalculateDistance(p1, p2);

            var increment = 1 / distance;

            var screenBlocks = new HashSet<Point>();

            for (double lambda = 0; lambda <= 1; lambda = lambda + increment)
            {
                Point screenBlock = new Point(
                    (int)Math.Ceiling(lambda * p1.X + (1 - lambda) * p2.X),
                    (int)Math.Ceiling(lambda * p1.Y + (1 - lambda) * p2.Y));

                screenBlocks.Add(screenBlock);
            }

            return new CoursorPath(screenBlocks);

            //if (p1 == new Point(0, 0) && p2 == new Point(0, 0))
            //{
            //    return new CoursorPath(new HashSet<Point> { p1 });
            //}

            //var system = new LinearSystem(p1.X, p1.Y, 1, p2.X, p2.Y, 1);
            //var solution = system.Solve();

            //if (solution.IsIndeterminate)
            //{
            //    return new CoursorPath(new HashSet<Point> { p1 });
            //}

            //var A = solution.X;
            //var B = solution.Y;


        }

        private static double CalculateDistance(Point p1, Point p2) => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));

        public IEnumerator<Point> GetEnumerator()
        {
            return _screenBlocks.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}