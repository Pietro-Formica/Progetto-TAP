using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.MyDatabase.DataEntities
{
    public class SessionEntity
    {
        public SessionEntity() { }
        public SessionEntity(string id, DateTime validUntil, string siteName,UserEntity user)
        {
            Id = id;
            ValidUntil = validUntil;
            User = user;
            SiteId = siteName;

        }

        public string Id { get; set; }
        public DateTime ValidUntil { get; set; }     
        public string SiteId { get; set; }
        public SiteEntity Site { get; set; }
        public UserEntity User { get; set; }
    }
}
