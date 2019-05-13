using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyImplementation
{
   public  class ElementsFactory
   {
        public string ConnectionString { get; }
        children cazzo = new children();
        cazzo.

        class children : IUser
        {
            private readonly string _connectionString;
            public children()
            {
                _connectionString = ConnectionString;
            }
        }
    }
}
