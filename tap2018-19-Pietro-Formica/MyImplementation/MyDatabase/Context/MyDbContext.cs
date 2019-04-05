using System.Data.Entity;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.MyDatabase.Context
{
    class MyDBdContext : DbContext
    {
        public MyDBdContext(string connectionString) : base(connectionString)
        {


        }
        public DbSet<SiteEntity> SiteEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteEntity>()
                        .HasKey(x => x.Name);



        }
    }
}
