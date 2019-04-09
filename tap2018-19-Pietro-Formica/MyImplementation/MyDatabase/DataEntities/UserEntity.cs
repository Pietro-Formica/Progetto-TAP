using System.ComponentModel.DataAnnotations;

namespace MyImplementation.MyDatabase.DataEntities
{
    public class UserEntity
    {
       
       public string Username { get; set; }
       public string Password { get; set; }
       public virtual SiteEntity Site { get; set; }
    }
}
