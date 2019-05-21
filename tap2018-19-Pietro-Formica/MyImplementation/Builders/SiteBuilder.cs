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
        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                if (_connectionString is null) throw new ArgumentNullException();
                return _connectionString;
            }
        }
        private IAlarmClock _alarmClock;
        public IAlarmClock AlarmClock
        {
            get
            {
                if (_alarmClock is null) throw new ArgumentNullException();
                return _alarmClock;
            }
        }

        private SiteEntity _siteEntity;
        public SiteEntity SiteEntity
        {
            get
            {
                if (_siteEntity is null) throw new ArgumentNullException();
                return _siteEntity;
            }
            set => _siteEntity = value;
        }
        public SiteBuilder SetConnectionString(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            _connectionString = connectionString;
            return this;
        }
        public SiteBuilder SetAlarmClock(IAlarmClock alarmClock)
        {
            _alarmClock = alarmClock;
            return this;
        }
        private SiteBuilder()
        {
        }
        public static SiteBuilder NewSiteBuilder() => new SiteBuilder();
        public Site Build()
        {
            Control.CheckAlarmClock(AlarmClock, SiteEntity.Timezone);
            var site = new Site(SiteEntity.Id, SiteEntity.Timezone, SiteEntity.SessionExpirationInSeconds,
                SiteEntity.MinimumBidIncrement, ConnectionString, AlarmClock);
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
