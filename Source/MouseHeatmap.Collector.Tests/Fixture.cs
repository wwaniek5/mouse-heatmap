using NUnit.Framework;
using System.Collections.Generic;
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
             TryDeleteDatabase();
            _mockEvents = new MockKeyboardMouseEvents();
            _mockTimeProvider = new MockTimeProvider();

        }

        private void TryDeleteDatabase() => new DirectoryFinder()
                .Find(TestDatabaseLocation)
                .GetFiles(TestDatabaseName).ToList()
                .ForEach(file => file.Delete());


        protected void StartCollecting()
        {
            _dbContextFactory = new MouseHeatmapDbContextFactory(
                 databaseLocation: TestDatabaseLocation,
                 databaseName: TestDatabaseName);

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
           var dbContextFactory= new MouseHeatmapDbContextFactory(
                 databaseLocation: TestDatabaseLocation,
                 databaseName: TestDatabaseName);

            dbContextFactory.FindDatabase();

            using (var dbContext = dbContextFactory.Create())
            {
                return dbContext.ScreenUnits.ToList();
            }


        }


    }
}