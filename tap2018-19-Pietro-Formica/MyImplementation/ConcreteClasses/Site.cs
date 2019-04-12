using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.MyDatabase.Implements;
using MyImplementation.ValidateArguments;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{
     public class Site : ISite
     {
        public Site(string name, int timezone, int sessionExpirationTimeInSeconds, double minimumBidIncrement)
        {
            Name = name;
            Timezone = timezone;
            SessionExpirationInSeconds = sessionExpirationTimeInSeconds;
            MinimumBidIncrement = minimumBidIncrement;
         
        }
            
        public IEnumerable<IUser> GetUsers()
        {
            var prova = ManagerSetup.ConnectionString;
            if(prova is null)
                throw new ArgumentNullException("sono io");
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
           Control.CheckName(DomainConstraints.MaxUserName, DomainConstraints.MinUserName, username);
           Control.CheckPassword(password);
           UserEntity user = new UserEntity(username, password,Name);
           var manager = new ManagerUser();
           manager.AddUserOnDb(Name, user);


        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public void CleanupSessions()
        {
            throw new NotImplementedException();
        }
        public string Name { get; }
        public int Timezone { get; }
        public int SessionExpirationInSeconds { get; }
        public double MinimumBidIncrement { get; }

    }
}
