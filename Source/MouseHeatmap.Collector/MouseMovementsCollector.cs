using Gma.System.MouseKeyHook;
using Serilog;
using System.Linq;

namespace MouseHeatmap.Collector
{
    internal class MouseMovementsCollector
    {
        private MouseHeatmapDbContext _dbContext;

        public MouseMovementsCollector(MouseHeatmapDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal void Start()
        {
          // new ScreenUnitsInitializer(_dbContext).PopulateTableIfEmpty();


            Hook.GlobalEvents().MouseMove += (sender, e) =>
            {
                Log.Information(e.X + " , " + e.Y);

               

              var screenUnit=  _dbContext.ScreenUnits.FirstOrDefault(unit=>(unit.X==e.X/10 && unit.Y==e.Y/10));

                if (screenUnit == null)
                {
                    _dbContext.ScreenUnits.Add(new ScreenUnit {
                        X=e.X,
                        Y=e.Y,
                        MouseEnteredCount=1,
                    });
                }
                else
                {
                    screenUnit.MouseEnteredCount++;
                }
              
                
                _dbContext.SaveChanges();
            };
        }
    }
}