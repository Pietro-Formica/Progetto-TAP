using System;
using System.Collections.Generic;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.MyDatabase.Implements;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;



namespace MyImplementation.ConcreteClasses
{

    public class SiteFactory : ISiteFactory
    {
        private readonly ManagerEntitySite _entityShooter;
        private readonly ManagerSetup _managerSetup;
        public SiteFactory()
        {
            _managerSetup = new ManagerSetup();
            _entityShooter = new ManagerEntitySite();
        }

        public void Setup(string connectionString)
        {
            if (connectionString is null)
                throw new ArgumentNullException();
            try
            {
                _managerSetup.SetStrategy();
                _managerSetup.Initialize(connectionString);
               
            }
            catch
            {
                throw new UnavailableDbException();
            }
        }

        public IEnumerable<string> GetSiteNames(string connectionString)
        {
            if(connectionString is null)
                throw new ArgumentNullException();

           _entityShooter.ControlConnectionString(connectionString);
           IEnumerable<string> names = _entityShooter.GetSiteNames(connectionString);

           return names;




        }

        public void CreateSiteOnDb(string connectionString, string name, int timezone, int sessionExpirationTimeInSeconds,double minimumBidIncrement)
        {
            if (connectionString is null)
                throw new ArgumentNullException();

             _entityShooter.ControlConnectionString(connectionString);
             var siteEntity = new SiteEntity(name , timezone, sessionExpirationTimeInSeconds, minimumBidIncrement);
             _entityShooter.Add(siteEntity);                     
        }

        public ISite LoadSite(string connectionString, string name, IAlarmClock alarmClock)
        {
            if (connectionString is null)
                throw new ArgumentNullException();

            _entityShooter.ControlConnectionString(connectionString);
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            Control.CheckAlarmClock(alarmClock);
            var siteEntity = _entityShooter.FindByKey(name);
            if(siteEntity is null)
                throw new InexistentNameException(name);

            return WrapSiteEntity(siteEntity, alarmClock);

        }

        public int GetTheTimezoneOf(string connectionString, string name)
        {
            if(connectionString is null)
                throw new ArgumentNullException();
            Control.CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            var siteEntity = _entityShooter.FindByKey(name);
            if (siteEntity is null)
                throw new InexistentNameException(name);

            return siteEntity.Timezone;

        }

        private ISite WrapSiteEntity( SiteEntity entity, IAlarmClock alarmClock)
        {
            if (alarmClock.Timezone == entity.Timezone)
            {
                ISite site = new Site(entity.Name, entity.Timezone, entity.SessionExpirationInSeconds, entity.MinimumBidIncrement);
                return site;
            }
            throw new ArgumentException();

        }






    }

}
