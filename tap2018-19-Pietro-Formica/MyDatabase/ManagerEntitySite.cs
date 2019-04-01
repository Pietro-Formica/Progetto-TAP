using System;
using System.Data.Common;
using System.Data.Entity;
using TAP2018_19.AuctionSite.Database.Interface;


namespace MyDatabase
{
    class ManagerEntitySite : IRepository<SiteEntity, string>
    {
        private readonly string _connectionString;
        public ManagerEntitySite(string connectionString)
        {
            if (!Database.Exists(connectionString))
                throw new ArgumentException();
            _connectionString = connectionString;
        }
        public void Add(SiteEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
