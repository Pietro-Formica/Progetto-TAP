using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
