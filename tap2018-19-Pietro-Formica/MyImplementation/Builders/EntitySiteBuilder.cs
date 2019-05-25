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
        public SiteEntity Build()
        {
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, _id);
            Control.CheckTimezone(_timezone);
            Control.CheckSessionExpirationTimeInSeconds(_sessionExpirationInSeconds);
            Control.CheckMinimumBidIncrement(_minimumBidIncrement);
            return new SiteEntity
            {
                Id = _id,
                Timezone = _timezone,
                SessionExpirationInSeconds = _sessionExpirationInSeconds,
                MinimumBidIncrement = _minimumBidIncrement
            };
            
        }

    }
}
