using System.Data.Entity;
using System.Linq;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.MyDatabase.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.MyDatabase.Implements
{
    class ManagerEntitySite : IEntityShooter<SiteEntity>
    {
        private string _connectionString;
        public void ControlConnectionString(string connectionString)
        {
            if (!Database.Exists(connectionString))
                throw new UnavailableDbException();
            _connectionString = connectionString;

        }

        public void Add( SiteEntity entity)
        {
            using (var contextDb = new MyDBdContext(_connectionString))
            {
                var blogs = from b in contextDb.SiteEntities
                    where b.Name == entity.Name
                select b;
                var prova = blogs.ToArray();

                if(prova.Length == 1)
                    throw new NameAlreadyInUseException("cazzo");
                contextDb.SiteEntities.Add(entity);
                contextDb.SaveChanges();

            }
              
        }

    }
}
