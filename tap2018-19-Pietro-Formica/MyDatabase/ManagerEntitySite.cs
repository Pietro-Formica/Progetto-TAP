using System;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using TAP2018_19.AuctionSite.Database.Interface;
using TAP2018_19.AuctionSite.Interfaces;


namespace MyDatabase
{
    class ManagerEntitySite : IRepository<IEntity<string>, string>
    {
        private readonly string _connectionString;

        public ManagerEntitySite(string connectionString)
        {
            if (!Database.Exists(connectionString))
                throw new UnavailableDbException();
            _connectionString = connectionString;
        }
        public void Add(IEntity<string> entity)
        {
            using (var contextDb = new MyBdContext(_connectionString))
            {
                var blogs = from b in contextDb.SiteEntities
                    where b.Id == entity.Id
                select b;
                var prova = blogs.ToArray();

                if(prova.Length == 1)
                    throw new NameAlreadyInUseException("cazzo");
                contextDb.SiteEntities.Add((SiteEntity) entity);
                contextDb.SaveChanges();

            }
              
        }
    }
}
