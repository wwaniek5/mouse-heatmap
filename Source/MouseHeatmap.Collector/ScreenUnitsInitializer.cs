using System;
using System.Collections.Generic;
using System.Linq;

namespace MouseHeatmap.Collector
{
    internal class ScreenUnitsInitializer
    {
        private MouseHeatmapDbContext _dbContext;

        public ScreenUnitsInitializer(MouseHeatmapDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        internal void PopulateTableIfEmpty()
        {
            var a = new List<ScreenUnit>();
            //if (_dbContext.ScreenUnits.Any())
            //  {
            //for (int i=0; i < 200; i++)
            //    {
            //    Console.WriteLine(i);
            //        for (int j = 0; j < 200; j++)
            //        {
            //            a.Add(new ScreenUnit
            //            {
            //                X = i,
            //                Y = j
            //            }
            //            );
            //    }
                   
            //  //  }
                
            //}


            _dbContext.ScreenUnits.AddRange(a);
            _dbContext.SaveChanges();
        }
    }
}