using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation
{

    public class UserManager : IManager<UserEntity>
    {
        private readonly string _connectionString;
        private readonly string _mySite;
        public UserManager(string connectionString, string mySite)
        {
            _connectionString = connectionString;
            _mySite = mySite;
        }
        public UserEntity SearchEntity(string key)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<UserEntity> SearchAllEntities()
        {
            using (var context = new MyDBdContext(_connectionString))
            {
                var user = context.SiteEntities
                    .Where(site => site.Id.Equals(_mySite))
                    .Select(site => site.Users)
                    .Single();

                return user;
            }
        }
    }
}
