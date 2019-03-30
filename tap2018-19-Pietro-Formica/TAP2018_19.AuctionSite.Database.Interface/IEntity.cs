using System.Dynamic;

namespace TAP2018_19.AuctionSite.Database.Interface
{
    public interface IEntity<T>
    {
        T Id { set; get; }
    }
}
