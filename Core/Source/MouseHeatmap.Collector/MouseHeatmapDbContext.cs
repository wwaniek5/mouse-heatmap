using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MouseHeatmap.Collector
{
   public class MouseHeatmapDbContext : DbContext
    {
        private SQLiteConnectionString _connectionString;
        public MouseHeatmapDbContext(SQLiteConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<ScreenUnit> ScreenUnits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_connectionString.ToString());
            
        }
    }
}
