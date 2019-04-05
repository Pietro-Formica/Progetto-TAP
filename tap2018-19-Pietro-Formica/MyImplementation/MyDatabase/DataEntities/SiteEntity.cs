using System.ComponentModel.DataAnnotations;
using MyImplementation.MyDatabase.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.MyDatabase.DataEntities
{
    public class SiteEntity : IEntity
    {
        public SiteEntity(string name, int timezone, int sessionExpirationTimeInSeconds, double minimumBidIncrement)
        {
            Name = name;
            Timezone = timezone;
            SessionExpirationInSeconds = sessionExpirationTimeInSeconds;
            MinimumBidIncrement = minimumBidIncrement;
        }

        public SiteEntity()
        {

        }
        [MaxLength(DomainConstraints.MaxSiteName), MinLength(DomainConstraints.MinSiteName)]
        public string Name { get; set; }
        public int Timezone { get; set; }
        public int SessionExpirationInSeconds { get; set; }
        public double MinimumBidIncrement { get; set; }
    }
}
