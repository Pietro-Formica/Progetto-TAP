using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using TAP2018_19.AuctionSite.Database.Interface;


namespace MyDatabase
{
    class SiteEntity : IEntity<string>
    {
        public SiteEntity(string name, int timezone, int sessionExpirationTimeInSeconds, double minimumBidIncrement)
        {
            Id = name;
            Timezone = timezone;
            SessionExpirationInSeconds = sessionExpirationTimeInSeconds;
            MinimumBidIncrement = minimumBidIncrement;
        }
        public SiteEntity() { }


        public string Id { get; set; }
        public int Timezone { get; set; }
        public int SessionExpirationInSeconds { get; set; }
        public double MinimumBidIncrement { get; set; }
    }
}
