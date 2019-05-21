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
        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                if(_connectionString is null) throw new ArgumentNullException();
                return _connectionString;
            }
        }
        private IAlarmClock _alarmClock;
        public IAlarmClock AlarmClock
        {
            get
            {
                if (_alarmClock is null) throw new ArgumentNullException();
                    return _alarmClock;
            }
        }

        private UserEntity _userEntity;
        public UserEntity UserEntity
        {
            get
            {
                if (_userEntity is null) throw new ArgumentNullException();
                return _userEntity;
            }
            set => _userEntity = value;
        }

        private UserBuilder() { }
        public static UserBuilder NewUserBuilder() => new UserBuilder();
        public UserBuilder SetConnectionString(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            _connectionString = connectionString;
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
            var user = new User(UserEntity.Id, UserEntity.SiteId, ConnectionString, AlarmClock);
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
