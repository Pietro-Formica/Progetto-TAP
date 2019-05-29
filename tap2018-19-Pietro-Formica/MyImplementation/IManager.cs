using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyImplementation
{

    public interface IManager<T> where T : class
    {
        T SearchEntity(string key);
        IEnumerable<T> SearchAllEntities();
        void DeleteEntity(T entity);
        void SaveOnDb(T entity, bool upDate = false);
    }
}
