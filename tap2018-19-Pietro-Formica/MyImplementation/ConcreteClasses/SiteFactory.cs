using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyImplementation.Builders;
using MyImplementation.MyDatabase.Context;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{

    public class SiteFactory : ISiteFactory
    {
        public void Setup(string connectionString)
        {
            DropCreateDatabaseAlways<MyDBdContext> strategy = new DropCreateDatabaseAlways<MyDBdContext>();

            if (connectionString is null)
                throw new ArgumentNullException();
            try
            {
                Database.SetInitializer(strategy);
                strategy.InitializeDatabase(new MyDBdContext(connectionString));              
            }
            catch(Exception exception)
            {
                throw new UnavailableDbException("lol",exception.InnerException);
            }
        }
        public IEnumerable<string> GetSiteNames(string connectionString)
        {
            var manager = new SiteFactoryManager(connectionString);
            var list = manager.SearchAllEntities().ToList();
            return SiteBuilder.NewSiteBuilder()
                .BuildAll(list);


        }
        public void CreateSiteOnDb(string connectionString, string name, int timezone, int sessionExpirationTimeInSeconds,double minimumBidIncrement)
        {
            var siteManager = new SiteFactoryManager(connectionString);

            siteManager.SaveOnDb(EntitySiteBuilder.NewBuilder(name)
                .Timezone(timezone)
                .SessionExpirationInSeconds(sessionExpirationTimeInSeconds)
                .MinimumBidIncrement(minimumBidIncrement)
                .Build()); 

        }

        public ISite LoadSite(string connectionString, string name, IAlarmClock alarmClock)
        {
            var siteManager = new SiteFactoryManager(connectionString);
            var siteEntity = siteManager.SearchEntity(name) ?? throw new InexistentNameException(name);
            var site = SiteBuilder.NewSiteBuilder()
                .SetConnectionString(connectionString)
                .SetAlarmClock(alarmClock)
                .SetEntity(siteEntity)
                .Build();
            return site;           
        }

        public int GetTheTimezoneOf(string connectionString, string name)
        {
            var manager = new SiteFactoryManager(connectionString);
            var site = manager.SearchEntity(name) ?? throw new InexistentNameException(name);

            return site.Timezone;
        }

    }
}
