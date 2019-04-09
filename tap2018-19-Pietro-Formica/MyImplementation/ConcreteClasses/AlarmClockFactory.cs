using System;
using TAP2018_19.AlarmClock.Interfaces;

namespace MyImplementation.ConcreteClasses
{
    class AlarmClockFactory : IAlarmClockFactory
    {
        public IAlarmClock InstantiateAlarmClock(int timezone)
        {
            throw new NotImplementedException();
        }
    }
}
