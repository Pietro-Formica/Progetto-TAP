using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{

    public class EntityAuctionBuilder
    {
        private readonly IAlarmClock _alarmClock;
        private string _userId;
        private string _siteId;
        private string _description;
        private DateTime _endsOn;
        private double _startingPrice;
        private EntityAuctionBuilder(IAlarmClock alarmClock) => _alarmClock = alarmClock;

        public static EntityAuctionBuilder NewBuilder (IAlarmClock alarmClock) => new EntityAuctionBuilder(alarmClock);

        public EntityAuctionBuilder SetUserSeller(string userId)
        {
            _userId = userId;
            return this;
        }

        public EntityAuctionBuilder SetSiteId(string siteId)
        {
            _siteId = siteId;
            return this;
        }

        public EntityAuctionBuilder SetDescription(string description)
        {
            _description = description;
            return this;
        }

        public EntityAuctionBuilder SetEndsOn(DateTime endsOn)
        {
            _endsOn = endsOn;
            return this;
        }

        public EntityAuctionBuilder SetStartingPrice(double startingPrice)
        {
            _startingPrice = startingPrice;
            return this;
        }

        public AuctionEntity Build()
        {
            if (_startingPrice < 0) throw new ArgumentOutOfRangeException();
            if (_description is null) throw new ArgumentNullException();
            if (_description.Equals(string.Empty)) throw new ArgumentException();
            if (DateTime.Compare(_endsOn,_alarmClock.Now) < 0) throw new UnavailableTimeMachineException();
            if(_siteId is null) throw new ArgumentNullException();
            if(_siteId.Equals(string.Empty)) throw new ArgumentException();
         
             var auctionEntity = new AuctionEntity()
            {
                SellerId = _userId,
                SiteID = _siteId,
                Description = _description,
                EndsOn = _endsOn,
                StartingPrice = _startingPrice,
                CurrentOffer = _startingPrice
                
            };
            return auctionEntity;
        }


    }
}
