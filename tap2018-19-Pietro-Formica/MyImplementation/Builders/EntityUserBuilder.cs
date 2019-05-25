using System;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{

    class EntityUserBuilder
    {
        private string _id;
        private string _password;
        private string _siteName;
        private EntityUserBuilder(string id) => _id = id;
        public static EntityUserBuilder NewBuilder(string id) => new EntityUserBuilder(id);
        public EntityUserBuilder Password(string password)
        {
            _password = password;
            return this;
        }
        public EntityUserBuilder SiteName(string siteName)
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

    }

}
