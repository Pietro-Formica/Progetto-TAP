using System;
using System.Collections.Generic;
using System.Data.Entity;
using MyImplementation.Builders;
using MyImplementation.Exceptions;
using MyImplementation.Extensions;
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
            catch
            {
                throw new UnavailableDbException();
            }
        }
        public IEnumerable<string> GetSiteNames(string connectionString)
        {
            var builder = SiteBuilder.NewSiteBuilder().SetConnectionString(connectionString);
            var list = builder.GetAllSiteName();
            return builder.BuildAll(list);


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
                .SearchEntity(name,new DbInexistentNameException(name))
                .Build();
            return site;           
        }

        public int GetTheTimezoneOf(string connectionString, string name)
        {
            var siteBuilder = SiteBuilder.NewSiteBuilder()
                .SetConnectionString(connectionString)
                .SearchEntity(name, new DbInexistentNameException(name));
                
            return siteBuilder.SiteEntity.Timezone;
            

        }
    }
}
