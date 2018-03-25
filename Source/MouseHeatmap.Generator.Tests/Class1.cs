using FluentAssertions;
using MouseHeatmap.Collector;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Generator.Tests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void cleaning_screenunits_removes_negative()
        {
            var dirtyScreenUnits = new List<ScreenUnit>
            {
                new ScreenUnit
                {
                       Y =-1,
                      X =0,
                 },

                   new ScreenUnit
                {
                       Y =-1,
                      X =-2,
                 },

                       new ScreenUnit
                {
                       Y =1,
                      X =-2,
                 },

                       new ScreenUnit
                {
                       Y =1,
                      X =2,
                 },

            };
            var cleanScreenUnits = Program.Clean(dirtyScreenUnits).ToList();

            cleanScreenUnits.Count.Should().Be(1);
            cleanScreenUnits[0].ShouldBeEquivalentTo(

                       new ScreenUnit
                       {
                           Y = 1,
                           X = 2,
                       }, options => options
                        .Excluding(su => su.ScreenUnitId)
                        .Excluding(su => su.MouseFinishedCount)
                        .Excluding(su => su.MousePassedCount)
                        .Excluding(su => su.SpeedCount)

            );
        }

        [Test]
        public void cleaning_screenunits_adds_duplicates()
        {
            var dirtyScreenUnits = new List<ScreenUnit>
            {
                new ScreenUnit
                {
                       Y =1,
                      X =0,
                      MouseFinishedCount=1,
                      MousePassedCount=1,
                      SpeedCount=1
                 },

                   new ScreenUnit
                {
                       Y =1,
                      X =2,
                      MouseFinishedCount=1,
                      MousePassedCount=1,
                      SpeedCount=1
                 },

                       new ScreenUnit
                {
                       Y =1,
                      X =2,
                       MouseFinishedCount=1,
                      MousePassedCount=1,
                      SpeedCount=1
                 }

            };
            var cleanScreenUnits = Program.Clean(dirtyScreenUnits).ToList();

            cleanScreenUnits.Count.Should().Be(2);
            cleanScreenUnits[0].ShouldBeEquivalentTo(

                       new ScreenUnit
                       {
                           Y = 1,
                           X = 0,
                           MouseFinishedCount = 1,
                           MousePassedCount = 1,
                           SpeedCount = 1
                       }, options => options
                        .Excluding(su => su.ScreenUnitId)


            );

            cleanScreenUnits[1].ShouldBeEquivalentTo(

           new ScreenUnit
           {
               Y = 1,
               X = 2,
               MouseFinishedCount = 2,
               MousePassedCount = 2,
               SpeedCount = 2
           }, options => options
            .Excluding(su => su.ScreenUnitId));

        }
    }
}
