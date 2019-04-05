using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Diagnostics;
using MyImplementation.MyDatabase.DataEntities;
using MyImplementation.MyDatabase.Implements;
using MyImplementation.MyDatabase.Interfaces;
using Ninject;
using Ninject.Parameters;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;
using IManagerSetup = MyImplementation.MyDatabase.Interfaces.IManagerSetup;
using StringValidator = System.Configuration.StringValidator;


namespace MyImplementation.ConcreteClasses
{

    public class SiteFactory : ISiteFactory
    {
        private readonly IEntityShooter<SiteEntity> _entityShooter;
        private readonly IManagerSetup _managerSetup;
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

            throw new NotImplementedException();
        }

        public void CreateSiteOnDb(string connectionString, string name, int timezone, int sessionExpirationTimeInSeconds,double minimumBidIncrement)
        {
            if (connectionString is null || name is null)
                throw new ArgumentNullException();
            try
            {
                _entityShooter.ControlConnectionString(connectionString);
                CheckName(name);
                CheckTimezone(timezone);
                CheckSessionExpirationTimeInSeconds(sessionExpirationTimeInSeconds);
                CheckMinimumBidIncrement(minimumBidIncrement);
                var siteEntity = new SiteEntity(name , timezone, sessionExpirationTimeInSeconds, minimumBidIncrement);
                _entityShooter.Add(siteEntity);              
            }
            catch (NameAlreadyInUseException)
            {
                throw new NameAlreadyInUseException("lazzo");
            }
            catch (UnavailableDbException)
            {
                throw new UnavailableDbException();
            }
            catch (ArgumentOutOfRangeException)
            {
                throw new ArgumentOutOfRangeException();
            }
            catch (ArgumentException)
            {
                throw new ArgumentException();
            }
            

        }

        public ISite LoadSite(string connectionString, string name, IAlarmClock alarmClock)
        {
            throw new NotImplementedException();
        }

        public int GetTheTimezoneOf(string connectionString, string name)
        {
            throw new NotImplementedException();
        }

        private void CheckTimezone(int timezone)
        {
            var timezoneValidate = new IntegerValidator(DomainConstraints.MinTimeZone,DomainConstraints.MaxTimeZone,false);
            try
            {
                timezoneValidate.Validate(timezone);
            }
            catch (ArgumentException)
            {
              throw new ArgumentOutOfRangeException();
            }

        }

        private void CheckSessionExpirationTimeInSeconds(int sessionExpirationTimeInSeconds)
        {
            if(sessionExpirationTimeInSeconds < 0)
                throw new ArgumentOutOfRangeException();
  
        }

        private void CheckMinimumBidIncrement(double minimumBidIncrement)
        {
            if(minimumBidIncrement < 0)
                throw new ArgumentOutOfRangeException();
        }

        private void CheckName(string name)
        {
            var nameValidate = new StringValidator(DomainConstraints.MinSiteName,DomainConstraints.MaxSiteName);
            try
            {
                nameValidate.Validate(name);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException();
            }

        }
    }

}
