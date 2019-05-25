using System.Collections.Generic;
using TAP2018_19.AlarmClock.Interfaces;

namespace MyImplementation
{

    public interface IBuilder
    {
        string ConnectionString { get; }
        IAlarmClock AlarmClock { get; }
        IBuilder SetConnectionString(string connectionString);
        IBuilder SetAlarmClock(IAlarmClock alarmClock);
        IBuilder SetSiteName(string siteName);
        IBuilder SearchEntity(string key);
        T Build<T>();
        IEnumerable<TK> BuildAll<TK>();
    }

 

}
