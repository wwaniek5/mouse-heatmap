using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MouseHeatmap.Collector
{
   public class MouseHeatmapDbContext : DbContext
    {
        public DbSet<ScreenUnit> ScreenUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\Wojtek\Source\Repos\mouse-heatmap\Core\Source\MouseHeatmapDb.sqlite;");
            
        }
    }
}
