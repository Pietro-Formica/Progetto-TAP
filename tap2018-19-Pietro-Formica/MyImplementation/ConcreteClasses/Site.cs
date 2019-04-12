using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Castle.Core.Internal;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{
     public class Site : ISite
     {
         private readonly string _connectionString;
         private readonly IAlarmClock _alarmClock;
        public Site(string name, int timezone, int sessionExpirationTimeInSeconds, double minimumBidIncrement, string connectionString, IAlarmClock alarmClock)
        {
            Name = name;
            Timezone = timezone;
            SessionExpirationInSeconds = sessionExpirationTimeInSeconds;
            MinimumBidIncrement = minimumBidIncrement;
            _connectionString = connectionString;
            _alarmClock = alarmClock;
        }
            
        public IEnumerable<IUser> GetUsers()
        {
            Control.CheckConnectionString(_connectionString);
            using (var context = new MyDBdContext(_connectionString))
            {
                var query = from tmp in context.UserEntities
                    where tmp.SiteId == Name
                    select tmp.Id;
                var entityList = query.ToList();
                var usersList = new List<IUser>();
                foreach (var tmp in entityList)
                {
                    var user = new User(tmp);
                    usersList.Add(user);
                }

                return usersList;

            }

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
            using (var context = new MyDBdContext(_connectionString))
            {
                var userEntity = context.UserEntities.FirstOrDefault(u =>
                    u.Id == username && u.Password == password && u.SiteId == Name);
                   if (userEntity is null)
                    return null;
                   var sessionUser = userEntity.Session;
                   if (sessionUser is null)
                   {
                        var time = _alarmClock.Now.AddSeconds(SessionExpirationInSeconds);
                        var id = Guid.NewGuid().ToString();
                        context.Session.Add(new SessionEntity(id, time, Name, userEntity));
                        try
                        {
                            context.SaveChanges();
                            return new Session(id, time, new User(username), _alarmClock);

                        }
                        catch (DbUpdateException e)
                        {
                            Console.WriteLine(e);
                            throw new ArgumentNullException();
                        }
                   }
                var times = _alarmClock.Now.AddSeconds(SessionExpirationInSeconds);
                sessionUser.ValidUntil = times;
                try
                {
                    context.SaveChanges();
                    return new Session(sessionUser.Id, sessionUser.ValidUntil, new User(username), _alarmClock);
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine(e);
                    throw new ArgumentNullException();
                }
            }      
        }

        public ISession GetSession(string sessionId)
        {
            throw new NotImplementedException();
        }

        public void CreateUser(string username, string password)
        {
           Control.CheckName(DomainConstraints.MaxUserName, DomainConstraints.MinUserName, username);
           Control.CheckPassword(password);
           UserEntity user = new UserEntity(username, password, Name);
           using (var contextDb = new MyDBdContext(_connectionString))
           {
               contextDb.UserEntities.Add(user);
               try
               {
                   contextDb.SaveChanges();
               }
               catch (DbUpdateException)//da rivedere lol
               {
                   throw new NameAlreadyInUseException(user.Id);
               }
           }
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
