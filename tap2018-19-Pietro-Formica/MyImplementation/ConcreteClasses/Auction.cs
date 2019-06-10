using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using MyImplementation.Builders;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{

    public class Auction : IAuction, IEquatable<Auction>
    {
        private readonly string _connectionString;
        private readonly IAlarmClock _alarmClock;
        private readonly string _mySite;
        private readonly IManager<AuctionEntity, int> _auctionManager;
        private readonly IManager<SessionEntity, string> _sessionManager;
        private readonly IManager<UserEntity, string> _userManager;
        public Auction(int id, IUser seller, string description, DateTime endsOn, string connectionString, IAlarmClock alarmClock, string mySite)
        {
            Id = id;
            Seller = seller;
            Description = description;
            EndsOn = endsOn;
            _connectionString = connectionString;
            _alarmClock = alarmClock;
            _mySite = mySite;
            _auctionManager = new AuctionManager(connectionString, mySite);
            _sessionManager = new SessionManager(connectionString, mySite);
            _userManager = new UserManager(connectionString, mySite);
        }

        public IUser CurrentWinner()
        {
           var auction = _auctionManager.SearchEntity(Id);
           if (auction?.CurrentWinner is null) return null;
           return UserBuilder.NewUserBuilder()
               .SetAlarmClock(_alarmClock)
               .SetConnectionString(_connectionString)
               .SetEntity(auction.CurrentWinner)
               .Build();
        }

        public double CurrentPrice()
        {

            return _auctionManager.SearchEntity(Id).CurrentOffer;
        }

        public void Delete()
        {

            var auctionEntity = _auctionManager.SearchEntity(Id);
            if(auctionEntity is null) throw new InvalidOperationException();
            _auctionManager.DeleteEntity(auctionEntity);
        }

        public bool BidOnAuction(ISession session, double offer)
        {
            Control.CheckNegativeOffer(offer);
            var realSession = session as Session;
            if (realSession is null) throw new ArgumentNullException();
            var auction = _auctionManager.SearchEntity(Id);
            if(auction is null || auction.EndsOn <= _alarmClock.Now) throw new InvalidOperationException();
            if(!session.IsValid()) throw new ArgumentException();
            var sessionEntity = _sessionManager.SearchEntity(realSession.Id);
            var bidder = sessionEntity.EntityUser;
            var seller = auction.Seller;
            if(bidder is null || seller is null) throw new InvalidOperationException();
            if (bidder.Id.Equals(seller.Id) || !bidder.SiteId.Equals(seller.SiteId)) throw new ArgumentException();
            sessionEntity.ValidUntil = _alarmClock.Now.AddSeconds(auction.Site.SessionExpirationInSeconds);
            realSession.ValidUntil = sessionEntity.ValidUntil;
            _sessionManager.SaveOnDb(sessionEntity,true);

            if (auction.CurrentWinner is null)
            {
                if (offer < auction.CurrentOffer) return false;
                auction.MaxOffer = offer;
                auction.WinnerId = bidder.Id;
                auction.WinnerSiteId = bidder.SiteId;
                _auctionManager.SaveOnDb(auction,true);
                return true;
            }



            if (auction.CurrentWinner.Id.Equals(bidder.Id) && auction.CurrentWinner.SiteId.Equals(bidder.SiteId))
            {
                if (offer <= auction.MaxOffer + auction.Site.MinimumBidIncrement) return false;
                auction.MaxOffer = offer;
                _auctionManager.SaveOnDb(auction, true);
                return true;
            }

            if (offer < auction.CurrentOffer + auction.Site.MinimumBidIncrement) return false;
            if (offer > auction.MaxOffer)
            {
                var userEntity = _userManager.SearchEntity(auction.WinnerId);
                userEntity.WinnerAuctionId = Id;
                _userManager.SaveOnDb(userEntity,true);
                auction.CurrentWinner.WinnerAuctionId = Id;
                var auction1 = _auctionManager.SearchEntity(Id);
                auction1.CurrentOffer = Math.Min(offer, auction.MaxOffer + auction.Site.MinimumBidIncrement);
                auction1.MaxOffer = offer;
                auction1.WinnerId = bidder.Id;
                auction1.WinnerSiteId = bidder.SiteId;
                _auctionManager.SaveOnDb(auction1, true);
                return true;
            }

            if (auction.MaxOffer > offer)
            {
                auction.CurrentOffer = Math.Min(auction.MaxOffer, offer + auction.Site.MinimumBidIncrement);
                _auctionManager.SaveOnDb(auction, true);
                return true;
            }

            return false;
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
