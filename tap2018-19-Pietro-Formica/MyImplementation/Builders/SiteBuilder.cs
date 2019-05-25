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
        public SiteBuilder SetEntity(IManager<SiteEntity> manager, string nameEntity)
        {
            _siteEntity = manager.SearchEntity(nameEntity);
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
        public IEnumerable<string> BuildAll(IManager<SiteEntity> manager)
        {
            //var enumerable = siteEntities.ToList();
             var siteList = manager.SearchAllEntities();
             var siteEntities = siteList.ToList();
             if (siteEntities.Any()) yield break;
            foreach (var siteEntity in siteEntities)
            {
                yield return siteEntity.Id;
            }
        }


    }


}

 
