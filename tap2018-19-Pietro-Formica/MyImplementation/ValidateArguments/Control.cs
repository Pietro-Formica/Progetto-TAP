using System;
using System.Configuration;
using System.Data.Entity;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ValidateArguments
{
    static class Control
    {
        static void CheckSessionExpirationTimeInSeconds(int sessionExpirationTimeInSeconds)
        {

            if (sessionExpirationTimeInSeconds < 0)
                throw new ArgumentOutOfRangeException();

        }
        static void CheckTimezone(int timezone)
        {
            var timezoneValidate = new IntegerValidator(DomainConstraints.MinTimeZone, DomainConstraints.MaxTimeZone, false);
            try
            {
                timezoneValidate.Validate(timezone);
            }
            catch (ArgumentException)
            {
                throw new ArgumentOutOfRangeException();
            }

        }

        static void CheckMinimumBidIncrement(double minimumBidIncrement)
        {
            if (minimumBidIncrement < 0)
                throw new ArgumentOutOfRangeException();
        }

        public static void CheckName(int maxValue, int minValue, string name)
        {
            if(name is null)
                throw new ArgumentNullException();

            var nameValidate = new StringValidator(minValue,maxValue);
            nameValidate.Validate(name);
        }

        public static void CheckAlarmClock(IAlarmClock alarmClock)
        {
            if (alarmClock is null)
                throw new ArgumentNullException();
        }

        public static void CheckPassword(string password)
        {
            if(password is null)
                throw new ArgumentNullException();
            if(password.Length < DomainConstraints.MinUserPassword)
                throw new ArgumentException();
        }

        public static void CheckArgumentSiteEntity(string name, int timezone, int sessionExpirationTimeInSeconds,double minimumBidIncrement)
        {
            CheckName(DomainConstraints.MaxSiteName, DomainConstraints.MinSiteName, name);
            CheckTimezone(timezone);
            CheckSessionExpirationTimeInSeconds(sessionExpirationTimeInSeconds);
            CheckMinimumBidIncrement(minimumBidIncrement);
        }

        public static void CheckConnectionString(string connectionString)
        {
            if (connectionString is null)
                throw new ArgumentNullException();

            if (!Database.Exists(connectionString))
                throw new UnavailableDbException();
        }
    }
}
