using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
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
        public string ConnectionString { get; private set; }
        private IAlarmClock _alarmClock;
        public SiteEntity SiteEntity { get; set; }
        private SiteBuilder()
        {
        }
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
        public Site Build()
        {
            if (ConnectionString is null) throw new ArgumentNullException();
            if (SiteEntity is null) throw new ArgumentNullException();
            Control.CheckAlarmClock(_alarmClock, SiteEntity.Timezone);
            var site = new Site(SiteEntity.Id, SiteEntity.Timezone, SiteEntity.SessionExpirationInSeconds,
                SiteEntity.MinimumBidIncrement, ConnectionString, _alarmClock);
            return site;
        }
        public IEnumerable<string> BuildAll(IEnumerable<SiteEntity> siteEntities)
        {
            var enumerable = siteEntities.ToList();
            if(enumerable.IsNullOrEmpty()) yield break;
            foreach (var siteEntity in enumerable)
            {
                yield return siteEntity.Id;
            }
        }
    }

}
