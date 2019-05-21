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
    class SessionBuilder
    {
        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                if (_connectionString is null) throw new ArgumentNullException();
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
        private SessionEntity _sessionEntity;
        public SessionEntity SessionEntity
        {
            get
            {
                if (_sessionEntity is null) throw new ArgumentNullException();
                return _sessionEntity;
            }
            set => _sessionEntity = value;
        }
        public SessionBuilder SetConnectionString(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            _connectionString = connectionString;
            return this;
        }
        public SessionBuilder SetAlarmClock(IAlarmClock alarmClock)
        {
            _alarmClock = alarmClock;
            return this;
        }
        private SessionBuilder() { }
        public static SessionBuilder NewSessionBuilder() => new SessionBuilder();
        public Session Build()
        {
            var user = UserBuilder.NewUserBuilder()
                .SetConnectionString(_connectionString)
                .SetAlarmClock(_alarmClock)
                .SetEntity(SessionEntity.EntityUser)
                .Build();
            var session = new Session(SessionEntity.Id, SessionEntity.ValidUntil, user, ConnectionString, _alarmClock);
            return session;
        }
        public IEnumerable<ISession> BuildAll(IEnumerable<SessionEntity> sessionEntities)
        {
            var enumerable = sessionEntities.ToList();
            if(enumerable.IsNullOrEmpty()) yield break;
            foreach (var sessionEntity in enumerable)
            {
                SessionEntity = sessionEntity;
                yield return Build();

            }

           
        }

    }
}
