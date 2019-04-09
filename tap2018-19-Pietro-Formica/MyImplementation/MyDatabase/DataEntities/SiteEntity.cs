using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyImplementation.ValidateArguments;

namespace MyImplementation.MyDatabase.DataEntities
{
    public class SiteEntity
    {
        public SiteEntity() { }
        public SiteEntity(string name, int timezone, int sessionExpirationTimeInSeconds, double minimumBidIncrement)
        {
            Control.CheckArgumentSiteEntity(name, timezone, sessionExpirationTimeInSeconds, minimumBidIncrement);
            Name = name;
            Timezone = timezone;
            SessionExpirationInSeconds = sessionExpirationTimeInSeconds;
            MinimumBidIncrement = minimumBidIncrement;
        }       
        public string Name { get; set; }
        public int Timezone { get; set; }
        public int SessionExpirationInSeconds { get; set; }
        public double MinimumBidIncrement { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }


    }
}
