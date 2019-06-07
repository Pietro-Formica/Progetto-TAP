using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using TAP2018_19.AlarmClock.Interfaces;

namespace TAP2018_19.AuctionSite.Interfaces.Tests {
    public class UserTests : InstrumentedAuctionSiteTest {
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite Site;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected Mock<IAlarmClock> AlarmClock;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected IUser User;
        protected ISession Session;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected const string UserName = "My Dear Friend";
        protected const string Pw = "f86d 78ds6^^^55";

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'SetUp' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'SetUpAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Initializes Site:
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>site for user tests</description>
        /// </item>
        /// <item>
        /// <term>time zone</term>
        /// <description>-5</description>
        /// </item>
        /// <item>
        /// <term>expiration time</term>
        /// <description>360 seconds</description>
        /// </item>
        /// <item>
        /// <term>minimum bid increment</term>
        /// <description>7</description>
        /// </item>
        /// <item>
        /// <term>users</term>
        /// <description>username = "My Dear Friend", pw = "f86d 78ds6^^^55"</description>
        /// </item>
        /// <item>
        /// <term>auctions</term>
        /// <description>empty list</description>
        /// </item>
        /// <item>
        /// <term>sessions</term>
        /// <description>empty list</description>
        /// </item>
        /// </list>  
        /// </summary>
        [SetUp]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'SetUpAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'SetUp' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void Initialize() {
            Site = CreateAndLoadEmptySite(-5, "site for user tests", 360, 7, out AlarmClock);
            Site.CreateUser(UserName, Pw);
            User = Site.GetUsers().SingleOrDefault(u => u.Username == UserName);
            Session = Site.Login(UserName, Pw);

            Assert.That(User, Is.Not.Null, "Set up should be successful");
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that after deleting a user, its name is not anymore known to the site 
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void Delete_ExistingUser_Deletes() {
            User.Delete();
            var survived = Site.GetUsers().Any(u => u.Username == UserName);
            Assert.That(!survived);
        }
        [Test]
        public void DeleteUser_WinnnerInAuction_null()
        {
            var userSession = Site.Login(UserName, Pw);
            const string sellerName = "very lucky seller";
            const string sellerPw = "seller's password";
            Site.CreateUser(sellerName, sellerPw);
            var seller = Site.GetUsers().SingleOrDefault(u => u.Username == sellerName);
            var sellerSession = Site.Login(sellerName, sellerPw);
            var randomGen = new Random();
            var startingPrice = randomGen.NextDouble() * 100 + 1;
            var auction = sellerSession.CreateAuction($"The th auction for {sellerName}",
                AlarmClock.Object.Now.AddDays(1), startingPrice);
            auction.BidOnAuction(userSession, startingPrice * 2);
            var user = Site.GetUsers().SingleOrDefault(u => u.Username == UserName);
            var now = AlarmClock.Object.Now;
            AlarmClock.Setup(ac => ac.Now).Returns(now.AddHours(25));
            Site = siteFactory.LoadSite(connectionString, Site.Name, AlarmClock.Object); //needed to refresh time
            user.Delete();
            var auction1 = Site.GetAuctions(false).SingleOrDefault(x => x.Id == auction.Id);
            var usr = auction1.CurrentWinner();
            Assert.That(usr,Is.Null);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a call to Delete on a deleted user throws InvalidOperationException
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void Delete_DeletedUser_Throws() {
            User.Delete();
            Assert.That(() => User.Delete(), Throws.TypeOf<InvalidOperationException>());
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
/// <summary>
/// Verify that a newly created use has no won auctions
/// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void WonAuctions_NewUser_NoAuctions() {
            var wonAuctions = User.WonAuctions();
            Assert.That(wonAuctions, Is.Empty);
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that WonAuctions returns the won auctions of a user who has won some 
        /// </summary>
        /// <param name="howManyAuctions"></param>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0616 // 'Random' non è una classe Attribute
        public void WonAuctions_UserWithWonAuctions_NonEmpty([Random(1, 10, 1)] int howManyAuctions) {
#pragma warning restore CS0616 // 'Random' non è una classe Attribute
            var userSession = Site.Login(UserName, Pw);
            const string sellerName = "very lucky seller";
            const string sellerPw = "seller's password";
            Site.CreateUser(sellerName, sellerPw);
            var seller = Site.GetUsers().SingleOrDefault(u => u.Username == sellerName);
            var sellerSession = Site.Login(sellerName, sellerPw);
            var randomGen = new Random();
            var auctions = new List<IAuction>();
            for (int i = 0; i < howManyAuctions; i++) {
                var startingPrice = randomGen.NextDouble() * 100 + 1;
                var auction = sellerSession.CreateAuction($"The {i}th auction for {sellerName}",
                    AlarmClock.Object.Now.AddDays(randomGen.Next(3650)), startingPrice);
                auctions.Add(auction);
                auction.BidOnAuction(userSession, startingPrice * 2);
            }

            SetNowToFutureTime(3650 * 24 * 60 * 60 + 1, AlarmClock);
            var wonAuctions = User.WonAuctions();
            Assert.That(auctions, Is.EquivalentTo(wonAuctions));
        }
        protected IUser CreateAndLogUser(string username, out ISession session, ISite site)
        {
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            site.CreateUser(username, username);
            session = site.Login(username, username);
            return site.GetUsers().FirstOrDefault(u => u.Username == username);
        }
    }
}