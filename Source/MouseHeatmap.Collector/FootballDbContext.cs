using SQLite.CodeFirst;
using System.Data.Entity;

namespace MouseHeatmap.Collector
{
    public class FootballDbContext : DbContext
    {
        public FootballDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        public DbSet<Stadion> Stadions { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stadion>();
            var initializer = new SqliteCreateDatabaseIfNotExists<FootballDbContext>(modelBuilder);
            Database.SetInitializer(initializer);
        }
    }
}