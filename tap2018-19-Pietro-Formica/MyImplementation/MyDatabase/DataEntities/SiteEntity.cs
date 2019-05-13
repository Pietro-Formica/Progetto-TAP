using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyImplementation.ValidateArguments;

namespace MyImplementation.MyDatabase.DataEntities
{
    public class SiteEntity
    {  
        public string Id { get; set; }
        public int Timezone { get; set; }
        public int SessionExpirationInSeconds { get; set; }
        public double MinimumBidIncrement { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
        public virtual ICollection<SessionEntity> SessionEntities { get; set; }


    }
}
