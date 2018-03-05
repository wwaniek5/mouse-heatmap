using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseHeatmap.Collector
{
    class MouseHeatmapDbContext : DbContext
    {
        public DbSet<ScreenUnit> ScreenUnits { get; set; }

    }
}
