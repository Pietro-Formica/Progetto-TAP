using System;
using System.Configuration;
using System.Data.Entity;
using System.Security.Cryptography;
using System.Text;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

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
        public static void CheckNegativeOffer(double offer)
        {
            if(offer < 0) throw new ArgumentOutOfRangeException();
        }
        public static string HashPassword(string password)
        {
            var sha256Hash = SHA256.Create();

            var data = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var sBuilder = new StringBuilder();

            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static bool CompareHashPassword(string userPassword, string databasePassword)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(userPassword, databasePassword) == 0;
        }
    }

}
