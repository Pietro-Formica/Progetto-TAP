using System;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.AuctionSite.Interfaces;

namespace MyImplementation.ConcreteClasses
{
    class Session : ISession
    {
        private readonly IAlarmClock _alarmClock;
        public Session(string id, DateTime validUntil, IUser user, IAlarmClock alarmClock)
        {
            Id = id;
            ValidUntil = validUntil;
            User = user;
            _alarmClock = alarmClock;
        }

        public bool IsValid()
        {
            return ValidUntil > _alarmClock.Now;
        }

        public void Logout()
        {
            throw new NotImplementedException();
        }

        public IAuction CreateAuction(string description, DateTime endsOn, double startingPrice)
        {
            throw new NotImplementedException();
        }

        public string Id { get; }
        public DateTime ValidUntil { get; }
        public IUser User { get; }
    }
}
