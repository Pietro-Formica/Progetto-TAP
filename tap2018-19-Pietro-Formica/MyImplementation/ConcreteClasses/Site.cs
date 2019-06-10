using System;
using System.Collections.Generic;
using System.Linq;
using MyImplementation.Builders;
using MyImplementation.MyDatabase.DataEntities;
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
         private readonly IManager<UserEntity, string> _userManager;
         private readonly IManager<SessionEntity, string> _sessionManager;
         private readonly IManager<AuctionEntity,int> _auctionManager;
         private readonly IManager<SiteEntity, string> _siteManager;

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
             _userManager = new UserManager(connectionString,name);
             _sessionManager = new SessionManager(connectionString, name);
             _auctionManager = new AuctionManager(connectionString, name);
             _siteManager = new SiteFactoryManager(connectionString);
        }
         public IEnumerable<IUser> GetUsers()
         {
             var users = _userManager.SearchAllEntities();
             var builder = UserBuilder.NewUserBuilder()
                 .SetAlarmClock(_alarmClock)
                 .SetConnectionString(_connectionString)
                 .BuildAll(users);

             return builder;



         }
         public IEnumerable<ISession> GetSessions()
        {
            var sessions = _sessionManager.SearchAllEntities();

            return SessionBuilder.NewSessionBuilder()
                .SetAlarmClock(_alarmClock)
                .SetConnectionString(_connectionString)
                .BuildAll(sessions);
        }

        public IEnumerable<IAuction> GetAuctions(bool onlyNotEnded)
        {
            var listAuctionEntities = _auctionManager.SearchAllEntities();
            if (onlyNotEnded is false)
            {
                return AuctionBuilder.NewAuctionBuilder()
                    .SetAlarmClock(_alarmClock)
                    .SetConnectionString(_connectionString)
                    .BuildAll(listAuctionEntities);
            }
            var listEndedAuctionEntities = listAuctionEntities.Where(au => au.EndsOn >= _alarmClock.Now);
            return AuctionBuilder.NewAuctionBuilder()
                .SetAlarmClock(_alarmClock)
                .SetConnectionString(_connectionString)
                .BuildAll(listEndedAuctionEntities);


        }

        public ISession Login(string username, string password)
        { 
            Control.CheckPassword(password);

            var userEntity = _userManager.SearchEntity(username);
            if (userEntity is null || !Control.CompareHashPassword(Control.HashPassword(password),userEntity.Password)) return null;
            if (userEntity.Session is null)
            {
                var sessionEntity = EntitySessionBuilder.NewBuilder()
                    .Id(username)
                    .SiteName(Name)
                    .ValidUntil(_alarmClock.Now.AddSeconds(SessionExpirationInSeconds))
                    .Build();

                _sessionManager.SaveOnDb(sessionEntity);
                 
                return SessionBuilder.NewSessionBuilder()
                    .SetAlarmClock(_alarmClock)
                    .SetConnectionString(_connectionString)
                    .SetSessionEntity(sessionEntity)
                    .Build();
            }

            userEntity.Session.ValidUntil = _alarmClock.Now.AddSeconds(SessionExpirationInSeconds);

            _sessionManager.SaveOnDb(userEntity.Session,true);

            return SessionBuilder.NewSessionBuilder()
                .SetAlarmClock(_alarmClock)
                .SetConnectionString(_connectionString)
                .SetSessionEntity(userEntity.Session)
                .Build();


        }

          
        public ISession GetSession(string sessionId)
        {
            var sessionEntity = _sessionManager.SearchEntity(sessionId);
            if (sessionEntity is null || !Session.DataValid(_alarmClock,sessionEntity)) return null;
            return SessionBuilder.NewSessionBuilder()
                .SetConnectionString(_connectionString)
                .SetAlarmClock(_alarmClock)
                .SetSessionEntity(sessionEntity)
                .Build();

        }

        public void CreateUser(string username, string password)
        {
            var userEntity = EntityUserBuilder.NewBuilder(username)
                .Password(password)
                .SiteName(Name)
                .Build();

            _userManager.SaveOnDb(userEntity);

        }

        public void Delete()
        {
            var siteEntity = _siteManager.SearchEntity(Name) ?? throw new InvalidOperationException();
            _siteManager.DeleteEntity(siteEntity);
        }

        public void CleanupSessions()
        {

            var sessionList = _sessionManager.SearchAllEntities().ToList();
            sessionList.ForEach(session =>
            {
                if (!Session.DataValid(_alarmClock, session))
                {
                    _sessionManager.DeleteEntity(session);
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
