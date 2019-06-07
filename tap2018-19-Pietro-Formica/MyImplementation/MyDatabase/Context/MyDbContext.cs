using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Runtime.InteropServices;
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
        public DbSet<SessionEntity> Session { get; set; }

        public DbSet<AuctionEntity> AuctionEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteEntity>().ToTable("Sites");
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<SessionEntity>().ToTable("Sessions");
            modelBuilder.Entity<AuctionEntity>().ToTable("Auctions").Property(x => x.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<SiteEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasKey(x => new {x.Id, x.SiteId});                                       
            modelBuilder.Entity<SessionEntity>().HasKey(x => new{x.Id,x.SiteId});
            modelBuilder.Entity<AuctionEntity>().HasKey(x => new {x.ID, x.SiteID});

            modelBuilder.Entity<UserEntity>().HasRequired(s => s.Site).WithMany(x => x.Users).HasForeignKey(k => k.SiteId);

            modelBuilder.Entity<SiteEntity>().HasMany(ss => ss.SessionEntities).WithRequired(ss => ss.Site).HasForeignKey(ss => ss.SiteId);

            modelBuilder.Entity<SessionEntity>().HasRequired(x => x.EntityUser).WithOptional(x => x.Session);

            modelBuilder.Entity<SiteEntity>().HasMany(au => au.AuctionEntities).WithRequired(s => s.Site);

            modelBuilder.Entity<UserEntity>().HasMany(au => au.SellerAuctionEntities).WithRequired(u => u.Seller)
                .HasForeignKey(u => new {u.SellerId, u.SiteID}).WillCascadeOnDelete(false);

            modelBuilder.Entity<AuctionEntity>().HasOptional(us => us.CurrentWinner).WithMany(au => au.WinnerAuctionEntities)
                .HasForeignKey(us => new { us.WinnerId,us.WinnerSiteId} ).WillCascadeOnDelete(false);



        }
    }

}
