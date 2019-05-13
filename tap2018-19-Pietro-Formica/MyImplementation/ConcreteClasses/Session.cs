using System;
using System.Linq;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{
    class Session : ISession, IEquatable<Session>
    {
        public Session(string id, DateTime validUntil, IUser user)
        {
            Id = id;
            ValidUntil = validUntil;
            User = user;
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
        public static implicit operator Session(SessionEntity sessionEntity)
        {
            User user = sessionEntity.EntityUser;
            var session = new Session(sessionEntity.Id, sessionEntity.ValidUntil,user);
            return session;
        }

        public string Id { get; }
        public DateTime ValidUntil { get; }
        public IUser User { get; }
    }
}
