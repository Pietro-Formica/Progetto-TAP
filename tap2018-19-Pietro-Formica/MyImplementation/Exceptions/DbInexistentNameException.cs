using MyImplementation.Exceptions.Interface;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Exceptions
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
