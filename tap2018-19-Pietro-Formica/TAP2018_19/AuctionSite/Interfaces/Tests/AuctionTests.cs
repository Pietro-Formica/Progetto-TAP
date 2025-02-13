﻿using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using TAP2018_19.AlarmClock.Interfaces;

namespace TAP2018_19.AuctionSite.Interfaces.Tests {
    public class AuctionTests : InstrumentedAuctionSiteTest {
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISite Site;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected Mock<IAlarmClock> AlarmClock;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAlarmClock' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Mock<>' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected IUser Seller;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISession SellerSession;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected IUser Bidder1;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISession Bidder1Session;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected IUser Bidder2;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected ISession Bidder2Session;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IAuction' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected IAuction TheAuction;
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IAuction' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.

        protected const string SiteName = "site for auction tests";
        /// <summary>
        /// Initializes Site:
        /// <list type="table">
        /// <item>
        /// <term>name</term>
        /// <description>SiteName = "site for auction tests"</description>
        /// </item>
        /// <item>
        /// <term>time zone</term>
        /// <description>-2</description>
        /// </item>
        /// <item>
        /// <term>expiration time</term>
        /// <description>300 seconds</description>
        /// </item>
        /// <item>
        /// <term>minimum bid increment</term>
        /// <description>7</description>
        /// </item>
        /// <item>
        /// <term>users</term>
        /// <description>Seller, Bidder1, Bidder2</description>
        /// </item>
        /// <item>
        /// <term>auctions</term>
        /// <description>TheAuction ("Beautiful object to be desired by everybody",
        /// starting price 5, ends in 7 days)</description>
        /// </item>
        /// <item>
        /// <term>sessions</term>
        /// <description>SellerSession, Bidder1Session, Bidder2Session</description>
        /// </item>
        /// </list>  
        /// </summary>

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'SetUp' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'SetUpAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        [SetUp]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'SetUpAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'SetUp' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void SiteUSersAuctionInitialize() {
            const int timeZone = -2;
            siteFactory.CreateSiteOnDb(connectionString, SiteName, timeZone, 300, 7);
            AlarmClockMock(timeZone, out AlarmClock);
            Site = siteFactory.LoadSite(connectionString, SiteName, AlarmClock.Object);
            Seller = CreateAndLogUser("seller", out SellerSession, Site);
            Bidder1 = CreateAndLogUser("bidder1", out Bidder1Session, Site);
            Bidder2 = CreateAndLogUser("bidder2", out Bidder2Session, Site);
            TheAuction = SellerSession.CreateAuction("Beautiful object to be desired by everybody",
                AlarmClock.Object.Now.AddDays(7), 5);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        protected IUser CreateAndLogUser(string username, out ISession session, ISite site) {
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISite' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'IUser' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            site.CreateUser(username, username);
            session = site.Login(username, username);
            return site.GetUsers().FirstOrDefault(u => u.Username == username);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentWinner on an auction with no bids returns null
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void CurrentWinner_NoBids_Null() {
            var currentWinner = TheAuction.CurrentWinner();
            Assert.That(currentWinner, Is.Null);
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentWinner on an auction which has received just
        /// one bid returns the owner of the session used to make the bid
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_SingleUserBids_Null() {
            TheAuction.BidOnAuction(Bidder1Session, 10);
            var winner = TheAuction.CurrentWinner();
            Assert.That(winner, Is.EqualTo(Bidder1));
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentWinner on an auction which has received
        /// two bids returns the owner of the session used to make the higher bid
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_SingleUserBidsTwice_User() {
            TheAuction.BidOnAuction(Bidder1Session, 10);
            TheAuction.BidOnAuction(Bidder2Session, 20);
            var winner = TheAuction.CurrentWinner();
            Assert.That(winner, Is.EqualTo(Bidder2));
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentWinner on an auction which has received
        /// two bids returns the owner of the session used to make the higher bid
        /// also when the higher bidder is the first to bid
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_TwoUsers_U1() {
            TheAuction.BidOnAuction(Bidder1Session, 100);
            TheAuction.BidOnAuction(Bidder2Session, 20);
            var winner = TheAuction.CurrentWinner();
            Assert.That(winner, Is.EqualTo(Bidder1));
        }


#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentPrice on an auction with no bids returns
        /// the starting price of the auction
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void CurrentPrice_NoBids_5() {
            Assert.That(TheAuction.CurrentPrice(), Is.EqualTo(5));
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentPrice on an auction with one bid returns
        /// the starting price of the auction
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_SingleUserBids_5() {
            TheAuction.BidOnAuction(Bidder1Session, 10);
            Assert.That(TheAuction.CurrentPrice(), Is.EqualTo(5));
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentPrice on an auction with two bids by the same user
        /// returns the starting price of the auction
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_SingleUserBidsTwice_10() {
            TwoBidsGetPrice(Bidder1Session, 10, Bidder1Session, 20, 5);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentPrice on an auction with two bids by the different users
        /// differing more than the minimum bid increment of the auction site
        /// returns the lower bid increased by the minimum bid increment
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_TwoUsers_27() {
            TwoBidsGetPrice(Bidder1Session, 100, Bidder2Session, 20, 27);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the CurrentPrice on an auction with two bids by the different users
        /// differing less than the minimum bid increment of the auction site
        /// returns the higher bid
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_TwoUsers_30() {
            TwoBidsGetPrice(Bidder2Session, 25, Bidder1Session, 30, 30);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Make two offers and assert that the final CurrentPrice is <see cref="expectedCurrentPrice"/>
        /// </summary>
        /// <param name="FirstBidderSession">The session of the first bidder</param>
        /// <param name="firstOffer">The first bid</param>
        /// <param name="secondBidderSession">The session of the second bidder</param>
        /// <param name="secondOffer">The second bid</param>
        /// <param name="expectedCurrentPrice">The expected final price</param>
        private void TwoBidsGetPrice(ISession FirstBidderSession, int firstOffer, ISession secondBidderSession,
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'ISession' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
            int secondOffer, int expectedCurrentPrice) {
            TheAuction.BidOnAuction(FirstBidderSession, firstOffer);
            TheAuction.BidOnAuction(secondBidderSession, secondOffer);
            Assert.That(TheAuction.CurrentPrice(), Is.EqualTo(expectedCurrentPrice));
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that bidding on an auction increases the validity time
        /// of the bidder session
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_ValidOffer_UpdatesSessionsValidUntil() {
            SetNowToFutureTime(3*60,AlarmClock);
            var validUntilBeforeBid = Bidder1Session.ValidUntil;
            TheAuction.BidOnAuction(Bidder1Session, 30);
            var validUntilAfterBid = Bidder1Session.ValidUntil;
            Assert.That(validUntilBeforeBid, Is.LessThan(validUntilAfterBid));
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that after deleting an auction, its id is not anymore known to the auction site 
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void Delete_ExistingAuction_Deletes() {
            var auctionId = TheAuction.Id;
            TheAuction.Delete();
            var deletedAuctionSurvives = Site.GetAuctions(false).Any(a => a.Id == auctionId);
            Assert.That(!deletedAuctionSurvives);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a call to Delete on a deleted auction throws InvalidOperationException
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void Delete_DeletedAuction_Throws() {
            TheAuction.Delete();
            Assert.That(() => TheAuction.Delete(), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void Delete_Site_Auction_Throws()
        {
            Site.Delete();
            Assert.That(() => TheAuction.Delete(), Throws.TypeOf<InvalidOperationException>());

        }

        [Test]
        public void Delete_Site_Auction_BinOnAuction_Throws()
        {
            Site.Delete();
            Assert.That(() => TheAuction.BidOnAuction(Bidder1Session, 10), Throws.TypeOf<InvalidOperationException>());

        }

        [Test]
        public void Delete_Site_Auction_CurrentWinner_Throws()
        {
            Site.Delete();
            Assert.That(() => TheAuction.CurrentWinner(), Throws.TypeOf<InvalidOperationException>());

        }

        [Test]
        public void Delete_Site_Auction_CurrentPrice_Throws()
        {
            Site.Delete();
            Assert.That(() => TheAuction.CurrentPrice(), Throws.TypeOf<InvalidOperationException>());

        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a call to BidOnAuction on a deleted auction throws InvalidOperationException
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_DeletedAuction_Throws() {
            TheAuction.Delete();
            Assert.That(() => TheAuction.BidOnAuction(Bidder1Session, 10), Throws.TypeOf<InvalidOperationException>());
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a call to BidOnAuction on a null session throws ArgumentNullException
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_NullSession_Throws() {
            Assert.That(() => TheAuction.BidOnAuction(null, 44), Throws.TypeOf<ArgumentNullException>());
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a call to BidOnAuction on a negative bid throws ArgumentOutOfRangeException
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_NegativeOffer_Throws() {
            Assert.That(() => TheAuction.BidOnAuction(Bidder1Session, -77),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a call to BidOnAuction on a session done
        /// after its expiration time throws ArgumentException
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_InvalidSession_Throws()
            //invalid as expired
        {
            var sessionExpirationTime = Bidder1Session.ValidUntil;
            AlarmClock.Setup(ac => ac.Now).Returns(sessionExpirationTime.AddSeconds(1));
            Assert.That(() => TheAuction.BidOnAuction(Bidder1Session, 101), Throws.TypeOf<ArgumentException>());
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a call to BidOnAuction on a session done
        /// after the owner logged out throws ArgumentException
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_InvalidSession1_Throws()
            //invalid as logged out
        {
            Bidder1Session.Logout();
            Assert.That(() => TheAuction.BidOnAuction(Bidder1Session, 101), Throws.TypeOf<ArgumentException>());
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a bid smaller than the starting price is not accepted
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_NotEnoughMoney_False0() {
            var anotherAuction = SellerSession.CreateAuction("another auction", AlarmClock.Object.Now.AddDays(5), 100);
            var accepted = anotherAuction.BidOnAuction(Bidder2Session, 10);
            Assert.That(!accepted);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a bid smaller than another bid by the same user
        /// is not accepted
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_NotEnoughMoney_False1() {
            TheAuction.BidOnAuction(Bidder1Session, 100);
            TheAuction.BidOnAuction(Bidder2Session, 99);

            var accepted = TheAuction.BidOnAuction(Bidder2Session, 10);
            Assert.That(!accepted);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that a bid higher than the current price is accepted
        /// even if smaller than the higher standing bidding (by a different user)
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_NotEnoughMoneyToWin_True() {
            var anotherAuction = SellerSession.CreateAuction("another auction", AlarmClock.Object.Now.AddDays(5), 10);
            anotherAuction.BidOnAuction(Bidder1Session, 100);
            var accepted = anotherAuction.BidOnAuction(Bidder2Session, 21);
            Assert.That(accepted);
        }
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the first bid is accepted when equal to the starting price
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_EnoughMoney_True1() {
            var anotherAuction = SellerSession.CreateAuction("another auction 1", AlarmClock.Object.Now.AddDays(5), 23);
            var accepted = anotherAuction.BidOnAuction(Bidder2Session, 23);
            Assert.That(accepted);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the first bid is accepted when greater than the starting price
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_EnoughMoney_True2() {
            var anotherAuction = SellerSession.CreateAuction("another auction 2", AlarmClock.Object.Now.AddDays(5), 23);
            var accepted = anotherAuction.BidOnAuction(Bidder2Session, 333);
            Assert.That(accepted);
        }

#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning disable CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        /// <summary>
        /// Verify that the second bid by the same user is accepted if it is greater than the first
        /// </summary>
        [Test]
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'Test' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
#pragma warning restore CS0246 // Il nome di tipo o di spazio dei nomi 'TestAttribute' non è stato trovato. Probabilmente manca una direttiva using o un riferimento all'assembly.
        public void BidOnAuction_EnoughMoney_True3() {
            TheAuction.BidOnAuction(Bidder1Session, 10);
            var accepted = TheAuction.BidOnAuction(Bidder1Session, 333);
            Assert.That(accepted);
        }


    }
}