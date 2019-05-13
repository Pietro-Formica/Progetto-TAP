using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyImplementation.ValidateArguments;
using TAP2018_19.AlarmClock.Interfaces;

namespace MyImplementation.Builders
{
    class UserBuilder
    {
        public string ConnectionString { get; private set; }
        private IAlarmClock _alarmClock;
        private UserBuilder() { }
        public static UserBuilder NewUserBuilder() => new UserBuilder();
        public UserBuilder SetConnectionString(string connectionString)
        {
            Control.CheckConnectionString(connectionString);
            ConnectionString = connectionString;
            return this;
        }
        public UserBuilder SetAlarmClock(IAlarmClock alarmClock)
        {
            _alarmClock = alarmClock;
            return this;
        }
        public
    }
}
