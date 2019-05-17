using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Internal;
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
        private IAlarmClock _alarmClock;
        public UserEntity UserEntity { get; set; }
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
            _alarmClock = alarmClock;
            return this;
        }
        public User Build()
        {
            if(ConnectionString is null) throw new ArgumentNullException();
            if(UserEntity is null) throw new ArgumentNullException();
            if(_alarmClock is null) throw new ArgumentNullException();
            var user = new User(UserEntity.Id, UserEntity.SiteId, ConnectionString, _alarmClock);
            return user;
        }
        public IEnumerable<IUser> BuildAll(IEnumerable<UserEntity> userEntities)
        {
            var enumerable = userEntities.ToList();
            if(enumerable.IsNullOrEmpty()) yield break;
            foreach (var userEntity in enumerable)
            {
                UserEntity = userEntity;
                

                yield return Build();
            }
        }
    }
}
