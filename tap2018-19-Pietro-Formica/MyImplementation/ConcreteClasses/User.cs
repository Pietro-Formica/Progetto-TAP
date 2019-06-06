using System;
using System.Collections.Generic;
using System.Linq;
using MyImplementation.Builders;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{

    public class User : IUser, IEquatable<User>
    {
        private readonly string _connectionString;
        private readonly IAlarmClock _alarmClock;
        private readonly string _mySite;
        public User(string username, string site, string connectionString, IAlarmClock alarmClock, string mySite)
        {
            Username = username;
            Site = site;
            _connectionString = connectionString;
            _alarmClock = alarmClock;
            _mySite = mySite;
        }

        public IEnumerable<IAuction> WonAuctions()
        {
            var userManager = new UserManager(_connectionString,_mySite);
            var listAuctionWin = userManager.SearchEntity(Username).WinnerAuctionEntities.Where(wau => wau.EndsOn < _alarmClock.Now);
            if(listAuctionWin is null) throw new InvalidOperationException();
            return AuctionBuilder.NewAuctionBuilder()
                .SetAlarmClock(_alarmClock)
                .SetConnectionString(_connectionString)
                .BuildAll(listAuctionWin);
        }

        public void Delete()
        {
            var userManager = new UserManager(_connectionString, _mySite);
            var userEntity = userManager.SearchEntity(Username);
            if(userEntity is null) throw new InvalidOperationException();
            var listAuctionWin = userEntity.WinnerAuctionEntities;
            var listAuctionSeller = userEntity.SellerAuctionEntities;
            if (listAuctionSeller.Count == 0 && listAuctionWin.Count == 0)
            {
                userManager.DeleteEntity(userEntity);
            }
            else
            {
                if(listAuctionWin.Count(auw => auw.EndsOn > _alarmClock.Now) != 0 || listAuctionSeller.Count(aus => aus.EndsOn > _alarmClock.Now) != 0)
                    throw new InvalidOperationException();
                var auctionManager = new AuctionManager(_connectionString, _mySite);
                listAuctionSeller.ToList().ForEach(aus =>
                {
                    auctionManager.DeleteEntity(aus);
                });
                userManager.DeleteEntity(userEntity);
            }
            
        }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Site, other.Site) && string.Equals(Username, other.Username);
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
                return ((Site != null ? Site.GetHashCode() : 0) * 397) ^ (Username != null ? Username.GetHashCode() : 0);
            }
        }
        public string Username { get; }
        public string Site { get; }

    }
    
}
