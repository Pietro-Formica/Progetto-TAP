using System.Data.Entity;
using MyImplementation.MyDatabase.DataEntities;

namespace MyImplementation.MyDatabase.Context
{
    class MyDBdContext : DbContext
    {
        public MyDBdContext(string connectionString) : base(connectionString)
        {


        }
        public DbSet<SiteEntity> SiteEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteEntity>()
                .HasKey(x => x.Name);

            modelBuilder.Entity<UserEntity>()
                .HasKey(x => x.Username);

            modelBuilder.Entity<SiteEntity>()
                .HasMany(x => x.Users);


        }
    }
}
