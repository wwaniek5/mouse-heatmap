using SQLite.CodeFirst;
using System.Data.Common;
using System.Data.Entity;

namespace MouseHeatmap.Collector
{
    public class MouseHeatmapDbContext : DbContext
    {

        public MouseHeatmapDbContext(DbConnection connection) : base(connection,true)
        {
        }

        public DbSet<ScreenUnit> ScreenUnits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ScreenUnit>();
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<MouseHeatmapDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
