
using PeterKottas.DotNetCore.WindowsService.Interfaces;


namespace MouseHeatmap.Collector
{
    public class Service : IMicroService
    {
        private MouseHeatmapDbContext _dbContext;
     
        public void Start()
        {

            var configuration = new DatabaseConfiguration();
            _dbContext = configuration.InitializeDbContext();

            var screenUnit = new ScreenUnit
            {
             
            };
            _dbContext.ScreenUnits.Add(screenUnit);
        }



        public void Stop()
        {
            _dbContext.SaveChanges();
            _dbContext.Dispose();
        }
    }
}