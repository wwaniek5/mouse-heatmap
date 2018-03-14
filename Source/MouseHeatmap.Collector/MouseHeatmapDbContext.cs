using SQLite.CodeFirst;
using System.Data.Common;
using System.Data.Entity;

namespace MouseHeatmap.Collector
{
    public class MouseHeatmapDbContext : DbContext
    {
        private SQLiteConnectionString _connectionString;

        public MouseHeatmapDbContext(DbConnection connection) : base(connection,true)
        {
        }

        //public MouseHeatmapDbContext(SQLiteConnectionString connectionString):base(connectionString.ToString())
        //{
        //    _connectionString = connectionString;

        //  //  base(connectionString.ToString());
        //}

        public DbSet<ScreenUnit> ScreenUnits { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ScreenUnit>();
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<MouseHeatmapDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }

    }
}
