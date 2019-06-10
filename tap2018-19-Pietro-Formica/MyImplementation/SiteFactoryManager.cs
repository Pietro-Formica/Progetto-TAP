using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation
{

    public class SiteFactoryManager : IManager<SiteEntity,string>
    {
        private readonly string _connectionString;
        public SiteFactoryManager(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            _connectionString = connectionString;
        }
        public SiteEntity SearchEntity(string key)
        {
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, key);
            using (var context = new MyDBdContext(_connectionString))
            {
                var siteEntity = context.SiteEntities.Find(key);
                return siteEntity;
            }            
        }
        public IEnumerable<SiteEntity> SearchAllEntities()
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                IEnumerable<SiteEntity> siteName = context.SiteEntities.ToList();
                return siteName;
            }
        }
        public void DeleteEntity(SiteEntity entity)
        {
            using (var contextDb = new MyDBdContext(_connectionString))
            {
                contextDb.SiteEntities.Attach(entity);
                contextDb.SiteEntities.Remove(entity);
                contextDb.SaveChanges();

            }
        }
        public void SaveOnDb(SiteEntity entity, bool upDate = false)
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
    }


}
