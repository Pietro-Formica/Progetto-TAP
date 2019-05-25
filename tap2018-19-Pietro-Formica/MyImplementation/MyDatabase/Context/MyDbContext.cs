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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteEntity>().ToTable("Sites");
            modelBuilder.Entity<UserEntity>().ToTable("Users");
            modelBuilder.Entity<SessionEntity>().ToTable("Sessions");

            modelBuilder.Entity<SiteEntity>().HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>().HasKey(x => new {x.Id, x.SiteId});
            modelBuilder.Entity<SessionEntity>().HasKey(x => x.Id);

            modelBuilder.Entity<UserEntity>().HasRequired(s => s.Site).WithMany(x => x.Users).HasForeignKey(k => k.SiteId);

            modelBuilder.Entity<SiteEntity>().HasMany(ss => ss.SessionEntities).WithRequired(ss => ss.Site).HasForeignKey(ss => ss.SiteId);

           // modelBuilder.Entity<SessionEntity>().HasRequired(x => x.EntityUser).WithOptional(x => x.Session);
           modelBuilder.Entity<UserEntity>().HasOptional(ss => ss.Session).WithOptionalDependent(ss => ss.EntityUser);
           //modelBuilder.Entity<SessionEntity>().HasRequired(ss => ss.EntityUser).WithRequiredPrincipal(ss => ss.Session);

        }
    }

}
