using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyImplementation
{

    public interface IManager<out T> where T : class
    {
        T SearchEntity(string key);
        IEnumerable<T> SearchAllEntities();
    }
}
