using Microsoft.EntityFrameworkCore;
using Tests.Classes;

namespace Tests
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(string databaseName)
        {
            DatabaseName = databaseName;
        }

        public TestDbContext(string databaseName, DbContextOptions<TestDbContext> options) : base(options)
        {
            DatabaseName = databaseName;
        }

        public string DatabaseName { get; set; }
        //public ModelBuilder ModelBuilder { get; set; }

        public DbSet<EntityWithoutIndex> EntitiesWithoutIndexes { get; set; }
        public DbSet<EntityWithImplicitNonUniqueIndex> EntitiesWithImplicitNonUniqueIndexes { get; set; }
        public DbSet<EntityWithNonUniqueIndex> EntitiesWithNonUniqueIndexes { get; set; }
        public DbSet<EntityWithUniqueIndex> EntitiesWithUniqueIndexes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase(DatabaseName);
            }
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // This will ensure all tests are correct.
            //modelBuilder.SetIndexOnEntitiesByAttribute();

            modelBuilder.Entity<EntityWithUniqueIndex>()
                .HasIndex(b => b.Name)
                .IsUnique();

            //ModelBuilder = modelBuilder;
        }
    }
}