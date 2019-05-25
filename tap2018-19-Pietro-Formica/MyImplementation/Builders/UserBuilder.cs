using System;
using System.Collections.Generic;
using System.Linq;
using MyImplementation.ConcreteClasses;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{
    public class UserBuilder
    {
        public string ConnectionString { get; private set; }

        public IAlarmClock AlarmClock { get; private set; }

        public UserEntity UserEntity { get; private set; }
  
        private UserBuilder() { }

        public static UserBuilder NewUserBuilder() => new UserBuilder();

        public UserBuilder SetConnectionString(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            ConnectionString = connectionString;
            return this;
        }

        public UserBuilder SetEntity(UserEntity userEntity)
        {
            UserEntity = userEntity;
            return this;
        }

        public UserBuilder SetAlarmClock(IAlarmClock alarmClock)
        {
            AlarmClock = alarmClock;
            return this;
        }

        public User Build()
        {
            if (UserEntity is null) throw new ArgumentNullException();
            if (ConnectionString is null) throw new ArgumentNullException();
            if (AlarmClock is null) throw new ArgumentNullException();
            var user = new User(UserEntity.Id, UserEntity.SiteId, ConnectionString, AlarmClock);
            return user;
        }

        public IEnumerable<IUser> BuildAll(IManager<UserEntity> manager)
        {
            var enumerable = manager.SearchAllEntities();
            var entities = enumerable.ToList();
            if(entities.Count == 0) yield break;
            foreach (var userEntity in entities)
            {
                UserEntity = userEntity;               
                yield return Build();
            }
        }
    }
}
