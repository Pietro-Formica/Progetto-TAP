using System;
using MyImplementation.Builders;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{

    public class Session : ISession, IEquatable<Session>
    {
        private readonly string _connectionString;
        private readonly IAlarmClock _alarmClock;
        public Session(string id, DateTime validUntil, IUser user,string mySite, string connectionString, IAlarmClock alarmClock)
        {
            Id = id;
            ValidUntil = validUntil;
            User = user;
            Site = mySite;
            _connectionString = connectionString;
            _alarmClock = alarmClock;
        }



        public bool IsValid()
        {
            var sessionManager = new SessionManager(_connectionString,Site);
            var sessionEntity = sessionManager.SearchEntity(Id);
            return (sessionEntity != null && DateTime.Compare(_alarmClock.Now, sessionEntity.ValidUntil) < 0);
        }

        public void Logout()
        {
            var sessionManager = new SessionManager(_connectionString, Site);
            var sessionEntity = sessionManager.SearchEntity(Id) ?? throw new InvalidOperationException();
            if(!DataValid(_alarmClock,sessionEntity)) throw new InvalidOperationException();
            sessionEntity.ValidUntil = sessionEntity.ValidUntil.AddDays(-20);
            sessionManager.SaveOnDb(sessionEntity,true);
        }

        public IAuction CreateAuction(string description, DateTime endsOn, double startingPrice)
        {
            var userManager = new UserManager(_connectionString,Site);
            var user = userManager.SearchEntity(User.Username);
            if(user?.Session is null || !DataValid(_alarmClock,user.Session)) throw new InvalidOperationException();
            var auctionEntity = EntityAuctionBuilder.NewBuilder(_alarmClock)
                .SetDescription(description)
                .SetEndsOn(endsOn)
                .SetSiteId(Site)
                .SetStartingPrice(startingPrice)
                .SetUserSeller(user.Id)
                .Build();
            var auctionManager = new AuctionManager(_connectionString,Site);
            auctionManager.SaveOnDb(auctionEntity);
            var session = user.Session;
            session.ValidUntil = _alarmClock.Now.AddSeconds(user.Site.SessionExpirationInSeconds);
            ValidUntil = session.ValidUntil;
            userManager.SaveOnDb(user,true);
            var auction = AuctionBuilder.NewAuctionBuilder()
                .SetAlarmClock(_alarmClock)
                .SetConnectionString(_connectionString)
                .SetAuctionEntity(auctionEntity)
                .Build();

            return auction;
        }

        public bool Equals(Session other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Id, other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Session)obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public static bool DataValid(IAlarmClock alarmClock, SessionEntity sessionEntity) => DateTime.Compare(alarmClock.Now,sessionEntity.ValidUntil) < 0;
  
        public string Id { get; }
        public DateTime ValidUntil { get; set; }
        public IUser User { get; }
        public string Site { get; }
    }
}
