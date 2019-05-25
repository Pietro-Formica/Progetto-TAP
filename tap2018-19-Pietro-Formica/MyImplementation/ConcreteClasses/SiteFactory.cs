using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MyImplementation.Builders;
using MyImplementation.Exceptions;
using MyImplementation.Extensions;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
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
            catch
            {
                throw new UnavailableDbException();
            }
        }
        public IEnumerable<string> GetSiteNames(string connectionString)
        {
/*            var builder = SiteBuilder.NewSiteBuilder().SetConnectionString(connectionString);
            var list = builder.GetAllSiteName();
            return builder.BuildAll(list);*/
            IManager<SiteEntity> manager = new SiteFactoryManager(connectionString);
            var list = manager.SearchAllEntities().ToList();
            return YieldReturn(list);


        }
        public void CreateSiteOnDb(string connectionString, string name, int timezone, int sessionExpirationTimeInSeconds,double minimumBidIncrement)
        {
           EntitySiteBuilder.NewBuilder(name)
                .Timezone(timezone)
                .SessionExpirationInSeconds(sessionExpirationTimeInSeconds)
                .MinimumBidIncrement(minimumBidIncrement)
                .Build()
                .SaveEntityOnDb(connectionString);

        }

        public ISite LoadSite(string connectionString, string name, IAlarmClock alarmClock)
        {
            var site = SiteBuilder.NewSiteBuilder()
                .SetConnectionString(connectionString)
                .SetAlarmClock(alarmClock)
                .SetEntity(new SiteFactoryManager(connectionString),name)
                .Build();
            return site;           
        }

        public int GetTheTimezoneOf(string connectionString, string name)
        {
/*            var siteBuilder = SiteBuilder.NewSiteBuilder()
                .SetConnectionString(connectionString)
                .SearchEntity(name, new DbInexistentNameException(name));
                
            return siteBuilder.SiteEntity.Timezone;*/
            var manager = new SiteFactoryManager(connectionString);
            var site = manager.SearchEntity(name);
            return site.Timezone;


        }
        private static IEnumerable<string> YieldReturn( List<SiteEntity> list)
        {
            if (list.Count == 0) yield break;
            foreach (var siteEntity in list)
            {
                yield return siteEntity.Id;
            }
        }
    }
}
