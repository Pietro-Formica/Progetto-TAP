using System;
using MyImplementation.ConcreteClasses;
using MyImplementation.MyDatabase.Context;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.Builders
{
    public class SiteBuilder
    {
/*        public string Id { get; private set; }
        public int Timezone { get; private set; }
        public int SessionExpirationInSeconds { get; private set; }
        public double MinimumBidIncrement { get; private set; }*/

        public string ConnectionString { get; private set; }
        private IAlarmClock _alarmClock;
        public SiteEntity SiteEntity { get; set; }

        private SiteBuilder() { }
        public static SiteBuilder NewSiteBuilder() => new SiteBuilder();
        public SiteBuilder SetConnectionString(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            ConnectionString = connectionString;
            return this;
        }
        public SiteBuilder SetAlarmClock(IAlarmClock alarmClock)
        {
            _alarmClock = alarmClock;
            return this;
        }
/*        public SiteBuilder SearchEntity(string name)
        {
            if (ConnectionString is null)
                throw new ArgumentNullException();
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            using (var context = new MyDBdContext(ConnectionString))
            {
               var siteEntity = context.SiteEntities.Find(name) ?? throw new InexistentNameException(name);
               Id = siteEntity.Id;
               Timezone = siteEntity.Timezone;
               SessionExpirationInSeconds = siteEntity.SessionExpirationInSeconds;
               MinimumBidIncrement = siteEntity.MinimumBidIncrement;
                return this;
            }
        }*/
/*        public Site Build()
        {
            if(ConnectionString is null)
                throw new ArgumentNullException();
            Control.CheckAlarmClock(_alarmClock,Timezone);
            var site = new Site(Id, Timezone, SessionExpirationInSeconds,MinimumBidIncrement, ConnectionString, _alarmClock);
            return site;
        }*/
        public Site Build()
        {
            if (ConnectionString is null) throw new ArgumentNullException();
            if(SiteEntity is null) throw new ArgumentNullException();
            Control.CheckAlarmClock(_alarmClock, SiteEntity.Timezone);
            var site = new Site(SiteEntity.Id, SiteEntity.Timezone, SiteEntity.SessionExpirationInSeconds,SiteEntity.MinimumBidIncrement, ConnectionString, _alarmClock);
            return site;
        }
    }
}
