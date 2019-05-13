using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{

    class UserEntityBuilder
    {
        private string _id;
        private string _password;
        private string _siteName;
        private UserEntity _userEntity;

        private UserEntityBuilder(string id) => _id = id;
        public static UserEntityBuilder NewBuilder(string id) => new UserEntityBuilder(id);
        public UserEntityBuilder Password(string password)
        {
            _password = password;
            return this;
        }
        public UserEntityBuilder SiteName(string siteName)
        {
            _siteName = siteName;
            return this;
        }
        public UserEntity Build()
        {
            Control.CheckName(DomainConstraints.MaxUserName, DomainConstraints.MinUserName, _id);
            Control.CheckPassword(_password);
            if (_siteName is null) throw new ArgumentNullException();
             var userEntity = new UserEntity
            {
                Id = _id,
                Password = _password,
                SiteId = _siteName
            };
            return userEntity;
        }
/*        public void SaveEntityOnDb(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            using (var contextDb = new MyDBdContext(connectionString))
            {
                if(_userEntity is null) throw new ArgumentNullException();
                contextDb.UserEntities.Add(_userEntity);
                try
                {
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException)//da rivedere lol
                {
                    throw new NameAlreadyInUseException(_userEntity.Id);
                }
            }
        }*/
    }

}
