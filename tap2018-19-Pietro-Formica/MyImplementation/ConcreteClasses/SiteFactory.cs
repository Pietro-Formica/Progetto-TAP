using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
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
            Control.CheckConnectionString(connectionString);
            using (var context = new MyDBdContext(connectionString))
            {
                var query = from tmp in context.SiteEntities
                    select tmp.Id;
                return query.ToList();
            }
        }

        public void CreateSiteOnDb(string connectionString, string name, int timezone, int sessionExpirationTimeInSeconds,double minimumBidIncrement)
        {
            Control.CheckConnectionString(connectionString);
            var siteEntity = new SiteEntity(name , timezone, sessionExpirationTimeInSeconds, minimumBidIncrement);
            using (var contextDb = new MyDBdContext(connectionString))
            {
                contextDb.SiteEntities.Add(siteEntity);
                try
                {
                    contextDb.SaveChanges();
                }
                catch (DbUpdateException)//da rivedere lol
                {
                    throw new NameAlreadyInUseException(siteEntity.Id);
                }
            }                  
        }

        public ISite LoadSite(string connectionString, string name, IAlarmClock alarmClock)
        {
            Control.CheckConnectionString(connectionString);
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            Control.CheckAlarmClock(alarmClock);
            //alarmClock.InstantiateAlarm(5 * 60 * 1000);
            using (var context = new MyDBdContext(connectionString))
            {
                var siteEntity = context.SiteEntities.Find(name);
                if (siteEntity is null)
                    throw new InexistentNameException(name);
                return WrapSiteEntity(siteEntity, alarmClock,connectionString);
            }


        }

        public int GetTheTimezoneOf(string connectionString, string name)
        {
            Control.CheckConnectionString(connectionString);
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            using (var context = new MyDBdContext(connectionString))
            {
                
                    var siteEntity = context.SiteEntities.SingleOrDefault(s => s.Id == name);
                    if (siteEntity is null)
                        throw new InexistentNameException(name);
                    return siteEntity.Timezone;             
            }
        }

        private ISite WrapSiteEntity( SiteEntity entity, IAlarmClock alarmClock, string connectionString)
        {
            if (alarmClock.Timezone == entity.Timezone)
            {
                ISite site = new Site(entity.Id, entity.Timezone, entity.SessionExpirationInSeconds, entity.MinimumBidIncrement,connectionString, alarmClock);
                return site;
            }
            throw new ArgumentException();

        }






    }

}
