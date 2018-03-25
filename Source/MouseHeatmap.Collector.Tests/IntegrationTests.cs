using FluentAssertions.Collections;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector.Tests
{
    [TestFixture]
    public class IntegrationTests : Fixture
    {
        [Test]
        public async Task adds_correct_entries_to_database()
        {
            SetNow(0);
            StartCollecting();

            SetNow(1000000);
            MoveMouseTo(25, 0);

            await WaitForLastDatabaseUpdate();

            long speedCount = 25*(long)Math.Pow(10,7)/ 1000000;
            var expectedScreenUnits = new List<ScreenUnit>
            {
                   new ScreenUnit
                   {
                      Y =0,
                      X =0,
                      MouseFinishedCount=0,
                      MousePassedCount=1,
                      SpeedCount=speedCount
                    },
                   new ScreenUnit
                   {
                       Y =0,
                      X =1,
                      MouseFinishedCount=0,
                      MousePassedCount=1,
                      SpeedCount=speedCount
                    },
                   new ScreenUnit
                   {
                       Y =0,
                       X =2,
                       MouseFinishedCount=1,
                       MousePassedCount=1,
                       SpeedCount=speedCount
                 }
            };
            GetScreenUnits().ShouldAllBeEquivalentTo(expectedScreenUnits, options => options.Excluding(su => su.ScreenUnitId));
        }

        [Test]
        public async Task does_not_add_duplicates_to_database()
        {
            SetNow(0);
            StartCollecting();

            SetNow(1000000);
            MoveMouseTo(25, 0);

            await WaitForLastDatabaseUpdate();

            long speedCount = 25 * (long)Math.Pow(10, 7) / 1000000;
            var expectedScreenUnits = new List<ScreenUnit>
            {
                   new ScreenUnit
                   {
                      Y =0,
                      X =0,
                      MouseFinishedCount=0,
                      MousePassedCount=1,
                      SpeedCount=speedCount
                    },
                   new ScreenUnit
                   {
                       Y =0,
                      X =1,
                      MouseFinishedCount=0,
                      MousePassedCount=1,
                      SpeedCount=speedCount
                    },
                   new ScreenUnit
                   {
                       Y =0,
                       X =2,
                       MouseFinishedCount=1,
                       MousePassedCount=1,
                       SpeedCount=speedCount
                 }
            };
            GetScreenUnits().ShouldAllBeEquivalentTo(expectedScreenUnits, options => options.Excluding(su => su.ScreenUnitId));
        }

        //[Test]
        //public async Task stress_test()
        //{
        //    SetNow(0);
        //    StartCollecting();

        //    for(int i = 1; i < 100; i++)
        //    {
        //        SetNow(i*1000000);
        //        MoveMouseTo(new Random().Next(0,2000), new Random().Next(0, 2000));
        //    }

        //    Thread.Sleep(10000);

        //    SetNow(101 * 1000000);
        //    MoveMouseTo(new Random().Next(0, 2000), new Random().Next(0, 2000));

        //    await WaitForLastDatabaseUpdate();
        //}


    }
}
