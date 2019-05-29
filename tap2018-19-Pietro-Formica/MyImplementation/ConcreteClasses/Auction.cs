using System;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{

    public class Auction : IAuction, IEquatable<Auction>
    {
        private readonly string _connectionString;
        private readonly IAlarmClock _alarmClock;
        private readonly string _mySite;
        public Auction(int id, IUser seller, string description, DateTime endsOn, string connectionString, IAlarmClock alarmClock, string mySite)
        {
            Id = id;
            Seller = seller;
            Description = description;
            EndsOn = endsOn;
            _connectionString = connectionString;
            _alarmClock = alarmClock;
            _mySite = mySite;
        }

        public IUser CurrentWinner()
        {
            throw new NotImplementedException();
        }

        public double CurrentPrice()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public bool BidOnAuction(ISession session, double offer)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Auction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_mySite, other._mySite) && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Auction)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_mySite != null ? _mySite.GetHashCode() : 0) * 397) ^ Id;
            }
        }

        public int Id { get; }
        public IUser Seller { get; }
        public string Description { get; }
        public DateTime EndsOn { get; }

    }
}
