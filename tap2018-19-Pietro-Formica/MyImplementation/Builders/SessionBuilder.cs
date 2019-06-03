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

        public SessionBuilder SetSessionEntity(IManager<SessionEntity> manager, string key)
        {
            var session = manager.SearchEntity(key);
            SessionEntity = session;
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

        public Session Build(SessionEntity sessionEntity = null)
        {
            if (sessionEntity != null)
                SessionEntity = sessionEntity;
            
            if(SessionEntity is null) throw new ArgumentNullException();
            if(ConnectionString is null) throw new ArgumentNullException();
            if(AlarmClock is null) throw new ArgumentNullException();
            var userManager = new UserManager(ConnectionString,SessionEntity.SiteId);
            var userEntity = userManager.SearchEntity(SessionEntity.Id);
            var user = UserBuilder.NewUserBuilder()
                .SetConnectionString(ConnectionString)
                .SetAlarmClock(AlarmClock)
                .SetEntity(userEntity)
                .Build();
            var session = new Session(SessionEntity.Id, SessionEntity.ValidUntil, user, SessionEntity.SiteId, ConnectionString, AlarmClock);
            return session;
        }

        public IEnumerable<ISession> BuildAll(IEnumerable<SessionEntity> enumerable)
        {


            var sessionEntities = enumerable.ToList();

            if (sessionEntities.Count == 0) yield break;

            foreach (var sessionEntity in sessionEntities)
            {
                SessionEntity = sessionEntity;
                yield return Build();

            }
        }
    }
}
