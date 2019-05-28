using System;

namespace MyImplementation.MyDatabase.DataEntities
{
    public class SessionEntity
    {
        public string Id { get; set; }
        public string SiteId { get; set; }
        public DateTime ValidUntil { get; set; }
        public virtual SiteEntity Site { get; set; }
        public virtual UserEntity EntityUser { get; set; }
    }
}
