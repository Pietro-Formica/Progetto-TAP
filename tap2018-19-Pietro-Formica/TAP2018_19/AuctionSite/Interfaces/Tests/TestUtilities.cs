using System;
using System.Collections.Generic;
using System.Diagnostics;
using Moq;
using NUnit.Framework;
using TAP2018_19.AlarmClock.Interfaces;
using TAP2018_19.TestBaseClasses;

namespace TAP2018_19.AuctionSite.Interfaces.Tests
{
    public abstract class InstrumentedAuctionSiteTest : AuctionSiteTest
    {
        protected string connectionString;
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISiteFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISiteFactory siteFactory;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISiteFactory' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'SetUp' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'SetUpAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        [SetUp]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'SetUpAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'SetUp' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void InitSiteFactory()
        {
            siteFactory = this.GetSiteFactory();
            connectionString = this.GetConnectionString();
            siteFactory.Setup(connectionString);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite LoadSiteFromName(string siteName, out Mock<IAlarmClock> alarmClockMoq)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            var connectionString = this.GetConnectionString();
            int timezone = this.GetSiteFactory().GetTheTimezoneOf(connectionString, siteName);
            var alarmClock = AlarmClockMock(timezone, out alarmClockMoq);
            return this.GetSiteFactory().LoadSite(connectionString, siteName, alarmClock);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite LoadSiteFromNamePlus(string siteName, out Mock<IAlarmClock> alarmClockMoq,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarm' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            out Mock<IAlarm> alarm)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarm' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            var connectionString = this.GetConnectionString();
            int timezone = this.GetSiteFactory().GetTheTimezoneOf(connectionString, siteName);
            var alarmClock = AlarmClockMockPlus(timezone, out alarmClockMoq, out alarm);
            return this.GetSiteFactory().LoadSite(connectionString, siteName, alarmClock);
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Returns a stub of IAlarmClock where Now is set to the current time adjusted by the timezone
        /// </summary>
        /// <param name="timeZone">the (auction site) timezone</param>
        /// <param name="alarmClock">the mock of the returned stub for further programming and usages</param>
        /// <returns>a stub of IAlarmClock</returns>
        protected IAlarmClock AlarmClockMock(int timeZone, out Mock<IAlarmClock> alarmClock)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            alarmClock = new Mock<IAlarmClock>();
            alarmClock.Setup(a => a.Now).Returns(DateTime.UtcNow.AddHours(timeZone));
            alarmClock.Setup(a => a.Timezone).Returns(timeZone);
            alarmClock.Setup(ac => ac.InstantiateAlarm(It.Is<int>(i => i > 0))).Returns(new Mock<IAlarm>().Object);

            return alarmClock.Object;
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarm' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Returns a stub of IAlarmClock where Now is set to the current time adjusted by the timezone
        /// </summary>
        /// <param name="timeZone">the (auction site) timezone</param>
        /// <param name="alarmClock">the mock of the returned stub for further programming and usages</param>
        /// <param name="alarm">the mock of the alarm used by the alarmClockMock for further programming and usages</param>
        /// <returns>a stub of IAlarmClock</returns>
        protected IAlarmClock AlarmClockMockPlus(int timeZone, out Mock<IAlarmClock> alarmClock, out Mock<IAlarm> alarm)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarm' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            alarmClock = new Mock<IAlarmClock>();
            alarmClock.Setup(a => a.Now).Returns(DateTime.UtcNow.AddHours(timeZone));
            alarmClock.Setup(a => a.Timezone).Returns(timeZone);
            alarm = new Mock<IAlarm>();
            alarmClock.Setup(ac => ac.InstantiateAlarm(It.Is<int>(i => i > 0))).Returns(alarm.Object);

            return alarmClock.Object;
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected void SetNowToFutureTime(int intervalTimeInSeconds, Mock<IAlarmClock> clockMock)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            var now = clockMock.Object.Now;
            clockMock.Setup(a => a.Now).Returns(now.AddSeconds(intervalTimeInSeconds));
            Debug.Assert(clockMock.Object.Now == now.AddSeconds(intervalTimeInSeconds));
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite CreateAndLoadEmptySite(int timeZone, string siteName, int sessionExpirationTimeInSeconds,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarm' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            double minimumBidIncrement, out Mock<IAlarmClock> alarmClockMoq, out Mock<IAlarm> alarmMoq)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarm' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            AlarmClockMockPlus(timeZone, out alarmClockMoq, out alarmMoq);
            this.GetSiteFactory()
                .CreateSiteOnDb(this.GetConnectionString(), siteName, timeZone, sessionExpirationTimeInSeconds,
                    minimumBidIncrement);
            var newSite = this.GetSiteFactory().LoadSite(this.GetConnectionString(), siteName, alarmClockMoq.Object);
            return newSite;
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite CreateAndLoadEmptySite(int timeZone, string siteName, int sessionExpirationTimeInSeconds,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            double minimumBidIncrement, out Mock<IAlarmClock> alarmClockMoq)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            Mock<IAlarm> alarmMock;
            return CreateAndLoadEmptySite(timeZone, siteName, sessionExpirationTimeInSeconds, minimumBidIncrement,
                out alarmClockMoq, out alarmMock);
        }


#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite CreateAndLoadSite(int timeZone, string siteName, int sessionExpirationTimeInSeconds,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            double minimumBidIncrement, out Mock<IAlarmClock>alarmClockMoq,List<string> userNameList=null, string password = "puffo")
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            var newSite = CreateAndLoadEmptySite(timeZone, siteName, sessionExpirationTimeInSeconds, minimumBidIncrement,
                out alarmClockMoq);
            if (null!=userNameList)
                foreach (var user in userNameList)
                    newSite.CreateUser(user, password);
            return GetSiteFactory().LoadSite(GetConnectionString(), siteName, alarmClockMoq.Object);
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite CreateAndLoadSite(int timeZone, string siteName, int sessionExpirationTimeInSeconds,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            double minimumBidIncrement, List<string> userNameList = null, string password = "puffo")
        {
            Mock<IAlarmClock> alarmClockMoq;
            return CreateAndLoadSite(timeZone, siteName, sessionExpirationTimeInSeconds, minimumBidIncrement,
                out alarmClockMoq,userNameList,password);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite CreateAndLoadSite(int timeZone, string siteName, int sessionExpirationTimeInSeconds,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            double minimumBidIncrement, List<string> userNameList, List<string> loggedUserNameList,
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            int delayBetweenLoginInSeconds, out List<ISession> sessionList, out Mock<IAlarmClock> alarmClockMoq,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarm' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            out Mock<IAlarm> alarmMoq, string password = "puffo")
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarm' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            //Pre: loggedUserNameList non empty and included in userNameList

            var newSite = CreateAndLoadEmptySite(timeZone, siteName, sessionExpirationTimeInSeconds, minimumBidIncrement,
                out alarmClockMoq, out alarmMoq);
            foreach (var user in userNameList)
            {
                newSite.CreateUser(user, password);
            }
            sessionList = new List<ISession>();
            var howManySessions = loggedUserNameList.Count;

            sessionList.Add(newSite.Login(loggedUserNameList[0], password));
            for (var i = 1; i < howManySessions; i++)
            {
                var newNow = alarmClockMoq.Object.Now.AddSeconds(delayBetweenLoginInSeconds);
                alarmClockMoq.Setup(a => a.Now).Returns(newNow);
                sessionList.Add(newSite.Login(loggedUserNameList[i], password));
            }
            return GetSiteFactory().LoadSite(GetConnectionString(), siteName, alarmClockMoq.Object);

        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite CreateAndLoadSite(int timeZone, string siteName, int sessionExpirationTimeInSeconds,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            double minimumBidIncrement, List<string> userNameList, List<string> loggedUserNameList,
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            int delayBetweenLoginInSeconds, out List<ISession> sessionList, out Mock<IAlarmClock> alarmClockMoq,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            string password = "puffo")
        {
            Mock<IAlarm> alarmMock;
            return CreateAndLoadSite(timeZone, siteName, sessionExpirationTimeInSeconds, minimumBidIncrement,
                userNameList, loggedUserNameList, delayBetweenLoginInSeconds, out sessionList, out alarmClockMoq,
                out alarmMock, password);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected bool AreEquivalentSessions(ISession session1, ISession session2)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            return session1.Id == session2.Id && session1.ValidUntil == session2.ValidUntil &&
                   session1.User.Equals(session2.User);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected bool CheckSessionValues(ISession session, string sessionId,DateTime validUntil, string username)
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        {
            return session.Id == sessionId && session.ValidUntil == validUntil && session.User.Username == username;
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAuction' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected bool CheckAuctionValues(IAuction auction, int auctionId, string sellerUsername, DateTime endsOn,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAuction' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            string auctionDescription, double auctionCurrentPrice, string currentWinnerUsername = null)
        {
            IUser currentWinner = auction.CurrentWinner();
            var correctCurrentWinner = (currentWinnerUsername == null && currentWinner == null) ||
                                       (currentWinner != null && currentWinner.Username != null && currentWinner.Username == currentWinnerUsername);
            return auction.Id == auctionId && auction.Seller.Username == sellerUsername && SameDateTime(auction.EndsOn,endsOn) &&
                      auction.Description == auctionDescription&&correctCurrentWinner&&Math.Abs(auction.CurrentPrice()- auctionCurrentPrice)<.001;
        }
        protected bool SameDateTime(DateTime x,DateTime y)
            //Saving Date on DB introduces approximations, so equality (as ticks) does not hold even for "same" date
        {
            return x.Year == y.Year && x.Month == y.Month && x.Day == y.Day && x.Hour == y.Hour && x.Minute == y.Minute &&
                   x.Second == y.Second;
        }
    }
}