using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation
{
    class DbInexistentNameException : IExceptionDb
    {
        private readonly string _name;
        public DbInexistentNameException(string name)
        {
            _name = name;
        }
        public void GetException()
        {
            throw new InexistentNameException(_name);
        }
    }
}
