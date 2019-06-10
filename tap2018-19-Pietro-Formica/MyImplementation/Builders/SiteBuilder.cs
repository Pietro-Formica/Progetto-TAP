using System;
using System.Collections.Generic;
using System.Linq;
using MyImplementation.ConcreteClasses;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;

namespace MyImplementation.Builders
{

    public class SiteBuilder
    {
        public string ConnectionString { get; private set; }

        public IAlarmClock AlarmClock { get; private set; }

        public SiteEntity SiteEntity { get; private set; }


        public SiteBuilder SetConnectionString(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            ConnectionString = connectionString;
            return this;
        }
        public SiteBuilder SetAlarmClock(IAlarmClock alarmClock)
        {
            AlarmClock = alarmClock;
            return this;
        }
        public SiteBuilder SetEntity(SiteEntity siteEntity)
        {
            SiteEntity = siteEntity;
            return this;
        }
        private SiteBuilder()
        {
        }
        public static SiteBuilder NewSiteBuilder() => new SiteBuilder();
        public Site Build()
        {

            if (SiteEntity is null) throw new ArgumentNullException();
            if (ConnectionString is null) throw new ArgumentNullException();
            if (AlarmClock is null) throw new ArgumentNullException();
            Control.CheckAlarmClock(AlarmClock, SiteEntity.Timezone);
            var site = new Site(SiteEntity.Id, SiteEntity.Timezone, SiteEntity.SessionExpirationInSeconds,
                SiteEntity.MinimumBidIncrement, ConnectionString, AlarmClock);
            return site;
        }
        public IEnumerable<string> BuildAll(IEnumerable<SiteEntity> enumerable)
        {
             var siteEntities = enumerable.ToList();
             if (siteEntities.Count == 0) yield break;
            foreach (var siteEntity in siteEntities)
            {
                yield return siteEntity.Id;
            }
        }


    }


}

 
