using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector.Tests
{
    public class Fixture
    {
        const string TestDatabaseLocation = "MouseHeatmap.Collector.Tests";
        const string TestDatabaseName = "TestMouseHeatmapDb.sqlite";
        private MockKeyboardMouseEvents _mockEvents;
        private MouseMovementsCollector _collector;
        private MouseHeatmapDbContextFactory _dbContextFactory;

        internal MockTimeProvider _mockTimeProvider { get; private set; }

        [SetUp]
        public void SetUp()
        {
            UnlockDatabase();
            TryDeleteDatabase();
            _mockEvents = new MockKeyboardMouseEvents();
            _mockTimeProvider = new MockTimeProvider();

        }

        private static void UnlockDatabase()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void TryDeleteDatabase()
        {
            try{
                File.Delete(
                    Path.Combine(
                        new DirectoryFinder().Find(TestDatabaseLocation).FullName,
                        TestDatabaseName));
            }catch(Exception e){}
        }

        protected void StartCollecting()
        {
            _dbContextFactory = new MouseHeatmapDbContextFactory(
                 databaseLocation: TestDatabaseLocation,
                 databaseName: TestDatabaseName);

            _dbContextFactory.FindDatabase();
            _dbContextFactory.Create().Database.Initialize(true);

            _collector = new MouseMovementsCollector(
             _mockTimeProvider,
             new DataRecorder(_dbContextFactory),
             new MockKeyboardMouseEventsFactory(_mockEvents)
             );

            _collector.Start();
        }

        protected void SetNow(long time)
        {
            _mockTimeProvider.CurrentTime = time;
        }


        protected void MoveMouseTo(int x, int y)
        {
            _mockEvents.FireMouseMoved(x, y);
        }


        protected async Task WaitForLastDatabaseUpdate()
        {
           await _collector.DatabaseUpdateTask;
        }

        protected List<ScreenUnit> GetScreenUnits()
        {
           using (var dbContext = _dbContextFactory.Create())
            {
                return dbContext.ScreenUnits.ToList();
            }


        }
    }
}