using System;
using System.Collections.Generic;
using System.Linq;
using MyImplementation.ConcreteClasses;
using MyImplementation.Extensions;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{

    public class SessionBuilder
    {

        public string ConnectionString { get; private set; }

        public IAlarmClock AlarmClock { get; private set; }

        public SessionEntity SessionEntity { get; private set; }

        private SessionBuilder() { }

        public static SessionBuilder NewSessionBuilder() => new SessionBuilder();

        public SessionBuilder SetSessionEntity(SessionEntity sessionEntity)
        {
            SessionEntity = sessionEntity;
            return this;
        }

        public SessionBuilder SetConnectionString(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            ConnectionString = connectionString;
            return this;
        }

        public SessionBuilder SetAlarmClock(IAlarmClock alarmClock)
        {
            AlarmClock = alarmClock;
            return this;
        }

        public Session Build()
        {
            
            if(SessionEntity is null) throw new ArgumentNullException();
            if(ConnectionString is null) throw new ArgumentNullException();
            if(AlarmClock is null) throw new ArgumentNullException();
            var user = UserBuilder.NewUserBuilder()
                .SetConnectionString(ConnectionString)
                .SetAlarmClock(AlarmClock)
                .SetEntity(null)
                .Build();
            var session = new Session(SessionEntity.Id, SessionEntity.ValidUntil, user, ConnectionString, AlarmClock);
            return session;
        }

        public IEnumerable<ISession> BuildAll(IEnumerable<SessionEntity> sessionEntities)
        {
            var enumerable = sessionEntities.ToList();
            if (enumerable.Count == 0) yield break;
            foreach (var sessionEntity in enumerable)
            {
                SessionEntity = sessionEntity;
                yield return Build();

            }
        }
    }
}
