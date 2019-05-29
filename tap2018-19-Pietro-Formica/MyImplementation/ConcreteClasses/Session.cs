using System;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{

    public class Session : ISession, IEquatable<Session>
    {
        private readonly string _connectionString;
        private readonly IAlarmClock _alarmClock;
        private readonly string _mySite;
        public Session(string id, DateTime validUntil, IUser user,string mySite, string connectionString, IAlarmClock alarmClock)
        {
            Id = id;
            ValidUntil = validUntil;
            User = user;
            _mySite = mySite;
            _connectionString = connectionString;
            _alarmClock = alarmClock;
        }



        public bool IsValid()
        {
            var sessionManager = new SessionManager(_connectionString,_mySite);
            var sessionEntity = sessionManager.SearchEntity(Id);
            return (sessionEntity != null && DateTime.Compare(_alarmClock.Now, sessionEntity.ValidUntil) < 0);
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public IAuction CreateAuction(string description, DateTime endsOn, double startingPrice)
        {
            throw new NotImplementedException();
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
        public DateTime ValidUntil { get; }
        public IUser User { get; }
    }
}
