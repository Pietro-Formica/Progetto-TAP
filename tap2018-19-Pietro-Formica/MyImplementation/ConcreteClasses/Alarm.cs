using System;
using TAP2018_19.AlarmClock.Interfaces;

namespace MyImplementation.ConcreteClasses
{
    class Alarm : IAlarm
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public event Action RingingEvent;
    }
}
