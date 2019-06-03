using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation
{

    public class AuctionManager : IManager<AuctionEntity,int>
    {
        private readonly string _connectionString;
        private readonly string _mySite;
        public AuctionManager(string connectionString, string mySite)
        {
            _connectionString = connectionString;
            _mySite = mySite;
        }
        public AuctionEntity SearchEntity(int key)
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                var auction = context.SiteEntities.Single(s => s.Id.Equals(_mySite))
                    .AuctionEntities.SingleOrDefault(au => au.ID.Equals(key));
                return auction;

            }

   
        }
        public IEnumerable<AuctionEntity> SearchAllEntities()
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                var auctions = context.SiteEntities
                    .Where(site => site.Id.Equals(_mySite))
                    .Select(site => site.AuctionEntities)
                    .Single();

                return auctions;
            }
        }
        public void DeleteEntity(AuctionEntity entity)
        {
            throw new NotImplementedException();
        }
        public void SaveOnDb(AuctionEntity entity, bool upDate = false)
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                context.AuctionEntities.Add(entity);
                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateException exception)//da rivedere lol
                {
                    throw new NameAlreadyInUseException(entity.Description, "booo", exception.InnerException);
                }
            }
        }
    }
}
