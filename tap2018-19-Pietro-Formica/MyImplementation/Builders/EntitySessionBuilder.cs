using System;
using MyImplementation.MyDatabase.DataEntities;

namespace MyImplementation.Builders
{

    public class EntitySessionBuilder
    {
        private string _id;
        private DateTime _validUntil;
        private UserEntity _entityUser;
        private string _siteName;
        private string _userId;

        private EntitySessionBuilder() { }

        public static EntitySessionBuilder NewBuilder() => new EntitySessionBuilder();

        public EntitySessionBuilder Id(string userId)
        {
            _id = Guid.NewGuid().ToString();
            _userId = userId;
            return this;
        }

        public EntitySessionBuilder ValidUntil(DateTime validUntil)
        {
            _validUntil = validUntil;
            return this;
        }

        public EntitySessionBuilder SetEntityUser(UserEntity user)
        {
            _entityUser = user;
            return this;
        }
 
        public EntitySessionBuilder SiteName(string siteName)
        {
            _siteName = siteName;
            return this;
        }

        public SessionEntity Build()
        {
            if(_id is null) throw new ArgumentNullException();
            if(_siteName is null) throw new ArgumentNullException();
            if(_entityUser is null) throw new ArgumentNullException();
            var sessionEntity = new SessionEntity()
            {
                Id = _id,
                ValidUntil = _validUntil,
               // EntityUser = _entityUser,
                UserId = _userId,
                SiteId = _siteName
            };
            return sessionEntity;
        }

    }
}
