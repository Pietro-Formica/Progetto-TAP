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
        public string Id { get; set; }
        public DateTime ValidUntil { get; set; }     
        public virtual SiteEntity Site { get; set; }
        public virtual UserEntity EntityUser { get; set; }
    }
}
