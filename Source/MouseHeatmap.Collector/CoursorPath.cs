using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace MouseHeatmap.Collector
{
    public class CoursorPath : IEnumerable<Point>
    {
        private HashSet<Point> _screenBlocks = new HashSet<Point>();


        private CoursorPath(HashSet<Point> points)
        {
            this._screenBlocks = points;
        }

        public static CoursorPath FromEndBlocks(Point p1, Point p2)
        {
            var distance = PointUtils.CalculateDistance(p1, p2)+4;//+ anything
            if (distance == 0)
            {
                return new CoursorPath(new HashSet<Point> { p1 });
            }

            var increment = 1 / distance;

            var screenBlocks = new HashSet<Point>();

            for (double lambda = 0; lambda <= 1; lambda = lambda + increment)
            {
                Point screenBlock = new Point(
                    (int)Math.Ceiling(lambda * p1.X + (1 - lambda) * p2.X),
                    (int)Math.Ceiling(lambda * p1.Y + (1 - lambda) * p2.Y));

                screenBlocks.Add(screenBlock);
            }

            screenBlocks.Add(p1);
            screenBlocks.Add(p1);

            return new CoursorPath(screenBlocks);
        }

       

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