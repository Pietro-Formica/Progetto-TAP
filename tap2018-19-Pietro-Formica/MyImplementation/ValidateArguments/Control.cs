using System;
using System.Configuration;
using System.Data.Entity;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ValidateArguments
{

    static class Control
    {
        public static void CheckSessionExpirationTimeInSeconds(int sessionExpirationTimeInSeconds)
        {
            if (sessionExpirationTimeInSeconds < 0) throw new ArgumentOutOfRangeException();
        }
        public static void CheckTimezone(int timezone)
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

        public static void CheckMinimumBidIncrement(double minimumBidIncrement)
        {
            if (minimumBidIncrement < 0) throw new ArgumentOutOfRangeException();
        }

        public static void CheckName(int maxValue, int minValue, string name)
        {
            if (name is null) throw new ArgumentNullException();

            var nameValidate = new StringValidator(minValue, maxValue);
            nameValidate.Validate(name);
        }

        public static void CheckAlarmClock(IAlarmClock alarmClock, int siteTimezone)              
        {
            if (alarmClock is null) throw new ArgumentNullException();
            if (alarmClock.Timezone != siteTimezone) throw new ArgumentException();
 
        }
        public static void CheckPassword(string password)
        {
            if (password is null) throw new ArgumentNullException();
            if (password.Length < DomainConstraints.MinUserPassword) throw new ArgumentException();
        }
        public static void CheckConnectionString(string connectionString)
        {
            if (connectionString is null) throw new ArgumentNullException();

            if (!Database.Exists(connectionString)) throw new UnavailableDbException();
        }
    }

}
