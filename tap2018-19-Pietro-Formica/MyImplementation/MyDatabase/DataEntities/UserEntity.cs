using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyImplementation.MyDatabase.DataEntities
{
    public class UserEntity
    {
        public UserEntity() { }

        public UserEntity(string username, string password, string siteName)
        {
            Id = username;
            Password = password;
            SiteId = siteName;

        }
       
       public string Id { get; set; }
       public string Password { get; set; }
       public string SiteId { get; set; }
       public virtual SiteEntity Site { get; set; }
       public virtual SessionEntity Session { get; set; }
    
    }
}
