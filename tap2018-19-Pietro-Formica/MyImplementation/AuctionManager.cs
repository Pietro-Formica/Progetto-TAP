using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                if (auction is null) return null;
                context.Entry(auction).Reference(us => us.Seller).Load();
                context.Entry(auction).Reference(au => au.CurrentWinner).Load();
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
            using (var context = new MyDBdContext(_connectionString))
            {
                try
                {
                    context.AuctionEntities.Attach(entity);
                    context.AuctionEntities.Remove(entity);
                    context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public void SaveOnDb(AuctionEntity entity, bool upDate = false)
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                if (upDate is false)
                {
                    context.AuctionEntities.Add(entity);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch (DbUpdateException exception) //da rivedere lol
                    {
                        throw new NameAlreadyInUseException(entity.Description, "booo", exception.InnerException);

                    }
                }
                else
                {
                    try
                    {
                        if (entity.CurrentWinner != null && entity.FutureWinner != null)
                        {
                            var siteId = entity.SiteID;

                            context.AuctionEntities.Attach(entity);

                            context.Entry(entity).Reference(us => us.CurrentWinner).CurrentValue = null;
                            entity.WinnerId = entity.FutureWinner;
                            context.Entry(entity).Property(us => us.WinnerId).CurrentValue = entity.FutureWinner;
                            entity.FutureWinner = null;
                            context.Entry(entity).Property(s => s.SiteID).CurrentValue = siteId;
                           // context.Entry(entity).Property(cp => cp.CurrentOffer).CurrentValue = entity.CurrentOffer;
                            context.Entry(entity).State = EntityState.Modified;
                            //context.Entry(entity).Property(us => us.FutureWinner).IsModified = true;
                            context.SaveChanges();

                        }
                        else
                        {
                            context.AuctionEntities.Attach(entity);
                            context.Entry(entity).State = EntityState.Modified;
                            context.SaveChanges();

                        }
 


                    }
                    catch (DbUpdateException)
                    {
                        throw new InvalidOperationException();
                    }
                }

            }
        }
    }
}
