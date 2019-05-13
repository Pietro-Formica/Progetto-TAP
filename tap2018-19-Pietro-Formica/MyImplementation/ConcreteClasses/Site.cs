using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net.Sockets;
using Castle.Core.Internal;
using MyImplementation.Builders;
using MyImplementation.Extensions;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using Ninject.Infrastructure.Language;
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
             throw new NotImplementedException();
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
            throw new NotImplementedException();

        }

        public ISession GetSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(string username, string password)
        {
            UserEntityBuilder.NewBuilder(username)
                .Password(password)
                .SiteName(Name)
                .Build()
                .SaveEntityOnDb(_connectionString);                     
        }

        public void Delete()
        {
            throw new NotImplementedException();
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
