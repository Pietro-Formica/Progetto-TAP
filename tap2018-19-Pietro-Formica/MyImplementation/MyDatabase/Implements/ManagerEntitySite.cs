using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.MyDatabase.Implements
{
    class ManagerEntitySite
    {
        private string _connectionString;

        public void ControlConnectionString(string connectionString)
        {
            if (!Database.Exists(connectionString))
                throw new UnavailableDbException();
            _connectionString = connectionString;

        }

        public void Add(SiteEntity entity)
        {
            using (var contextDb = new MyDBdContext(_connectionString))
            {
                var entityPresent = FindByKey(entity.Name);
                if (entityPresent != null)
                    throw new NameAlreadyInUseException(entity.Name);
                contextDb.SiteEntities.Add(entity);
                contextDb.SaveChanges();
            }
        }
        public SiteEntity FindByKey(string key)
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                return context.SiteEntities.Find(key);
            }
        }

        public IEnumerable<string> GetSiteNames(string connectionString)
        {
            using (var contex = new MyDBdContext(connectionString))
            {
                //var suca = contex.SiteEntities.Select(s => s.Name).ToList();
                //var usr = contex.UserEntities.Single(u => u.Username == "Pinco");
                var query = from tmp in contex.SiteEntities
                    select tmp.Name;
                return query.ToList();
            }
        }
        
    }
}