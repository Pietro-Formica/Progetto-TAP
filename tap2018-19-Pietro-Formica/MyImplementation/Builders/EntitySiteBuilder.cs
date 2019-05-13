using System.Data.Entity.Infrastructure;
using System.Runtime.Remoting.Messaging;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{
    class EntitySiteBuilder
    {
        private string _id;
        private int _timezone;
        private int _sessionExpirationInSeconds;
        private double _minimumBidIncrement;
        private SiteEntity _siteEntity;

        private EntitySiteBuilder(string id) => _id = id;
        public static EntitySiteBuilder NewBuilder(string id) => new EntitySiteBuilder(id);
        public EntitySiteBuilder Timezone(int timezone)
        {
            _timezone = timezone;
            return this;
        }
        public EntitySiteBuilder SessionExpirationInSeconds(int sessionExpirationInSeconds)
        {
            _sessionExpirationInSeconds = sessionExpirationInSeconds;
            return this;
        }
        public EntitySiteBuilder MinimumBidIncrement(double minimumBidIncrement)
        { 
            _minimumBidIncrement = minimumBidIncrement;
            return this;
        }
        public EntitySiteBuilder Build()
        {
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, _id);
            Control.CheckTimezone(_timezone);
            Control.CheckSessionExpirationTimeInSeconds(_sessionExpirationInSeconds);
            Control.CheckMinimumBidIncrement(_minimumBidIncrement);
            _siteEntity = new SiteEntity
            {
                Id = _id,
                Timezone = _timezone,
                SessionExpirationInSeconds = _sessionExpirationInSeconds,
                MinimumBidIncrement = _minimumBidIncrement
            };
            return this;
        }
        public void SaveEntityOnDb(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            using (var contextDb = new MyDBdContext(connectionString))
            {
                contextDb.SiteEntities.Add(_siteEntity);
                try
                {
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException)//da rivedere lol
                {
                    throw new NameAlreadyInUseException(_siteEntity.Id);
                }
            }
        }
    }
}
