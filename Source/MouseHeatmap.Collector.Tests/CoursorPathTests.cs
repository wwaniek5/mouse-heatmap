using FluentAssertions;
using FluentAssertions.Collections;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector.Tests
{
    [TestFixture]
    public class CoursorPathTests
    {
        [Test]
        public void lists_all_points_that_the_mouse_passed()
        {
            var p1 = new Point(1, 2);
            var p2 = new Point(6, 4);
            var sut = CoursorPath.FromEndBlocks(p1, p2);

            sut.Should().BeEquivalentTo(new HashSet<Point>
                {
                    new Point(6,4),
                    new Point(5,4),
                    new Point(4,4),
                    new Point(4,3),
                    new Point(3,3),
                    new Point(2,3),
                    new Point(1,2),
                }
            );
        }

        [Test]
        public void works_for_identical_start_amd_endpoint()
        {
            var p1 = new Point(1, 2);
            var p2 = new Point(1, 2);
            var sut = CoursorPath.FromEndBlocks(p1, p2);

            sut.Should().BeEquivalentTo(new HashSet<Point>
                {
                    new Point(1,2),
                }
            );
        }

        [Test]
        public void works_for_vertical_segment()
        {
            var p1 = new Point(1, 2);
            var p2 = new Point(1, 5);
            var sut = CoursorPath.FromEndBlocks(p1, p2);

            sut.Should().BeEquivalentTo(new HashSet<Point>
                {
                 new Point(1, 2),
            new Point(1,3),
                     new Point(1,4),
                     new Point(1,5),
                }
            );
        }

        [Test]
        public void works_for_horizontal_segment()
        {
            var p1 = new Point(5, 2);
            var p2 = new Point(1, 2);
            var sut = CoursorPath.FromEndBlocks(p1, p2);

            sut.Should().BeEquivalentTo(new HashSet<Point>
                {
                    new Point(1,2),
                    new Point(2,2),
                    new Point(3,2),
                    new Point(4,2),
                    new Point(5,2),
                }
            );
        }
    }
}
