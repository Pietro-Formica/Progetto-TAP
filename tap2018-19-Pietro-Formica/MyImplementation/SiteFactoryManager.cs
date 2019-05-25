using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation
{

    public class SiteFactoryManager : IManager<SiteEntity>
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

                if (siteEntity is null)
                {
                    throw new InexistentNameException(name: key);
                }
      
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
    }


}
