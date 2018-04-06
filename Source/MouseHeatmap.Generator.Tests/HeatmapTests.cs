using FluentAssertions;
using MouseHeatmap.Collector;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MouseHeatmap.Generator.Tests
{
    [TestFixture]
    public class HeatmapTests
    {
        [Test]
        public void Should_draw_correct_heatmap()
        {
            var screenUnits = new List<ScreenUnit>
            {
                   new ScreenUnit
                   {
                      Y =0,
                      X =0,
                      MouseFinishedCount=0,
                      MousePassedCount=4,
                      SpeedCount=0
                    },
                   new ScreenUnit
                   {
                       Y =0,
                      X =1,
                      MouseFinishedCount=0,
                      MousePassedCount=9,
                      SpeedCount=0
                    },
                   new ScreenUnit
                   {
                       Y =0,
                      X =2,
                      MouseFinishedCount=0,
                      MousePassedCount=16,
                      SpeedCount=0
                    },
            };

            var sut = new Heatmap(screenUnits);

            var result= sut.Draw(MousePassedSelector);

            result.GetPixel(0, 0).Should().Be(Color.FromArgb(255, 255, 0));
            result.GetPixel(1, 0).Should().Be(Color.FromArgb(255, 255/2, 255/2));
            result.GetPixel(2, 0).Should().Be(Color.FromArgb(255, 0, 255));
        }

        private static long MousePassedSelector(ScreenUnit screenUnit) =>
                (long)Math.Pow(screenUnit.MousePassedCount, 0.5);
    }
}
