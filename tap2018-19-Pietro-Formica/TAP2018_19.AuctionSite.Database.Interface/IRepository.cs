using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAP2018_19.AuctionSite.Database.Interface
{
    public interface IRepository<in T, in TK> where T : IEntity<TK>
    {
        void Add(T entity);
    }
}
