using System;

namespace MyImplementation.MyDatabase.DataEntities
{
    public class SessionEntity
    {
        public string Id { get; set; }
        public DateTime ValidUntil { get; set; }
        public string SiteId { get; set; }
        public virtual SiteEntity Site { get; set; }
        public string UserId { get; set; }
        public virtual UserEntity EntityUser { get; set; }
    }
}
