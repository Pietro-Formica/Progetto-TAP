using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.ConcreteClasses;
using MyImplementation.MyDatabase.DataEntities;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{

    public class AuctionBuilder
    {
        public string ConnectionString { get; private set; }

        public IAlarmClock AlarmClock { get; private set; }

        public AuctionEntity AuctionEntity { get; private set; }

        private AuctionBuilder()
        {
        }

        public static AuctionBuilder NewAuctionBuilder() => new AuctionBuilder();

        public AuctionBuilder SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
            return this;
        }

        public AuctionBuilder SetAlarmClock(IAlarmClock alarmClock)
        {
            AlarmClock = alarmClock;
            return this;
        }

        public AuctionBuilder SetAuctionEntity(AuctionEntity auctionEntity)
        {
            AuctionEntity = auctionEntity;
            return this;
        }

        public Auction Build()
        {
            if (AuctionEntity is null) throw new ArgumentNullException();
            if (ConnectionString is null) throw new ArgumentNullException();
            if (AlarmClock is null) throw new ArgumentNullException();
            var userManager = new UserManager(ConnectionString, AuctionEntity.SiteID);
            var userEntity = userManager.SearchEntity(AuctionEntity.SellerId);
            var user = UserBuilder.NewUserBuilder()
                .SetConnectionString(ConnectionString)
                .SetAlarmClock(AlarmClock)
                .SetEntity(userEntity)
                .Build();

            var auction = new Auction(AuctionEntity.ID, user, AuctionEntity.Description, AuctionEntity.EndsOn,
                ConnectionString, AlarmClock, AuctionEntity.SiteID);
            return auction;

        }
        public IEnumerable<IAuction> BuildAll(IEnumerable<AuctionEntity> enumerable)
        {


            var auctionEntities = enumerable.ToList();

            if (auctionEntities.Count == 0) yield break;

            foreach (var auctionEntity in auctionEntities)
            {
                AuctionEntity = auctionEntity;
                yield return Build();

            }

        }
    }

}
