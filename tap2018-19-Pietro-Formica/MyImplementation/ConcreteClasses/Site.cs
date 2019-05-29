using System;
using System.Collections.Generic;
using System.Linq;
using MyImplementation.Builders;
using MyImplementation.Extensions;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{
     public class Site : ISite, IEquatable<Site>
     {
         private const int TimeToCleanup = 5;
         private readonly string _connectionString;
         private readonly IAlarmClock _alarmClock;
       
         public Site(string name, int timezone, int sessionExpirationInSeconds, double minimumBidIncrement, string connectionString, IAlarmClock alarmClock)
         {
             Name = name;
             Timezone = timezone;
             SessionExpirationInSeconds = sessionExpirationInSeconds;
             MinimumBidIncrement = minimumBidIncrement;
             _connectionString = connectionString;
             _alarmClock = alarmClock;
             var tick = _alarmClock.InstantiateAlarm(TimeToCleanup * 60 * 1000);
             tick.RingingEvent += CleanupSessions;
        }
         public IEnumerable<IUser> GetUsers()
         {
/*             var builder = UserBuilder.NewUserBuilder().SetAlarmClock(_alarmClock).SetConnectionString(_connectionString);
             var listUsers = builder.GetUsers(Name);
             return builder.BuildAll(listUsers);*/
             var userManager = new UserManager(_connectionString, Name);
             var users = userManager.SearchAllEntities();
             var builder = UserBuilder.NewUserBuilder()
                 .SetAlarmClock(_alarmClock)
                 .SetConnectionString(_connectionString)
                 .BuildAll(users);

             return builder;



         }
         public IEnumerable<ISession> GetSessions()
        {
            var sessionManager = new SessionManager(_connectionString,Name);
            var sessions = sessionManager.SearchAllEntities();

            return SessionBuilder.NewSessionBuilder()
                .SetAlarmClock(_alarmClock)
                .SetConnectionString(_connectionString)
                .BuildAll(sessions);
        }

        public IEnumerable<IAuction> GetAuctions(bool onlyNotEnded)
        {
            throw new NotImplementedException();
        }

        public ISession Login(string username, string password)
        {
           // Control.CheckName(DomainConstraints.MaxUserName, DomainConstraints.MinUserName,username);
            Control.CheckPassword(password);
/*            var session = SessionBuilder.NewSessionBuilder().SetConnectionString(_connectionString).SearchEntity(Name,null,username, password);
            if (session is null)
                return null;
            if (session.SessionEntity is null)
            {
                var sessionEntity = EntitySessionBuilder.NewBuilder()
                    .Id(username)
                    .SiteName(Name)
                    .ValidUntil(_alarmClock.Now.AddSeconds(SessionExpirationInSeconds))
                    .EntityUser(username, password, Name, _connectionString)
                    .Build();

                sessionEntity.SaveEntityOnDb(_connectionString);
                return session.SetAlarmClock(_alarmClock).SetSessionEntity(sessionEntity)
                    .Build();

            }
            if (DateTime.Compare(_alarmClock.Now, session.SessionEntity.ValidUntil) < 0)
            {
                return session.SetAlarmClock(_alarmClock).Build();

            }
            else
            {

             



            }*/
            var userManager = new UserManager(_connectionString,Name);
            var userEntity = userManager.SearchEntity(username);
            if (userEntity is null || !userEntity.Password.Equals(password)) return null;
            if (userEntity.Session is null)
            {
                var sessionEntity = EntitySessionBuilder.NewBuilder()
                    .Id(username)
                    .SiteName(Name)
                    .ValidUntil(_alarmClock.Now.AddSeconds(SessionExpirationInSeconds))
                    .Build();
                sessionEntity.SaveEntityOnDb(_connectionString);
                    

                return SessionBuilder.NewSessionBuilder()
                    .SetAlarmClock(_alarmClock)
                    .SetConnectionString(_connectionString)
                    .Build(sessionEntity);
            }

            userEntity.Session.ValidUntil = _alarmClock.Now.AddSeconds(SessionExpirationInSeconds);
            var sessionManager = new SessionManager(_connectionString,Name);
            sessionManager.SaveOnDb(userEntity.Session,true);

            return SessionBuilder.NewSessionBuilder()
                .SetAlarmClock(_alarmClock)
                .SetConnectionString(_connectionString)
                .Build(userEntity.Session);


        }

          
        public ISession GetSession(string sessionId)
        {
            var sessionManager = new SessionManager(_connectionString, Name);
            var sessionEntity = sessionManager.SearchEntity(sessionId);
            if (sessionEntity is null || !Session.DataValid(_alarmClock,sessionEntity)) return null;
            return SessionBuilder.NewSessionBuilder()
                .SetConnectionString(_connectionString)
                .SetAlarmClock(_alarmClock)
                .Build(sessionEntity);

        }

        public void CreateUser(string username, string password)
        {
            EntityUserBuilder.NewBuilder(username)
                .Password(password)
                .SiteName(Name)
                .Build()
                .SaveEntityOnDb(_connectionString);                     
        }

        public void Delete()
        {
            SiteBuilder.NewSiteBuilder().SetConnectionString(_connectionString).DestroyEntity(Name);
        }

        public void CleanupSessions()
        {
            var sessionManager = new SessionManager(_connectionString, Name);
            var sessionList = sessionManager.SearchAllEntities().ToList();
            sessionList.ForEach(session =>
            {
                if (!Session.DataValid(_alarmClock, session))
                {
                    sessionManager.DeleteEntity(session);
                }
            });
        }
        public bool Equals(Site other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Site) obj);
        }
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public string Name { get; }
        public int Timezone { get; }
        public int SessionExpirationInSeconds { get; }
        public double MinimumBidIncrement { get; }

    }
}
