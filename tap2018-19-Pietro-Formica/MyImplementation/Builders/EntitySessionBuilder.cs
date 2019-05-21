using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{
    class EntitySessionBuilder
    {
        private string _id;
        private DateTime _validUntil;
        private UserEntity _entityUser;
        private SiteEntity _entitySite;

        private EntitySessionBuilder() { }
        public static EntitySessionBuilder NewBuilder() => new EntitySessionBuilder();
        public EntitySessionBuilder Id()
        {
            _id = Guid.NewGuid().ToString();
            return this;
        }
        public EntitySessionBuilder ValidUntil(DateTime validUntil)
        {
            _validUntil = validUntil;
            return this;
        }
        public EntitySessionBuilder EntityUser(UserEntity userEntity)
        {
            _entityUser = userEntity;
            return this;
        }
        public EntitySessionBuilder EntitySite(SiteEntity siteEntity)
        {
            _entitySite = siteEntity;
            return this;
        }
        public SessionEntity Build()
        {
            if(_id is null) throw new ArgumentNullException();
            if(_entitySite is null) throw new ArgumentNullException();
            if(_entityUser is null) throw new ArgumentNullException();
            var sessionEntity = new SessionEntity()
            {
                Id = _id,
                ValidUntil = _validUntil,
                EntityUser = _entityUser,
                Site = _entitySite
            };
            return sessionEntity;
        }

    }
}
