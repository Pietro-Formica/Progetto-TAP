using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
                contextDb.SiteEntities.Add(entity);
                try
                {
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException)//da rivedere lol
                {

                    throw new NameAlreadyInUseException(entity.Id);
                }

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
            using (var context = new MyDBdContext(connectionString))
            {

                var query = from tmp in context.SiteEntities
                    select tmp.Id;
                return query.ToList();
            }
        }
        
    }
}