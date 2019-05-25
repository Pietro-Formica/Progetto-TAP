using System;
using System.Collections.Generic;
using MyImplementation.Builders;
using MyImplementation.Extensions;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{
     public class Site : ISite, IEquatable<Site>
     {
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
         }
         public IEnumerable<IUser> GetUsers()
         {
             var builder = UserBuilder.NewUserBuilder().SetAlarmClock(_alarmClock).SetConnectionString(_connectionString);
             var listUsers = builder.GetUsers(Name);
             return builder.BuildAll(listUsers);


         }
         public IEnumerable<ISession> GetSessions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IAuction> GetAuctions(bool onlyNotEnded)
        {
            throw new NotImplementedException();
        }

        public ISession Login(string username, string password)
        {
            Control.CheckName(DomainConstraints.MaxUserName, DomainConstraints.MinUserName,username);
            Control.CheckPassword(password);
            var session = SessionBuilder.NewSessionBuilder().SetConnectionString(_connectionString).SearchEntity(Name,null,username, password);
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

             



            }
            throw new NotImplementedException();

        }

        public ISession GetSession(string sessionId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
