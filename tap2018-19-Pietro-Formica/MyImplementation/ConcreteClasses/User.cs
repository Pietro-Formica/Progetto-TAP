using System;
using System.Collections.Generic;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{
    class User : IUser
    {
        public User(string username)
        {
            Username = username;
        }
        public IEnumerable<IAuction> WonAuctions()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public string Username { get; }
    }
}
