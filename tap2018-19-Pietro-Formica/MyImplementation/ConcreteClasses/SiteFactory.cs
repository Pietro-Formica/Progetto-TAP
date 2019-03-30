using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Diagnostics;
using Ninject;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Database.Interface;
using TAP2018_19.AuctionSite.Interfaces;


namespace MyImplementation.ConcreteClasses
{

    public class SiteFactory : ISiteFactory
    {
        internal static class Configuration
        {
            internal const string ImplementationAssembly =
                @"..\..\..\MyDatabase\bin\Debug\MyDatabase.dll";
        }

        public void Setup(string connectionString)
        {
            if(connectionString is null)
                throw new ArgumentNullException();
            try
            {
                var kernel = new StandardKernel();
                kernel.Load(Configuration.ImplementationAssembly);
                var managerSetup = kernel.Get<IManagerSetup>();
                managerSetup.SetStrategy();
                managerSetup.Initialize(connectionString);
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

        public void CreateSiteOnDb(string connectionString, string name, int timezone, int sessionExpirationTimeInSeconds,
            double minimumBidIncrement)
        {
            throw new NotImplementedException();
        }

        public ISite LoadSite(string connectionString, string name, IAlarmClock alarmClock)
        {
            throw new NotImplementedException();
        }

        public int GetTheTimezoneOf(string connectionString, string name)
        {
            throw new NotImplementedException();
        }
    }
}
