using System;
using System.Linq;
using System.Security.Policy;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{
    class Session : ISession, IEquatable<Session>
    {
        private readonly string _connectionString;
        private readonly IAlarmClock _alarmClock;
        public Session(string id, DateTime validUntil, IUser user, string connectionString, IAlarmClock alarmClock)
        {
            Id = id;
            ValidUntil = validUntil;
            User = user;
            _connectionString = connectionString;
            _alarmClock = alarmClock;
        }


        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public bool IsValid()
        {
            throw new NotImplementedException();
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
        public string Id { get; }
        public DateTime ValidUntil { get; }
        public IUser User { get; }
    }
}
