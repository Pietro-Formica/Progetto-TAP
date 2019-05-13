using System;
using System.Collections.Generic;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{

    class User : IUser, IEquatable<User>
    {
        private readonly string _site;
        private readonly string _connectionString;
        private readonly IAlarmClock _alarmClock;
        public User(string username, string site, string connectionString, IAlarmClock alarmClock)
        {
            Username = username;
            _site = site;
            _connectionString = connectionString;
            _alarmClock = alarmClock;
        }

        public IEnumerable<IAuction> WonAuctions()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(_site, other._site) && string.Equals(Username, other.Username);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((_site != null ? _site.GetHashCode() : 0) * 397) ^ (Username != null ? Username.GetHashCode() : 0);
            }
        }
        public string Username { get; }

    }
    
}
